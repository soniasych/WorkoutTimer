using System;
using System.Globalization;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using Newtonsoft.Json;
using WorkoutTimer.Managers;
using WorkoutTimer.Models;
using WorkoutTimer.Utils;


namespace WorkoutTimer.Activities
{
    [Activity(Label = "WorkoutActivity")]
    public class WorkoutActivity : Activity
    {
        private TrainingModel _trainingInfo;
        private TextView _exercisesCountTextView;
        private TextView _setsCountTextView;
        private TextView _timeTextView;
        private TextView _commandTextView;
        private ImageView _pauseWorkoutImageView;
        private LinearLayout _mainLayout;
        private bool isPaused = false;
        private string currentBackgroundColor = string.Empty;
        private int _getReadyTime = 5;
        private Timer _timer;
        private TrainManager _manager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_Workout);

            InitView();
            SetDefaulAppearence();
            InitializeTrainingInfo(savedInstanceState);
            StartWorkout();
        }

        protected override void OnStart()
        {
            base.OnStart();
            _pauseWorkoutImageView.Click += PauseWorkoutImageViewClick;
        }

        private void PauseWorkoutImageViewClick(object sender, EventArgs e)
        {
            if (isPaused)
            {
                _manager.RestartWorkout();
                _pauseWorkoutImageView.SetBackgroundResource(Resource.Drawable.ic_pauseWork);
                _mainLayout.SetBackgroundColor(Color.ParseColor(currentBackgroundColor));
                isPaused = !isPaused;
            }
            else
            {
                _manager.PauseWorkout();
                _mainLayout.SetBackgroundColor(Color.ParseColor("#89827a"));
           
                //Bitmap bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_startWork);
               // Bitmap b = Bitmap.CreateScaledBitmap(bitmap,)
                _pauseWorkoutImageView.SetBackgroundResource(Resource.Drawable.ic_startWork);
                isPaused = !isPaused;
            }

        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        private void InitializeTrainingInfo(Bundle savedInstanceState)
        {
            if (!Intent.Extras.IsEmpty && Intent.Extras.ContainsKey("TrainingInfo"))
            {
                _trainingInfo = JsonConvert.DeserializeObject<TrainingModel>(Intent.Extras.GetString("TrainingInfo"));

                _exercisesCountTextView.Text = _trainingInfo.ExercisesNumber.ToString();
                _setsCountTextView.Text = _trainingInfo.SetsNumber.ToString();
            }
        }

        private void InitView()
        {
            _exercisesCountTextView = FindViewById<TextView>(Resource.Id.Activity_Workout_ExercisesCountTextView);
            _setsCountTextView = FindViewById<TextView>(Resource.Id.Activity_Workout_SetCountTextView);
            _timeTextView = FindViewById<TextView>(Resource.Id.Activity_Workout_TimeTextView);
            _commandTextView = FindViewById<TextView>(Resource.Id.Activity_Workout_CommandTextView);
            _pauseWorkoutImageView = FindViewById<ImageView>(Resource.Id.Activity_Workout_PauseWorkoutImageButton);
            _mainLayout = FindViewById<LinearLayout>(Resource.Id.Activity_Workout_Layout);
        }

        private void SetDefaulAppearence()
        {
            _exercisesCountTextView.Text = "0";
            _setsCountTextView.Text = "0";
            _timeTextView.Text = "00:0" + _getReadyTime;
            _commandTextView.Text = "Get Ready";
        }

        private void StartWorkout()
        {
            _manager = new TrainManager(_trainingInfo);
            _manager.TrainChangeEvent += Manager_TrainChangeEvent;
            _manager.TickEvent += _manager_TickEvent;

            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void _manager_TickEvent(object sender, TrainChangeEventArgs e)
        {
            int seconds = 10 * int.Parse(_timeTextView.Text[3].ToString()) + int.Parse(_timeTextView.Text[4].ToString());
            int minutes = 10 * int.Parse(_timeTextView.Text[0].ToString()) + int.Parse(_timeTextView.Text[1].ToString());
            --seconds;
            if (seconds < 0)
            {
                --minutes;
                seconds = 59;
            }
            RunOnUiThread(() =>
            {
                if (minutes > 0 && seconds > 0)
                {
                    DateTime time = new DateTime(2016, 10, 10, 0, minutes, seconds);
                    _timeTextView.Text = time.ToString("mm:ss", CultureInfo.InvariantCulture);
                }       
            });
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_getReadyTime == 0)
            {
                _timer.Stop();
                _timer.Dispose();
                RunOnUiThread(() =>
                {
                    _manager.StartWorkout();
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    DateTime time = new DateTime(2016, 10, 10, 0, 0, --_getReadyTime);
                    _timeTextView.Text = time.ToString("mm:ss", CultureInfo.InvariantCulture);
                });
            }
        }

        private void SendNotification(string message)
        {
            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            //Setting notification channel (for API 26 and higher)
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.O)
            {
                notificationManager.CreateNotificationChannel(new NotificationChannel("notdefault", "notdefault", NotificationManager.ImportanceDefault)
                {
                    Description = "notdescription"
                });
            }

            var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

            var notificationBuilder = new NotificationCompat.Builder(this, "notdefault")
                .SetSmallIcon(Resource.Drawable.ic_notification)
                .SetColor(Color.ParseColor("#AA1BEEF5"))
                .SetContentTitle("Workout Timer")
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetSound(defaultSoundUri)
                .SetContentIntent(null)
                .SetPriority(NotificationCompat.PriorityDefault);

            notificationManager.Notify(0, notificationBuilder.Build());
        }

        private void Manager_TrainChangeEvent(object sender, TrainChangeEventArgs e)
        {
            RunOnUiThread(() =>
            {
                switch (e.State)
                {
                    case Utils.State.Work:
                        _commandTextView.Text = "Work It!";
                        _mainLayout.SetBackgroundColor(Color.ParseColor("#1fb94d"));
                        currentBackgroundColor = "#1fb94d";
                        if (_trainingInfo.IsNotificationsEnabled)
                            SendNotification("Work it!");
                        break;
                    case Utils.State.Rest:
                        _commandTextView.Text = "Rest";
                        _mainLayout.SetBackgroundColor(Color.ParseColor("#5c9ddf"));
                        currentBackgroundColor = "#1fb94d";
                        if (_trainingInfo.IsNotificationsEnabled)
                            SendNotification("Rest!");
                        break;
                    case Utils.State.LongRest:
                        _commandTextView.Text = "Long Rest";
                        _mainLayout.SetBackgroundColor(Color.ParseColor("#e2ce42"));
                        currentBackgroundColor = "#1fb94d";
                        if (_trainingInfo.IsNotificationsEnabled)
                            SendNotification("Long Rest!");
                        break;
                }
                DateTime time = new DateTime(2016, 10, 10, 0, e.MinutesToNextMessage, e.SecondsToNextMessage);

                _timeTextView.Text = time.ToString("mm:ss", CultureInfo.InvariantCulture);

                if (e.State == Utils.State.Rest || e.State == Utils.State.LongRest)
                {
                    if ((int.Parse(_setsCountTextView.Text)) > 1)
                        _setsCountTextView.Text = (int.Parse(_setsCountTextView.Text) - 1).ToString();
                    else
                    {
                        _setsCountTextView.Text = _trainingInfo.SetsNumber.ToString();
                        _exercisesCountTextView.Text = (int.Parse(_exercisesCountTextView.Text) - 1).ToString();
                    }
                }
            });

        }
    }
}
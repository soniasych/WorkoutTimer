using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using WorkoutTimer.Views;
using Android.Content;
using WorkoutTimer.Models;
using Newtonsoft.Json;

namespace WorkoutTimer.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class OptionsSettingActivity : AppCompatActivity
    {
        private NumericalOptionView _exercisesCountOptionView;
        private NumericalOptionView _setsCountOptionView;

        private TimeOptionView _workIntervalOptionView;
        private TimeOptionView _restIntervalOptionView;
        private TimeOptionView _restBetweenExercisesOptionView;

        private ImageView _starWorkoutImageView;
        private Switch _enableNotificationsSwitch;

        protected override void OnStart()
        {
            base.OnStart();
            _starWorkoutImageView.Click += _starWorkoutImageView_Click;

        }

        protected override void OnPause()
        {
            base.OnPause();
            _starWorkoutImageView.Click -= _starWorkoutImageView_Click;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Activity_OptionsSetting);
            InitView();
        }

        private void InitView()
        {
            _exercisesCountOptionView = new NumericalOptionView(FindViewById(Resource.Id.Activity_OptionsSetting_ExercisesCountOption), "Exercises", this);
            _setsCountOptionView = new NumericalOptionView(FindViewById(Resource.Id.Activity_OptionsSetting_SetsCountOption), "Sets", this);
            _workIntervalOptionView = new TimeOptionView(FindViewById(Resource.Id.Activity_OptionsSetting_WorkIntervalOption), "Work Interval", this);
            _restIntervalOptionView = new TimeOptionView(FindViewById(Resource.Id.Activity_OptionsSetting_RestIntervalOption), "Rest Interval", this);
            _restBetweenExercisesOptionView = new TimeOptionView(FindViewById(Resource.Id.Activity_OptionsSetting_RestBetweenExercisesOption), "Rest Between Exercises", this);

            _starWorkoutImageView = FindViewById<ImageView>(Resource.Id.Activity_OptionsSetting_StartWorkoutImageButton);
            _enableNotificationsSwitch = FindViewById<Switch>(Resource.Id.Activity_OptionsSetting_EnableNotifications);
        }

        private void _starWorkoutImageView_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(WorkoutActivity));
            intent.PutExtra("TrainingInfo", JsonConvert.SerializeObject(new TrainingModel()
            {
                ExercisesNumber = _exercisesCountOptionView.GetValue(),
                SetsNumber = _setsCountOptionView.GetValue(),
                SetsNumberCopy = _setsCountOptionView.GetValue(),
                RestInterval = _restIntervalOptionView.GetValue(),
                WorkInterval = _workIntervalOptionView.GetValue(),
                RestBetweenExercisesInterval = _restBetweenExercisesOptionView.GetValue(),
                IsNotificationsEnabled = _enableNotificationsSwitch.Checked
            }));

            StartActivity(intent);
        }

    }
}
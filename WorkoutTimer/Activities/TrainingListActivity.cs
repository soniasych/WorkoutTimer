using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using WorkoutTimer.Helpers;
using WorkoutTimer.Models;

namespace WorkoutTimer.Activities
{
    [Activity(Label = "TrainingListActivity")]
    public class TrainingListActivity : Activity
    {
        public TextView _getTrainingTextView;
        private TrainingDTO _trainingInfo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TrainingList);
            InitView();
        }

        private void InitView()
        {
            _getTrainingTextView = FindViewById<TextView>(Resource.Id.textView1);
            var trainings = DbHelper.GetTrainingData();
            foreach (var training in trainings)
            {
                _getTrainingTextView.Text += training.ToString();
            }
        }



        //private void InitializeTrainingInfo(Bundle savedInstanceState)
        //{
        //    if (!Intent.Extras.IsEmpty && Intent.Extras.ContainsKey("GetTrainingInfo"))
        //    {
        //        _trainingInfo = JsonConvert.DeserializeObject<TrainingDTO>(Intent.Extras.GetString("GetTrainingInfo"));

        //        _getTrainingTextView.Text = _trainingInfo.ExercisesNumber.ToString(); ////???????????
        //        _getTrainingTextView.Text = _trainingInfo.SetsNumber.ToString();
        //        _getTrainingTextView.Text = _trainingInfo.WorkInterval;
        //        _getTrainingTextView.Text = _trainingInfo.RestInterval;
        //        _getTrainingTextView.Text = _trainingInfo.RestBetweenExercisesInterval;
        //        _getTrainingTextView.Text = _trainingInfo.IsNotificationsEnabled.ToString();

        //    }
        //}

    }
}
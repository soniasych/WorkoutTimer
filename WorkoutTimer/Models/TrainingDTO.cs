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

namespace WorkoutTimer.Models
{
    class TrainingDTO
    {
        public int Id { get; set; }
        public int ExercisesNumber { get; set; }
        public int SetsNumber { get; set; }
        public string WorkInterval { get; set; }
        public string RestInterval { get; set; }
        public string RestBetweenExercisesInterval { get; set; }
        public bool IsNotificationsEnabled { get; set; }

        public TrainingDTO()
        {

        }

        public TrainingDTO(int exercisesNumber, 
            int setsNumber, 
            string workInterval,
            string restInterval,
            string restBetweenExercisesInterval,
            bool isNotificationsEnabled)
        {
            this.ExercisesNumber = exercisesNumber;
            this.SetsNumber = setsNumber;
            this.WorkInterval = workInterval;
            this.RestInterval = restInterval;
            this.RestBetweenExercisesInterval = restBetweenExercisesInterval;
            this.IsNotificationsEnabled = isNotificationsEnabled;

        }
    }
}
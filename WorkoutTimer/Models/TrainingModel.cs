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
using WorkoutTimer.Helpers;

namespace WorkoutTimer.Models
{
    public class TrainingModel
    {
        public int ExercisesNumber { get; set; }
        public int SetsNumber { get; set; }
        public int SetsNumberCopy { get; set; }
        public TimeStruct WorkInterval { get; set; }
        public TimeStruct RestInterval { get; set; }
        public TimeStruct RestBetweenExercisesInterval { get; set; }
        public bool IsNotificationsEnabled { get; set; }
    }
}
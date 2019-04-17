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

        public TrainingModel() { }

        public TrainingModel(int exercisesNumber,
            int setsNumber,
            int setsNumberCopy,
            TimeStruct workInterval,
            TimeStruct restInterval,
            TimeStruct restBetweenExercisesInterval,
            bool isNotificationsEnabled)
        {
            this.ExercisesNumber = exercisesNumber;
            this.SetsNumber = setsNumber;
            this.SetsNumberCopy = setsNumberCopy;
            this.WorkInterval = workInterval;
            this.RestInterval = restInterval;
            this.RestBetweenExercisesInterval = restBetweenExercisesInterval;
            this.IsNotificationsEnabled = isNotificationsEnabled;

        }
        public override string ToString()
        {
            return $"Exercises Number {ExercisesNumber} \n " +
                   $"Sets Number {SetsNumber} \n" +
                   $"Work Interval {WorkInterval.Minutes}:{WorkInterval.Seconds} \n" +
                   $"Rest Interval {RestInterval.Minutes}:{RestInterval.Seconds} \n" +
                   $"Rest Between Exercises Interval {RestBetweenExercisesInterval.Minutes}:{RestBetweenExercisesInterval.Seconds} \n" +
                   $"{(IsNotificationsEnabled ? "Notification Enabled" : "Notification Disabled")} \n\n";
        }
    }
}
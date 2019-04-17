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
using SQLite;

namespace WorkoutTimer.Models
{
    [Table("Training")]
    class TrainingDTO
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
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

        public override string ToString()
        {
            return "Exercises Number" + " " + ExercisesNumber + "\n" +
                   "Sets Number" + " " + SetsNumber + "\n" +
                   "Work Interval" + " " + WorkInterval + "\n" +
                   "Rest Interval" + " " + RestInterval + "\n" +
                   "Rest Between Exercises Interval" + " " + RestBetweenExercisesInterval + "\n" +
                   "Is Notifications Enabled" + " " + IsNotificationsEnabled + "\n\n";
        }
    }
}
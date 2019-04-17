using System;
using System.Collections.Generic;
using System.IO;
using SQLitePCL;
using WorkoutTimer.Models;
using SQLitePCL;
using System.IO;
using System.Linq;
using SQLite;

namespace WorkoutTimer.Helpers
{
    public static class DbHelper
    {
        private static TimeStruct ConvertToTimeStruct(string input)
        {
            var intArray = input.Split(':').Select(n => Convert.ToInt32(n)).ToArray();
            return new TimeStruct
            {
                Minutes = intArray[0],
                Seconds = intArray[1]
            };
        }

        internal static void SaveTrainingData(TrainingDTO modelToSave)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "training.db3");

            var db = new SQLiteConnection(dbPath);

            db.CreateTable<TrainingDTO>();

            db.Insert(modelToSave);
        }

        internal static List<TrainingModel> GetTrainingData()
        {
            var trainings = new List<TrainingModel>();

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "training.db3");

            var db = new SQLiteConnection(dbPath);

            var table = db.Table<TrainingDTO>();

            foreach (var item in table)
            {
                var myTraining = new TrainingDTO(item.ExercisesNumber, item.SetsNumber, item.WorkInterval,
                    item.RestInterval, item.RestBetweenExercisesInterval, item.IsNotificationsEnabled);

                var recoveredModel = 
                    new TrainingModel(myTraining.ExercisesNumber,
                    myTraining.SetsNumber,
                    myTraining.SetsNumber,
                    ConvertToTimeStruct(myTraining.WorkInterval),
                    ConvertToTimeStruct(myTraining.RestInterval),
                    ConvertToTimeStruct(myTraining.RestBetweenExercisesInterval),
                    myTraining.IsNotificationsEnabled);
                trainings.Add(recoveredModel);
            }
            return trainings;
        }

    }
}
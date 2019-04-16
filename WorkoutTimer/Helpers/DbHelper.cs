using System;
using System.IO;
using SQLitePCL;
using WorkoutTimer.Models;
using SQLite;
using SQLitePCL;
using System.IO;

namespace WorkoutTimer.Helpers
{
    public static class DbHelper
    {
        internal static void SaveTrainingData(TrainingDTO modelToSave)
        {
            var dto = new TrainingDTO
                (
                modelToSave.ExercisesNumber, 
                modelToSave.SetsNumber,
                modelToSave.WorkInterval.ToString(),
                modelToSave.RestInterval.ToString(),
                modelToSave.RestBetweenExercisesInterval.ToString(),
                modelToSave.IsNotificationsEnabled
                );
            // Tonn of logic

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Training.db3");

            var db = new SQLiteConnection(dbPath);

            db.CreateTable<TrainingDTO>();

            db.Insert(modelToSave);
        }
    }
}
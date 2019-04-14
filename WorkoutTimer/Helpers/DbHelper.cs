using System;
using System.IO;
using SQLitePCL;
using WorkoutTimer.Models;

namespace WorkoutTimer.Helpers
{
    public static class DbHelper
    {
        internal static void SaveTrainingData(TrainingModel modelToSave)
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
            // db.Insert(modelToSave);
        }
    }
}
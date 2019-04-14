using System;
using WorkoutTimer.Helpers;
using WorkoutTimer.Models;
using WorkoutTimer.Utils;

namespace WorkoutTimer.Managers
{
    public class TrainManager
    {
        private TrainingModel _trainingModel;
        private WorkoutCountDownTimer _changeActivityTimer;

        public event EventHandler<TrainChangeEventArgs> TrainChangeEvent;
        public event EventHandler<TrainChangeEventArgs> TickEvent;

        public TrainManager(TrainingModel trainingModel)
        {
            _trainingModel = trainingModel;
        }

        public void StartWorkout()
        {
            _changeActivityTimer = new WorkoutCountDownTimer(_trainingModel.WorkInterval.TotalMiliseconds(), 1000);

            TrainChangeEvent?.Invoke(this, new TrainChangeEventArgs()
            {
                State = State.Work,
                SecondsToNextMessage = _trainingModel.WorkInterval.Seconds,
                MinutesToNextMessage = _trainingModel.WorkInterval.Minutes
            });

            _changeActivityTimer.Tick += SecondTick;
            _changeActivityTimer.Start();
            _changeActivityTimer.Finished += WorkoutTimerFinished;
        }

        public void PauseWorkout()
        {
            _changeActivityTimer.Cancel();
        }

        public void RestartWorkout()
        {
            _changeActivityTimer.Start();
        }


        private void Rest()
        {
            _changeActivityTimer = new WorkoutCountDownTimer(_trainingModel.RestInterval.TotalMiliseconds(), 1000);
            TrainChangeEvent?.Invoke(this, new TrainChangeEventArgs()
            {
                State = State.Rest,
                SecondsToNextMessage = _trainingModel.RestInterval.Seconds,
                MinutesToNextMessage = _trainingModel.RestInterval.Minutes
            });


            _changeActivityTimer.Tick += SecondTick;
            _changeActivityTimer.Finished += RestTimerFinished;
            _changeActivityTimer.Start();
        }

        private void RestBetweenExercises()
        {
            _changeActivityTimer = new WorkoutCountDownTimer(_trainingModel.RestBetweenExercisesInterval.TotalMiliseconds(), 1000);
            TrainChangeEvent?.Invoke(this, new TrainChangeEventArgs()
            {
                State = State.LongRest,
                SecondsToNextMessage = _trainingModel.RestBetweenExercisesInterval.Seconds,
                MinutesToNextMessage = _trainingModel.RestBetweenExercisesInterval.Minutes
            });


            _changeActivityTimer.Tick += SecondTick;
            _changeActivityTimer.Finished += RestBetweenExercisesTimerFinished;
            _changeActivityTimer.Start();
        }

        private void SecondTick(object sender, WorkoutTimerChangesEventArgs e)
        {
            TickEvent?.Invoke(this, new TrainChangeEventArgs());
        }

        private void WorkoutTimerFinished(object sender, WorkoutTimerChangesEventArgs e)
        {

            _trainingModel.SetsNumberCopy -= 1;
            _changeActivityTimer.Dispose();
            if (_trainingModel.SetsNumberCopy == 0)
            {
                RestBetweenExercises();
                _trainingModel.SetsNumberCopy = _trainingModel.SetsNumber;
            }
            else
            {
                Rest();
            }

        }

        private void RestTimerFinished(object sender, WorkoutTimerChangesEventArgs e)
        {
            //_changeActivityTimer.Dispose();
            StartWorkout();
        }

        private void RestBetweenExercisesTimerFinished(object sender, WorkoutTimerChangesEventArgs e)
        {
            _trainingModel.ExercisesNumber -= 1;
            _changeActivityTimer.Dispose();
            if (_trainingModel.ExercisesNumber == 0)
            {
            }
            else
            {
                StartWorkout();
            }
        }
    }
}
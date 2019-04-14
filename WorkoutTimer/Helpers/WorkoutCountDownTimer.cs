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
using WorkoutTimer.Utils;

namespace WorkoutTimer.Helpers
{
    class WorkoutCountDownTimer : CountDownTimer
    {

        public event EventHandler<WorkoutTimerChangesEventArgs> Tick;
        public event EventHandler<WorkoutTimerChangesEventArgs> Finished;
       
        public WorkoutCountDownTimer(long millisInFuture, long countdownIntervall) : base(millisInFuture, countdownIntervall)
        {
        }

        public override void OnFinish()
        {
            Finished?.Invoke(this, new WorkoutTimerChangesEventArgs());
        }

        public override void OnTick(long millisUntilFinished)
        {
            Tick?.Invoke(this, new WorkoutTimerChangesEventArgs());
        }
    }
}
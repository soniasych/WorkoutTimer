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

namespace WorkoutTimer.Utils
{
    public class TrainChangeEventArgs
    {
        public State State { get; set; }
        public int SecondsToNextMessage { get; set; }
        public int MinutesToNextMessage { get; set; }
    }

    public enum State { Work, Rest, LongRest }
}
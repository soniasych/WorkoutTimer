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

namespace WorkoutTimer.Helpers
{
    public struct TimeStruct
    {

        private int _seconds;
        private int _minutes;

        public int Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                _seconds = value;
                while (_seconds > 60)
                {
                    _minutes += 1;
                    _seconds -= 60;
                }
            }
        }
        public int Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                _minutes = value;
                if (_minutes > 99)
                {
                    _minutes = 99;
                }
            }
        }

        public int TotalSeconds()
        {
            return 60 * Minutes + Seconds;
        }

        public int TotalMiliseconds()
        {
            return TotalSeconds() * 1000;
        }

        public void Clear()
        {
            Seconds = 0;
            Minutes = 0;
        }

    }
}
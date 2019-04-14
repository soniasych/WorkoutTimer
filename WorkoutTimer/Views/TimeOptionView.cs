using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using WorkoutTimer.Helpers;

namespace WorkoutTimer.Views
{
    public class TimeOptionView : View
    {
        private TextView _optionName;
        private EditText _optionSecondsValue;
        private EditText _optionMinutesValue;
        private ImageView _optionValueIncreaser;
        private ImageView _optionValueDecreaser;

        public TimeOptionView(View view, string optionName, Context context, IAttributeSet attrs = null) :
            base(context, attrs)
        {
            Initialize(view);
            SetDefaults(optionName);
            SubscribeOnEvents();
        }
        private void Initialize(View view)
        {

            _optionName = view.FindViewById<TextView>(Resource.Id.View_TimeOption_OptionNameTextView);
            _optionSecondsValue = view.FindViewById<EditText>(Resource.Id.View_TimeOption_OptionSecondsValueTextView);
            _optionMinutesValue = view.FindViewById<EditText>(Resource.Id.View_TimeOption_OptionMinutesValueTextView);
            _optionValueIncreaser = view.FindViewById<ImageView>(Resource.Id.View_TimeOption_OptionPlusValueImageView);
            _optionValueDecreaser = view.FindViewById<ImageView>(Resource.Id.View_TimeOption_OptionMinusValueImageView);
        }

        private void SetDefaults(string optionName)
        {
            _optionName.Text = optionName;
            _optionSecondsValue.Text = "00";
            _optionMinutesValue.Text = "00";
        }

        private void SubscribeOnEvents()
        {
            _optionValueIncreaser.Click += _optionValueIncreaser_Click;
            _optionValueDecreaser.Click += _optionValueDecreaser_Click;
        }

        private void _optionValueDecreaser_Click(object sender, EventArgs e)
        {
            if ((int.Parse(_optionSecondsValue.Text)) > 0 )
            {
                _optionSecondsValue.Text = (int.Parse(_optionSecondsValue.Text) - 1).ToString();
            }
            else if (int.Parse(_optionSecondsValue.Text) == 0 && int.Parse(_optionMinutesValue.Text) != 0)
            {
                _optionSecondsValue.Text = "59";
                _optionMinutesValue.Text = (int.Parse(_optionMinutesValue.Text)-1).ToString();
            }
        }

        private void _optionValueIncreaser_Click(object sender, EventArgs e)
        {
            if ((int.Parse(_optionSecondsValue.Text)) == 59)
            {
                _optionSecondsValue.Text = "0";
                _optionMinutesValue.Text = (int.Parse(_optionMinutesValue.Text) + 1).ToString();
            }
            else
            {
                _optionSecondsValue.Text = (int.Parse(_optionSecondsValue.Text) + 1).ToString();
            }
        }

        public TimeStruct GetValue()
        {
            TimeStruct timeStruct = new TimeStruct();
            timeStruct.Seconds = int.Parse(_optionSecondsValue.Text);
            timeStruct.Minutes = int.Parse(_optionMinutesValue.Text);
            return timeStruct;
        }
    }
}
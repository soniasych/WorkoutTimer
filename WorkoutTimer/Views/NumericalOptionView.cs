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

namespace WorkoutTimer.Views
{
    public class NumericalOptionView : View
    {
        private TextView _optionName;
        private EditText _optionValue;
        private ImageView _optionValueIncreaser;
        private ImageView _optionValueDecreaser;

        public NumericalOptionView(View view, string optionName, Context context, IAttributeSet attrs = null) :
            base(context, attrs)
        {
            Initialize(view);
            SetDefaults(optionName);
            SubscribeOnEvents();
        }


        private void Initialize(View view)
        {
            _optionName = view.FindViewById<TextView>(Resource.Id.View_NumericalOption_OptionNameTextView);
            _optionValue = view.FindViewById<EditText>(Resource.Id.View_NumericalOption_OptionValueEditText);
            _optionValueIncreaser = view.FindViewById<ImageView>(Resource.Id.View_NumericalOption_OptionPlusValueImageView);
            _optionValueDecreaser = view.FindViewById<ImageView>(Resource.Id.View_NumericalOption_OptionMinusValueImageView);
        }

        private void SetDefaults(string optionName)
        {
            _optionName.Text = optionName;
            _optionValue.Text = "0";
        }

        private void SubscribeOnEvents()
        {
            _optionValueIncreaser.Click += _optionValueIncreaser_Click;
            _optionValueDecreaser.Click += _optionValueDecreaser_Click;
        }

        private void _optionValueDecreaser_Click(object sender, EventArgs e)
        {
            if ((int.Parse(_optionValue.Text)) > 0)
            {
                _optionValue.Text = (int.Parse(_optionValue.Text) - 1).ToString();
            }
        }

        private void _optionValueIncreaser_Click(object sender, EventArgs e)
        {
            _optionValue.Text = (int.Parse(_optionValue.Text) + 1).ToString();
        }

        public int GetValue()
        {
            return int.Parse(_optionValue.Text); 
        }
    }
}
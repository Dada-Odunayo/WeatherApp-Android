using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WeatherApp
{
    public class ProgressDialogFragment : AppCompatDialogFragment
    {
        string thisStatus;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public ProgressDialogFragment()
        {
            
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.auth, container, false);
            //TextView statusTextView = view.FindViewById<TextView>(Resource.Id.progressStatus);
            //statusTextView.Text = thisStatus;
            return view;
        }
    }
}
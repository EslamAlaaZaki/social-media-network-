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
using Xamarin.Controls;

namespace KeepConnected
{
    [Activity(Label = "Keep Connected")]
    public class logincontrol : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginpage);
            var loginbut = FindViewById<Button>(Resource.Id.loginbutton_login);
            var cancelbut = FindViewById<Button>(Resource.Id.cancelbutton_login);
            var email = FindViewById<TextView>(Resource.Id.email_login);
            var password = FindViewById<TextView>(Resource.Id.password_login);

            loginbut.Click += delegate
            {
                int i = algorithm_functions.login(email.Text, password.Text);
                if (i == 0)
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Wrong login !");
                    alert.SetMessage("Wrong E-mail address !");
                    alert.SetNeutralButton("Cancel", delegate { });
                    alert.Show();
                }
                else if (i == 1)
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Wrong login !");
                    alert.SetMessage("Wrong Password !");
                    alert.SetNeutralButton("Cancel", delegate { });
                    alert.Show();
                }
                else
                {
                    AlertCenter.Default.Init(Application);
                    AlertCenter.Default.PostMessage("Keep Connected", "you Successfully login", Resource.Drawable.logo);
                    var intent = new Intent(this, typeof(timelinecontrol));
                    StartActivity(intent);
                }

            };
            cancelbut.Click += delegate
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}
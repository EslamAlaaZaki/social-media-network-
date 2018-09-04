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
    public class signupcontrol : Activity
    {
        
      protected override void OnCreate(Bundle savedInstanceState)
         {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signuppage);
            var signupbut = FindViewById<Button>(Resource.Id.signup_registration);
            var cancelbut = FindViewById<Button>(Resource.Id.cancelbutton_registration);

            var name = FindViewById<TextView>(Resource.Id.name_registraion);
            var email = FindViewById<TextView>(Resource.Id.email_registration);
            var mobile = FindViewById<TextView>(Resource.Id.mobile_registration);
            var password = FindViewById<TextView>(Resource.Id.password_registration);
            var birthday = FindViewById<TextView>(Resource.Id.birthday_registration);
            var male = FindViewById<RadioButton>(Resource.Id.male_registration);
            var female = FindViewById<RadioButton>(Resource.Id.female_registration);

            signupbut.Click += delegate
            {
                int id = Global.UsersList.Count + 1;
                string gender = "";
                List<bool> error = new List<bool>(6);
                if (male.Checked)
                {
                    gender = "male";
                }
                else if (female.Checked)
                {
                    gender = "female";
                }
                string NAME;
                string EMAILE;
                string MOBILE;
                string PASSWORD;
                string BIRTHDAY;
                if (name.Text == "") NAME = " ";
                else NAME = name.Text;
                if (email.Text == "") EMAILE = " ";
                else EMAILE = email.Text;
                if (mobile.Text == "") MOBILE = "1";
                else MOBILE = mobile.Text;
                if (password.Text == "") PASSWORD = " ";
                else PASSWORD = password.Text;
                if (birthday.Text == "") BIRTHDAY = " ";
                else BIRTHDAY = birthday.Text;
                bool i = algorithm_functions.create_account(new Users(NAME, int.Parse(MOBILE), EMAILE, PASSWORD, gender, BIRTHDAY, 0, "", 0, "", id), error);
                if (i)
                {
                    AlertCenter.Default.Init(Application);
                    AlertCenter.Default.PostMessage("Keep Connected", "Congratulation you Successfully Registrate", Resource.Drawable.logo);
                    var intent = new Intent(this, typeof(timelinecontrol));
                    StartActivity(intent);
                }
                else
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Wrong registration !");
                    string e = "";
                    if (error[0])
                    {
                        e = "invalid user name";
                    }
                    if (error[1])
                    {
                        e += "\ninvalid e-mail address";
                    }
                    if (error[2])
                    {
                        e = "\ninvalid mobile number";
                    }
                    if (error[3])
                    {
                        e = "\ninvalid password";
                    }
                    if (error[4])
                    {
                        e = "\ninvalid birthday";
                    }
                    if (error[5])
                    {
                        e = "\ninvalid gender";
                    }
                    alert.SetMessage(e);
                    alert.SetNeutralButton("Cancel", delegate { });
                    alert.Show();
                }


            };
            cancelbut.Click += delegate
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
            // Create your application here
        }
    }
    
}
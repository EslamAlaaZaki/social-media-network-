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
    [Activity(Label = "creategroupcontrol")]
    public class creategroupcontrol : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.creategroup);
            var name = FindViewById<EditText>(Resource.Id.groubname_creategroup);
            var create = FindViewById<Button>(Resource.Id.creategroup);
            create.Click += delegate 
            {
                if (name.Text != null)
                {
                    if (algorithm_functions.namevalidation(name.Text))
                    {
                        AlertCenter.Default.Init(Application);
                        AlertCenter.Default.PostMessage("Keep Connected", name.Text+"\t"+ "is created successfully", Resource.Drawable.logo);
                       /* var intent = new Intent(this, typeof(profilecontrol));
                        StartActivity(intent);*/
                    }
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Wrong!");
                        alert.SetMessage("invalied group name !");
                        alert.SetNeutralButton("Cancel", delegate { });
                        alert.Show();
                    }


                    
                }
            };
            // Create your application here
        }
    }
}
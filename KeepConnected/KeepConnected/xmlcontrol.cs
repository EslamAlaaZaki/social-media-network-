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
using System.IO;
using Xamarin.Controls;

namespace KeepConnected
{
    [Activity(Label = "xmlcontrol")]
    public class xmlcontrol : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.xmlpage);
            var xml = FindViewById<TextView>(Resource.Id.xml);
            Spinner choose = FindViewById<Spinner>(Resource.Id.xmlorgraph);
            choose.ItemSelected += delegate
            {
                if (choose.SelectedItem.ToString() == "XML")
                {
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "xml.txt");
                    using (StreamWriter sw = File.CreateText(dbPath))
                    {
                        sw.Flush();
                        sw.Write(algorithm_functions.XML());
                        AlertCenter.Default.Init(Application);
                        AlertCenter.Default.PostMessage("Keep Connected", "xml.txt is generated successfully", Resource.Drawable.logo);
                    }

                    xml.Text = algorithm_functions.XML();
                }
            };

        }
    }
}
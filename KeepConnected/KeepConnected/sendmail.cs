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
    [Activity(Label = "sendmail")]
    public class sendmail : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sentmail);
            var content = FindViewById<EditText>(Resource.Id.messagecontent);
            var email = FindViewById<EditText>(Resource.Id.messageemail);
            var send = FindViewById<Button>(Resource.Id.messagesendbutton);
            send.Click += delegate 
            {
                if(content.Text!=null || email.Text != null)
                {
                    int id = -1;
                    int n = Global.UsersList.Count;
                    for(int i = 0; i < n; i++)
                    {
                        if (Global.UsersList[i].Email == email.Text) { id = i;  break;}
                    }
                    if (Global.User.ID == id)
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error!");
                        alert.SetMessage("you cant send message to yourself");
                        alert.SetNeutralButton("Cancel", delegate { });
                        alert.Show();
                    }
                   else if (id == -1)
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error!");
                        alert.SetMessage("no one hase this E-mail");
                        alert.SetNeutralButton("Cancel", delegate { });
                        alert.Show();
                    }
                    else
                    {
                        Database.insert_inbox(new Inboxs(Global.User.ID, id, content.Text));
                        AlertCenter.Default.Init(Application);
                        AlertCenter.Default.PostMessage("Keep Connected", "Your message is sent successfully", Resource.Drawable.logo);
                       
                    }
                }
            };
            // Create your application here
        }
    }
}
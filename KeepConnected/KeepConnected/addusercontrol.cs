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
    [Activity(Label = "addusercontrol")]
    public class addusercontrol : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.adduser);
            var email = FindViewById<EditText>(Resource.Id.username_adduser);
            var send = FindViewById<Button>(Resource.Id.adduserbutton_adduser);
            send.Click += delegate
            {
                if (email.Text != null)
                {
                    int id = -1;
                    int n = Global.UsersList.Count;
                    for (int i = 0; i < n; i++)
                    {
                        if (Global.UsersList[i].Email == email.Text) { id = i; break; }
                    }
                    if (Global.User.ID == id)
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error!");
                        alert.SetMessage("you cant Add yourself");
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
                        int flag = 0;
                        if(Global.UsersList[id-1].FriendsIDs!="" || Global.UsersList[id - 1].FriendsIDs !=null)
                        {
                            if(Global.UsersList[id - 1].FriendsIDs.Contains(",+"+Global.User.ID.ToString()+","))
                            {
                                flag = 1;
                                var alert = new AlertDialog.Builder(this);
                                alert.SetTitle("Error!");
                                alert.SetMessage("your request was sent before");
                                alert.SetNeutralButton("Cancel", delegate { });
                                alert.Show();

                            }
                        }
                        if (flag == 0)
                        {
                            if (Global.User.FriendsNO != 0)
                            {
                                if (Global.User.FriendsIDs.Contains("," + id.ToString() + ","))
                                {
                                    var alert = new AlertDialog.Builder(this);
                                    alert.SetTitle("Error!");
                                    alert.SetMessage("you and " + Global.UsersList[id - 1].Name + " are acually friends");
                                    alert.SetNeutralButton("Cancel", delegate { });
                                    alert.Show();
                                }
                                else
                                {
                                    algorithm_functions.Send_Friend_Request(id);
                                    AlertCenter.Default.Init(Application);
                                    AlertCenter.Default.PostMessage("Keep Connected", "Your request is sent successfully", Resource.Drawable.logo);
                                }
                            }
                            else
                            {
                                algorithm_functions.Send_Friend_Request(id);
                                AlertCenter.Default.Init(Application);
                                AlertCenter.Default.PostMessage("Keep Connected", "Your request is sent successfully", Resource.Drawable.logo);
                            }
                        }

                       
                        

                    }
                }
            };
            // Create your application here
        }
    }
}
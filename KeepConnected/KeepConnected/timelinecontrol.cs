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
    [Activity(Label = "timelinecontrol")]
    public class timelinecontrol : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            postitem adp;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.timelinepage);
            var searchinput = FindViewById<SearchView>(Resource.Id.search_timeline);
            var searchtype = FindViewById<Spinner>(Resource.Id.searchtype_timeline);
            var searchbutton = FindViewById<ImageView>(Resource.Id.searchbutton_timeline);
            var img = FindViewById<ImageView>(Resource.Id.img);
            var postcontent = FindViewById<EditText>(Resource.Id.postcontent_timeline);
            var share = FindViewById<Button>(Resource.Id.sharepost_timeline);
            var postlist = FindViewById<ListView>(Resource.Id.listposts_timeline);
            var functions = FindViewById<Spinner>(Resource.Id.functions);
            var functionsbutton = FindViewById<Button>(Resource.Id.functionsbutton);
            functionsbutton.Click += delegate
            {
                if (functions.SelectedItem.ToString() == "Profile")
                {
                    
                }
                else if (functions.SelectedItem.ToString() == "Friends")
                {

                }
                else if (functions.SelectedItem.ToString() == "Add Friend")
                {
                    var intent = new Intent(this, typeof(addusercontrol));
                    StartActivity(intent);
                }
                else if (functions.SelectedItem.ToString() == "Friends Requests")
                {

                }
                else if (functions.SelectedItem.ToString() == "Groups")
                {

                }
                else if (functions.SelectedItem.ToString() == "Inbox")
                {
                    var intent = new Intent(this, typeof(inboxcontrol));
                    intent.PutExtra("inbox", 1);
                    StartActivity(intent);
                }
                else if (functions.SelectedItem.ToString() == "Sent")
                {
                    var intent = new Intent(this, typeof(inboxcontrol));
                    intent.PutExtra("inbox", 0);
                    StartActivity(intent);
                }
                else if (functions.SelectedItem.ToString() == "Create Group")
                {
                    var intent = new Intent(this, typeof(creategroupcontrol));
                    StartActivity(intent);
                }
                else if (functions.SelectedItem.ToString() == "Logout")
                {
                    AlertCenter.Default.Init(Application);
                    AlertCenter.Default.PostMessage("Keep Connected", "you Successfully login", Resource.Drawable.logo);
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else if (functions.SelectedItem.ToString() == "Send E-mail")
                {
                    var intent = new Intent(this, typeof(sendmail));
                    StartActivity(intent);
                }

            };
            if (Global.User.Gender != "male")
            {
                img.SetImageResource(Resource.Drawable.girlinprofile);
            }
            algorithm_functions.Get_Posts(Global.printedposts, Global.User.ID, 0);
            adp = new postitem(this, Global.printedposts);
            postlist.Adapter = adp;
            share.Click += delegate
            {
                if (postcontent.Text != "")
                {

                    Database.insert_post(new Posts(Global.User.ID, 0, postcontent.Text, 0, ""));
                    if (Global.printedposts.Count == 0) Global.printedposts.Add(Global.PostsList[Global.PostsList.Count - 1]);
                    else Global.printedposts.Insert(0, Global.PostsList[Global.PostsList.Count - 1]);
                    adp.NotifyDataSetChanged();

                }
            };
            postlist.ItemClick += (s, e) =>
            {
                var listView = s as ListView;
                var t = Global.printedposts[e.Position];
            };


        }

    }
}
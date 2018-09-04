using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace KeepConnected
{
    [Activity(Label = "KeepConnected", MainLauncher = true, Icon = "@drawable/logo")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Database db = new Database();
            // Set our view from the "main" layout resource
            if (Global.UsersList.Count == 0)
            {
                Database.select_user(Global.UsersList);
                Database.select_group(Global.GroupsList);
                Database.select_post(Global.PostsList);
                Database.select_inbox(Global.InboxList);
                Database.select_comment(Global.CommentsList);
            }
            SetContentView(Resource.Layout.Main);
            var loginbut = FindViewById<Button>(Resource.Id.loginbutton_main);
            var loginimg = FindViewById<ImageView>(Resource.Id.loginimage_main);
            var signupbut = FindViewById<Button>(Resource.Id.registraionbutton_main);
            var signupimg = FindViewById<ImageView>(Resource.Id.registrationimage_main);
            var xmlbut = FindViewById<Button>(Resource.Id.xmlbutton_main);
            var xmlimg = FindViewById<ImageView>(Resource.Id.xmlimage_main);

            //////////////////////////////////////////////
            loginbut.Click += delegate
            {
                var intent = new Intent(this, typeof(logincontrol));
                StartActivity(intent);
            };
            loginimg.Click += delegate
            {
                var intent = new Intent(this, typeof(logincontrol));
                StartActivity(intent);
            };
            ////////////////////////////////////////////////
            signupbut.Click += delegate
            {
                var intent = new Intent(this, typeof(signupcontrol));
                StartActivity(intent);
            };
            signupimg.Click += delegate
            {
                var intent = new Intent(this, typeof(signupcontrol));
                StartActivity(intent);
            };
            ///////////////////////////////////////////////
            xmlbut.Click += delegate
            {
                var intent = new Intent(this, typeof(xmlcontrol));
                StartActivity(intent);
            };
            xmlimg.Click += delegate
            {
                var intent = new Intent(this, typeof(xmlcontrol));
                StartActivity(intent);
            };

        }
    }
}


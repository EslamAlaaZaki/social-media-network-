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

namespace KeepConnected
{
    [Activity(Label = "Keep Connected")]
    public class inboxcontrol : Activity
    {
        public static int _inbox = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Inbox);
            var emailslist = FindViewById<ListView>(Resource.Id.list_inbox);
            int i = Intent.GetIntExtra("inbox", 10);
            if (i != 1) _inbox = 0;
            List<Inboxs> printed = new List<Inboxs>();
            if(_inbox==1)algorithm_functions.Get_Inbox(printed);
            else algorithm_functions.Get_Sendbox(printed);
            emailslist.Adapter = new inboxadp(this, printed);

        }
    }
    public class inboxadp : BaseAdapter<Inboxs>
    {
        List<Inboxs> items;
        Activity context;
        public inboxadp(Activity context, List<Inboxs> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Inboxs this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.message, null);
            var creator = view.FindViewById<TextView>(Resource.Id.emailcreator_message);
            var email = view.FindViewById<TextView>(Resource.Id.emailcreator_message);
            var date = view.FindViewById<TextView>(Resource.Id.date_message);
            var content = view.FindViewById<TextView>(Resource.Id.content_message);
            if (inboxcontrol._inbox == 1)
            {
                creator.Text = Global.UsersList[item.Sender - 1].Name;
                email.Text = Global.UsersList[item.Sender - 1].Email;
                date.Text = "Date: " + item.Date + "  Time: " + algorithm_functions.convertdate(item.Time);
                content.Text = item.Content;
            }
            else
            {
                creator.Text = Global.UsersList[item.Receiver - 1].Name;
                email.Text = Global.UsersList[item.Receiver - 1].Email;
                date.Text = "Date: " + item.Date + "  Time: " + algorithm_functions.convertdate(item.Time);
                content.Text = item.Content;
            }


            return view;
        }

    }
}
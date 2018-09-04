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
using SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace KeepConnected
{
    class Database
    {
        static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "socialnetwork.db3");


        public Database()
        {
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<User>();
                db.CreateTable<Post>();
                db.CreateTable<Inbox>();
                db.CreateTable<Group>();
                db.CreateTable<Comment>();

            }
        }


        [Table("User")]
        public class User
        {
            [PrimaryKey, AutoIncrement, Unique]
            public int ID { get; set; }
            public string Name { get; set; }
            public int Mobile { get; set; }
            [Unique]
            public string Email { get; set; }
            public string Password { get; set; }
            public string Gender { get; set; }
            public string Birthday { get; set; }
            public int GroupsNO { get; set; }
            public string GroupsIDs { get; set; }
            public int FriendsNO { get; set; }
            public string FriendsIDs { get; set; }
        }

        [Table("Post")]
        public class Post
        {
            [PrimaryKey, AutoIncrement, Unique]
            public int ID { get; set; }
            public int CreatorID { get; set; }
            public int GroupID { get; set; }
            public string Content { get; set; }
            public string Date { get; set; }
            public int Time { get; set; }
            public int LikesNO { get; set; }
            public string LikesIDs { get; set; }

        }

        [Table("Comment")]
        public class Comment
        {
            [PrimaryKey, AutoIncrement, Unique]
            public int ID { get; set; }
            public int PostID { get; set; }
            public int CreatorID { get; set; }
            public string Content { get; set; }
            public string Date { get; set; }
            public int Time { get; set; }
        }


        [Table("Inbox")]
        public class Inbox
        {
            [PrimaryKey, AutoIncrement, Unique]
            public int ID { get; set; }
            public int Sender { get; set; }
            public int Receiver { get; set; }
            public string Content { get; set; }
            public string Date { get; set; }
            public int Time { get; set; }
        }

        [Table("Group")]
        public class Group
        {
            [PrimaryKey, AutoIncrement, Unique]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Admins { get; set; }
        }


        static public void insert_comment(Comments _comment)
        {
            var db = new SQLiteConnection(dbPath);
            var newComment = new Comment();
            newComment.CreatorID = _comment.CreatorID;
            newComment.PostID = _comment.PostID;
            newComment.Content = _comment.Content;
            DateTime Now = DateTime.Now;
            string date = Now.Date.ToString("d");
            int time = (Now.Hour * 3600) + (Now.Minute * 60) + Now.Second;
            newComment.Date = date;
            newComment.Time = time;
            db.Insert(newComment);
            /////////////////////
            Global.CommentsList.Add(select_Last_comment());
        }

        static public void insert_user(Users _user)
        {
            var db = new SQLiteConnection(dbPath);
            var newUser = new User();
            newUser.Name = _user.Name;
            newUser.Mobile = _user.Mobile;
            newUser.Email = _user.Email;
            newUser.Password = _user.Password;
            newUser.Gender = _user.Gender;
            newUser.Birthday = _user.Birthday;
            newUser.GroupsNO = _user.GroupsNO;
            newUser.GroupsIDs = _user.GroupsIDs;
            newUser.FriendsNO = _user.FriendsNO;
            newUser.FriendsIDs = _user.FriendsIDs;
            db.Insert(newUser);
            ////////////////////////
            _user.ID = Global.UsersList.Count + 1;
            Global.UsersList.Add(_user);

        }

        static public void insert_inbox(Inboxs _inbox)
        {
            var db = new SQLiteConnection(dbPath);
            var newInbox = new Inbox();
            newInbox.Sender = _inbox.Sender;
            newInbox.Receiver = _inbox.Receiver;
            newInbox.Content = _inbox.Content;
            DateTime Now = DateTime.Now;
            string date = Now.Date.ToString("d");
            int time = (Now.Hour * 3600) + (Now.Minute * 60) + Now.Second;
            newInbox.Date = date;
            newInbox.Time = time;
            db.Insert(newInbox);
            //////////////////////////////
            _inbox.Date = date;
            _inbox.Time = time;
            _inbox.ID = Global.InboxList.Count + 1;
            Global.InboxList.Add(_inbox);
        }

        static public void insert_post(Posts _post)
        {
            var db = new SQLiteConnection(dbPath);
            var newPost = new Post();
            DateTime Now = DateTime.Now;
            string date = Now.Date.ToString("d");
            int time = (Now.Hour * 3600) + (Now.Minute * 60) + Now.Second;
            newPost.CreatorID = _post.CreatorID;
            newPost.GroupID = _post.GroupID;
            newPost.Content = _post.Content;
            newPost.Date = date;
            newPost.Time = time;
            newPost.LikesNO = _post.LikesNO;
            newPost.LikesIDs = _post.LikesIDs;
            db.Insert(newPost);
            ////////////////////////
            Global.PostsList.Add(select_Last_post());

        }

        static public void insert_group(Groups _group)
        {
            var db = new SQLiteConnection(dbPath);
            var newGroup = new Group();
            newGroup.Name = _group.Name;
            newGroup.Admins = _group.Admins;
            db.Insert(newGroup);
            _group.ID = Global.GroupsList.Count + 1;
            Global.GroupsList.Add(_group);
        }

        static public void select_comment(List<Comments> input)
        {
            input.Clear();
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Comment>();
            foreach (var s in table)
            {
                Comments c = new Comments();
                c.ID = s.ID;
                c.CreatorID = s.CreatorID;
                c.PostID = s.PostID;
                c.Content = s.Content;
                c.Date = s.Date;
                c.Time = s.Time;
                input.Add(c);
            }

        }

        static public Comments select_Last_comment()
        {
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Comment>();
            Comments c = new Comments();
            foreach (var s in table)
            {
                c = new Comments();
                c.ID = s.ID;
                c.CreatorID = s.CreatorID;
                c.PostID = s.PostID;
                c.Content = s.Content;
                c.Date = s.Date;
                c.Time = s.Time;

            }
            return c;
        }

        static public void select_user(List<Users> input)
        {
            input.Clear();
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<User>();
            foreach (var v in table)
            {
                Users u = new Users();
                u.ID = v.ID;
                u.Name = v.Name;
                u.Mobile = v.Mobile;
                u.Email = v.Email;
                u.Password = v.Password;
                u.Birthday = v.Birthday;
                u.Gender = v.Gender;
                u.GroupsIDs = v.GroupsIDs;
                u.GroupsNO = v.GroupsNO;
                u.FriendsIDs = v.FriendsIDs;
                u.FriendsNO = v.FriendsNO;
                input.Add(u);
            }
        }

        static public void select_inbox(List<Inboxs> input)
        {
            input.Clear();
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Inbox>();
            foreach (var v in table)
            {
                Inboxs i = new Inboxs();
                i.ID = v.ID;
                i.Sender = v.Sender;
                i.Receiver = v.Receiver;
                i.Content = v.Content;
                i.Date = v.Date;
                i.Time = v.Time;
                input.Add(i);
            }
        }

        static public void select_post(List<Posts> input)
        {
            input.Clear();
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Post>();
            foreach (var v in table)
            {
                Posts p = new Posts();
                p.ID = v.ID;
                p.CreatorID = v.CreatorID;
                p.GroupID = v.GroupID;
                p.Content = v.Content;
                p.Date = v.Date;
                p.Time = v.Time;
                p.LikesIDs = v.LikesIDs;
                p.LikesNO = v.LikesNO;
                input.Add(p);
            }
        }

        static public Posts select_Last_post()
        {
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Post>();
            Posts p = new Posts();
            foreach (var v in table)
            {
                p = new Posts();
                p.ID = v.ID;
                p.CreatorID = v.CreatorID;
                p.GroupID = v.GroupID;
                p.Content = v.Content;
                p.Date = v.Date;
                p.Time = v.Time;
                p.LikesIDs = v.LikesIDs;
                p.LikesNO = v.LikesNO;

            }
            return p;
        }

        static public void select_group(List<Groups> input)
        {
            input.Clear();
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Group>();
            foreach (var v in table)
            {
                Groups g = new Groups();
                g.ID = v.ID;
                g.Name = v.Name;
                g.Admins = v.Admins;
                input.Add(g);
            }
        }

        static public void update_user(Users _user)
        {
            var db = new SQLiteConnection(dbPath);
            var newUser = new User();
            newUser.ID = _user.ID;
            newUser.Name = _user.Name;
            newUser.Mobile = _user.Mobile;
            newUser.Email = _user.Email;
            newUser.Password = _user.Password;
            newUser.Gender = _user.Gender;
            newUser.Birthday = _user.Birthday;
            newUser.GroupsIDs = _user.GroupsIDs;
            newUser.GroupsNO = _user.GroupsNO;
            newUser.FriendsIDs = _user.FriendsIDs;
            newUser.FriendsNO = _user.FriendsNO;
            db.Update(newUser);
            ////////////////////
            Global.UsersList[_user.ID - 1] = _user;
        }

        static public void update_post(Posts _post)
        {
            var db = new SQLiteConnection(dbPath);
            var newPost = new Post();
            newPost.ID = _post.ID;
            newPost.Content = _post.Content;
            newPost.LikesIDs = _post.LikesIDs;
            newPost.LikesNO = _post.LikesNO;
            db.Update(newPost);
            ////////////////////////
            int n = Global.PostsList.Count;
            for (int i = 0; i < n; i++)
            {
                if (Global.PostsList[i].ID == _post.ID)
                {
                    Global.PostsList[i] = _post;
                    break;
                }
            }

        }

        static public void update_group(Groups _group)
        {
            var db = new SQLiteConnection(dbPath);
            var newGroup = new Group();
            newGroup.ID = _group.ID;
            newGroup.Name = _group.Name;
            newGroup.Admins = _group.Admins;
            db.Update(newGroup);
            ////////////////////////
            int n = Global.GroupsList.Count;
            for (int i = 0; i < n; i++)
            {
                if (Global.GroupsList[i].ID == _group.ID)
                {
                    Global.GroupsList[i] = _group;
                    break;
                }
            }
        }

        static public void remove_comment(Comments _comment)
        {
            var db = new SQLiteConnection(dbPath);
            var newComment = new Comment();
            newComment.ID = _comment.ID;
            db.Delete(newComment);
            ////////////////////////
            Global.CommentsList.Remove(_comment);

        }

        static public void remove_user(Users _user)
        {
            var db = new SQLiteConnection(dbPath);
            var newUser = new User();
            newUser.ID = _user.ID;
            db.Delete(newUser);
            ///////////////////
            Global.UsersList.Remove(_user);
        }

        static public void remove_post(Posts _post)
        {
            var db = new SQLiteConnection(dbPath);
            var newPost = new Post();
            newPost.ID = _post.ID;
            db.Delete(newPost);
            /////////////////////////////
            Global.PostsList.Remove(_post);
        }

        static public void remove_group(Groups _group)
        {
            var db = new SQLiteConnection(dbPath);
            var newGroup = new Group();
            newGroup.ID = _group.ID;
            db.Delete(newGroup);
            ////////////////////////////
            Global.GroupsList.Remove(_group);
        }

    }

    class Users
    {
        public int ID;
        public string Name;
        public int Mobile;
        public string Email;
        public string Password;
        public string Gender;
        public string Birthday;
        public int GroupsNO;
        public string GroupsIDs;
        public int FriendsNO;
        public string FriendsIDs;

        public Users(string _name, int _mobile, string _email, string _password, string _gender, string _birthday, int _groupsno, string _groupsids, int _friendsno, string _friendsids, int _id = 0)
        {
            Name = _name;
            Mobile = _mobile;
            Email = _email;
            Password = _password;
            Gender = _gender;
            Birthday = _birthday;
            GroupsNO = _groupsno;
            GroupsIDs = _groupsids;
            FriendsNO = _friendsno;
            FriendsIDs = _friendsids;
            ID = _id;
        }
        public Users()
        {
            Name = "";
            Mobile = 0;
            Email = "";
            Password = "";
            Gender = "";
            Birthday = "";
            GroupsNO = 0;
            GroupsIDs = "";
            FriendsNO = 0;
            FriendsIDs = "";
            ID = 0;
        }

    }

    public class Posts
    {

        public int ID;
        public int CreatorID;
        public int GroupID;
        public string Content;
        public string Date;
        public int Time;
        public int LikesNO;
        public string LikesIDs;

        public Posts(int _creatorid, int _groupid, string _content, int _likesno, string _likesids, string _date = "", int _time = 0, int _id = 0)
        {
            ID = _id;
            CreatorID = _creatorid;
            GroupID = _groupid;
            Content = _content;
            Date = _date;
            Time = _time;
            LikesNO = _likesno;
            LikesIDs = _likesids;
        }
        public Posts()
        {
            ID = 0;
            CreatorID = 0;
            GroupID = 0;
            Content = "";
            Date = "";
            Time = 0;
            LikesNO = 0;
            LikesIDs = "";
        }

    }

    class Comments
    {
        public int ID;
        public int PostID;
        public int CreatorID;
        public string Content;
        public string Date;
        public int Time;

        public Comments(int _postid, int _creatorid, string _content, string _date = "", int _time = 0, int _id = 0)
        {
            PostID = _postid;
            CreatorID = _creatorid;
            Content = _content;
            Date = _date;
            Time = _time;
            ID = _id;
        }
        public Comments()
        {
            PostID = 0;
            CreatorID = 0;
            Content = "";
            Date = "";
            Time = 0;
            ID = 0;
        }
    }

   public class Inboxs
    {
        public int ID;
        public int Sender;
        public int Receiver;
        public string Content;
        public string Date;
        public int Time;

        public Inboxs(int _sender, int _receiver, string _content, string _date = "", int _time = 0, int _id = 0)
        {
            ID = _id;
            Sender = _sender;
            Receiver = _receiver;
            Content = _content;
            Date = _date;
            Time = _time;
        }
        public Inboxs()
        {
            ID = 0;
            Sender = 0;
            Receiver = 0;
            Content = "";
            Date = "";
            Time = 0;
        }

    }

    class Groups
    {
        public int ID;
        public string Name;
        public string Admins;

        public Groups(string _name = "", string _admins = "", int _id = 0)
        {
            Name = _name;
            Admins = _admins;
            ID = _id;
        }


    }

    class Global
    {
        static public List<Users> UsersList = new List<Users>(50);
        static public List<Comments> CommentsList = new List<Comments>(50);
        static public List<Posts> PostsList = new List<Posts>(50);
        static public List<Groups> GroupsList = new List<Groups>(50);
        static public List<Inboxs> InboxList = new List<Inboxs>(50);
        static public List<Posts> printedposts = new List<Posts>(50);
        static public List<Users> printedusers = new List<Users>(50);
        static public List<Groups> printedgroups = new List<Groups>(50);
        static public target Target = new target();
        static public Users User = new Users();
    }
    class target
    {
        public int group;
        public int id;
        public target()
        {
            group = 0;
            id = 0;
        }
    }

    class algorithm_functions
    {
        static public void parse(List<int> output, string input, bool a)
        {
            // set a to false to get requests and true if not
            output.Clear();
            if (input == "" || input == " " || input == null) return;
            if (a)
            {
                while (input.Contains(',') && input.Length != 1)
                {
                    if (input[input.IndexOf(',') + 1] != '+')
                    {
                        input = input.Remove(0, 1);
                        string num = input.Substring(0, input.IndexOf(','));
                        output.Add(int.Parse(num));
                        input = input.Remove(0, input.IndexOf(','));
                    }
                    else
                    {
                        input = input.Remove(0, 1);
                        int i = input.IndexOf(',');
                        input = input.Remove(0, i);

                    }
                }

            }
            else
            {
                while (input.Contains("+"))
                {
                    int i = input.IndexOf('+');
                    input = input.Remove(0, i + 1);
                    string num = input.Substring(0, input.IndexOf(','));
                    output.Add(int.Parse(num));
                    input = input.Remove(0, input.IndexOf(','));

                }
            }
        }
        static public void Get_Posts(List<Posts> output, int id, int target)
        {
            // set target to 0 if u want posts in timeline and 1 if profile and else if group  
            output.Clear();
            if (target == 0) // timeline
            {
                for (int i = Global.PostsList.Count - 1; i >= 0; i--)
                {
                    int creator = Global.PostsList[i].CreatorID;
                    int group = Global.PostsList[i].GroupID;
                    if ((group == 0) && ((creator == id) || (Global.User.FriendsIDs.Contains("," + creator.ToString() + ","))))
                    {
                        output.Add(Global.PostsList[i]);
                    }
                    else if (group != 0 && Global.User.GroupsIDs.Contains("," + group.ToString() + ","))
                    {
                        output.Add(Global.PostsList[i]);
                    }
                }
            }
            else if (target == 1) //profile
            {
                for (int i = Global.PostsList.Count - 1; i >= 0; i--)
                {
                    int creator = Global.PostsList[i].CreatorID;
                    int group = Global.PostsList[i].GroupID;
                    if ((group == 0) && (creator == id))
                    {
                        output.Add(Global.PostsList[i]);
                    }
                }
            }
            else //group
            {
                for (int i = Global.PostsList.Count - 1; i >= 0; i--)
                {
                    int group = Global.PostsList[i].GroupID;
                    if (group == id)
                    {
                        output.Add(Global.PostsList[i]);
                    }
                }
            }



        }
        static public void Get_Comments(List<Comments> output, int post_id)
        {
            output.Clear();
            int n = Global.CommentsList.Count;
            for (int i = 0; i < n; i++)
                if (Global.CommentsList[i].PostID == post_id) output.Add(Global.CommentsList[i]);

        }
        static public void Get_LikersIDs(List<int> output, int post_id)
        {

            int n = Global.PostsList.Count;
            for (int i = 0; i < n; i++)
            {
                if (Global.PostsList[i].ID == post_id)
                {
                    parse(output, Global.PostsList[i].LikesIDs, true);
                    break;
                }
            }
        }
        static public void Get_Inbox(List<Inboxs> output)
        {
            output.Clear();
            int n = Global.InboxList.Count;
            for (int i = n - 1; i >= 0; i++)
            {
                if (Global.InboxList[i].Receiver == Global.User.ID)
                {
                    output.Add(Global.InboxList[i]);
                }
            }
        }
        static public void Get_Sendbox(List<Inboxs> output)
        {
            output.Clear();
            int n = Global.InboxList.Count;
            for (int i = n - 1; i >= 0; i++)
            {
                if (Global.InboxList[i].Sender == Global.User.ID)
                {
                    output.Add(Global.InboxList[i]);
                }
            }
        }
        static public void Get_Friends(List<int> output, int id)
        {
            parse(output, Global.UsersList[id - 1].FriendsIDs, true);

        }
        static public void Get_Friends_Requests(List<int> output)
        {
            parse(output, Global.UsersList[Global.User.ID - 1].FriendsIDs, false);
        }
        static public void Send_Friend_Request(int id)
        {
            string s = Global.UsersList[id - 1].FriendsIDs;
            if (Global.UsersList[id - 1].FriendsNO == 0) s = ",+" + Global.User.ID.ToString() + ",";
            else s += "+" + Global.User.ID.ToString() + ",";

            Global.UsersList[id - 1].FriendsIDs = s;
            Database.update_user(Global.UsersList[id - 1]);
        }
        static public void Cancle_Friend_Request(int id)
        {
            string s = ",+" + id.ToString() + ",";
            int i = Global.User.FriendsIDs.IndexOf(s);
            Global.User.FriendsIDs = Global.User.FriendsIDs.Remove(i, s.Length - 1);
            if (Global.User.FriendsIDs.Length == 1) Global.User.FriendsIDs = "";
            Global.UsersList[Global.User.ID - 1].FriendsIDs = Global.User.FriendsIDs;
            Database.update_user(Global.User);
        }
        static public void Confirm_Friend_Request(int id)
        {
            string s = ",+" + id.ToString() + ",";
            Global.User.FriendsIDs = Global.User.FriendsIDs.Remove(Global.User.FriendsIDs.IndexOf(s) + 1, 1);
            Global.User.FriendsNO += 1;
            Global.UsersList[Global.User.ID - 1].FriendsIDs = Global.User.FriendsIDs;
            Global.UsersList[Global.User.ID - 1].FriendsNO = Global.User.FriendsNO;
            Database.update_user(Global.User);
            if (Global.UsersList[id - 1].FriendsNO == 0)
            {
                Global.UsersList[id - 1].FriendsNO += 1;
                Global.UsersList[id - 1].FriendsIDs = "," + Global.User.ID.ToString() + ",";
                Database.update_user(Global.UsersList[id - 1]);
            }
            else
            {
                Global.UsersList[id - 1].FriendsNO += 1;
                Global.UsersList[id - 1].FriendsIDs += Global.User.ID.ToString() + ",";
                Database.update_user(Global.UsersList[id - 1]);
            }
        }
        static public void Remove_Friend(int id)
        {
            string s = "," + id.ToString() + ",";
            Global.User.FriendsIDs = Global.User.FriendsIDs.Remove(Global.User.FriendsIDs.IndexOf(s), s.Length - 1);
            if (Global.User.FriendsIDs.Length == 1) Global.User.FriendsIDs = "";
            Global.User.FriendsNO -= 1;
            Global.UsersList[Global.User.ID - 1].FriendsIDs = Global.User.FriendsIDs;
            Global.UsersList[Global.User.ID - 1].FriendsNO = Global.User.FriendsNO;
            Database.update_user(Global.User);

            string a = "," + Global.User.ID.ToString() + ",";
            Global.UsersList[id - 1].FriendsIDs = Global.UsersList[id - 1].FriendsIDs.Remove(Global.UsersList[id - 1].FriendsIDs.IndexOf(a), a.Length - 1);
            if (Global.UsersList[id - 1].FriendsIDs.Length == 1) Global.UsersList[id - 1].FriendsIDs = "";
            Global.UsersList[id - 1].FriendsNO -= 1;
            Database.update_user(Global.UsersList[id - 1]);
        }
        static public void Remove_Group(int groupid)
        {
            string s = "," + groupid.ToString() + ",";
            if (Global.User.GroupsNO == 1)
            {
                Global.User.GroupsIDs = "";
                Global.User.GroupsNO -= 1;
            }
            else
            {
                Global.User.GroupsIDs = Global.User.GroupsIDs.Remove(Global.User.GroupsIDs.IndexOf(s), s.Length - 1);
                Global.User.GroupsNO -= 1;
            }
            Database.update_user(Global.User);
        }
        static public void Add_group(int id, int groupid)
        {
            if (Global.User.GroupsNO == 0) Global.User.GroupsIDs = groupid.ToString() + ",";
            else
            {
                Global.User.GroupsIDs += "," + groupid.ToString() + ",";
            }
            Global.User.GroupsNO -= 1;
            Database.update_user(Global.User);
        }
        static public bool remove_like(Posts _post)
        {
            string _userid = "," + Global.User.ID.ToString() + ",";
            if (_post.LikesIDs.Contains(_userid))
            {
                _post.LikesIDs = _post.LikesIDs.Replace(_userid, ",");
                _post.LikesNO--;
                if (_post.LikesNO == 0) _post.LikesIDs = "";
                Database.update_post(_post);
                return true;
            }
            else return false;
        }
        static public void add_like(Posts _post)
        {
            string _userid = Global.User.ID.ToString() + ",";
            if (_post.LikesNO == 0)
                _post.LikesIDs = "," + _userid;
            else _post.LikesIDs += _userid;
            _post.LikesNO++;
            Database.update_post(_post);
        }
        static public int login(string email, string password)
        {
            // return 0 if email is wrong
            // return 1 if password is wrong
            // return 2 mean write data entred
            int n = Global.UsersList.Count;
            for (int i = 0; i < n; i++)
            {
                if (Global.UsersList[i].Email == email)
                {
                    if (Global.UsersList[i].Password == password)
                    {
                        Global.User = Global.UsersList[i];
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }

                }

            }
            return 0;

        }
        static public bool create_account(Users a, List<bool> error)
        {

            error.Clear();
            error.Add(!namevalidation(a.Name));
            error.Add(!emailvalidation(a.Email));
            error.Add(!eg_mobilevalidation("0" + a.Mobile.ToString()));
            error.Add(!passwordvalidation(a.Password));
            error.Add(!datevalidation(a.Birthday));
            error.Add(!gendervalidation(a.Gender));
            for (int i = 0; i < 6; i++)
            {
                if (error[i]) return false;
            }
            Global.User = a;
            Database.insert_user(a);
            return true;

        }
        static public bool namevalidation(string username)
        {
            Regex name = new Regex("^[a-zA-z]{3,10}$");
            if (!username.Contains(" "))
            {
                return (name.IsMatch(username));
            }
            else
            {
                int flag = 0;
                while (username.Length != 0)
                {
                    int i;
                    int j;
                    for (i = 0; i < username.Length; i++)
                    {
                        if (username[i] != ' ') break;
                    }
                    for (j = i + 1; j < username.Length; j++)
                    {
                        if (username[j] == ' ') break;
                    }

                    if (i == username.Length)
                    {
                        if (flag == 0) { return false; }
                        else
                        {
                            return true;
                        }
                    }

                    else
                    {
                        if (!name.IsMatch(username.Substring(i, j - i)))
                        {
                            return false;
                        }
                        else
                        {
                            flag = 1;
                            username = username.Substring(j);

                        }
                    }
                }
                return true;
            }

        }
        static public bool emailvalidation(string email)
        {
            Regex e = new Regex("^[a-zA-Z0-9_.]{3,20}@[a-zA-Z0-9]{3,20}.[a-zA-Z]{2,3}$");
            if (e.IsMatch(email))
            {
                int n = Global.UsersList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (Global.UsersList[i].Email == email) return false;
                }
                return true;
            }
            else return false;
        }
        static public bool eg_mobilevalidation(string mobile)
        {
            Regex e = new Regex("^01[0/1/2/5]{1}[0-9]{8}$");
            if (e.IsMatch(mobile))
            {
                int n = Global.UsersList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (("0" + Global.UsersList[i].Mobile.ToString()) == mobile) return false;
                }
                return true;
            }
            else return false;
        }
        static public bool datevalidation(string date)
        {
            return true;
            /* DateTime A;
             DateTime B = DateTime.Now;
             if (DateTime.TryParse(date, out A))
             {
                 if ((A.Second != 0) | (A.Millisecond != 0) | (A.Minute != 0) | (A.Hour != 0)) return false;
                 else if (DateTime.Compare(A, B) >= 0) return false;
                 else return true;

             }
             else return false;*/
            /*int i = date.IndexOf('/');
            int value;
            if (i == -1 | i==date.Length-1) return false;
            else 
            {
                value = int.Parse(date.Substring(0, i));
                if (value < 1 | value > 31) return false;
                else
                {
                    date = date.Substring(i+1);
                }
            }

            i = date.IndexOf('/');
            if (i == -1 | i == date.Length - 1) return false;
            else
            {
                value = int.Parse(date.Substring(0, i));
                if (value < 1 | value > 12) return false;
                else
                {
                    date = date.Substring(i + 1);
                }
            }

             i= date.IndexOf('/');
           
            if (i == -1 | i == date.Length - 1) return false;
            else
            {
                value = int.Parse(date.Substring(0, i));
                if (value < 1900 | value > 2008) return false;
                else
                {
                    return true;
                }
            }

    */

        }
        static public bool gendervalidation(string gender)
        {
            gender = gender.ToLower();
            if (gender == "male" | gender == "female") return true;
            else return false;
        }
        static public bool passwordvalidation(string password)
        {
            return password.Length > 5;
        }
        static public string convertdate(int i)
        {
            int hr = i / 3600;
            i -= hr * 3600;
            int min = i / 60;
            i -= min * 60;
            int sec = i;
            return hr.ToString() + ":" + min.ToString() + "  " + i.ToString() + " sec";

        }
        static public string XML()
        {
            string xml = "<Users>\n";
            for (int i = 0; i < Global.UsersList.Count; i++)
            {
                xml += "\t";
                xml += "<User  Name=" + "'" + Global.UsersList[i].Name + "' E_Mail='" + Global.UsersList[i].Email + "' Gender='" + Global.UsersList[i].Gender + "'>\n";
                xml += "\t\t<Friends NO='" + Global.UsersList[i].FriendsNO.ToString() + "'>";
                int flag = 0;
                for (int f = 0; f < Global.UsersList.Count; f++)
                {
                    if (Global.UsersList[i].FriendsIDs.Contains("," + Global.UsersList[f].ID.ToString() + ","))
                    {
                        if (flag != 0)
                            xml += "-";
                        xml += " " + Global.UsersList[f].Name + " ";
                        flag++;
                        if (flag == Global.UsersList[i].FriendsNO) break;
                    }
                }
                xml += "</Friends>\n";
                for (int j = 0; j < Global.PostsList.Count; j++)
                {
                    int id = Global.UsersList[i].ID;
                    if (Global.PostsList[j].CreatorID == id && Global.PostsList[j].GroupID == 0)
                    {
                        xml += "\t\t<Post Date='" + Global.PostsList[j].Date + "'>\n";
                        xml += "\t\t\t" + Global.PostsList[j].Content + "\n";
                        xml += "\t\t\t<Likes NO='" + Global.PostsList[j].LikesNO.ToString() + "'>";
                        int flag1 = 0;
                        for (int f = 0; f < Global.UsersList.Count; f++)
                        {
                            if (Global.PostsList[j].LikesIDs.Contains("," + Global.UsersList[f].ID.ToString() + ","))
                            {
                                if (flag1 != 0)
                                    xml += "-";
                                xml += " " + Global.UsersList[f].Name + " ";
                                flag1++;
                                if (flag1 == Global.PostsList[j].LikesNO) break;
                            }
                        }
                        xml += "</Likes>\n";
                        for (int k = 0; k < Global.CommentsList.Count; k++)
                        {
                            int postid = Global.PostsList[j].ID;
                            if (Global.CommentsList[k].PostID == postid)
                            {
                                xml += "\t\t\t<Comment Name='";
                                int creator = Global.CommentsList[k].CreatorID;
                                for (int l = 0; l < Global.UsersList.Count; l++)
                                    if (Global.UsersList[l].ID == creator)
                                    { xml += Global.UsersList[l].Name + "' Date='" + Global.CommentsList[k].Date + "'>"; break; }
                                xml += Global.CommentsList[k].Content + "</Comment>\n";
                            }
                        }
                        xml += "\t\t</Post>\n";
                    }
                }
                xml += "\t</User>\n";
            }
            xml += "</Users>\n\n";
            xml += "<Groups>\n";
            for (int i = 0; i < Global.GroupsList.Count; i++)
            {
                xml += "\t<Group Name='" + Global.GroupsList[i].Name + "'>\n";
                xml += "\t\t<Admins>";
                List<int> admins = new List<int>();
                parse(admins, Global.GroupsList[i].Admins, true);
                int flag = 0;
                for (int j = 0; j < admins.Count; j++)
                    for (int k = 0; k < Global.UsersList.Count; k++)
                        if (Global.UsersList[k].ID == admins[j])
                        {
                            if (flag != 0) xml += "-";
                            xml += " " + Global.UsersList[k].Name + " ";
                            flag = 1;
                        }
                xml += "</Admins>\n";
                List<string> members = new List<string>();
                for (int j = 0; j < Global.UsersList.Count; j++)
                    if (Global.UsersList[j].GroupsIDs.Contains("," + Global.GroupsList[i].ID.ToString() + ","))
                        members.Add(Global.UsersList[j].Name);
                xml += "\t\t<Members NO='" + members.Count.ToString() + "'>";
                for (int j = 0; j < members.Count; j++)
                {
                    if (j != 0) xml += ",";
                    xml += " " + members[j] + " ";
                }
                xml += "</Members>\n";
                List<Posts> posts = new List<Posts>();
                for (int j = 0; j < Global.PostsList.Count; j++)
                    if (Global.PostsList[j].GroupID == Global.GroupsList[i].ID)
                        posts.Add(Global.PostsList[j]);
                for (int j = 0; j < posts.Count; j++)
                {
                    xml += "\t\t<Post Creator='";
                    for (int k = 0; k < Global.UsersList.Count; k++)
                        if (posts[j].CreatorID == Global.UsersList[k].ID)
                        { xml += Global.UsersList[k].Name + "'>\n"; break; }
                    xml += "\t\t" + posts[j].Content + "\n";
                    List<int> likes = new List<int>();
                    parse(likes, posts[j].LikesIDs, true);
                    xml += "\t\t\t<Likes NO='" + likes.Count + "'>";
                    for (int k = 0; k < likes.Count; k++)
                        for (int l = 0; l < Global.UsersList.Count; l++)
                            if (likes[k] == Global.UsersList[l].ID)
                            {
                                if (k != 0) xml += "-";
                                xml += " " + Global.UsersList[l].Name + " ";
                                break;
                            }
                    xml += "</Likes>\n";
                    for (int k = 0; k < Global.CommentsList.Count; k++)
                    {
                        int postid = posts[j].ID;
                        if (Global.CommentsList[k].PostID == postid)
                        {
                            xml += "\t\t\t<Comment Name='";
                            int creator = Global.CommentsList[k].CreatorID;
                            for (int l = 0; l < Global.UsersList.Count; l++)
                                if (Global.UsersList[l].ID == creator)
                                { xml += Global.UsersList[l].Name + "' Date='" + Global.CommentsList[k].Date + "'>"; break; }
                            xml += Global.CommentsList[k].Content + "</Comment>\n";
                        }
                    }
                    xml += "\t\t</Post>\n";

                }
                xml += "\t</Group>\n";
            }
            xml += "</Groups>\n";
            return xml;

        }
        static public void search_user(List<Users> _users, string s)
        {

            _users.Clear();
            foreach (Users u in Global.UsersList)
                if (u.Name.Contains(s) || u.Email.Contains(s) || ("0" + u.Mobile.ToString()).Contains(s))
                    _users.Add(u);

        }
        static public void searc_post(List<Posts> _posts, string s)
        {
            _posts.Clear();
            foreach (Posts p in Global.PostsList)
                if (p.Content.Contains(s))
                    _posts.Add(p);

        }
        static public void search_group(List<Groups> _group, string s)
        {
            _group.Clear();
            foreach (Groups g in Global.GroupsList)
                if (g.Name.Contains(s))
                    _group.Add(g);

        }
        static public void Get_members(List<int> output, int id)
        {
            int n = Global.UsersList.Count;
            for (int i = 0; i < n; i++)
            {
                if (Global.UsersList[i].GroupsNO != 0)
                {
                    if (Global.UsersList[i].GroupsIDs.Contains("," + id.ToString() + ",")) output.Add(i + 1);
                }
            }
        }
    }

    public class postitem : BaseAdapter<Posts>
    {
        List<Posts> items;
        Activity context;
        public postitem(Activity context, List<Posts> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Posts this[int position]
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
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.post, null);
            var img = view.FindViewById<ImageView>(Resource.Id.imageinpost);
            var creator = view.FindViewById<TextView>(Resource.Id.postcreator);
            var location = view.FindViewById<TextView>(Resource.Id.postlocation);
            var date = view.FindViewById<TextView>(Resource.Id.postdate);
            var content = view.FindViewById<TextView>(Resource.Id.postcontent);
            var likesnum = view.FindViewById<TextView>(Resource.Id.numlikes_post);
            var like = view.FindViewById<Button>(Resource.Id.likebutton);
            var inputcomment = view.FindViewById<EditText>(Resource.Id.inputcomment);
            var submitcomment = view.FindViewById<Button>(Resource.Id.submitcomment);
            var commentslist = view.FindViewById<TextView>(Resource.Id.commentslistView);
            var showcomments = view.FindViewById<TextView>(Resource.Id.showcomments);
            var more = view.FindViewById<Spinner>(Resource.Id.more_post);
            if (Global.UsersList[item.CreatorID - 1].Gender == "male")
            {
                img.SetImageResource(Resource.Drawable.maninprofile);
            }
            else
            {
                img.SetImageResource(Resource.Drawable.girlinprofile);
            }
            creator.Text = Global.UsersList[item.CreatorID - 1].Name;
            if (item.GroupID == 0)
            {
                location.Visibility = ViewStates.Invisible;
            }
            else
            {
                location.Text = Global.GroupsList[item.GroupID - 1].Name;
            }
            if (item.CreatorID != Global.User.ID) more.Visibility = ViewStates.Invisible;
            date.Text = item.Date + algorithm_functions.convertdate(item.Time);
            content.Text = item.Content;
            likesnum.Text = item.LikesNO.ToString();
            if (item.LikesIDs.Contains("," + Global.User.ID.ToString() + ","))
            {
                like.Text = "Unlike";
            }
            else
            {
                like.Text = "Like";
            }

            img.Click += delegate
            {
                var intent = new Intent(context, typeof(profilecontrol));
                intent.PutExtra("id", item.CreatorID);
                context.StartActivity(intent);
            };
            creator.Click += delegate
            {
                var intent = new Intent(context, typeof(profilecontrol));
                intent.PutExtra("id", item.CreatorID);
                context.StartActivity(intent);

            };
            location.Click += delegate
            {
                var intent = new Intent(context, typeof(profilecontrol));
                intent.PutExtra("id", item.GroupID);
                context.StartActivity(intent);
            };
            more.ItemSelected += delegate
            {
                string a = more.SelectedItem.ToString();
                if (a == "Remove")
                {
                    var alert = new AlertDialog.Builder(context);
                    alert.SetTitle("Keep Connected");
                    alert.SetMessage("Do you want to delete this post?");
                    alert.SetNeutralButton("Yes", delegate {
                        Database.remove_post(item); Global.printedposts.Remove(item); this.NotifyDataSetChanged();
                    });
                    alert.Show();
                }

            };
            like.Click += delegate
            {
                if (like.Text == "Like")
                {
                    like.Text = "Unlike";
                    likesnum.Text = (int.Parse(likesnum.Text) + 1).ToString();
                    algorithm_functions.add_like(item);
                }
                else
                {
                    like.Text = "Like";
                    likesnum.Text = (int.Parse(likesnum.Text) - 1).ToString();
                    algorithm_functions.remove_like(item);
                }
            };
            submitcomment.Click += delegate
            {
                if (inputcomment.Text != null)
                {
                    Database.insert_comment(new Comments(item.ID, Global.User.ID, inputcomment.Text));
                    Comments a = Global.CommentsList[Global.CommentsList.Count - 1];
                    commentslist.Text += Global.User.Name + "    " + a.Date + "    " + algorithm_functions.convertdate(a.Time) + "\n" + a.Content + "\n";

                }
            };
            showcomments.Click += delegate
            {
                commentslist.Text = "";
                List<Comments> a = new List<Comments>();
                algorithm_functions.Get_Comments(a, item.ID);
                int n = a.Count;
                for (int i = 0; i < n; i++)
                {
                    if (i == 0)
                    {
                        commentslist.Text = Global.UsersList[a[i].CreatorID - 1].Name + "    " + a[i].Date + "    " + algorithm_functions.convertdate(a[i].Time) + "\n" + a[i].Content + "\n";
                    }
                    else
                    {
                        commentslist.Text += Global.UsersList[a[i].CreatorID - 1].Name + "    " + a[i].Date + "    " + algorithm_functions.convertdate(a[i].Time) + "\n" + a[i].Content + "\n";
                    }
                }
            };
            return view;
        }

    }

}
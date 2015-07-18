using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;


namespace TCheck.Droid
{
	[Activity(Label = "MyReports_Activity",Theme="@style/MyTheme")]
	public class My_Reports_Activity: AppCompatActivity
	{
		private RecyclerView mRecyclerView;
		private RecyclerView.LayoutManager mLayoutManager;
		private RecyclerView.Adapter mAdapter;
		private MyReports<MyReports> mMyReportsLists;

		private SupportToolbar mToolbar;
		private NavigationBar mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mLeftDrawer;
		private ListView mRightDrawer;
		private ArrayAdapter mLeftAdapter;
		private ArrayAdapter mRightAdapter;
		private List<string> mLeftDataSet;
		private List<string> mRightDataSet;



		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.recycler_report_screen);
			mRecyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerScreen);

			mMyReportsLists = new MyReports<MyReports>();
			mMyReportsLists.Add(new MyReports() { Name = "Firstname Lastname", Subject = "Report Status", Message = "Ready" });
			mMyReportsLists.Add(new MyReports() { Name = "Firstname Lastname", Subject = "Report Status", Message = "Ready" });
			mMyReportsLists.Add(new MyReports() { Name = "Firstname Lastname", Subject = "Report Status", Message = "Ready" });
			mMyReportsLists.Add(new MyReports() { Name = "Firstname Lastname", Subject = "Report Status", Message = "Ready" });




			//Create our layout manager
			mLayoutManager = new LinearLayoutManager(this);
			mRecyclerView.SetLayoutManager(mLayoutManager);
			mAdapter = new RecyclerAdapter(mMyReportsLists, mRecyclerView);
			mMyReportsLists.Adapter = mAdapter;
			mRecyclerView.SetAdapter(mAdapter);

			mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
			mRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);


			mLeftDrawer.Tag = 0;
			mRightDrawer.Tag = 1;

			SetSupportActionBar(mToolbar);

			mLeftDataSet = new List<string>();
			mLeftDataSet.Add(GetString(Resource.String.my_profile));
			mLeftDataSet.Add(GetString(Resource.String.log_out));
			mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
			mLeftDrawer.Adapter = mLeftAdapter;

			//this.mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;
			//this.mRightDrawer.ItemClick += mRightDrawer_ItemClick;

			mRightDataSet = new List<string>();
			mRightDataSet.Add(GetString(Resource.String.drawer_faq));
			mRightDataSet.Add(GetString (Resource.String.support));
			mRightDataSet.Add(GetString(Resource.String.rentproof_summary));
			mRightAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mRightDataSet);
			mRightDrawer.Adapter = mRightAdapter;

			mDrawerToggle = new NavigationBar(
				this,							//Host Activity
				mDrawerLayout,					//DrawerLayout
				Resource.String.openDrawer,		//Opened Message
				Resource.String.closeDrawer		//Closed Message
			);

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			mDrawerToggle.SyncState();



			if (bundle != null){
				if (bundle.GetString("DrawerState") == "Opened"){
					SupportActionBar.SetTitle(Resource.String.openDrawer);
				}

				else{
					SupportActionBar.SetTitle(Resource.String.closeDrawer);
				}
			}

			else{
				//This is the first the time the activity is ran
				SupportActionBar.SetTitle(Resource.String.closeDrawer);
			}
		
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.add_and_remove_bar, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch(item.ItemId)
			{
			case Resource.Id.add:
				//Add button clicked
				mMyReportsLists.Add(new MyReports() { Name = "Firstname Lastname", Subject = "Report Status", Message = "Pending" });
				return true;

			case Resource.Id.discard:
				//Discard button clicked
				mMyReportsLists.Remove(mMyReportsLists.Count - 1);
				return true;

			case Android.Resource.Id.Home:
				mDrawerLayout.CloseDrawer (mRightDrawer);
				mDrawerToggle.OnOptionsItemSelected(item);
				return true;
			
			case Resource.Id.action_help:
				if (mDrawerLayout.IsDrawerOpen (mRightDrawer)) {
					//Right Drawer is already open, close it
					mDrawerLayout.CloseDrawer (mRightDrawer);
				} else {
					//Right Drawer is closed, open it and just in case close left drawer
					mDrawerLayout.OpenDrawer (mRightDrawer);
					mDrawerLayout.CloseDrawer (mLeftDrawer);
				}
				return true;

			}
			return base.OnOptionsItemSelected(item);
		}
		protected override void OnSaveInstanceState (Bundle outState){
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left)){
				outState.PutString("DrawerState", "Opened");
			}

			else{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState){
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig){
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}
	}


	public class MyReports<T>
	{
		private List<T> mItems;
		private RecyclerView.Adapter mAdapter;

		public MyReports()
		{
			mItems = new List<T>();
		}

		public RecyclerView.Adapter Adapter
		{
			get { return mAdapter; }
			set { mAdapter = value; }
		}

		public void Add(T item)
		{
			mItems.Add(item);

			if (Adapter != null)
			{
				Adapter.NotifyItemInserted(0);
			}

		}

		public void Remove (int position)
		{
			mItems.RemoveAt(position);

			if (Adapter != null)
			{
				Adapter.NotifyItemRemoved(0);
			}
		}

		public T this[int index]
		{
			get { return mItems[index]; }
			set { mItems[index] = value; }
		}

		public int Count
		{
			get { return mItems.Count; }
		}

	}

	public class RecyclerAdapter : RecyclerView.Adapter
	{
		private MyReports<MyReports> mMyReportsLists;
		private RecyclerView mRecyclerView;

		public RecyclerAdapter(MyReports<MyReports> MyReportss, RecyclerView recyclerView)
		{
			mMyReportsLists = MyReportss;
			mRecyclerView = recyclerView;
		}

		public class MyView : RecyclerView.ViewHolder
		{
			public View mMainView { get; set; }
			public TextView mName { get; set; }
			public TextView mSubject { get; set; }
			public TextView mMessage { get; set; }

			public MyView (View view) : base(view)
			{
				mMainView = view;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.report_row, parent, false);

			TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
			TextView txtSubject = row.FindViewById<TextView>(Resource.Id.txtSubject);
			TextView txtMessage = row.FindViewById<TextView>(Resource.Id.txtMessage);

			MyView view = new MyView(row) { mName = txtName, mSubject = txtSubject, mMessage = txtMessage };
			return view;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			MyView myHolder = holder as MyView;
			int indexPosition = (mMyReportsLists.Count - 1) - position;
			myHolder.mMainView.Click += mMainView_Click;

			myHolder.mName.Text = mMyReportsLists[indexPosition].Name;
			myHolder.mSubject.Text = mMyReportsLists[indexPosition].Subject;
			myHolder.mMessage.Text = mMyReportsLists[indexPosition].Message;
		}

		void mMainView_Click(object sender, EventArgs e)
		{
			//int position = mRecyclerView.GetChildPosition((View)sender);
			//int position = mRecyclerView.GetChildAdapterPosition((View)sender);
			//int indexPosition = (mMyReportsLists.Count - 1) - position;
			//Console.WriteLine(mMyReportsLists[indexPosition].Name);


	

		}

	




		public override int ItemCount
		{
			get { return mMyReportsLists.Count; }
		}
	}
}


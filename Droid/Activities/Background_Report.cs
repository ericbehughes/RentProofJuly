using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Content;

using Android.Runtime;
using Android.Views;
using Android.Widget;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;


namespace TCheck.Droid
{
	[Activity(Label = "",Theme="@style/MyTheme")]
	public class Background_Report : AppCompatActivity
	{
		private RecyclerView _recyclerView;
		private RecyclerView.LayoutManager _layoutManager;
		private BackgroundCheckRecyclerViewAdapter  _adapter;

		private ProgressDialog _progressDialog;

		private SupportToolbar mToolbar;
		private NavigationBar mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mLeftDrawer;
		private ListView mRightDrawer;
		private ArrayAdapter mLeftAdapter;
		private ArrayAdapter mRightAdapter;
		private List<string> mLeftDataSet;
		private List<string> mRightDataSet;



		protected override async void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.My_BackgroundReports);

			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
			_progressDialog.SetMessage("Loading Team . . .");
			_progressDialog.Show();

			//If the device is portrait, then show the RecyclerView in a vertical list,
			//else show it in horizontal list.
			_layoutManager = Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait 
				? new LinearLayoutManager(this, LinearLayoutManager.Vertical, false) 
				: new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);

			//Experiement with a GridLayoutManger! You can create some cool looking UI!
			//This create a gridview with 2 rows that scrolls horizontally.
			//            _layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Horizontal, false);

			//Create a reference to our RecyclerView and set the layout manager;
			_recyclerView = FindViewById<RecyclerView>(Resource.Id.mainActivity_recyclerView);
			_recyclerView.SetLayoutManager(_layoutManager);

			//Get our crew member data. This could be a web service.
			SharedData.CrewManifest = await BackgroundCheck_List_Data.GetAllCrewAsync();

			//Create the adapter for the RecyclerView with our crew data, and set
			//the adapter. Also, wire an event handler for when the user taps on each
			//individual item.
			_adapter = new BackgroundCheckRecyclerViewAdapter(SharedData.CrewManifest, this.Resources);
			_adapter.ItemClick += OnItemClick;
			_recyclerView.SetAdapter(_adapter);

			_progressDialog.Dismiss();

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








		public override bool OnOptionsItemSelected (IMenuItem item){		
			switch (item.ItemId){

			case Android.Resource.Id.Home:
				//The hamburger icon was clicked which means the drawer toggle will handle the event
				//all we need to do is ensure the right drawer is closed so the don't overlap
				mDrawerLayout.CloseDrawer (mRightDrawer);
				mDrawerToggle.OnOptionsItemSelected(item);


				return true;

			case Resource.Id.toolbar:
				//Refresh
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

			default:
				return base.OnOptionsItemSelected (item);
			}
		}

		public override bool OnCreateOptionsMenu (IMenu menu){
			MenuInflater.Inflate (Resource.Menu.main_action_bar, menu);
			return base.OnCreateOptionsMenu (menu);
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
		//This method doesnt work for some reason
		/*protected override void OnPostCreate (Bundle savedInstanceState){
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}
		*/
		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig){
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}



		private void OnItemClick (object sender, int position)
		{
			var crewProfileIntent = new Intent(this, typeof(BackgroundCheckProfileActivity));
			crewProfileIntent.PutExtra("index", position);
			crewProfileIntent.PutExtra("imageResourceId", SharedData.CrewManifest[position].PhotoResourceId);

			StartActivity(crewProfileIntent);
		}
	}

	internal static class SharedData
	{
		public static List<BackgroundCheck> CrewManifest { get; set;}
	}
}



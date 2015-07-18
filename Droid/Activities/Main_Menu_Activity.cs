using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;

namespace TCheck.Droid{
	[Activity (Label = "main_menu_activity",Theme="@style/MyTheme")]
	public class Main_Menu_Activity : AppCompatActivity{

		private Button mButtonBackgroundCheck; 
		private Button mButtonMyReview;
		private Button mButtonMyQueries;
		private Button mButtonMyProfile;

		private SupportToolbar mToolbar;
		private NavigationBar mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mLeftDrawer;
		private ListView mRightDrawer;
		private ArrayAdapter mLeftAdapter;
		private ArrayAdapter mRightAdapter;
		private List<string> mLeftDataSet;
		private List<string> mRightDataSet;


		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.Main_Menu);

			mButtonBackgroundCheck = FindViewById<Button>(Resource.Id.buttonQuery);
			mButtonBackgroundCheck.Click += mButtonBackgroundCheck_Click;

			mButtonMyReview = FindViewById<Button>(Resource.Id.buttonReview);
			mButtonMyReview.Click += mButtonMyReview_Click;

			mButtonMyQueries = FindViewById<Button>(Resource.Id.buttonMyQueries);
			mButtonMyQueries.Click += mButtonMyQueries_Click;

			mButtonMyProfile = FindViewById<Button>(Resource.Id.buttonMyProfile);
			mButtonMyProfile.Click += mButtonMyProfile_Click;

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

			this.mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;
			this.mRightDrawer.ItemClick += mRightDrawer_ItemClick;

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


		void mLeftDrawer_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			
			switch (e.Position) {

			case 0:
				Intent mDrawerButtonMyProfile = new Intent (this, typeof(Main_Menu_Activity));
				this.StartActivity (mDrawerButtonMyProfile);
				break;
		
			case 1:
				Intent mLogout = new Intent (this, typeof(MainActivity));
				this.StartActivity (mLogout);
			break;
			}
		}

		void mRightDrawer_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{

			switch (e.Position) {

			case 0:
				Intent mDrawerButtonFAQ = new Intent (this, typeof(Main_Menu_Activity));
				this.StartActivity (mDrawerButtonFAQ);
				break;

			case 1:
				Intent mDrawerButtonSupport = new Intent (this, typeof(Main_Menu_Activity));
				this.StartActivity (mDrawerButtonSupport);
				break;
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

		protected override void OnPostCreate (Bundle savedInstanceState){
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig){
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}


		void mButtonBackgroundCheck_Click (object sender, EventArgs args){
			Intent intent = new Intent (this, typeof(Query_Payment_Activity));
			this.StartActivity (intent);
		}

		void mButtonMyQueries_Click (object sender, EventArgs args){
			Intent intent = new Intent (this, typeof(Background_Report));
			this.StartActivity (intent);
		}

		void mButtonMyReview_Click (object sender, EventArgs e){
			Intent intent = new Intent (this, typeof(My_Reviews_Activity));
			this.StartActivity (intent);
		}



		void mButtonMyProfile_Click (object sender, EventArgs args){
			Intent intent = new Intent (this, typeof(Tenant_Search));
			this.StartActivity (intent);
		}
	
	}
}







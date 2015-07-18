
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

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace TCheck.Droid
{
	[Activity (Label = "Tenant_Search",Theme="@style/MyTheme")]			
	public class Tenant_Search3 : AppCompatActivity

	{
		private Button mButtonDislike;
		private Button mButtonLike;


		private SupportToolbar mToolbar;
		private NavigationBar mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mLeftDrawer;
		private ListView mRightDrawer;
		private ArrayAdapter mLeftAdapter;
		private ArrayAdapter mRightAdapter;
		private List<string> mLeftDataSet;
		private List<string> mRightDataSet;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Tenant_Search3);


			mButtonDislike = FindViewById<Button>(Resource.Id.buttonDislike);
			mButtonDislike.Click += (object sender, EventArgs args) =>{
				//pull up dialog
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				Support_PopUp supportPopUp = new Support_PopUp();
				supportPopUp.Show(transaction, "do you like fragment");
				supportPopUp.mFeatureSurveyComplete += mSupportPopUpButton_Click;
			};

			mButtonLike = FindViewById<Button>(Resource.Id.buttonLike);
			mButtonLike.Click += (object sender, EventArgs args) =>{
				//pull up dialog
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				Support_PopUp supportPopUp = new Support_PopUp();
				supportPopUp.Show(transaction, "do you like fragment");
				supportPopUp.mFeatureSurveyComplete += mSupportPopUpButton_Click;
			};

			mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
			mRightDrawer = FindViewById<ListView>(Resource.Id.right_drawer);





			//**************TOOLBAR***************************/
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

			// Create your application here
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
				Intent mDrawerButtonSupport = new Intent (this, typeof(Support_PopUp));
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


		void mDislikeButton_CLick (object sender, EventArgs args){
			Intent intent = new Intent (this, typeof(Tenant_Search));
			this.StartActivity (intent);
			Finish ();
		}

		void mLikeButton_CLick (object sender, EventArgs e){
			Intent intent = new Intent (this, typeof(Tenant_Search));
			this.StartActivity (intent);
			Finish ();
		}

		void mSupportPopUpButton_Click (object sender, OnSupportEvent e){
			Intent intent = new Intent (this, typeof(Tenant_Search));
			this.StartActivity (intent);
			Finish (); 
		}
	}


}


using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TCheck.Droid{
	[Activity (Label = "RentProof", MainLauncher = true, Icon = "@drawable/rent_proof_icon" )]
	public class MainActivity : Activity{
		//Icon = "@drawable/rent_proof_icon"
		private Button mButtonSignUp;
		private Button mButtonSignIn;
	
		protected override void OnCreate (Bundle bundle){
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Launch);
			mButtonSignIn = FindViewById<Button>(Resource.Id.buttonSignIn);
			mButtonSignIn.Click += mButtonSignIn_Click;
			mButtonSignUp = FindViewById<Button>(Resource.Id.buttonSignUp);
			mButtonSignUp.Click += (object sender, EventArgs args) =>{
				//pull up dialog
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				SignUpPopUp signUpPopUp = new SignUpPopUp();
				signUpPopUp.Show(transaction, "sign_up fragment");
				signUpPopUp.mOnSignUpComplete += signUpPopUp_mOnSignUpComplete;
				};
				

		}

		void signUpPopUp_mOnSignUpComplete (object sender, OnSignUpEvent e){
			Intent intent = new Intent (this, typeof(Main_Menu_Activity));
			this.StartActivity (intent);
			Finish (); 
		}

		void mButtonSignIn_Click (object sender, EventArgs e){
			Intent intent = new Intent (this, typeof(Login_Activity));
			this.StartActivity (intent);
			Finish (); 
		}


	}
}



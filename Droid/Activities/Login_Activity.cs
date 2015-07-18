
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

namespace TCheck.Droid{
	[Activity (Label = "login_activity")]
	public class Login_Activity : Activity{
		private Button mButtonLogin;

		protected override void OnCreate (Bundle bundle){
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);
			mButtonLogin = FindViewById<Button>(Resource.Id.buttonLogIn);
			mButtonLogin.Click += mButtonLogin_Click;

		}

		void mButtonLogin_Click (object sender, EventArgs args){
			Intent intent = new Intent (this, typeof(Main_Menu_Activity));
			this.StartActivity (intent);
			Finish ();

		}
	}
}


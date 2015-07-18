
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
	[Activity (Label = "survey_activity")]			
	public class survey_activity : Activity{
		private Button mButtonSurveySubmit;
		//private Button mButtonMenuButton;

		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Submit_Survey);
		
			mButtonSurveySubmit = FindViewById<Button>(Resource.Id.buttonSurveySubmit);
			mButtonSurveySubmit.Click += mButtonSurveySubmit_Click;
		}

		void mButtonSurveySubmit_Click (object sender, EventArgs e){
			Intent intent = new Intent (this, typeof(Main_Menu_Activity));
			this.StartActivity (intent);
			Finish ();

		}

	}
}




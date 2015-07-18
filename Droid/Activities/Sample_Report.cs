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
	[Activity (Label = "sample_report",Theme="@style/MyTheme")]		
	public class Sample_Report : Activity{



		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();

		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Accordion_SampleReportNOTUSING);



			CreateExpendableListData ();

			var ctlExListBox = FindViewById<ExpandableListView> (Resource.Id.ctlExListBox);
			ctlExListBox.SetAdapter (new ExpendListAdapter (this, dictGroup));

			ctlExListBox.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
				var itmGroup = lstKeys [e.GroupPosition];
				var itmChild = dictGroup [itmGroup] [e.ChildPosition];

				Toast.MakeText (this, string.Format ("You Click on Group {0} with child {1}", itmGroup, itmChild), 
					ToastLength.Long).Show ();	
				
			};



		}
			void CreateExpendableListData ()
		{
			for (int iGroup = 1; iGroup <= 4; iGroup++) {
				var lstChild = new List<string> ();
				for (int iChild = 1; iChild <= 3; iChild++) {
					lstChild.Add (string.Format ("Criteria {0}", iGroup));
				}
				if (iGroup == 1)
					dictGroup.Add (string.Format ("RentScore"), lstChild);
				if (iGroup == 2)
					dictGroup.Add (string.Format ("CreditCheck"), lstChild);
				if (iGroup == 3)
					dictGroup.Add (string.Format ("BackgroundCheck"), lstChild);
				if (iGroup == 4)
					dictGroup.Add (string.Format ("SomethingCheck"), lstChild);
					

			}

			lstKeys = new List<string> (dictGroup.Keys);
		}


	}
}





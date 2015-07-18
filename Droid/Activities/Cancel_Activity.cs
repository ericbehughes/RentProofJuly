using System;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TCheck.Droid{
	[Activity (Label = "cancel_activity")]
	public class OnCancelEvent : EventArgs{
		public OnCancelEvent(){}
	}
	
	class Cancel_Activity : DialogFragment{
		private Button mYesCancelButton;
		private Button mNoCancelButton;

		public event EventHandler<OnCancelEvent> mOnCancel ;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
			
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.Cancel_PopUp, container, false);

			mYesCancelButton = view.FindViewById<Button> (Resource.Id.buttonYesCancel);
			mYesCancelButton.Click += mYesCancelButton_Click; 

			mNoCancelButton = view.FindViewById<Button> (Resource.Id.buttonNoCancel);
			mNoCancelButton.Click += mNoCancelButton_Click; 
			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState){
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.popup_animation;
		}

		void mYesCancelButton_Click(object sender, EventArgs e){
			//user has clicked on signup button
			mOnCancel.Invoke(this, new OnCancelEvent());
			this.Dismiss();
		}
		void mNoCancelButton_Click(object sender, EventArgs e){
			//user has clicked on signup button
			//mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text,mTxtSecurityNumber.Text, mTxtPassword.Text ));
			this.Dismiss();
		}
	}
}


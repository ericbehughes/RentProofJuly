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
	public class OnSignUpEvent : EventArgs{
		private string mFirstName;
		private string mEmail;
		private string mSecurityNumber;
		private string mPassword;

		public string FirstName{
			get{ return mFirstName;}
			set {mFirstName = value;}
		}
		public string Email{
			get{ return mEmail;}
			set {mEmail = value;}
		}
		public string SecurityNumber{
			get{ return mSecurityNumber;}
			set {mSecurityNumber = value;}
		}
		public string Password{
			get{ return mPassword;}
			set {mPassword = value;}
		}


		public OnSignUpEvent(string firstName, string email, string securitynumber, string password): base() {
			mFirstName = firstName;
			mEmail = email;
			mSecurityNumber = securitynumber;
			mPassword = password;

		}
	}
	class SignUpPopUp : DialogFragment{
		private EditText mTxtFirstName;
		private EditText mTxtEmail;
		private EditText mTxtSecurityNumber;
		private EditText mTxtPassword;
		private Button mPopUpButton;

		public event EventHandler<OnSignUpEvent> mOnSignUpComplete;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.SignUp_Pop_Up, container, false);
			mTxtFirstName = view.FindViewById<EditText> (Resource.Id.txtFirstName);
			mTxtEmail = view.FindViewById<EditText> (Resource.Id.txtEmail);
			mTxtSecurityNumber = view.FindViewById<EditText> (Resource.Id.txtSecurityNumber);
			mTxtPassword = view.FindViewById<EditText> (Resource.Id.txtPassword);
			mPopUpButton = view.FindViewById<Button> (Resource.Id.popUpButton);

			mPopUpButton.Click += mPopUpButton_Click; 

			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState){
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.popup_animation;
		}

		void mPopUpButton_Click(object sender, EventArgs e){

			//user has clicked on signup button
			mOnSignUpComplete.Invoke(this, new OnSignUpEvent(mTxtFirstName.Text, mTxtEmail.Text,mTxtSecurityNumber.Text, mTxtPassword.Text ));
			this.Dismiss ();



		}
	}
}


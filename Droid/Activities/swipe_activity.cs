
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
	
	public class swipe_activity :   Activity, GestureDetector.IOnGestureListener{
		private GestureDetector _gestureDetector;
		private TextView _textView;

		protected override void OnCreate(Bundle bundle){
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Tenant_Search);
			//_textView = FindViewById<TextView>(Resource.Id.velocity_text_view);
			//_textView.Text = "Fling Velocity: ";
			//_gestureDetector = new GestureDetector(this);
		}

		public override bool OnTouchEvent(MotionEvent e){
			_gestureDetector.OnTouchEvent(e);
			return false;
		}

		public bool OnDown(MotionEvent e){
			return false;
		}

		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY){
			_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
			return true;
		}

		public void OnLongPress(MotionEvent e) {}

		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY){
			return false;
		}

		public void OnShowPress(MotionEvent e) {}

		public bool OnSingleTapUp(MotionEvent e){
			return false;
		}
	}
}
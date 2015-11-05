using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Framework;
using Android.Graphics;

namespace Implementation
{
	class AndroidInput : IInput
	{
		ITouchHandler touchHandler;

		public AndroidInput (Context ctxt, View view, float scaleX, float scaleY)
		{
			touchHandler = new MultiTouchHandler (view, scaleX, scaleY);
		}


		#region IInput implementation
		public bool IsTouchDown (int pointer)
		{
			return touchHandler.IsTouchDown (pointer);
		}
		public int GetTouchX (int pointer)
		{
			return touchHandler.GetTouchX (pointer);
		}
		public int GetTouchY (int pointer)
		{
			return touchHandler.GetTouchY (pointer);
		}
		public System.Collections.Generic.List<TouchEvent> TouchEvents
		{
			get { return touchHandler.TouchEvents; }
		}
		#endregion
	}

}



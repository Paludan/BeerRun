using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using GameFramework;
using Android.Graphics;
using System.Collections.Generic;

namespace Implementation
{
	
	public class MultiTouchHandler : ITouchHandler
	{
		#region properties
		private static readonly int MAX_TOUCHPOINTS = 10;

		private bool[] isTouched = new bool[MAX_TOUCHPOINTS];
		private int[] touchX = new int[MAX_TOUCHPOINTS];
		private int[] touchY = new int[MAX_TOUCHPOINTS];
		private int[] id = new int[MAX_TOUCHPOINTS];

		Pool<TouchEvent> touchEventPool;
		List<TouchEvent> touchEvents = new List<TouchEvent>();
		List<TouchEvent> touchEventBuffer = new List<TouchEvent>();

		float scaleX, scaleY;

		/// <summary>
		/// Gets the touch events.
		/// </summary>
		/// <value>The touch events.</value>
		public List<TouchEvent> TouchEvents{
			get { 
				lock (this) {
					int len = touchEvents.Count;
					for (int i = 0; i < len; i++) 
						touchEventPool.Free (touchEvents [i]);
					touchEvents.Clear ();
					touchEvents.AddRange (touchEventBuffer);
					touchEventBuffer.Clear ();
					return touchEvents;
				}
			}
		}
		#endregion

		public MultiTouchHandler (View view, float scaleX, float scaleY)
		{
			this.scaleX = scaleX;
			this.scaleY = scaleY;
			touchEventPool = new Pool<TouchEvent>(touchEventCreater, 100);
			view.SetOnTouchListener (this);
		}

		private TouchEvent touchEventCreater()
		{
			return new TouchEvent ();
		}

		public bool IsTouchDown (int pointer)
		{
			lock (this) {
				int index = getIndex (pointer);
				if (index < 0 || index >= MAX_TOUCHPOINTS)
					return false;
				else
					return true;
			}
		}
		public int GetTouchX (int pointer)
		{
			lock (this) {
				int index = getIndex (pointer);
				if (index < 0 || index >= MAX_TOUCHPOINTS)
					return 0;
				else
					return touchX [index];
			}
		}
		public int GetTouchY (int pointer)
		{
			lock (this) {
				int index = getIndex (pointer);
				if (index < 0 || index >= MAX_TOUCHPOINTS)
					return 0;
				else
					return touchY [index];
			}
		}

		private int getIndex(int pointer){
			for (int i = 0; i < MAX_TOUCHPOINTS; i++) {
				if (id [i] == pointer) {
					return i;
				}
			}
			return -1;
		}

		public bool OnTouch (View v, MotionEvent e)
		{
			lock (this) {
				int action = e.Action & MotionEventActions.Mask;
				int pointerIndex = (e.Action & MotionEventActions.PointerIdMask >> MotionEventActions.PointerIdShift);
				int pointerCount = e.PointerCount;
				TouchEvent touchEvent;

				for (int i = 0; i < MAX_TOUCHPOINTS; i++) {
					//Resets touch-events with an index higher than the found number of current touches
					if (i >= pointerCount) {
						isTouched [i] = false;
						id [i] = -1;
					}
					int pointerId = e.GetPointerId (i);
					if (e.Action != MotionEventActions.Move && i != pointerIndex)
						continue;

					switch (action) {
					case MotionEventActions.Down:
					case MotionEventActions.PointerDown:
						touchEvent = touchEventPool.NewObject ();
						touchEvent.type = TouchEvent.TOUCH_DOWN;
						touchEvent.pointer = pointerId;
						touchEvent.x = touchX [i] = (int)(e.GetX (i) * scaleX);
						touchEvent.y = touchY [i] = (int)(e.GetY (i) * scaleY);
						isTouched [i] = true;
						id [i] = pointerId;
						touchEventBuffer.Add (touchEvent);
						break;

					case MotionEventActions.Up:
					case MotionEventActions.PointerUp:
					case MotionEventActions.Cancel:
						touchEvent = touchEventPool.NewObject ();
						touchEvent.type = TouchEvent.TOUCH_UP;
						touchEvent.pointer = pointerId;
						touchEvent.x = touchX [i] = (int)(e.GetX (i) * scaleX);
						touchEvent.y = touchY [i] = (int)(e.GetY (i) * scaleY);
						isTouched [i] = false;
						id [i] = -1;
						touchEventBuffer.Add (touchEvent);
						break;

					case MotionEventActions.Move:
						touchEvent = touchEventPool.NewObject();
						touchEvent.type = TouchEvent.TOUCH_DRAGGED;
						touchEvent.pointer = pointerId;
						touchEvent.x = touchX[i] = (int) (e.GetX(i) * scaleX);
						touchEvent.y = touchY[i] = (int) (e.GetY(i) * scaleY);
						isTouched[i] = true;
						id[i] = pointerId;
						touchEventBuffer.Add(touchEvent);
						break;
					}
				}

				return true;
			}
		}
		public void Dispose ()
		{
			isTouched = null;
			touchX = null;;
			touchY = null;
			id = null;

			touchEventPool = null;
			touchEvents.Clear();
			touchEventBuffer.Clear ();;
		}
		public IntPtr Handle {
			get {
				return new IntPtr ();
			}
		}
	}



}



using System;
using System.Collections.Generic;

namespace Framework
{
	public class TouchEvent {
		public static readonly int TOUCH_DOWN = 0;
		public static readonly int TOUCH_UP = 1;
		public static readonly int TOUCH_DRAGGED = 2;
		public static readonly int TOUCH_HOLD = 3;

		public int type;
		public int x, y;
		public int pointer;
	}

	public interface IInput
	{
		bool IsTouchDown (int pointer);
		int GetTouchX(int pointer);
		int GetTouchY(int pointer);
		List<TouchEvent> GetTouchEvents();
	}
}


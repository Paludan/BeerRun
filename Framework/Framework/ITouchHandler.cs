using System;
using System.Collections.Generic;

namespace Framework
{
	public interface ITouchHandler :Android.Views.View.IOnTouchListener
	{
		bool IsTouchDown(int pointer);

		int GetTouchX(int pointer);
		int GetTouchY(int pointer);

		List<TouchEvent> TouchEvents { get; }
	}
}


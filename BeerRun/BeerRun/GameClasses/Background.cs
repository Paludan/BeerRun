using System;

using Implementation;
using Framework;

namespace BeerRun
{
	public class Background
	{
		private int _x, _y, _speedX;
		public int x {
			get { return _x; }
		}
		public int y {
			get { return _y; }
		}
		public int speedX {
			get { return _speedX; }
			set { _speedX = value; }
		}
		private AndroidImage _bgImage;
		public AndroidImage Image {
			get { return _bgImage; }
		}

		private Robot robot;

		public Background (int x, int y, AndroidImage image, Robot r)
		{
			this._x = x;
			this._y = y;
			_speedX = 0;

			this._bgImage = image;
		}

		public void Update(float deltaTime)
		{
			if (robot.speedX <= 0)
				this._speedX = 0;
			
			_x += _speedX;

			if (_x <= -2160)
				_x += 4320;
		}
	}

}


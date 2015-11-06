using System;

using Implementation;
using Framework;

namespace BeerRun
{
	public class Background
	{
		public int x, y, _speedX;
		public int speedX {
			set { _speedX = value; }
		}
		private AndroidImage _bgImage;
		public AndroidImage Image {
			get { return _bgImage; }
		}

		public Background (int x, int y, AndroidImage image)
		{
			this.x = x;
			this.y = y;
			_speedX = 0;

			this._bgImage = image;
		}

		public void Update(float deltaTime)
		{
			x += _speedX;

			if (x <= -2160)
				x += 4320;
		}
	}

}


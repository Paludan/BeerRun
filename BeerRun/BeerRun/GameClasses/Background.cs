using System;

using Implementation;
using Framework;

namespace BeerRun
{
	public class Background
	{
		public int x, y;
		private AndroidImage _bgImage;
		public AndroidImage Image {
			get { return _bgImage; }
		}

		public Background (int x, int y, AndroidImage image)
		{
			this.x = x;
			this.y = y;

			this._bgImage = image;
		}

		public void Update(float deltaTime)
		{
			x++;
		}
	}

}


using System;

using Implementation;
using Framework;

namespace BeerRun
{
	class Heliboy : Enemy
	{
		public Heliboy (int x, int y, Background bg, Robot r)
			: base (bg, r, x, y, PictureManager.Pictures ["heliboy1"])
		{
			
		}
	}

}


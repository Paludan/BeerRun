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

		#region implemented abstract members of AbsEntity

		public override void Die ()
		{
			//Moves the heliboy out of the screen
			this._x = -100;
		}

		#endregion

		#region implemented abstract members of Enemy

		public override void Attack ()
		{
			//Purposly left blank
		}

		#endregion
	}

}


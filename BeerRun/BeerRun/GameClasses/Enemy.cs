using System;
using Framework;
using Android.Graphics;

namespace BeerRun
{
	public abstract class Enemy : AbsEntity
	{
		protected int power, movementSpeed;
		protected Background bg;
		protected Robot robot;

		public Enemy (Background bg, Robot r, int x, int y, IImage image)
			: base (x, y, image)
		{
			robot = r;
			this.bg = bg;
		}

		public override void Update (float deltaTime)
		{
			base.Update (deltaTime);

			follow ();
			_speedX = bg.speedX * 5 + movementSpeed;

			collisionBoundary.Boundary.Set (_x - (Image.Width / 2), _y - (Image.Height / 2), _x + (Image.Width / 2), _y + (Image.Height / 2));

			//if (Rect.Intersects (collisionBoundary, robot.collosionBoundary))
			//	checkCollision ();
		}

		/// <summary>
		/// Follows the robot in the game.
		/// </summary>
		private void follow(){
			if (x < -95 || x > 810)
				movementSpeed = 0;
			else if (Math.Abs (robot.x - this.x) < 5)
				movementSpeed = 0;
			else {
				if (robot.x >= x)
					movementSpeed = 1;
				else
					movementSpeed = -1;
			}
		}

		public abstract void Attack ();
	}
}


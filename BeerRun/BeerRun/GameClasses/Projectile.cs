using System;
using Android.Graphics;
using System.Collections.Generic;

namespace BeerRun
{
	public class Projectile
	{
		private int _x, _y, _speedX;
		private readonly int xOFFSET = 50, yOFFSET = -25, HEIGHT = 5, WIDTH = 10;

		private bool _visible;
		public bool Visible {
			get { return _visible; }
		}

		private Rect boundary;

		public Projectile (int x, int y)
		{
			_x = x + xOFFSET;
			_y = y + yOFFSET;
			_speedX = 7;
			_visible = true;

			boundary = new Rect ();
		}

		public void Update(List<Enemy> enemies){
			_x += _speedX;

			boundary.Set (_x, _y, _x + WIDTH, _y + HEIGHT);

			if (_x > 800) {
				_visible = false;
				boundary = null;
			} else
				foreach (var enemy in enemies) {
					checkCollision (enemy);
				}

		}

		private void checkCollision(Enemy e){
			if (Rect.Intersects (boundary, e.CollisionBoundary)) {
				_visible = false;

				e.Lives--;
			}
		}

		public void Paint(){

		}
	}
}


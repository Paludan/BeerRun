using System;
using Framework;
using Android.Graphics;

namespace BeerRun
{
	public abstract class AbsEntity
	{
		protected int _x, _y, _speedX;
		public int x{
			get { return _x; }
		}
		public int y{
			get { return _y; }
		}

		protected int _lives;
		public int Lives {
			get { return _lives; }
		}

		protected IImage _image;
		public IImage Image {
			get { return _image; }
		}

		protected Rect collisionBoundary;
		public Rect CollisionBoundary{
			get { return collisionBoundary; }
		}

		public AbsEntity (int x, int y, IImage image)
		{
			this._x = x;
			this._y = y;
			this._image = image;
		}

		public virtual void Paint(IGraphics graphics){

		}

		public virtual void Update(float deltaTime){
			this.x += _speedX;
		}

		public abstract void Die ();
	}
}


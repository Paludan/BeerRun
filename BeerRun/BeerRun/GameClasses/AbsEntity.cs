using System;
using Framework;
using Android.Graphics;
using Implementation;

namespace BeerRun
{
	public abstract class AbsEntity
	{
		protected int _x, _y, _speedX, _speedY, _lives;

		/// <summary>
		/// Gets the x-coordinate.
		/// </summary>
		/// <value>The x-coordinate.</value>
		public int x{
			get { return _x; }
		}
		/// <summary>
		/// Gets the y-coordinate.
		/// </summary>
		/// <value>The y-coordinate.</value>
		public int y{
			get { return _y; }
		}
		/// <summary>
		/// Gets the lives left.
		/// </summary>
		/// <value>The lives.</value>
		public int Lives {
			get { return _lives; }
			set { _lives = value; }
		}

		protected IImage _image;
		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public IImage Image {
			get { return _image; }
		}

		protected Collider collisionBoundary;
		/// <summary>
		/// Gets the collision boundary used to determine which other objects to check for collisions with.
		/// </summary>
		/// <value>The collision boundary.</value>
		public Collider CollisionBoundary{
			get { return collisionBoundary; }
		}

		public AbsEntity (int x, int y, IImage image)
		{
			this._x = x;
			this._y = y;
			this._image = image;
		}

		/// <summary>
		/// Paint this game-object.
		/// </summary>
		/// <param name="graphics">Graphics.</param>
		public virtual void Paint(IGraphics graphics){

		}

		/// <summary>
		/// Update the game-object with specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public virtual void Update(float deltaTime){
			this._x += _speedX;
			if (_lives <= 0)
				Die ();
		}

		/// <summary>
		/// Indicates what should happen upon death.
		/// </summary>
		public abstract void Die ();
	}
}


using System;
using Android.Graphics;
using Framework;

namespace Implementation
{
	public class Collider
	{
		public Rect Boundary;
		private readonly int leftOffset, rightOffset, topOffset, bottomOffset; 

		public Collider (int leftOffset, int rightOffset, int topOffset, int bottomOffset)
		{
			this.leftOffset = leftOffset;
			this.rightOffset = rightOffset;
			this.topOffset = topOffset;
			this.bottomOffset = bottomOffset;

			Boundary = new Rect (0, 0, 0, 0);
		}

		/// <summary>
		/// Update this collider to specified newX and newY.
		/// </summary>
		/// <param name="newX">New x.</param>
		/// <param name="newY">New y.</param>
		public void Update(int newX, int newY){
			Boundary.Set (newX + leftOffset, newY + topOffset, newX + rightOffset, newY + bottomOffset);
		}

		/// <summary>
		/// Paints the outline of the collider in purple.
		/// </summary>
		/// <param name="g">The graphics component.</param>
		public void PaintOutline(IGraphics g){
			g.DrawRect (Boundary.Left, Boundary.Top, Boundary.Right - Boundary.Left, Boundary.Bottom - Boundary.Top, Color.Purple);
		}

		public static implicit operator Rect(Collider c){
			return c.Boundary;
		}
	}
}


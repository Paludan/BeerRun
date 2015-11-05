using System;
using Android.Graphics;
using Framework;
using Implementation;

namespace BeerRun
{
	public class GameButton
	{
		#region properties
		private Rect boundary;
		public int x {
			get { return boundary.Left; }
		}
		public int y {
			get { return boundary.Top; }
		}
		public int Width {
			get { return boundary.Width(); }
		}
		public int Height {
			get { return boundary.Height(); }
		}

		private Color _color;
		public Color Color {
			set { _color = value; }
		}
		#endregion
		/// <summary>
		/// Initializes a new instance of the <see cref="BeerRun.GameButton"/> class.
		/// </summary>
		/// <param name="x">The x coordinate of upper left corner.</param>
		/// <param name="y">The y coordinate of upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <remarks>The color of the button is defaulted as white</remarks>
		public GameButton (int x, int y, int width, int height)
			: this (x, y, width, height, Color.White)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BeerRun.GameButton"/> class.
		/// </summary>
		/// <param name="x">The x coordinate of upper left corner.</param>
		/// <param name="y">The y coordinate of upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="color">Color.</param>
		public GameButton (int x, int y, int width, int height, Color color)
		{
			boundary = new Rect (x, y, x + width, y + height);
			this._color = color;
		}

		/// <summary>
		/// Checks if the given TouchEvent is on the button.
		/// </summary>
		/// <param name="e">TouchEvent to check.</param>
		public bool Clicked(TouchEvent e)
		{
			bool inX = e.x >= boundary.Left && e.x <= boundary.Right;
			bool inY = e.y >= boundary.Top && e.y <= boundary.Bottom;

			return inX && inY;
		}
		/// <summary>
		/// Paint the in the previously indicated colour
		/// </summary>
		/// <param name="g">The graphics object.</param>
		/// <remarks>The color defaults to white, if not explicitly specified</remarks>
		public void Paint(IGraphics g)
		{
			g.DrawRect (boundary.Left, boundary.Top, boundary.Width(), boundary.Height(), _color);
		}

		public void PaintWithImage(IGraphics g, AndroidImage image)
		{
			g.DrawImage (image, boundary.Left, boundary.Top, 0, 0, image.Width, image.Height);
		}
	}
}


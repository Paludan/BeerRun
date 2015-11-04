using System;
using GameFramework;

using Android.Content.Res;
using Android.Graphics;
using Java.IO;

namespace Implementation
{
	public class AndroidGraphics : IGraphics
	{
		AssetManager assets;
		Bitmap frameBuffer;
		Canvas canvas;
		Paint paint;
		Rect srcRect = new Rect();
		Rect dstRect = new Rect();

		public int Height {
			get { return canvas.Height; }
		}
		public int Width {
			get { return canvas.Width; }
		}

		public AndroidGraphics (AssetManager am, Bitmap bm)
		{
			this.assets = am;
			this.frameBuffer = bm;
			this.canvas = new Canvas (frameBuffer);
			this.paint = new Paint ();
		}
			
		#region IGraphics implementation
		public IImage NewImage (string fileName, GameFramework.ImageFormat format)
		{
			//Handles options and configuration for the desired image
			Bitmap.Config config = null; 
			switch (format) {
			case GameFramework.ImageFormat.ARGB4444:
				config = Bitmap.Config.Argb4444;
				format = GameFramework.ImageFormat.ARGB4444;
				break;
			case GameFramework.ImageFormat.ARGB8888:
				config = Bitmap.Config.Argb8888;
				format = GameFramework.ImageFormat.ARGB8888;
				break;
			case GameFramework.ImageFormat.RGB565:
				config = Bitmap.Config.Rgb565;
				format = GameFramework.ImageFormat.RGB565;
				break;
			}

			BitmapFactory.Options o = new BitmapFactory.Options ();
			o.InPreferredConfig = config;

			System.IO.Stream inStream = null;
			Bitmap bitmap = null;

			//Load the image from the assets folder
			try {
				inStream = assets.Open(fileName);
				bitmap = BitmapFactory.DecodeStream(inStream, null, o);
				if(bitmap == null)
					throw new NullReferenceException("Couldn't load bitmap from assets '" + fileName + "'");
			} catch {
				throw new NullReferenceException("Couldn't load bitmap from assets '" + fileName + "'");
			} finally {
				if (inStream != null){
					try {
						inStream.Close();
					}
					catch{
					}
				}
			}

			return new AndroidImage (bitmap, format);
		}

		/// <summary>
		/// Clears the screen with specified color.
		/// </summary>
		/// <param name="color">Color.</param>
		public void ClearScreen (int color)
		{
			canvas.DrawRGB ((color & 0xFF0000) >> 16, (color & 0x00FF00) >> 8, (color & 0x0000FF));
		}

		/// <summary>
		/// Draws a line between (x, y) and (x2, y2) in specified color.
		/// </summary>
		/// <param name="x">The start x coordinate.</param>
		/// <param name="y">The start y coordinate.</param>
		/// <param name="x2">The end x value.</param>
		/// <param name="y2">The end y value.</param>
		/// <param name="color">Color.</param>
		public void DrawLine (int x, int y, int x2, int y2, int color)
		{
			paint.Color = new Color(color);
			canvas.DrawLine (x, y, x2, y2, paint);
		}

		/// <summary>
		/// Draws a filled rectangle in specified color.
		/// </summary>
		/// <param name="x">The x coordinate of upper left corner.</param>
		/// <param name="y">The y coordinate of upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="color">Color.</param>
		public void DrawRect (int x, int y, int width, int height, int color)
		{
			paint.Color = new Color(color);
			paint.SetStyle (Paint.Style.Fill);
			canvas.DrawRect (new Rect (x, y, x + width - 1, y + height - 1), paint);
		}

		/// <summary>
		/// Draws the image on specified coordinate.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="srcX">Source x.</param>
		/// <param name="srcY">Source y.</param>
		/// <param name="srcWidth">Source width.</param>
		/// <param name="srcHeight">Source height.</param>
		public void DrawImage (IImage image, int x, int y, int srcX, int srcY, int srcWidth, int srcHeight)
		{
			srcRect.Left = srcX;
			srcRect.Top = srcY;
			srcRect.Right = srcX + srcWidth;
			srcRect.Bottom = srcY + srcHeight;

			dstRect.Left = x;
			dstRect.Top = y;
			dstRect.Right = x + srcWidth;
			dstRect.Bottom = y + srcHeight;

			canvas.DrawBitmap (((AndroidImage)image).Bitmap, srcRect, dstRect, null);
		}

		/// <summary>
		/// Draws the image on specified coordinate.
		/// </summary>
		/// <param name="Image">Image.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void DrawImage (IImage Image, int x, int y)
		{
			canvas.DrawBitmap(((AndroidImage) Image).Bitmap, x, y, null);
		}

		public void DrawScaledImage(IImage Image, int x, int y, int width, int height, int srcX, int srcY, 
			int srcWidth, int srcHeight){
			srcRect.Left = srcX;
			srcRect.Top = srcY;
			srcRect.Left = srcX + srcWidth;
			srcRect.Right = srcY + srcHeight;

			dstRect.Left = x;
			dstRect.Top = y;
			dstRect.Right = x + width;
			dstRect.Bottom = y + height;

			canvas.DrawBitmap(((AndroidImage) Image).Bitmap, srcRect, dstRect, null);
		}

		/// <summary>
		/// Draws a string at specified coordinate with given paint.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="paint">Paint.</param>
		public void DrawString (string text, int x, int y, Paint paint)
		{
			canvas.DrawText (text, x, y, paint);
		}

		/// <summary>
		/// Draws an ARGB color.
		/// </summary>
		/// <param name="a">The alpha component.</param>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		public void DrawARGB (int a, int r, int g, int b)
		{
			canvas.DrawARGB (a, r, g, b);
		}
		#endregion
	}
}


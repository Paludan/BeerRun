using System;
using GameFramework;

using Android.Content.Res;
using Android.Graphics;
using Java.IO;

namespace Implementation
{
	public class AndroidImage : IImage
	{
		Bitmap bitmap;
		public Bitmap Bitmap {
			get { return bitmap; }
		}
		GameFramework.ImageFormat format;

		public AndroidImage (Bitmap bitmap, GameFramework.ImageFormat format)
		{
			this.bitmap = bitmap;
			this.format = format;
		}

		#region IImage implementation
		public int Width
		{
			get { return bitmap.Width; }
		}

		public int Height
		{
			get { return bitmap.Height; }
		}

		public void Dispose ()
		{
			bitmap.Recycle ();
		}

		public GameFramework.ImageFormat Format {
			get {
				return format;
			}
		}

		#endregion
	}

}


using System;
using Framework;

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
		Framework.ImageFormat format;

		public AndroidImage (Bitmap bitmap, Framework.ImageFormat format)
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

		public Framework.ImageFormat Format {
			get {
				return format;
			}
		}

		#endregion
	}

}


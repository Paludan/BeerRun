using System;
using Framework;
using Android.Content.Res;
using Android.Media;
using Android.App;

namespace Implementation
{
	public class AndroidSound : ISound
	{
		int soundID;
		SoundPool sp;

		public AndroidSound (SoundPool sp, int id)
		{
			this.soundID = id;
			this.sp = sp;
		}

		#region ISound implementation
		/// <summary>
		/// Play sound with specified volume.
		/// </summary>
		/// <param name="volume">Volume.</param>
		public void Play (float volume)
		{
			sp.Play (soundID, volume, volume, 0, 0, 1);
		}
		/// <summary>
		/// Releases all resource used by the <see cref="Implementation.AndroidSound"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Implementation.AndroidSound"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Implementation.AndroidSound"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Implementation.AndroidSound"/> so
		/// the garbage collector can reclaim the memory that the <see cref="Implementation.AndroidSound"/> was occupying.</remarks>
		public void Dispose ()
		{
			sp.Unload (soundID);
		}

		#endregion
	}

}


using System;
using Framework;
using Android.Content.Res;
using Android.Media;
using Android.App;

namespace Implementation
{
	public class AndroidMusic : IMusic, Android.Media.MediaPlayer.IOnPreparedListener, Android.Media.MediaPlayer.IOnCompletionListener,
								Android.Media.MediaPlayer.IOnSeekCompleteListener, Android.Media.MediaPlayer.IOnVideoSizeChangedListener
	{
		MediaPlayer mp;
		bool isPrepared = false;

		public AndroidMusic (AssetFileDescriptor afd)
		{
			this.mp = new MediaPlayer ();
			try {
				mp.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
				mp.Prepare();
				isPrepared = true;
				mp.SetOnCompletionListener(this);
				mp.SetOnSeekCompleteListener(this);
				mp.SetOnPreparedListener(this);
				mp.SetOnVideoSizeChangedListener(this);
			} catch {
				throw new ApplicationException("Couldn't load music");
			}
		}

		#region IMusic implementation
		/// <summary>
		/// Play the music.
		/// </summary>
		public void Play ()
		{
			if (this.mp.IsPlaying)
				return;

			try {
				lock (this) {
					if(!isPrepared)
						mp.Prepare();
					mp.Start();
				}
			} catch (Exception e) {
				Console.WriteLine (e.Message);
			}
		}

		/// <summary>
		/// Stop the music.
		/// </summary>
		public void Stop ()
		{
			if (this.mp.IsPlaying) {
				mp.Stop ();

				lock (this) {
					isPrepared = false;
				}
			}
		}

		/// <summary>
		/// Pause the music.
		/// </summary>
		public void Pause ()
		{
			if (this.mp.IsPlaying)
				mp.Pause ();
		}

		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		/// <value>The volume.</value>
		public float Volume
		{
			set {
				mp.SetVolume(value, value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Implementation.AndroidMusic"/> is playing.
		/// </summary>
		/// <value><c>true</c> if playing; otherwise, <c>false</c>.</value>
		public bool Playing
		{
			get { return mp.IsPlaying; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Implementation.AndroidMusic"/> is stopped.
		/// </summary>
		/// <value><c>true</c> if stopped; otherwise, <c>false</c>.</value>
		public bool Stopped
		{
			get { return !isPrepared; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Implementation.AndroidMusic"/> is looping.
		/// </summary>
		/// <value><c>true</c> if looping; otherwise, <c>false</c>.</value>
		public bool Looping
		{
			get { return mp.Looping; }
			set { mp.Looping = value; }
		}

		/// <summary>
		/// Disposes this object and releases resources occuptied by it.
		/// </summary>
		public void Dispose ()
		{
			if (this.mp.IsPlaying )
				this.mp.Stop ();
			mp.Release ();
		}

		/// <summary>
		/// Seeks the beginning of the music-file.
		/// </summary>
		public void SeekBegin ()
		{
			mp.SeekTo (0);
		}
		#endregion
		#region Listeners
		public void OnCompletion(MediaPlayer mp){
			lock (this) {
				isPrepared = false;
			}
		}

		public void OnSeekComplete(MediaPlayer mp){
			//Left blank on purpose
		}

		public void OnPrepared(MediaPlayer mp){
			lock (this) {
				isPrepared = true;
			}
		}

		public IntPtr Handle {
			get {
				return new IntPtr ();
			}
		}

		public void OnVideoSizeChanged(MediaPlayer mp, int width, int height){
			//Left blank on purpose
		}
		#endregion
	}

}


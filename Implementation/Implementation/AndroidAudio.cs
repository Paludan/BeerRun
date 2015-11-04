using System;
using Framework;
using Android.Content.Res;
using Android.Media;
using Android.App;

namespace Implementation
{
	public class AndroidAudio : IAudio
	{
		AssetManager assets;
		SoundPool soundPool;

		public AndroidAudio (Activity act)
		{
			act.VolumeControlStream = Stream.Music;
			this.assets = act.Assets;
			this.soundPool = new SoundPool (20, Stream.Music, 0);
		}


		#region IAudio implementation
		/// <summary>
		/// Creates music from given file.
		/// </summary>
		/// <returns>The music.</returns>
		/// <param name="file">File.</param>
		public IMusic CreateMusic (string file)
		{
			try {
				AssetFileDescriptor afd = assets.OpenFd(file);
				return new AndroidMusic(afd);
			} catch {
				throw new Exception("Couldn't load music '" + file + "'");
			}
		}
		/// <summary>
		/// Creates sound-effects from specified file.
		/// </summary>
		/// <returns>The sound.</returns>
		/// <param name="file">File.</param>
		public ISound CreateSound (string file)
		{
			try {
				AssetFileDescriptor afd = assets.OpenFd(file);
				int soundId = soundPool.Load(afd, 0);
				return new AndroidSound(soundPool, soundId);
			} catch {
				throw new Exception ("Couldn't load sound '" + file + "'");
			}
		}
		#endregion
	}
}


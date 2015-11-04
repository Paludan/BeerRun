using System;

namespace GameFramework
{
	public interface IMusic
	{
		void Play();
		void Stop();
		void Pause();

		int Volume { get; set; }
		bool Playing { get; }
		bool Stopped { get; }
		bool Looping { get; set; }

		void Dispose();
		void SeekBegin();
	}
}


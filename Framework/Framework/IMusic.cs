using System;

namespace Framework
{
	public interface IMusic
	{
		void Play();
		void Stop();
		void Pause();

		float Volume { set; }
		bool Playing { get; }
		bool Stopped { get; }
		bool Looping { get; set; }

		void Dispose();
		void SeekBegin();
	}
}


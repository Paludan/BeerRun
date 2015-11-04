using System;

namespace GameFramework
{
	public interface ISound
	{
		void Play (float volume);
		void Dispose();
	}
}


using System;

namespace GameFramework
{
	public interface IAudio
	{
		IMusic CreateMusic(string file);
		ISound CreateSound(string file);
	}
}


using System;

namespace Framework
{
	public interface IAudio
	{
		IMusic CreateMusic(string file);
		ISound CreateSound(string file);
	}
}


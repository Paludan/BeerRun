using Implementation;
using Framework;
using System.Collections.Generic;

namespace BeerRun
{
	public static class MusicManager
	{
		public static Dictionary<string, IMusic> Music;

		/// <summary>
		/// Load the music in the game.
		/// </summary>
		/// <param name="game">Game.</param>
		public static void Load(IGame game) {
			var theme_song = game.Audio.CreateMusic ("theme.mp3");
			theme_song.Looping = true;
			theme_song.Volume = 0.85f;

			Music.Add ("theme", theme_song);
		}
	}
}


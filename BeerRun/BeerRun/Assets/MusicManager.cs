using System;
using Implementation;
using Framework;

namespace BeerRun
{
	public static class MusicManager
	{
		public AndroidMusic Theme {
			get;
			set;
		}
		public AndroidSound Click {
			get;
			set;
		}

		public static void Load(IGame game) {
			Theme = game.Audio.CreateMusic ("theme.mp3");
		}
	}
}


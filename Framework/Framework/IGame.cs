using System;

namespace Framework
{
	public interface IGame
	{
		IAudio Audio { get; }
		IInput Input { get; }
		IFileIO FileIO { get; }
		IGraphics Graphics { get; }
		Screen CurrentScreen { get; set; }

		Screen InitScreen { get; }
	}
}


using System;
using System.Collections.Generic;

using Framework;

namespace BeerRun
{
	public class MainMenu : Screen
	{
		private GameButton gBut;

		/// <summary>
		/// Initializes a new instance of the <see cref="BeerRun.MainMenu"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		public MainMenu (IGame game)
			: base (game)
		{
			gBut = new GameButton (50, 350, 250, 450);
		}

		#region implemented abstract members of Screen
		/// <summary>
		/// Updates the screen with specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public override void Update (float deltaTime)
		{
			IGraphics g = Game.Graphics;
			List<TouchEvent> touchEvents = Game.Input.TouchEvents;

			foreach (var te in touchEvents) {
				if (te.type == TouchEvent.TOUCH_UP) {
					bool wasClicked = gBut.Clicked (te);

					if (wasClicked)
						Game.CurrentScreen = new GameScreen (Game);
				}
			}
		}

		/// <summary>
		/// Paint the screen using specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public override void Paint (float deltaTime)
		{
			IGraphics g = Game.Graphics;
			g.DrawImage (PictureManager.Pictures ["menu"], 0, 0);
		}
		/// <summary>
		/// Pause the game.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Pause ()
		{
			//No need for a pause method here
		}

		/// <summary>
		/// Resume the game.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Resume ()
		{
			//No need for a resume method here
		}

		/// <summary>
		/// Releases all resource used by the <see cref="BeerRun.MainMenu"/> object.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Dispose ()
		{
			//Dispose handling by GC
		}

		/// <summary>
		/// Kills the program, when the user hits back.
		/// </summary>
		public override void BackButton ()
		{
			Android.OS.Process.KillProcess (Android.OS.Process.MyPid());
		}
		#endregion
	}



}



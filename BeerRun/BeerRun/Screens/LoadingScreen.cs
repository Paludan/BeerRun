using System;

using Implementation;
using Framework;

namespace BeerRun
{
	public class LoadingScreen : Screen
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeerRun.LoadingScreen"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		/// <remarks>This Screen simply displays a picture while it's loading the assets</remarks>
		public LoadingScreen (IGame game)
			: base (game)
		{
			IGraphics g = Game.Graphics;
			g.DrawImage (PictureManager.Pictures["splash"], 0, 0);
		}

		#region implemented abstract members of Screen
		/// <summary>
		/// Updates the screen with given delta-time
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public override void Update (float deltaTime)
		{
			//Gets graphic component from the game
			IGraphics g = Game.Graphics;

			//Add all wanted pictures to the dictionary
			PictureManager.Pictures.Add ("menu", g.NewImage ("menu.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("background", g.NewImage("background.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("character1", g.NewImage("character.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("character2", g.NewImage("character2.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("character3", g.NewImage("character3.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("character_jumped", g.NewImage("jumped.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("character_ducked", g.NewImage("ducked.png", ImageFormat.ARGB4444));

			PictureManager.Pictures.Add ("heliboy1", g.NewImage("heliboy.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("heliboy2", g.NewImage("heliboy2.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("heliboy3", g.NewImage("heliboy3.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("heliboy4", g.NewImage("heliboy4.png", ImageFormat.ARGB4444));
			PictureManager.Pictures.Add ("heliboy5", g.NewImage("heliboy5.png", ImageFormat.ARGB4444));

			PictureManager.Pictures.Add ("tile_dirt", g.NewImage("tiledirt.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("tile_grass_top", g.NewImage("tilegrasstop.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("tile_grass_left", g.NewImage("tilegrassleft.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("tile_grass_right", g.NewImage("tilegrassright.png", ImageFormat.RGB565));
			PictureManager.Pictures.Add ("tile_grass_bottom", g.NewImage("tilegrassbottom.png", ImageFormat.RGB565));

			PictureManager.Pictures.Add("button", g.NewImage("button.jpg", ImageFormat.RGB565));

			Game.CurrentScreen = new MainMenu (Game);
		}
		/// <summary>
		/// Paints the screen.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Paint (float deltaTime)
		{
			//Purposely left blank
		}

		/// <summary>
		/// Pause the game.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Pause ()
		{
			//Purposely left blank
		}
		/// <summary>
		/// Resume the game.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Resume ()
		{
			//Purposely left blank
		}
		/// <summary>
		/// Releases all resource used by the <see cref="BeerRun.LoadingScreen"/> object.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void Dispose ()
		{
			
		}
		/// <summary>
		/// Indicates what happens when the user hits the back-button.
		/// </summary>
		/// <remarks>This class holds no functionality for this method</remarks>
		public override void BackButton ()
		{
			//Purposely left blank
		}
		#endregion
	}


}



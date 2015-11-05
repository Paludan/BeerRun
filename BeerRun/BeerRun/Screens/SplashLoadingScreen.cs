using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Implementation;
using Framework;
using Java.IO;
using System.Text;
using Android.Util;

namespace BeerRun
{
	class SplashLoadingScreen : Screen
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BeerRun.SplashLoadingScreen"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		/// <remarks>This screen is used to load a single picture to show on loading screen.</remarks>
		public SplashLoadingScreen (IGame game)
			: base (game)
		{}

		#region implemented abstract members of Screen
		/// <summary>
		/// Update the screen in specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public override void Update (float deltaTime)
		{
			IGraphics g = Game.Graphics;
			PictureManager.Pictures.Add("splash", g.NewImage ("splash.jpg", ImageFormat.RGB565));

			Game.CurrentScreen = new LoadingScreen (Game);
		}
		/// <summary>
		/// Paints the screen in specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		/// <remarks>No functionality is provided by this method in this class.</remarks>
		public override void Paint (float deltaTime)
		{
			//Purposely left blank
		}
		/// <summary>
		/// Pause the game.
		/// </summary>
		/// <remarks>No functionality is provided by this method in this class.</remarks>
		public override void Pause ()
		{
			//Purposely left blank
		}
		/// <summary>
		/// Resume the game.
		/// </summary>
		/// <remarks>No functionality is provided by this method in this class.</remarks>
		public override void Resume ()
		{
			//Purposely left blank
		}
		/// <summary>
		/// Releases all resource used by the <see cref="BeerRun.SplashLoadingScreen"/> object.
		/// </summary>
		/// <remarks>No functionality is provided by this method in this class.</remarks>
		public override void Dispose ()
		{
			//Purposely left blank
		}
		/// <summary>
		/// Indicate what should happen on the screen, when user hits back.
		/// </summary>
		/// <remarks>No functionality is provided by this method in this class.</remarks>
		public override void BackButton ()
		{
			//Purposely left blank
		}
		#endregion
	}

}



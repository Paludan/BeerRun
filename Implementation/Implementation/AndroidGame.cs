using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Framework;
using Android.Graphics;

namespace Implementation
{
	[Activity (Label = "Implementation", MainLauncher = true, Icon = "@drawable/icon")]
	public abstract class AndroidGame : Activity, IGame
	{
		public abstract Screen InitScreen { get; }

		AndroidFastRenderView renderView;
		IGraphics graphics;
		IAudio audio;
		IInput input;
		IFileIO fileIO;
		Screen screen;
		Android.OS.PowerManager.WakeLock wakeLock;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Hides the titlebar and sets the window to fullscreen
			RequestWindowFeature (WindowFeatures.NoTitle);
			Window.SetFlags (WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

			//Create a frameBuffer for the background
			var rotation = WindowManager.DefaultDisplay.Rotation;
			bool isPortrait = rotation == SurfaceOrientation.Rotation0 || rotation == SurfaceOrientation.Rotation180;
			int frameBufferWidth = isPortrait ? 1280 : 1920;
			int frameBufferHeight = isPortrait ? 1920 : 1280;
			Bitmap frameBuffer = Bitmap.CreateBitmap (frameBufferWidth, frameBufferHeight, Bitmap.Config.Rgb565);

			//Find screen size to calculate relative size
			var screenSize = new Point();
			WindowManager.DefaultDisplay.GetSize(screenSize);
			float scaleX = (float)frameBufferWidth / screenSize.X;
			float scaleY = (float) frameBufferHeight / screenSize.Y;

			//Generate classes and start-up the first screen
			renderView = new AndroidFastRenderView (this, frameBuffer);
			graphics = new AndroidGraphics (Assets, frameBuffer);
			fileIO = new AndroidFileIO (this);
			audio = new AndroidAudio (this);
			input = new AndroidInput (this, renderView, scaleX, scaleY);
			screen = this.InitScreen;
			SetContentView (renderView);

			//Add a WakeLock to avoid the screen from going out
//			PowerManager powerManager = (PowerManager)GetSystemService (Context.PowerService);
//			wakeLock = powerManager.NewWakeLock (WakeLockFlags.Full, "SwordSlash");
		}

		protected override void OnResume ()
		{
			base.OnResume();
			//wakeLock.Acquire ();
			screen.Resume ();
			renderView.Resume ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			//wakeLock.Release ();
			renderView.Pause ();
			screen.Pause ();

			if (IsFinishing)
				screen.Dispose ();
		}

		#region IGame implementation
		/// <summary>
		/// Gets the audio.
		/// </summary>
		/// <value>The audio.</value>
		public IAudio Audio {
			get {
				return audio;
			}
		}

		/// <summary>
		/// Gets the input.
		/// </summary>
		/// <value>The input.</value>
		public IInput Input {
			get {
				return input;
			}
		}

		/// <summary>
		/// Gets the file input/output stream.
		/// </summary>
		/// <value>The file input/output stream.</value>
		public IFileIO FileIO {
			get {
				return fileIO;
			}
		}

		/// <summary>
		/// Gets the graphics.
		/// </summary>
		/// <value>The graphics.</value>
		public IGraphics Graphics {
			get {
				return graphics;
			}
		}

		/// <summary>
		/// Gets the current screen.
		/// </summary>
		/// <value>The current screen.</value>
		public Screen CurrentScreen {
			get {
				return screen;
			}
			set {
				if (screen == null)
					throw new Java.Lang.IllegalArgumentException ("Screen must not be null");

				this.screen.Pause ();
				this.screen.Dispose ();
				screen.Resume ();
				screen.Update (0);
				this.screen = screen;
			}
		}
		#endregion
	}
}



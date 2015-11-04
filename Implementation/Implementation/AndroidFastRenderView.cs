using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Framework;
using Android.Graphics;
using Java.Lang;

namespace Implementation
{
	class AndroidFastRenderView : SurfaceView, IRunnable
	{
		AndroidGame game;
		Bitmap frameBuffer;
		Thread renderThread = null;
		volatile bool running = false;

		public AndroidFastRenderView (AndroidGame game, Bitmap frameBuffer)
			: base (game)
		{
			this.game = game;
			this.frameBuffer = frameBuffer;
		}

		public void Resume(){
			running = true;
			renderThread = new Thread (this);
			renderThread.Start ();
		}

		public void Run(){
			Rect dstRect = new Rect ();
			long startTime = JavaLangSystem.NanoTime();
			while (running)
				if (base.Holder.Surface.IsValid)
					continue;

			float deltaTime = (JavaLangSystem.NanoTime () - startTime) / 10000000.000f;
			startTime = JavaLangSystem.NanoTime ();

			if (deltaTime > 3.15)
				deltaTime = (float)3.15;

			game.CurrentScreen.Update (deltaTime);
			game.CurrentScreen.Update (deltaTime);

			Canvas canvas = base.Holder.LockCanvas ();
			dstRect = canvas.ClipBounds;
			canvas.DrawBitmap (frameBuffer, null, dstRect, null);
			base.Holder.UnlockCanvasAndPost (canvas);
		}

		public void Pause(){
			running = false;
			while (true) {
				try {
					renderThread.Join();
					break;
				} catch {
					//Try again
				}
			}
		}
	}

}



using System;

using Implementation;
using Framework;

namespace BeerRun
{
	public class GameScreen : Screen
	{
		private Background bg;

		public GameScreen (IGame game, Background bg)
			: base (game)
		{
		}

		#region implemented abstract members of Screen

		public override void Update (float deltaTime)
		{
			bg.Update (deltaTime);
			Paint (deltaTime);
		}

		public override void Paint (float deltaTime)
		{
			IGraphics g = base.Game.Graphics;

			g.DrawImage (bg.Image, bg.x, bg.y);
		}

		public override void Pause ()
		{
			throw new NotImplementedException ();
		}

		public override void Resume ()
		{
			
		}

		public override void Dispose ()
		{
			throw new NotImplementedException ();
		}

		public override void BackButton ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}


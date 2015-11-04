using System;

namespace GameFramework
{
	public abstract class Screen
	{
		protected readonly IGame Game;

		public Screen (IGame game)
		{
			this.Game = game;
		}

		public abstract void Update (float deltaTime);

		public abstract void Paint(float deltaTime);

		public abstract void Pause();

		public abstract void Resume();

		public abstract void Dispose ();

		public abstract void BackButton();
	}
}


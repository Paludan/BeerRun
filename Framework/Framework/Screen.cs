using System;

namespace Framework
{
	public abstract class Screen
	{
		protected readonly IGame Game;

		public Screen (IGame game)
		{
			this.Game = game;
		}

		/// <summary>
		/// Update the screen in specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public abstract void Update (float deltaTime);
		/// <summary>
		/// Paints the screen in specified deltaTime.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public abstract void Paint(float deltaTime);
		/// <summary>
		/// Pause the game.
		/// </summary>
		public abstract void Pause();
		/// <summary>
		/// Resume the game.
		/// </summary>
		public abstract void Resume();
		/// <summary>
		/// Releases all resource used by the <see cref="Framework.Screen"/> object.
		/// </summary>
		public abstract void Dispose ();
		/// <summary>
		/// Indicate what should happen on the screen, when user hits back.
		/// </summary>
		public abstract void BackButton();
	}
}


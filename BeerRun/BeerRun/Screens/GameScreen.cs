using System;

using Implementation;
using Framework;
using System.Collections.Generic;
using Android.Graphics;
using System.IO;

namespace BeerRun
{
	public class GameScreen : Screen
	{
		enum GameState {
			Running, Paused, Ready, GameOver
		}

		#region variables and properties
		GameState state = GameState.Ready;
		private Background bg;
		private Robot robot;
		private Heliboy hb;

		private IImage currentCharacterSprite;
		private Animation anim, hAnim;

		private List<Tile> tiles;

		private Android.Graphics.Paint paint;

		private GameButton jump, duck, shoot, pause;
		#endregion

		public GameScreen (IGame game)
			: base (game)
		{
			//Initialize game-objects
			bg = new Background(0, 0, PictureManager.Pictures["background"] as AndroidImage, robot);

			robot = new Robot (0, 0);
			hb = new Heliboy (340, 360, bg, robot);

			addCharacterAnimations ();
			addHeliboyAnimations ();

			currentCharacterSprite = anim.Image;

			loadMap ();

			initializePaint ();
			initializeButtons ();
		}

		#region start-up methods
		/// <summary>
		/// Loads the map.
		/// </summary>
		private void loadMap(){
			List<string> lines = new List<string> ();
			int width = 0;
			int height = 0;

			StreamReader reader = new StreamReader ((Game as MainActivity).map);
			while (!reader.EndOfStream) {
				string line = reader.ReadLine ();

				//Checks if there are no more lines
				if (line == null)
					break;

				//Lines in the map file, that starts with ! is seen as comments and therefore ignored
				if (!line.StartsWith ("!")) {
					lines.Add (line);
					width = Math.Max (width, line.Length);
				}
			}

			height = lines.Count;
			reader.Close ();

			generateTiles (width, height, lines);
		}

		/// <summary>
		/// Generates the tiles based on the list of strings.
		/// </summary>
		/// <param name="width">Width of map.</param>
		/// <param name="height">Height of map.</param>
		/// <param name="lines">Lines from the map.txt file.</param>
		private void generateTiles(int width, int height, List<string> lines){
			for (int i = 0; i < height; i++) {
				string line = lines [i];
				for (int j = 0; j < width; j++) {
					if (j < line.Length) {
						char ch = line [j];
						TileType type;
						switch (ch) {
						case '8':
							type = TileType.GrassTop;
							break;
						case '4':
							type = TileType.GrassLeft;
							break;
						case '6':
							type = TileType.GrassRight;
							break;
						case '2':
							type = TileType.GrassBottom;
							break;
						case '5':
							type = TileType.Dirt;
							break;
						default:
							type = TileType.Air;
							break;
						}
						Tile t = new Tile (j, i, type, bg, robot);
						tiles.Add (t);
					}
				}
			}
		}

		/// <summary>
		/// Initializes the paint.
		/// </summary>
		private void initializePaint(){
			paint = new Paint ();
			paint.TextSize = 30;
			paint.TextAlign = Android.Graphics.Paint.Align.Center;
			paint.AntiAlias = true;
			paint.Color = Color.White;
		}

		private void initializeButtons(){
			jump = new GameButton (0, 285, 65, 65);
			shoot = new GameButton (0, 350, 65, 65);
			duck = new GameButton (0, 415, 65, 65);
			pause = new GameButton (0, 0, 35, 35);
		}

		/// <summary>
		/// Adds the character animations.
		/// </summary>
		private void addCharacterAnimations(){
			anim = new Animation ();
			anim.AddFrame (PictureManager.Pictures ["character1"], 1250);
			anim.AddFrame (PictureManager.Pictures ["character2"], 50);
			anim.AddFrame (PictureManager.Pictures ["character3"], 50);
			anim.AddFrame (PictureManager.Pictures ["character2"], 50);
		}

		/// <summary>
		/// Adds the heliboy animations.
		/// </summary>
		private void addHeliboyAnimations(){
			hAnim = new Animation();
			hAnim.AddFrame(PictureManager.Pictures["heliboy1"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy2"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy3"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy4"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy5"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy4"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy3"], 100);
			hAnim.AddFrame(PictureManager.Pictures["heliboy2"], 100);
		}
		#endregion

		#region implemented abstract members of Screen

		public override void Update (float deltaTime)
		{
			List<TouchEvent> touchEvents = Game.Input.TouchEvents;

			switch (state) {
			case GameState.GameOver:
				updateGameOver (touchEvents);
				break;
			case GameState.Paused:
				updatePaused (touchEvents);
				break;
			case GameState.Ready:
				updateReady (touchEvents);
				break;
			case GameState.Running:
				updateRunning (touchEvents, deltaTime);
				break;
			default:
				break;
			}
		}

		#region update-methods
		private void updateGameOver(List<TouchEvent> touchEvents){
			foreach (var touchEvent in touchEvents) {
				if (touchEvent.type == TouchEvent.TOUCH_DOWN) {
					if (new GameButton (0, 0, 800, 480).Clicked (touchEvent)) {
						nullify ();
						Game.CurrentScreen = new MainMenu (Game);
						return;
					}
				}
			}
		}

		private void updatePaused(List<TouchEvent> touchEvents){
			foreach (var touchEvent in touchEvents) {
				if (touchEvent.type == TouchEvent.TOUCH_UP) {
					if (pause.Clicked (touchEvent))
						Resume ();
					else if (new GameButton (0, 240, 800, 240).Clicked (touchEvent)) {
						nullify ();
						goToMenu ();
					}
				}
			}
		}

		/// <summary>
		/// Called when the state is ready and starts the game upon a tap.
		/// </summary>
		/// <param name="touchEvents">Touch events.</param>
		private void updateReady(List<TouchEvent> touchEvents){
			if (touchEvents.Count > 0)
				state = GameState.Ready;
		}

		private void updateRunning(List<TouchEvent> touchEvents, float deltaTime){
			foreach (var touchEvent in touchEvents) {
				//Handles new clicks that the user makes:
				if (touchEvent.type == TouchEvent.TOUCH_DOWN) {
					handleTouchDown (touchEvent);
				}
				//Then handles when user removes a finger from the screen
				if (touchEvent.type == TouchEvent.TOUCH_UP) {
					handleTouchUp (touchEvent);
				}
			}

			updateRobot (deltaTime);
			updateTiles ();
			hb.Update (deltaTime);
			bg.Update (deltaTime);
			animate ();
		}

		private void updateRobot(float deltaTime){
			if (robot.Lives <= 0)
				state = GameState.GameOver;

			robot.Update (deltaTime);
			if (robot.Jumped)
				currentCharacterSprite = PictureManager.Pictures ["character_jumped"];
			else if (!robot.Jumped && !robot.Ducked)
				currentCharacterSprite = anim.Image;

			foreach (var projectile in robot.Projectiles) {
				if (projectile.Visible)
					projectile.Update (new List<Enemy> {hb});
				else
					robot.Projectiles.Remove (projectile);
			}

			if (robot.x < 500)
				state = GameState.GameOver;
		}

		private void handleTouchDown(TouchEvent touchEvent){
			//First handles clicks on the shoot, duck and jump buttons
			if (jump.Clicked (touchEvent)) {
				robot.Jump ();
				currentCharacterSprite = PictureManager.Pictures ["character_jumped"];
			} else if (duck.Clicked (touchEvent) && !robot.Jumped) {
				currentCharacterSprite = PictureManager.Pictures ["character_ducked"];
				robot.Duck ();
			} else if (shoot.Clicked (touchEvent) && !robot.Ducked && robot.ReadyToFire) {
				robot.Shoot ();
			}

			//Then handles moving
			if (touchEvent.x > 400) {
				robot.MoveRight ();
			}
		}

		private void handleTouchUp(TouchEvent touchEvent){
			if (duck.Clicked (touchEvent)) {
				currentCharacterSprite = anim.Image;
				robot.Ducked = false;
			}
			if (pause.Clicked (touchEvent)) {
				Pause ();
			}
			if (touchEvent.x > 400)
				robot.StopMovingRight ();
		}

		private void updateTiles(){
			foreach (var tile in tiles) {
				tile.Update ();
			}
		}

		/// <summary>
		/// Animate the figures of the game.
		/// </summary>
		private void animate(){
			anim.Update (10);
			hAnim.Update (50);
		}

		/// <summary>
		/// Nullify this instance.
		/// </summary>
		private void nullify(){
			robot = null;
			hb = null;
			bg = null;
			paint = null;
			anim = null;
			hAnim = null;
			currentCharacterSprite = null;

			System.GC.Collect ();
		}

		/// <summary>
		/// Goes to the menu.
		/// </summary>
		private void goToMenu(){
			Game.CurrentScreen = new MainMenu (Game);
		}
		#endregion
		#region Paint-methods
		public override void Paint (float deltaTime)
		{
			IGraphics g = Game.Graphics;

			//Draw background, tiles i.e.
			g.DrawImage (bg.Image, bg.x, bg.y);
			paintTiles (g);
			paintProjectiles (g);

			//Draw robot and enemies
			robot.Paint (g);
			hb.Paint (g);

			//Draw UI-overlay
			switch (state) {
			case GameState.GameOver:
				drawUIGameOver (g);
				break;
			case GameState.Paused:
				drawUIPaused (g);
				break;
			case GameState.Ready:
				drawUIReady (g);
				break;
			case GameState.Running:
				drawUIRunning (g);
				break;
			}
		}

		/// <summary>
		/// Paints the tiles unless they are 'air'-tiles.
		/// </summary>
		/// <param name="g">The graphics object used to draw with.</param>
		private void paintTiles(IGraphics g){
			foreach (var tile in tiles) {
				if (tile.Type != TileType.Air)
					tile.Paint (g);
			}
		}

		/// <summary>
		/// Paints the projectiles.
		/// </summary>
		/// <param name="g">The graphics object used to draw with.</param>
		private void paintProjectiles(IGraphics g){
			foreach (var p in robot.Projectiles) {
				p.Paint ();
			}
		}

		/// <summary>
		/// Draws the user interface game over.
		/// </summary>
		/// <param name="g">The green component.</param>
		private void drawUIGameOver(IGraphics g){
			g.ClearScreen (Color.Black);
			g.DrawString ("Tap to return", 400, 290, paint);
			paint.TextSize = 100;
			g.DrawString ("Game over!", 400, 240, paint);
			paint.TextSize = 30;
		}

		/// <summary>
		/// Draws the user interface when GameState is paused.
		/// </summary>
		/// <param name="g">The green component.</param>
		private void drawUIPaused(IGraphics g){
			//Darkens the screen
			g.DrawARGB (155, 0, 0, 0);
			//Adds text
			paint.TextSize = 100;
			g.DrawString("Resume", 400, 165, paint);
			g.DrawString ("Menu", 400, 360, paint);

			paint.TextSize = 30;
		}

		/// <summary>
		/// Draws the user interface when GameState is ready.
		/// </summary>
		/// <param name="g">The graphics object used to paint with.</param>
		private void drawUIReady(IGraphics g){
			//Darkens the screen
			g.DrawARGB (155, 0, 0, 0);
			//Adds text
			g.DrawString ("Tap to start", 400, 240, paint);
		}

		/// <summary>
		/// Draws the user interface when GameState is running.
		/// </summary>
		/// <param name="g">The green component.</param>
		private void drawUIRunning (IGraphics g){
			IImage butImg = PictureManager.Pictures ["button"];
			g.DrawImage (butImg, 0, 285, 0, 0, 65, 65);
			g.DrawImage (butImg, 0, 350, 0, 65, 65, 65);
			g.DrawImage (butImg, 0, 415, 0, 130, 65, 65);
			g.DrawImage (butImg, 0, 0, 0, 195, 35, 35);
		}

		#endregion
		/// <summary>
		/// Pause the game.
		/// </summary>
		public override void Pause ()
		{
			if (state == GameState.Running)
				state = GameState.Paused;
		}

		/// <summary>
		/// Resume the game.
		/// </summary>
		public override void Resume ()
		{
			if (state == GameState.Paused)
				state = GameState.Running;
		}

		/// <summary>
		/// Releases all resource used by the <see cref="BeerRun.GameScreen"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="BeerRun.GameScreen"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="BeerRun.GameScreen"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="BeerRun.GameScreen"/> so the garbage
		/// collector can reclaim the memory that the <see cref="BeerRun.GameScreen"/> was occupying.</remarks>
		public override void Dispose ()
		{
			nullify ();
		}

		/// <summary>
		/// Indicate what should happen on the screen, when user hits back.
		/// </summary>
		public override void BackButton ()
		{
			Pause ();
		}

		#endregion
	}
}


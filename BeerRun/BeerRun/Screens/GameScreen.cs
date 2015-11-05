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
			bg = new Background(0, 0, PictureManager.Pictures["background"] as AndroidImage);

			robot = new Robot ();
			hb = new Heliboy (340, 360);

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
						Tile t = new Tile (j, i, Char.GetNumericValue (ch));
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

			updateRobot ();
			updateTiles ();
			hb.Update ();
			bg.Update ();
			animate ();
		}

		private void updateRobot(){
			if (robot.Lives <= 0)
				state = GameState.GameOver;

			robot.Update ();
			if (robot.Jumped)
				currentCharacterSprite = PictureManager.Pictures ["character_jumped"];
			else if (!robot.Jumped && !robot.Ducked)
				currentCharacterSprite = anim.Image;

			foreach (var projectile in robot.Projectiles) {
				if (projectile.Visible)
					projectile.Update ();
				else
					robot.Projectiles.Remove (projectile);
			}

			if (robot.CenterY < 500)
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
				robot.shoot ();
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
		#endregion

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


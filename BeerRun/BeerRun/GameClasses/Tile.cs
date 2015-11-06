using System;

using Implementation;
using Framework;
using System.Collections.Generic;
using Android.Graphics;

namespace BeerRun
{
	public enum TileType {
		Air, GrassTop, GrassLeft, GrassRight, GrassBottom, Dirt
	}

	public class Tile
	{
		public TileType Type;
		public IImage Image;
		private Background _bg;
		Robot robot;

		private int _x, _y, _speedX;
		public int y {
			get { return _y; }
		}

		private Rect boundary;

		public Tile (int x, int y, TileType type, Background bg, Robot robot)
		{
			this._x = x * 40;
			this._y = y * 40;
			this._bg = bg;
			this.robot = robot;

			boundary = new Rect ();

			this.Type = type;
			findPicture ();
		}

		/// <summary>
		/// Finds the picture based on the type of the tile.
		/// </summary>
		private void findPicture(){
			switch (Type) {
			case TileType.Air:
				Image = null;
				break;
			case TileType.Dirt:
				Image = PictureManager.Pictures ["tile_dirt"];
				break;
			case TileType.GrassBottom:
				Image = PictureManager.Pictures ["tile_grass_bottom"];
				break;
			case TileType.GrassLeft:
				Image = PictureManager.Pictures ["tile_grass_left"];
				break;
			case TileType.GrassRight:
				Image = PictureManager.Pictures ["tile_grass_right"];
				break;
			case TileType.GrassTop:
				Image = PictureManager.Pictures ["tile_grass_top"];
				break;
			}
		}

		public void Paint(IGraphics graphics){
			graphics.DrawImage(Image, _x, _y);
		}

		public void Update(){
			_speedX = _bg.speedX * 5;
			_x += _speedX;

			boundary.Set (_x, _y, _x + 40, _y + 40);

			if (Type != TileType.Air && Rect.Intersects (boundary, robot.CollisionBoundary.Boundary)) {
				checkVerticalCollision (robot.Colliders ["upper_body"], robot.Colliders ["lower_body"]);
				checkHorizontalCollision (robot.Colliders ["left_hand"], robot.Colliders ["right_hand"], robot.Colliders ["left_foot"], robot.Colliders ["right_foot"]);
			}
		}

		private void checkVerticalCollision (Collider rTop, Collider rBot){
			if (Rect.Intersects (rBot.Boundary, boundary) && Type == TileType.GrassTop) {
				robot.Landed (this);
			}
		}

		private void checkHorizontalCollision (Collider hLeft, Collider hRight, Collider fLeft, Collider fRight){

		}


	}

}


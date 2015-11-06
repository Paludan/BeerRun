using System;

using Implementation;
using Framework;
using System.Collections.Generic;

namespace BeerRun
{
	internal enum TileType {
		Air, GrassTop, GrassLeft, GrassRight, GrassBottom, Dirt
	}

	class Tile
	{
		public TileType Type;
		public IImage Image;

		private int x, y;

		public Tile (int x, int y, TileType type)
		{
			this.x = x;
			this.y = y;

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
			graphics.DrawImage(Image, x, y);
		}

		public void Update(){

		}


	}

}


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hoax.Engine.Extensions;

namespace Hoax.Engine.Graphics2D
{
	public class TileRegister
	{
		List<Color[]> register;


		public TileRegister (Game game, string tilemapAssetPath, Rectangle singleTileRectangle)
		{
			TileRectangle = singleTileRectangle;

			Texture2D tilemap = game.Content.Load<Texture2D> (tilemapAssetPath);
			Color[] tilemapColors = new Color[tilemap.Width * tilemap.Height];
			tilemap.GetData<Color> (tilemapColors);

			checkTilemapBounds (tilemapAssetPath, tilemap, singleTileRectangle);
			int tilesHorizontal = tilemap.Width / singleTileRectangle.Width;
			int tilesVertical = tilemap.Height / singleTileRectangle.Height;
			register = new List<Color[]> ();
			for (int vOffset = 0; vOffset < tilesVertical; vOffset++)
				for (int hOffset = 0; hOffset < tilesHorizontal; hOffset++) {
					Rectangle extractRect = new Rectangle (vOffset * singleTileRectangle.Width, hOffset * singleTileRectangle.Height, singleTileRectangle.Width, singleTileRectangle.Height);
					register.Add(tilemapColors.extract(tilemap.Width, extractRect));
				}
			tilemap.Dispose ();
		}

		public Rectangle TileRectangle { get; private set; }

		public Color[] this[int index] 
		{
			get {
				return Tile (index);
			}
		}

		public Color[] Tile(int index) 
		{
			return register [index];
		}

		public int TileCount()
		{
			return register.Count;
		}

		void checkTilemapBounds (string tilemapAssetPath, Texture2D tilemap, Rectangle singleTileRectangle)
		{
			if (tilemap.Width == 0 || tilemap.Height == 0)
				throw new BadImageFormatException ("Tilemap \"" + tilemapAssetPath + "\" is dimension is zero.");

			if (tilemap.Width % singleTileRectangle.Width != 0 || tilemap.Height % singleTileRectangle.Height != 0)
				throw new BadImageFormatException ( "Tilemap \"" + tilemapAssetPath + "\" is not devideable by tile rectangle [" + singleTileRectangle.Width + "," + singleTileRectangle.Height + "]" );
		}


	}
}


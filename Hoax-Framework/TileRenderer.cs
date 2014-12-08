using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Hoax.Engine.Graphics2D
{
	public class LayerRenderer
	{
		public LayerRenderer () 
		{
		}

		public Texture2D Render(Game game, LayerDescription layerDescription) 
		{
			int layerWidth = layerDescription.MapWidth * layerDescription.TileRegister.TileRectangle.Width;
			int layerHeight = (layerDescription.FlatMap.Length / layerDescription.MapWidth) * layerDescription.TileRegister.TileRectangle.Height;
			Texture2D result = new Texture2D (game.GraphicsDevice, layerWidth, layerHeight);
			Color[] resultData = new Color[layerWidth * layerHeight];

			Debug.WriteLine ("Flat:" + layerDescription.FlatMap.Length);
			Debug.WriteLine ("Flat:" + layerDescription.FlatMap.Length * layerDescription.TileRegister.TileRectangle.Width * layerDescription.TileRegister.TileRectangle.Height);
			Debug.WriteLine ("resultData:" + resultData.Length);

			int vOffset = 0;
			for (int i = 0; i < layerDescription.FlatMap.Length; i++) {
				int tileIndex = layerDescription.FlatMap [i];
				Color[] tileColor = layerDescription.TileRegister [tileIndex];

				int blockX = i * layerDescription.TileRegister.TileRectangle.Width % layerWidth;
				int blockY = i * layerDescription.TileRegister.TileRectangle.Width / layerWidth * layerDescription.TileRegister.TileRectangle.Height;




				for (int j = 0; j < tileColor.Length; j++) {
					int tileX = j % layerDescription.TileRegister.TileRectangle.Width;
					int tileY = j / layerDescription.TileRegister.TileRectangle.Width;

					resultData = setPixel (resultData, layerWidth, blockX + tileX, blockY + tileY, tileColor [j]);
				}
			}


			result.SetData<Color>(resultData);
			return result;
		}


		Color[] setPixel(Color[] flatBitmap, int bitmapWidth, int x, int y, Color c) 
		{
			flatBitmap [x + y * bitmapWidth] = c;
			return flatBitmap;
		}
	}


}


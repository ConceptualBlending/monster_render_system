using System;

namespace Hoax.Framework.Components.Graphics2D
{
	public struct LayerDescription
	{
		public LayerDescription (TileRegister tileRegister, int[] flatMap, int mapWidth)
		{
			TileRegister = tileRegister;
			FlatMap = flatMap;
			MapWidth = mapWidth;
		}

		public TileRegister TileRegister;
		public int[] FlatMap;
		public int MapWidth;
	}
}


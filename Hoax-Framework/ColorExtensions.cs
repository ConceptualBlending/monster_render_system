using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoax.Engine.Extensions
{
	public static class ColorExtensions
	{
		public static Color[] extract(this Color[] colorSource, int width, Rectangle rectangle)
		{
			Color[] color = new Color[rectangle.Width * rectangle.Height];
			for (int x = 0; x < rectangle.Width; x++)
				for (int y = 0; y < rectangle.Height; y++)
					color[x + y * rectangle.Width] = colorSource[x + rectangle.X + (y + rectangle.Y) * width];
			return color;
		}
	}
}


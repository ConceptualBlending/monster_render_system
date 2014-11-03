using System;
using Microsoft.Xna.Framework;

namespace Hoax.Framework.Common.Utils
{
	public static class Vector2Utils
	{
		public static Vector2 Project2D(this Vector2 vector2, Vector3 other) {
			return new Vector2 (other.X, other.Y);
		}
	}
}


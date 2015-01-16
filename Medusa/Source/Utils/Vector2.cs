using System;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils
{
	public class Vector2
	{
		public static readonly Vector2 Zero = new Vector2 (0f, 0f);

		public float X {
			get;
			set;
		}

		public float Y {
			get;
			set;
		}

		public Vector2 (float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		public static Vector2 operator + (Vector2 lhs, Vector2 rhs)
		{
			return new Vector2 (lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

		public static Vector2 operator - (Vector2 lhs, Vector2 rhs)
		{
			return new Vector2 (lhs.X - rhs.X, lhs.Y - rhs.Y);
		}

		public override string ToString ()
		{
			return string.Format ("[Vector2: X={0}, Y={1}]", X, Y);
		}
	}
}


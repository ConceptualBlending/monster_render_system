using System;
using Microsoft.Xna.Framework;

namespace Hoax.Engine.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector2 Project2D(this Vector2 vector2, Vector3 other) {
			return new Vector2 (other.X, other.Y);
		}

		/**
		 * Returns true if the vector q lies on the line (p, r) 
		 */
		public static bool LiesOnLine(Vector2 p, Vector2 q, Vector2 r)
		{
			if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
				q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
				return true;

			return false;
		}

		public enum Orientation
		{
			Colinear, Clockwise, CounterClockwise
		}

		/**
		 * Returns the orientation of the vectors p,q and r
		 * 
		 * Source: http://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
		 */ 
		public static Orientation GetOrientation(Vector2 p, Vector2 q, Vector2 r)
		{
			float val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
			if (val == 0) return Orientation.Colinear;
			return (val > 0)? Orientation.Clockwise : Orientation.CounterClockwise; 
		}
			
		/*
		 * Checks if the line (p1,q1) and the line (p2,q2) intersects.
		 * Returns true if both lines intersects otherwise false.
		 * 
		 * Source: http://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
		 */ 
		public static bool Intersects(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
		{
			Orientation o1 = GetOrientation(p1, q1, p2);
			Orientation o2 = GetOrientation(p1, q1, q2);
			Orientation o3 = GetOrientation(p2, q2, p1);
			Orientation o4 = GetOrientation(p2, q2, q1);

			if (o1 != o2 && o3 != o4)
				return true;

			if ((o1 == 0 && LiesOnLine (p1, p2, q1)) || (o2 == 0 && LiesOnLine (p1, q2, q1)) ||
			    (o3 == 0 && LiesOnLine (p2, p1, q2)) || (o4 == 0 && LiesOnLine (p2, q1, q2)))
				return true;
			else
				return false;
		}
	}
}


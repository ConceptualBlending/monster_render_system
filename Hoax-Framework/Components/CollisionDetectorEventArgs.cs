using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HoaxFramework
{
	public class CollisionDetectorEventArgs : EventArgs
	{
		public DrawablePolygon2D PolygonA { get; private set; }
		public DrawablePolygon2D PolygonB { get; private set; }

		public CollisionDetectorEventArgs(DrawablePolygon2D polygonA, DrawablePolygon2D polygonB)
		{
			PolygonA = polygonA;
			PolygonB = polygonB;
		}
	}

}


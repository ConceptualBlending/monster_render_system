using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Hoax.Components.Graphics;

namespace HoaxFramework
{
	public class CollisionDetectorEventArgs : EventArgs
	{
		public Polygon2D PolygonA { get; private set; }
		public Polygon2D PolygonB { get; private set; }

		public CollisionDetectorEventArgs(Polygon2D polygonA, Polygon2D polygonB)
		{
			PolygonA = polygonA;
			PolygonB = polygonB;
		}
	}

}


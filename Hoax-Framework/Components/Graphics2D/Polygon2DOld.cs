using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HoaxFramework
{
	public class Polygon2DOld
	{
		public VertexPositionColor[] Vertices { get; protected set;}
		public PrimitiveType PrimitiveType { get; protected set; }

		public Polygon2DOld (Vector2[] points, Color lineColor)
		{
			PrimitiveType = PrimitiveType.LineStrip;
			Vertices = new VertexPositionColor[points.Length];
			for (int i = 0; i < points.Length; i++) {
				Vertices [i].Position = new Vector3(points[i].X, points[i].Y, 0);
				Vertices [i].Color = lineColor;
			}
		}
	}
}


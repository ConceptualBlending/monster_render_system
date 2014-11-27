using System;
using Hoax.Components.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Hoax.Framework.Components.Graphics2D;

namespace HoaxFramework
{
	public abstract class DrawablePolygon2D : Polygon2D
	{
		public Transformation2D Transformation2D { get; private set; }

		public DrawablePolygon2D (Polygon2D polygon) : base (polygon.Vertices)
		{
		}

		public virtual void Draw (Canvas canvas) 
		{
			MethodTimeTracker.Instance.Start ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			Transformation2D = canvas.Transformation2D;
			MethodTimeTracker.Instance.Stop ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public void DrawBoundingBox (Canvas canvas, Color lineColor)
		{
			MethodTimeTracker.Instance.Start ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			VertexPositionColor[] vertices = new VertexPositionColor[5];
			vertices [0] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			vertices [1] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Max.Y,0), lineColor);
			vertices [2] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Min.Y,0), lineColor);
			vertices [3] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Min.Y,0), lineColor);
			vertices [4] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			canvas.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, 4);
			MethodTimeTracker.Instance.Stop ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}


	}
}


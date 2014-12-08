using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Engine.Graphics2D;
using Hoax.Engine.Extensions;

namespace Hoax.Engine.Common
{
	public class Polygon2D
	{
		public enum OrientationType {Clockwise, CounterClockwise, Unknown}


		public Transformation2D LocalTransformation { get; protected set; }

		public OrientationType Orientation;
		public DrawableBoundingBox BoundingBox { get; private set; }
		public Vector2[] LocalVertices { get; private set; }
		public Vector2[] WorldVertices { get; private set; }
		public bool IsConvex { get; private set; }

		public void SetWorldMatrix (Matrix worldMatrix)
		{
			for (int i = 0; i < WorldVertices.Length; i++) {
				WorldVertices [i] = Vector2.Transform (LocalVertices [i], worldMatrix);
			}

			BoundingBox.SetWorldMatrix (worldMatrix);
		}

		public void DrawBoundingBox (Canvas canvas, Color lineColor)
		{
			VertexPositionColor[] vertices = new VertexPositionColor[5];
			vertices [0] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			vertices [1] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Max.Y,0), lineColor);
			vertices [2] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Min.Y,0), lineColor);
			vertices [3] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Min.Y,0), lineColor);
			vertices [4] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			canvas.Game.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, 4);
		}

		public virtual void Draw (SpriteBatch spriteBatch) 
		{
			//LocalTransformation = canvas.Transformation2D;
		}

		public Polygon2D (Vector2[] points)
		{
			LocalTransformation = new Transformation2D ();
			InitializePolygonPoints (points);
		}

		public virtual void SetPosition(int index, Vector2 position) {
			LocalVertices [index] = position;
			UpdateBoundingBox (LocalVertices);
		}

		public virtual void Translate (Vector2 translation)
		{
			for (int i = 0; i < LocalVertices.Length; i++)
				SetPosition (i, LocalVertices[i] + translation);
			UpdateBoundingBox (LocalVertices);
		}

		void InitializePolygonPoints (Vector2[] points)
		{
			int countOfPoints = points.Length;
			if (points [0].X != points [points.Length - 1].X || points [0].Y != points [points.Length - 1].Y) {
				countOfPoints++;
			}
			LocalVertices = new Vector2[countOfPoints];
			WorldVertices = new Vector2[countOfPoints];

			Array.Copy (points, LocalVertices, points.Length);
			if (points.Length != countOfPoints)
				Array.Copy (points, 0, LocalVertices, countOfPoints - 1, 1);
				
			UpdateConvexityState ();
			UpdateBoundingBox (points);
		}
			

		private void UpdateBoundingBox (Vector2[] points)
		{
			float xmin = float.MaxValue;
			float ymin = float.MaxValue;
			float xmax = float.MinValue;
			float ymax = float.MinValue;
			foreach (var point in points) {
				xmin = Math.Min (xmin, point.X);
				ymin = Math.Min (ymin, point.Y);
				xmax = Math.Max (xmax, point.X);
				ymax = Math.Max (ymax, point.Y);
			
			}

			BoundingBox = new DrawableBoundingBox (new Vector3(xmin, ymin, 0), new Vector3(xmax, ymax,0 ));
		}

		private void UpdateConvexityState()
		{
			if (!IsConvexClockwise (LocalVertices)) {
				Vector2[] verticesCpy = new Vector2[LocalVertices.Length];
				Array.Copy (LocalVertices, verticesCpy, LocalVertices.Length);
				Array.Reverse (verticesCpy);
				if (IsConvexClockwise (verticesCpy)) {
					IsConvex = true;
					Orientation = OrientationType.CounterClockwise;
				} else {
					IsConvex = false;
					Orientation = OrientationType.Unknown;
				}

			} else {
				Orientation = OrientationType.Clockwise;
				IsConvex = true;
			}
		}

		private static bool IsConvexClockwise(Vector2[] vertices)
		{
			if (vertices.Length < 4)
				return true;
			else {
				bool convex = false;
				int n = vertices.Length;
				for (int i = 0; i < n; i++) {

					float dx1 = vertices [(i + 2) % n].X - vertices [(i + 1) % n].X;
					float dy1 = vertices [(i + 2) % n].Y - vertices [(i + 1) % n].Y;
					float dx2 = vertices [i].X - vertices [(i+1) % n].X;
					float dy2 = vertices [i].Y - vertices [(i+1) % n].Y;
					float crossproduct = dx1 * dy2 - dy1 * dx2;
					if (i == 0)
						convex = crossproduct > 0;
					else {
						if (convex != crossproduct > 0)
							return false;
					}
				}
				return true;
			}
		}


		public static bool Intersecs (Polygon2D a, Polygon2D b)
		{
			if (a.BoundingBox.Intersects(b.BoundingBox)) {
				int vertexCountA = a.WorldVertices.Length;
				int vertexCountB = b.WorldVertices.Length;

				for (int i = 0; i < vertexCountA; i++) {
					for (int k = 0; k < vertexCountB; k++) {
						if (Vector2Extensions.Intersects (a.WorldVertices [i], a.WorldVertices [(i + 1) % vertexCountA], 
							b.WorldVertices [k], b.WorldVertices [(k + 1) % vertexCountB])) {
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}


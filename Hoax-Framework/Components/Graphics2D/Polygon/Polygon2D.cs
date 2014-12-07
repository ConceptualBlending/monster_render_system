using System;
using Microsoft.Xna.Framework;
using Hoax.Framework.Components.Graphics2D;
using Hoax.Framework.Common.Utils;
using HoaxFramework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoax.Components.Graphics
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
			MethodTimeTracker.Instance.Start ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			VertexPositionColor[] vertices = new VertexPositionColor[5];
			vertices [0] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			vertices [1] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Max.Y,0), lineColor);
			vertices [2] = new VertexPositionColor (new Vector3(BoundingBox.Max.X, BoundingBox.Min.Y,0), lineColor);
			vertices [3] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Min.Y,0), lineColor);
			vertices [4] = new VertexPositionColor (new Vector3(BoundingBox.Min.X, BoundingBox.Max.Y,0), lineColor);
			canvas.Game.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, 4);
			MethodTimeTracker.Instance.Stop ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void Draw (SpriteBatch spriteBatch) 
		{
			MethodTimeTracker.Instance.Start ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			//LocalTransformation = canvas.Transformation2D;
			MethodTimeTracker.Instance.Stop ("DrawablePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public Polygon2D (Vector2[] points)
		{
			LocalTransformation = new Transformation2D ();

			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			InitializePolygonPoints (points);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void SetPosition(int index, Vector2 position) {
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			LocalVertices [index] = position;
			UpdateBoundingBox (LocalVertices);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void Translate (Vector2 translation)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			for (int i = 0; i < LocalVertices.Length; i++)
				SetPosition (i, LocalVertices[i] + translation);
			UpdateBoundingBox (LocalVertices);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		void InitializePolygonPoints (Vector2[] points)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
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

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}
			

		private void UpdateBoundingBox (Vector2[] points)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
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

			/*_boundingBoxMinBuffer.X = xmin;
			_boundingBoxMinBuffer.Y = ymin;
			_boundingBoxMaxBuffer.X = xmax;
			_boundingBoxMaxBuffer.Y = ymax;
			Vector3.Transform (_boundingBoxMinBuffer, LocalTransformation.WorldMatrix);
			Vector3.Transform (_boundingBoxMaxBuffer, LocalTransformation.WorldMatrix);
			BoundingBox.Min = _boundingBoxMinBuffer;
			BoundingBox.Max = _boundingBoxMaxBuffer;
*/
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		private void UpdateConvexityState()
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
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
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
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
			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");

		

			if (a.BoundingBox.Intersects(b.BoundingBox)) {

				int vertexCountA = a.WorldVertices.Length;
				int vertexCountB = b.WorldVertices.Length;

				for (int i = 0; i < vertexCountA; i++) {
					for (int k = 0; k < vertexCountB; k++) {
						if (Vector2Utils.Intersects (a.WorldVertices [i], a.WorldVertices [(i + 1) % vertexCountA], 
							b.WorldVertices [k], b.WorldVertices [(k + 1) % vertexCountB])) {
							return true;
						}
					}
				}

			/*	MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");

				// Check line intersection
				int vertexCountA = polygonA.LocalVertices.Length;
				int vertexCountB = polygonB.LocalVertices.Length;

				MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				for (int i = 0; i < vertexCountA; i++) {
					for (int k = 0; k < vertexCountB; k++) {
						if (Vector2Utils.Intersects (polygonA.WorldVertices [i], polygonA.WorldVertices [(i + 1) % vertexCountA], 
							polygonB.WorldVertices [k], polygonB.WorldVertices [(k + 1) % vertexCountB])) {

							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
							return true;
						}
					}
				}

				MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				// Check point containment
				foreach (var vertex in polygonB.WorldVertices) {
					if (polygonA.Contains (vertex)) {
						MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

						MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");
						MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
						return true;
					}
				}*/

			}
			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
			return false;
		}
		/*

		public bool Contains(Vector2 vertex) 
		{
			return Contains (this, vertex);
		}

		public static bool Contains(Polygon2D polygon, Vector2 vertex)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			int j = polygon.WorldVertices.Length - 1;
			bool oddNodes = false;
			float x = vertex.X;
			float y = vertex.Y;

			for (int i = 0; i < polygon.WorldVertices.Length; i++) {
				if (polygon.WorldVertices [i].Y < y && polygon.WorldVertices [j].Y >= y
					|| polygon.WorldVertices [j].Y < y && polygon.WorldVertices [i].Y >= y) {
					oddNodes ^= polygon.WorldVertices [i].X + (y - polygon.WorldVertices [i].Y) / (polygon.WorldVertices [j].Y - polygon.WorldVertices [i].Y) * (polygon.WorldVertices [j].X - polygon.WorldVertices [i].X) < x;
				}
				j = i;
			}
			return oddNodes;
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public static bool Intersecs (Polygon2D polygonA, Polygon2D polygonB) 
		{
			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");

			if (polygonA.BoundingBox.Contains (polygonB.BoundingBox) != ContainmentType.Disjoint) {

				MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");

				// Check line intersection
				int vertexCountA = polygonA.LocalVertices.Length;
				int vertexCountB = polygonB.LocalVertices.Length;

				MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				for (int i = 0; i < vertexCountA; i++) {
					for (int k = 0; k < vertexCountB; k++) {
						if (Vector2Utils.Intersects (polygonA.WorldVertices [i], polygonA.WorldVertices [(i + 1) % vertexCountA], 
							polygonB.WorldVertices [k], polygonB.WorldVertices [(k + 1) % vertexCountB])) {

							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
							return true;
						}
					}
				}

				MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				// Check point containment
				foreach (var vertex in polygonB.WorldVertices) {
					if (polygonA.Contains (vertex)) {
						MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

						MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");
						MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
						return true;
					}
				}

			}
			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
			return false;
		}

		/*public static bool Intersecs (Polygon2D self, Transformation2D selfTransformation, Polygon2D other, Transformation2D otherTransformation) 
		{
			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs");

			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)");

			//Polygon2D selfTanslated = ApplyWorldTransformation (self, selfTransformation);
			//Polygon2D otherTanslated = ApplyWorldTransformation (other, otherTransformation);
			self.UpdateWorldTransformation (selfTransformation);
			other.UpdateWorldTransformation (otherTransformation);

			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)");

			bool result = Intersecs (self, other);

			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs");
			return result;
		}*/
	}
}


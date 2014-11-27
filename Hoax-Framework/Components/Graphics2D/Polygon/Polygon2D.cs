using System;
using Microsoft.Xna.Framework;
using Hoax.Framework.Components.Graphics2D;
using Hoax.Framework.Common.Utils;
using HoaxFramework;

namespace Hoax.Components.Graphics
{
	public class Polygon2D
	{
		public enum OrientationType {Clockwise, CounterClockwise, Unknown}

		public OrientationType Orientation;
		public BoundingBox BoundingBox { get; private set; }
		public Vector2[] Vertices { get; private set; }
		public bool IsConvex { get; private set; }

		public Polygon2D (Vector2[] points)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			InitializePolygonPoints (points);
			UpdateBoundingBox (points);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void SetPosition(int index, Vector2 position) {
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			Vertices [index] = position;
			UpdateBoundingBox (Vertices);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void Translate (Vector2 translation)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			for (int i = 0; i < Vertices.Length; i++)
				SetPosition (i, Vertices[i] + translation);
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		void InitializePolygonPoints (Vector2[] points)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			int countOfPoints = points.Length;
			if (points [0].X != points [points.Length - 1].X || points [0].Y != points [points.Length - 1].Y) {
				countOfPoints++;
			}
			Vertices = new Vector2[countOfPoints];
			Array.Copy (points, Vertices, points.Length);
			if (points.Length != countOfPoints)
				Array.Copy (points, 0, Vertices, countOfPoints - 1, 1);

			UpdateConvexityState ();
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
			BoundingBox = new BoundingBox(new Vector3(xmin, ymin, 0), new Vector3(xmax, ymax, 0));
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public bool Intersecs (Polygon2D other) 
		{
			return Intersecs (this, other);
		}

		private void UpdateConvexityState()
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			if (!IsConvexClockwise (Vertices)) {
				Vector2[] verticesCpy = new Vector2[Vertices.Length];
				Array.Copy (Vertices, verticesCpy, Vertices.Length);
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
					/*
					 * double dx1 = _vertices.get((i+2)%n).X-_vertices.get((i+1)%n).X;
				        double dy1 = _vertices.get((i+2)%n).Y-_vertices.get((i+1)%n).Y;
				        double dx2 = _vertices.get(i).X-_vertices.get((i+1)%n).X;
				        double dy2 = _vertices.get(i).Y-_vertices.get((i+1)%n).Y;
					 */
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

		public bool Contains(Vector2 vertex) 
		{
			return Contains (this, vertex);
		}

		public static bool Contains(Polygon2D polygon, Vector2 vertex)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			int j = polygon.Vertices.Length - 1;
			bool oddNodes = false;
			float x = vertex.X;
			float y = vertex.Y;

			for (int i = 0; i < polygon.Vertices.Length; i++) {
				if (polygon.Vertices [i].Y < y && polygon.Vertices [j].Y >= y
				    || polygon.Vertices [j].Y < y && polygon.Vertices [i].Y >= y) {
					oddNodes ^= polygon.Vertices [i].X + (y - polygon.Vertices [i].Y) / (polygon.Vertices [j].Y - polygon.Vertices [i].Y) * (polygon.Vertices [j].X - polygon.Vertices [i].X) < x;
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
				int vertexCountA = polygonA.Vertices.Length;
				int vertexCountB = polygonB.Vertices.Length;

				MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				for (int i = 0; i < vertexCountA; i++) {
					for (int k = 0; k < vertexCountB; k++) {
						if (Vector2Utils.Intersects (polygonA.Vertices [i], polygonA.Vertices [(i + 1) % vertexCountA], 
							    polygonB.Vertices [k], polygonB.Vertices [(k + 1) % vertexCountB])) {

							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box)");
							MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs");
							return true;
						}
					}
				}

				MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)/Intersecs (in bounding box) (line intersec test)");

				// Check point containment
				foreach (var vertex in polygonB.Vertices) {
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

		public static bool Intersecs (Polygon2D self, Transformation2D selfTransformation, Polygon2D other, Transformation2D otherTransformation) 
		{
			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs");

			MethodTimeTracker.Instance.Start ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)");

			Polygon2D selfTanslated = ApplyWorldTransformation (self, selfTransformation);
			Polygon2D otherTanslated = ApplyWorldTransformation (other, otherTransformation);

			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs (apply world trans)");

			bool result = Intersecs (selfTanslated, otherTanslated);

			MethodTimeTracker.Instance.Stop ("CollisionDetector: " + System.Reflection.MethodBase.GetCurrentMethod ().Name + " (intersec test)/Intersecs");
			return result;
		}
					
		public static Polygon2D ApplyWorldTransformation (Polygon2D polygon, Transformation2D transformation)
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			Vector2[] translatedPoints = new Vector2[polygon.Vertices.Length];
			for (int i = 0; i < translatedPoints.Length; i++) {
				translatedPoints [i] = Vector2.Transform (polygon.Vertices [i], transformation.WorldMatrix);
			}
			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			return new Polygon2D (translatedPoints);		//TODO: Garbage Collector!
		}
	}
}


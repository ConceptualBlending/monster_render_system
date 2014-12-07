using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public class DrawableBoundingBox
	{
		private Color _lineColor;
		public Color LineColor {
			get {
				return _lineColor;
			}
			set {
				_lineColor = value;

				Vertices [0].Color = value;
				Vertices [1].Color = value;
				Vertices [2].Color = value;
				Vertices [3].Color = value;
				Vertices [4].Color = value;
			}
		}

		public VertexPositionColor[] Vertices = new VertexPositionColor[5];

		public Vector3 LocalLeftBottom = new Vector3 ();

		//public Vector3 LocalLeftTop = new Vector3 ();

		public Vector3 LocalRightTop = new Vector3 ();

		public Vector3 WorldLeftBottom = new Vector3 ();

		public Vector3 WorldRightTop = new Vector3 ();


		public bool Intersects (DrawableBoundingBox other)
		{
			float xmin = WorldLeftBottom.X;
			float ymin = WorldLeftBottom.Y;
			float xmax = WorldRightTop.X;
			float ymax = WorldRightTop.Y;

			float oxmin = other.WorldLeftBottom.X;
			float oymin = other.WorldLeftBottom.Y;
			float oxmax = other.WorldRightTop.X;
			float oymax = other.WorldRightTop.Y;

			BoundingBox b1 = new BoundingBox (WorldLeftBottom, WorldRightTop);
			BoundingBox b2 = new BoundingBox (other.WorldLeftBottom, other.WorldRightTop);

			return b1.Contains (b2) != ContainmentType.Disjoint;

			//return (Math.Abs(xmin - oxmin) < (xmax - xmin + oxmax - oxmin) / 2) &&
			//	(Math.Abs(ymin - oymin) < (ymax - ymin + oymax - oymin) / 2);
		}

		public Vector3 Min {
			get {
				return LocalLeftBottom;
			}
			/*set {
				LocalLeftBottom = value;//BoundingBox = new BoundingBox (value, BoundingBox.Max);
				UpdateDrawableVertices ();
			}*/
		}

		public Vector3 Max {
			get {
				return LocalRightTop;
			}
			/*set {
				LocalRightTop = value; //BoundingBox = new BoundingBox (BoundingBox.Min, value);
				UpdateDrawableVertices ();
			}*/
		}

		public void Update (float xMin, float yMin, float xMax, float yMax)
		{
			LocalLeftBottom.X = xMin;
			LocalLeftBottom.Y = yMin;
			LocalRightTop.X = xMax;
			LocalRightTop.Y = yMax;

			LocalLeftTop.X = LocalLeftBottom.X;
			LocalLeftTop.Y = LocalRightTop.Y;
			LocalRightBottom.X = LocalRightTop.X;
			LocalRightBottom.Y = LocalLeftBottom.Y;

			UpdateWorldTranslation ();

			UpdateDrawableVertices ();


		}

		void UpdateWorldTranslation ()
		{
			WorldLeftBottom = Vector3.Transform (LocalLeftBottom, WorldMatrix);
			WorldRightTop = Vector3.Transform (LocalRightTop, WorldMatrix);

		}

		public Matrix WorldMatrix { get; set; }

		public void SetWorldMatrix (Matrix worldMatrix)
		{
			WorldMatrix = worldMatrix;
			UpdateWorldTranslation ();
		}

		public DrawableBoundingBox (Vector3 min, Vector3 max) 
		{
			WorldMatrix = new Matrix ();

			Update (min.X, min.Y, max.X, max.Y);

			LineColor = Color.DarkGray;

			createVertexArray ();
			UpdateDrawableVertices ();


		}

		private void createVertexArray ()
		{
			Vertices [0] = new VertexPositionColor (new Vector3(), Color.Red);
			Vertices [1] = new VertexPositionColor (new Vector3(), Color.Red);
			Vertices [2] = new VertexPositionColor (new Vector3(), Color.Red);
			Vertices [3] = new VertexPositionColor (new Vector3(), Color.Red);
			Vertices [4] = new VertexPositionColor (new Vector3(), Color.Red);
		}

		private Vector3 LocalLeftTop = new Vector3();
		private Vector3 LocalRightBottom = new Vector3();

		private void UpdateDrawableVertices() 
		{


	/*		LocalLeftBottom.X = BoundingBox.Min.X;
			LocalLeftBottom.Y = BoundingBox.Min.Y;
			LocalLeftTop.X = BoundingBox.Min.X;
			LocalLeftTop.Y = BoundingBox.Max.Y;
			LocalRightTop.X = BoundingBox.Max.X;
			LocalRightTop.Y = BoundingBox.Max.Y;
			LocalRightBottom.X = BoundingBox.Max.X;
			LocalRightBottom.Y = BoundingBox.Min.Y;
			*/
			Vertices [0].Position = LocalLeftBottom;
			Vertices [1].Position = LocalLeftTop;
			Vertices [2].Position = LocalRightTop;
			Vertices [3].Position = LocalRightBottom;
			Vertices [4].Position = LocalLeftBottom;


		}

		public void Draw(GraphicsDevice graphicsDevice)
		{
			graphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, Vertices, 0, 4);
		}
	}
}


using System;
using Hoax.Components.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HoaxFramework
{
	public class SolidLinePolygon2D : DrawablePolygon2D
	{
		public VertexPositionColor[] VertexPositionColor { get; private set; }
		public Color LineColor{ get; private set; }

		public SolidLinePolygon2D (Polygon2D polygon, Color lineColor) : base (polygon)
		{
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			LineColor = lineColor;
			updateVerticesList (polygon);
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public void SetLineColor(int index, Color lineColor) {
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			VertexPositionColor [index].Color = lineColor;
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public override void SetPosition(int index, Vector2 positon) {
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			base.SetPosition (index, positon);
			updateVerticesList (this);
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public override void Translate(Vector2 translation) {
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			base.Translate (translation);
			updateVerticesList (this);
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}


		#region implemented abstract members of DrawablePolygon2D

		public override void Draw (Canvas canvas)
		{
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			base.Draw (canvas);
			canvas.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, VertexPositionColor, 0, VertexPositionColor.Length - 1);
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		#endregion

		void updateVerticesList (Polygon2D polygon)
		{
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			VertexPositionColor = new VertexPositionColor[polygon.Vertices.Length];

			for (int i = 0; i < polygon.Vertices.Length; i++) {
				VertexPositionColor [i].Position = new Vector3 (polygon.Vertices [i].X, polygon.Vertices [i].Y, 0);
				VertexPositionColor [i].Color = LineColor;
				}

			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);

			}

			
		}
}


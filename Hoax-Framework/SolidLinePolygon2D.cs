using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Hoax.Engine.Common;

namespace Hoax.Engine.Graphics2D
{
	public class SolidLinePolygon2D : Polygon2D
	{
		public VertexPositionColor[] VertexPositionColor { get; private set; }

		private Color _lineColor;
		public Color LineColor { 
			get {
				return _lineColor;
			}

			set {
				_lineColor = value;
				updateVerticesList (this);
			}
		}
		public Game Game  { get; private set; } 
		private BasicEffect Effect;

		public float Width {
			get;
			private set;
		}

		public float Height {
			get;
			private set;
		}

		public SolidLinePolygon2D (Game game, Polygon2D polygon, Color lineColor) : base (polygon.LocalVertices)
		{
			LineColor = lineColor;

			ShowBoundingBox = false;
			Game = game;
			updateVerticesList (polygon);
		}

		public void SetLineColor(int index, Color lineColor) {
			VertexPositionColor [index].Color = lineColor;
		}

		public override void SetPosition(int index, Vector2 positon) {
			base.SetPosition (index, positon);
			updateVerticesList (this);
		}

		public override void Translate(Vector2 translation) {
			base.Translate (translation);
			updateVerticesList (this);
		}


		#region implemented abstract members of DrawablePolygon2D

		public override void Draw (SpriteBatch spriteBatch)
		{
			RenderPolygon (spriteBatch);
		}

		#endregion

		public float XMin { get; private set; }
		public float YMin { get; private set; }
		public float XMax { get; private set; }
		public float YMax { get; private set; }
		public Vector2 Position { get; private set; }

		void updateVerticesList (Polygon2D polygon)
		{
			XMin = float.MaxValue;
			YMin = float.MaxValue;
			XMax = float.MinValue;
			YMax = float.MinValue;

			VertexPositionColor = new VertexPositionColor[polygon.LocalVertices.Length];

			for (int i = 0; i < polygon.LocalVertices.Length; i++) {
				VertexPositionColor [i].Position = new Vector3 (polygon.LocalVertices [i].X, polygon.LocalVertices [i].Y, 0);
				VertexPositionColor [i].Color = LineColor;

				XMin = Math.Min (XMin, polygon.LocalVertices [i].X);
				YMin = Math.Min (YMin, polygon.LocalVertices [i].Y);
				XMax = Math.Max (XMax, polygon.LocalVertices [i].X);
				YMax = Math.Max (YMax, polygon.LocalVertices [i].Y);
			}

			Width = XMax - XMin;
			Height = YMax - YMin;

			Position = new Vector2 (XMin, YMin);
			BoundingBox.Update (XMin, YMin, XMax, YMax);
		}

		public bool ShowBoundingBox {
			get;
			set;
		}

			
		void RenderPolygon (SpriteBatch destinationBatch)
		{

			destinationBatch.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, VertexPositionColor, 0, VertexPositionColor.Length - 1);

			if (ShowBoundingBox) {
				base.BoundingBox.Draw (destinationBatch.GraphicsDevice);
			}
		}
	}
}


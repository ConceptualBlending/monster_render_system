using System;
using Hoax.Components.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HoaxFramework
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
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);

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

		public override void Draw (SpriteBatch spriteBatch)
		{
			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
			//base.Draw (spriteBatch);
			RenderPolygon (spriteBatch);
			//canvas.Game.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, VertexPositionColor, 0, VertexPositionColor.Length - 1);
			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		#endregion

		public float XMin { get; private set; }
		public float YMin { get; private set; }
		public float XMax { get; private set; }
		public float YMax { get; private set; }
		public Vector2 Position { get; private set; }

		//RenderTarget2D RenderTarget;
		/*Color[] c;
*/
		//public Texture2D RenderedPolygon {
		//	get;
		//	private set;
		//}

		//void RenderIntoTexture (GraphicsDevice graphicsDevice)
		//{
			//System.Console.Out.WriteLine ("RENDERING POLY");
			//RenderedPolygon = new Texture2D (graphicsDevice, (int)Math.Ceiling (Width), (int)Math.Ceiling (Height));
		    
			//RenderTarget2D target = new RenderTarget2D (graphicsDevice, (int)Math.Ceiling (Width), (int)Math.Ceiling (Height), false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 16, RenderTargetUsage.DiscardContents);

			//graphicsDevice.SetRenderTarget (target);
			//graphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, VertexPositionColor, 0, VertexPositionColor.Length - 1);
			//graphicsDevice.SetRenderTarget (null);

			//Color[]	c = new Color[target.Width * target.Height];
			//target.GetData (c);
			//RenderedPolygon.SetData (c);
		//	}

	

		void updateVerticesList (Polygon2D polygon)
		{
			XMin = float.MaxValue;
			YMin = float.MaxValue;
			XMax = float.MinValue;
			YMax = float.MinValue;

			MethodTimeTracker.Instance.Start ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);
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

			//RenderIntoTexture ();

			MethodTimeTracker.Instance.Stop ("SolidLinePolygon2D: " + System.Reflection.MethodBase.GetCurrentMethod ().Name);

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

			//if (RenderedPolygon == null)
			//	RenderIntoTexture (destinationBatch.GraphicsDevice);
				
			//destinationBatch.Draw (RenderedPolygon, new Vector2(10,10), Color.Transparent);

			/*Effect = new ProjectFixedEffect (GraphicsDevice, (int)Math.Ceiling (Width), (int)Math.Ceiling (Height));

			SpriteBatch spriteBatch = new SpriteBatch (GraphicsDevice);

			// Draw on canvas
			spriteBatch.GraphicsDevice.SetRenderTarget (RenderedPolygon);
			spriteBatch.GraphicsDevice.Clear (Color.Aqua);
			spriteBatch.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, VertexPositionColor, 0, VertexPositionColor.Length - 1);
*/



			//destinationBatch.Draw (RenderedPolygon, Position, Color.Yellow);

			//foreach (EffectPass pass in Effect.CurrentTechnique.Passes) {
			//	pass.Apply ();
			//	canvas.Draw (canvas.Game.GraphicsDevice);
			//}
		}
	}
}


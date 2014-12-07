using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Components.Graphics;
using Hoax.Framework.Components.Graphics2D;

namespace HoaxFramework
{
	public abstract class Canvas : Drawable
	{
		public bool ShowBorder { get; set; }

		public Color BackgroundColor {
			get;
			set;
		}

		public int Width { get; private set; }
		public int Height { get; private set; }
		public RenderTarget2D RenderTarget { get; private set; } 
		public DrawableBoundingBox BoundingBoxXXX { get; private set; } 

		public BasicEffect BasicEffect { get; private set; }
		public Game Game { get; private set; }

		public Canvas (string identifier, Game game, int width, int height, Color backgroundColor) : base(identifier)
		{
			BackgroundColor = backgroundColor;
			Width = width;
			Height = height;
			Game = game;
			ShowBorder = false;
			BoundingBoxXXX = new DrawableBoundingBox (new Vector3 (0, 0, 0), new Vector3 (width, height, 0));
			BoundingBoxXXX.LineColor = Color.Magenta;
		}

		public override void LoadContent ()
		{
			InitializeTransform ();
			InitializeRenderTarget ();
		}

		private void InitializeTransform()
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			BasicEffect = new ProjectFixedEffect (Game.GraphicsDevice, Width, Height);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		void InitializeRenderTarget ()
		{
			RenderTarget = new RenderTarget2D (Game.GraphicsDevice, Width, Height,false, SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.PlatformContents);
		}

		public virtual void OnPaint(SpriteBatch graphicsDevice) {
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			OnPaint (spriteBatch);
			if (ShowBorder) {
				System.Console.Out.WriteLine ("XXXXXXX");
				DrawBoundingBox (spriteBatch.GraphicsDevice);
			}
		}

		private void DrawBoundingBox (GraphicsDevice graphicsDevice)
		{
			BoundingBoxXXX.Draw (graphicsDevice);
		}

		/*protected void DrawPolygon (Polygon2D polygon, Color lineColor) 
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			VertexPositionColor[] vertices = new VertexPositionColor[polygon.LocalVertices.Length];
			for (int i = 0; i < polygon.LocalVertices.Length; i++) {
				vertices [i].Position = new Vector3(polygon.LocalVertices[i].X, polygon.LocalVertices[i].Y, 0);
				vertices [i].Color = lineColor;
			}

			Game.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		protected void FillPolygon (Polygon2D polygon, Color lineColor) 
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			VertexPositionColor[] vertices = new VertexPositionColor[polygon.LocalVertices.Length];
			for (int i = 0; i < polygon.LocalVertices.Length; i++) {
				vertices [i].Position = new Vector3(polygon.LocalVertices[i].X, polygon.LocalVertices[i].Y, 0);
				vertices [i].Color = lineColor;
			}

			Game.GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}*/
			
	}

	public static class SpriteBatchExtensions {

		public static void Draw(this SpriteBatch spriteBatch, Canvas canvas, Transformation2D transformation2D) {

			MethodTimeTracker.Instance.Start ("SpriteBatchExtensions:" + System.Reflection.MethodBase.GetCurrentMethod ().Name);

			// Draw on canvas
			spriteBatch.GraphicsDevice.SetRenderTarget(canvas.RenderTarget);
			spriteBatch.GraphicsDevice.Clear(canvas.BackgroundColor);

			foreach (EffectPass pass in canvas.BasicEffect.CurrentTechnique.Passes) {
				pass.Apply ();
				canvas.Draw (spriteBatch);
			}
	
			// Draw with sprite batch
			spriteBatch.GraphicsDevice.SetRenderTarget (null);
			spriteBatch.Draw (canvas.RenderTarget, transformation2D.WorldPosition, new Rectangle (0, 0, canvas.Width, canvas.Height), Color.White, transformation2D.WorldRotation, transformation2D.PivotPoint, transformation2D.WorldScale, SpriteEffects.None, 1);

			MethodTimeTracker.Instance.Stop ("SpriteBatchExtensions:" + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

	}
}


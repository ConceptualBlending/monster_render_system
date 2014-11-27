using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Components.Graphics;
using Hoax.Framework.Components.Graphics2D;

namespace HoaxFramework
{
	public class Canvas : Drawable
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public RenderTarget2D RenderTarget { get; private set; }

		public BasicEffect BasicEffect { get; private set; }
		public GraphicsDevice GraphicsDevice { get; private set; }

		public Canvas (string identifier, GraphicsDevice graphicsDevice, int width, int height) : base(identifier)
		{
			Width = width;
			Height = height;
			GraphicsDevice = graphicsDevice;
			RenderTarget = new RenderTarget2D (graphicsDevice, width, height,false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 16, RenderTargetUsage.PreserveContents);
			InitializeTransform ();
		}

		private void InitializeTransform()
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			BasicEffect = new BasicEffect(GraphicsDevice);
			BasicEffect.VertexColorEnabled = true;

			BasicEffect.World = Matrix.CreateTranslation(0,0, 0);
			BasicEffect.View = Matrix.CreateLookAt (
				new Vector3 (0.0f, 0.0f, 1.0f),
				Vector3.Zero,
				Vector3.Up
			);
			BasicEffect.Projection = Matrix.CreateOrthographicOffCenter (
				0,
				Width + 1,//(float)GraphicsDevice.Viewport.Width,
				Height + 1, //(float)GraphicsDevice.Viewport.Height,
				0,
				1.0f, 1000.0f);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		public virtual void OnPaint(GraphicsDevice graphicsDevice) {
		}

		protected void DrawPolygon (Polygon2D polygon, Color lineColor) 
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			VertexPositionColor[] vertices = new VertexPositionColor[polygon.Vertices.Length];
			for (int i = 0; i < polygon.Vertices.Length; i++) {
				vertices [i].Position = new Vector3(polygon.Vertices[i].X, polygon.Vertices[i].Y, 0);
				vertices [i].Color = lineColor;
			}

			GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

		protected void FillPolygon (Polygon2D polygon, Color lineColor) 
		{
			MethodTimeTracker.Instance.Start (System.Reflection.MethodBase.GetCurrentMethod ().Name);

			VertexPositionColor[] vertices = new VertexPositionColor[polygon.Vertices.Length];
			for (int i = 0; i < polygon.Vertices.Length; i++) {
				vertices [i].Position = new Vector3(polygon.Vertices[i].X, polygon.Vertices[i].Y, 0);
				vertices [i].Color = lineColor;
			}

			GraphicsDevice.DrawUserPrimitives (PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);

			MethodTimeTracker.Instance.Stop (System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}
			
	}

	public static class SpriteBatchExtensions {

		public static void Draw(this SpriteBatch spriteBatch, Canvas canvas, Transformation2D transformation2D, Color backgroundColor) {

			MethodTimeTracker.Instance.Start ("SpriteBatchExtensions:" + System.Reflection.MethodBase.GetCurrentMethod ().Name);

			// Draw on canvas
			spriteBatch.GraphicsDevice.SetRenderTarget(canvas.RenderTarget);
			//spriteBatch.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
			spriteBatch.GraphicsDevice.Clear(Color.Transparent);

			foreach (EffectPass pass in canvas.BasicEffect.CurrentTechnique.Passes) {
				pass.Apply ();
				canvas.OnPaint (canvas.GraphicsDevice);
			}
	
			// Draw with sprite batch
			spriteBatch.GraphicsDevice.SetRenderTarget (null);
			spriteBatch.Draw (canvas.RenderTarget, transformation2D.WorldPosition, new Rectangle (0, 0, canvas.Width, canvas.Height), Color.White, transformation2D.WorldRotation, transformation2D.PivotPoint, transformation2D.WorldScale, SpriteEffects.None, 1);

			MethodTimeTracker.Instance.Stop ("SpriteBatchExtensions:" + System.Reflection.MethodBase.GetCurrentMethod ().Name);
		}

	}
}


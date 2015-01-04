using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoax.Engine.Graphics2D
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
			BasicEffect = new ProjectFixedEffect (Game.GraphicsDevice, Width, Height);
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
				DrawBoundingBox (spriteBatch.GraphicsDevice);
			}
		}

		private void DrawBoundingBox (GraphicsDevice graphicsDevice)
		{
			BoundingBoxXXX.Draw (graphicsDevice);
		}	
	}
}


using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hoax.Engine.Input;

namespace HoaxFramework
{
	public abstract class AbstractScene
	{
		protected RenderTarget2D renderTargetScene;
		//protected Texture2D renderedSceneTexture;
		//protected Color[] renderedSceneColorBuffer;
		protected SpriteBatch spriteBatch;
		protected Game game;

		private ContentLoader contentLoader;
		public ContentLoader ContentLoader {
			get {
				if (this.contentLoader == null)
					this.contentLoader = new ContentLoader (this.GraphicsDevice);
				return this.contentLoader;
			}
			private set {
				this.contentLoader = value;
			}
		}

		public string Name {
			get;
			set;
		}

		public int Width {
			get; 
			private set;
		}

		public int Height {
			get; 
			private set;
		}

		public GraphicsDevice GraphicsDevice {
			get;
			private set;
		}

		public AbstractScene(Game game, GraphicsDevice graphicsDevice, string name, int width, int height) 
		{
			this.game = game;
			this.renderTargetScene = new RenderTarget2D (graphicsDevice, width, height);
			//this.renderedSceneTexture = new Texture2D(graphicsDevice, width, height);
			//this.renderedSceneColorBuffer = new Color[width * height];
			this.Width = width;
			this.Height = height;
			this.spriteBatch = new SpriteBatch (graphicsDevice);
			this.GraphicsDevice = graphicsDevice;
			this.Name = name;
		}

		public RenderTarget2D Render (GameTime gameTime, GraphicsDevice graphicsDevice) 
		{
			this.RenderScene(gameTime, graphicsDevice, renderTargetScene);

			/*Texture2D texture = 

			//graphicsDevice.pushRenderTarget(renderTargetScene);

			// ...
			spriteBatch.Begin ();
			spriteBatch.Draw (texture, Vector2.Zero);
			spriteBatch.End ();
			// TODO: Transition etc.

			//graphicsDevice.popRenderTarget ();*/

			//renderTargetScene.GetData(this.renderedSceneColorBuffer);
			//renderedSceneTexture.SetData(this.renderedSceneColorBuffer);
			return renderTargetScene;
		}

		public abstract void LoadContent ();
		public abstract void RenderScene (GameTime gameTime, GraphicsDevice graphicsDevice, RenderTarget2D renderTargetScene);
		public abstract void Update (GameTime gameTime, Dictionary<Player, VirtualGamePad> PlayerGamePads);

		public virtual void OnEntered () {}
		public virtual void OnEntering () {}
		public virtual void OnTransitionOutStarted () {}
		public virtual void OnTransitionInEnded () {}
		public virtual void OnLeaved () {}
		public virtual void OnLeaving () {}
		public virtual void OnPause () {}
		public virtual void OnResume () {}
	}
}


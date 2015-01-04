using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public abstract class TransitionBase
	{
		public RenderTarget2D Result {
			get;
			protected set;
		}

		protected RenderTarget2D Source;
		protected RenderTarget2D Destination;
		protected GraphicsDevice GraphicsDevice;
		protected SpriteBatch SpriteBatch;

		public delegate void TransitionEndedEventHandler(object sender, EventArgs e);

		public event TransitionEndedEventHandler TransitionEnded;

		protected void OnTransitionEnded(EventArgs e) 
		{
			if (TransitionEnded != null)
				TransitionEnded (this, e);
		}

//		public TransitionBase(GraphicsDevice graphicsDevice, Texture2D source, RenderTarget2D destination)
//		{
//			Texture2D destText = new Texture2D (graphicsDevice, source.Width, source.Height);
//			Color[] data = new Color[source.Width * source.Height];
//			destText.GetData<Color> (data);
//			init (graphicsDevice, source, destText);
//		}
//
//		public TransitionBase (GraphicsDevice graphicsDevice, Texture2D source, Texture2D destination) {
//			init (graphicsDevice, source, destination);
//		}

		public  TransitionBase(GraphicsDevice graphicsDevice, int width, int height /*, Texture2D source, Texture2D destination*/)
		{
			this.GraphicsDevice = graphicsDevice;
			Result = new RenderTarget2D (graphicsDevice, width, height);
			SpriteBatch = new SpriteBatch (graphicsDevice);

			Reset (width, height);
		}

		public void Render(GraphicsDevice graphicsDevice, RenderTarget2D source, RenderTarget2D destination) {
			if (source.Width != destination.Width || source.Height != destination.Height) {
				var msg = string.Format ("Source {0}x{1} does not match destination {2}x{3}", source.Width, source.Height, destination.Width, destination.Height);
				throw new FormatException (msg);
			}

			this.Source = source;
			this.Destination = destination;

			Color[] data = PerformTransition (graphicsDevice);
			Result.SetData (data);

		}
			
		public abstract void Reset (int width, int height);

		public void Update (GameTime gameTime) {
			OnUpdate (gameTime);

			if (IsTransitionComplete)
				OnTransitionEnded (new EventArgs ());
		}

		public abstract void OnUpdate (GameTime gameTime);

		public abstract bool IsTransitionComplete {
			get;
		}
		protected abstract Color[] PerformTransition (GraphicsDevice graphicsDevice);
	}
}


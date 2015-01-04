using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoaxFramework
{
	public class DissolveTransition : TransitionBase
	{
		public static Func<int, float> LinearTransitionSlow = (x) => x * .0005f;
		public static Func<int, float> LinearTransitionMedium = (x) => x * .001f;
		public static Func<int, float> LinearTransitionFast = (x) => x * .005f;
		public static Func<int, float> QuadraticTransitionSlow = (x) => (float) (Math.Pow((double)x, 2d) * .00005f);
		public static Func<int, float> QuadraticTransitionMedium = (x) =>(float) (Math.Pow((double)x, 2d) * .0001f);
		public static Func<int, float> QuadraticTransitionFast = (x) => (float) (Math.Pow((double)x, 2d) * .0005f);

		private float fadeAmount = 0;

		private RenderTarget2D renderTarget;

		private Func<int, float> transitionFunction;

		public DissolveTransition (GraphicsDevice graphicsDevice, int width, int height, Func<int, float> transitionFunction) : base(graphicsDevice, width, height)	{
			this.transitionFunction = transitionFunction;
		}

		#region implemented abstract members of TransitionBase

		public override bool IsTransitionComplete {
			get {
				//Console.WriteLine (fadeAmount);
				return fadeAmount > 2.0f;
			}
		}

		public override void Reset (int width, int height)
		{
			renderTarget = new RenderTarget2D (base.GraphicsDevice, width, height,
				true, base.GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24, 
				base.GraphicsDevice.PresentationParameters.MultiSampleCount, 
				RenderTargetUsage.PreserveContents);
		}

		protected override Color[] PerformTransition (GraphicsDevice graphicsDevice)
		{
			graphicsDevice.SetRenderTarget (renderTarget);
			base.SpriteBatch.Begin (SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			base.SpriteBatch.Draw (base.Destination, Vector2.Zero);
			base.SpriteBatch.Draw (base.Source, Vector2.Zero, new Color(new Vector4(Color.White.ToVector3(), 1.0f - fadeAmount)));
			base.SpriteBatch.End ();
			graphicsDevice.SetRenderTarget (null);

			Color[] data = new Color [base.Source.Width * base.Source.Height];
			renderTarget.GetData (data);
			return data;
		}

		public override void OnUpdate (GameTime gameTime)
		{
			if (fadeAmount <= 2.0f) {
				fadeAmount += this.transitionFunction.Invoke(gameTime.ElapsedGameTime.Milliseconds);
			}
		}
			
		#endregion
	}
}


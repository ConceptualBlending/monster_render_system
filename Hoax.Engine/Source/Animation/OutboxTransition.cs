using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HoaxFramework
{
	public class OutboxTransition : TransitionBase
	{
		private float fadeAmount = 0;

		private Color[] outboxData; 
		private Texture2D outboxTexture;
		private RenderTarget2D renderTarget;

		enum BoxMode { BlendOut, BlendIn }
		private BoxMode boxMode;

		public OutboxTransition (GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height)	{
		}

		#region implemented abstract members of TransitionBase

		public override bool IsTransitionComplete {
			get {
				//Console.WriteLine (fadeAmount);
				return fadeAmount == 1.0f && boxMode == BoxMode.BlendIn;
			}
		}

		public override void Reset (int width, int height)
		{
			renderTarget = new RenderTarget2D (base.GraphicsDevice, width, height,
				true, base.GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24, 
				base.GraphicsDevice.PresentationParameters.MultiSampleCount, 
				RenderTargetUsage.PreserveContents);
			boxMode = BoxMode.BlendOut;
		}

		protected override Color[] PerformTransition (GraphicsDevice graphicsDevice)
		{
			graphicsDevice.SetRenderTarget (renderTarget);

			if (outboxData == null || outboxTexture == null) {
				outboxData = new Color [base.Source.Width * base.Source.Height];
				outboxTexture = new Texture2D (base.GraphicsDevice, base.Source.Width, base.Source.Height);
			}

			//
			// Alt Ausblenden:
			//
			if (this.boxMode == BoxMode.BlendOut) {
				base.Source.GetData (outboxData);
				for (int v = 0; v < fadeAmount * base.Source.Height * base.Source.Width; v++) {
					outboxData [v] = Color.Black;
					outboxData [base.Source.Height * base.Source.Width - v - 1] = Color.Black;
				}
				for (int h = 0; h < base.Source.Height; h++) {
					for (int v = 0; v < base.Source.Width; v++) {
						if (v < fadeAmount / 2 * base.Source.Width) {
							outboxData [base.Source.Height * base.Source.Width - 1 - (v + h * base.Source.Width)] = Color.Black;
							outboxData [v + h * base.Source.Width] = Color.Black;
						}
					}
				}
			} else {
				//
				// Neu einblenden:
				//
				outboxData = Enumerable.Repeat (Color.Black, base.Destination.Height * base.Destination.Width).ToArray ();
				Color[] sceneContent = new Color[base.Destination.Height * base.Destination.Width];
				base.Destination.GetData (sceneContent);
				for (int v = 0; v < fadeAmount * base.Source.Height * base.Source.Width; v++) {
					outboxData [v] = sceneContent [v];

					int lowerIndex = base.Source.Height * base.Source.Width - v - 1;
					outboxData [lowerIndex] = sceneContent [lowerIndex];
				}
				for (int h = 0; h < base.Source.Height; h++) {
					for (int v = 0; v < base.Source.Width; v++) {
						if (v < fadeAmount / 2 * base.Source.Width) {
							int leftIndex = base.Source.Height * base.Source.Width - 1 - (v + h * base.Source.Width);
							int rightIndex = v + h * base.Source.Width;
							outboxData [leftIndex] = sceneContent [leftIndex];
							outboxData [rightIndex] = sceneContent [rightIndex];
						}
					}
				}
			}


					//outboxData [base.Source.Width * base.Source.Width - 1 - (i + k * base.Source.Width - 1)] = Color.White;


			this.outboxTexture.SetData (outboxData);

			base.SpriteBatch.Begin (SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			base.SpriteBatch.Draw (this.outboxTexture, Vector2.Zero);
			base.SpriteBatch.End ();
			graphicsDevice.SetRenderTarget (null);

			renderTarget.GetData (outboxData);
			return outboxData;
		}
			
		public override void OnUpdate (GameTime gameTime)
		{
			//Console.WriteLine ("El " + gameTime.TotalGameTime.TotalMilliseconds);

			if (fadeAmount >= 0.5f && boxMode == BoxMode.BlendOut) {
				boxMode = BoxMode.BlendIn;
				fadeAmount = 0.0f;
			}

			//if (gameTime.TotalGameTime.TotalMilliseconds % 100 == 0) {
				fadeAmount += 0.025f;//;this.transitionFunction.Invoke(gameTime.ElapsedGameTime.Milliseconds);
			//}

			fadeAmount = Math.Min (fadeAmount, 1.0f);


		}

		#endregion
	}
}


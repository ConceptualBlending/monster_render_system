using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Hoax.Engine.Graphics2D;

namespace Hoax.Engine.Common
{
	public static class SpriteBatchExtensions {

		public static void Draw(this SpriteBatch spriteBatch, Canvas canvas, Transformation2D transformation2D) 
		{
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
		}

	}
}


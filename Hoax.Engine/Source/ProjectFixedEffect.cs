using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hoax.Engine.Graphics2D
{
	class ProjectFixedEffect : BasicEffect
	{
		public ProjectFixedEffect(GraphicsDevice graphicsDevice, int width, int height) : base (graphicsDevice)
		{
			this.VertexColorEnabled = true;

			this.World = Matrix.CreateTranslation (0, 0, 0);
			this.View = Matrix.CreateLookAt (
				new Vector3 (0.0f, 0.0f, 1.0f),
				Vector3.Zero,
				Vector3.Up
			);
			this.Projection = Matrix.CreateOrthographicOffCenter (
				0,
				width + 1,
				height + 1,
				0,
				1.0f, 1000.0f);
		}
	}


	
}


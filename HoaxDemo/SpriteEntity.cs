using System;
using Array2D.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Array2DDemo
{
	public class SpriteEntity : Entity
	{
		Texture2D texture;

		public SpriteEntity (Game game, int i) : base(game, "Logo" + i)
		{
		}

		public override void Initialize ()
		{
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			base.LoadContent ();
			texture = Game.Content.Load<Texture2D> ("logo.png");
			base.updateTexture (texture);
		}

		protected override void UnloadContent ()
		{
			base.UnloadContent ();
			texture.Dispose ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
		}
			

	}
}


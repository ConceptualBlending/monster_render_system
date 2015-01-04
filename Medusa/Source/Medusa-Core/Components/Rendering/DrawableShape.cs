using System;
using Microsoft.Xna.Framework.Graphics;
using Hoax.Engine.Graphics2D;
using Microsoft.Xna.Framework;

namespace MedusaCore
{
	public class Shape
	{
		public class DrawableShape : Drawable
		{
			public Texture2D Texture { get; private set; }

			public DrawableShape (string identifier, Texture2D texture) : base (identifier)
			{
				Texture = texture;
			}

			#region implemented abstract members of Entity

			protected override void OnTransformationChanged (Matrix transformation2D)
			{

			}

			#endregion

			public override void LoadContent ()
			{

			}
		}

		public class DrawableConnectionPoint : Drawable
		{
			public Texture2D Texture { get; private set; }

			public DrawableConnectionPoint (Game game, string identifier) : base (identifier)
			{
				Texture = game.Content.Load<Texture2D>("connectionpoint.png");
			}

			#region implemented abstract members of Entity

			protected override void OnTransformationChanged (Matrix transformation2D)
			{

			}

			#endregion

			public override void LoadContent ()
			{

			}
		}
	}
}


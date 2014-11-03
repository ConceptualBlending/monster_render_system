using System;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;

namespace Hoax.Framework.Components.Graphics2D
{
	public class Sprite : Drawable
	{
		public Texture2D Texture { get; private set; }
		public string AssetName { get; private set; }

		public Sprite (string identifier, string assetName) : base (identifier)
		{
			AssetName = assetName;
		}

		public override void LoadContent ()
		{
			OnLoadContentStarts (new ReferenceResourceEventArgs (Identifier, AssetName));
			Texture = Game.Content.Load<Texture2D> (AssetName);
			OnLoadContentEnds (new ReferenceEventArg (Identifier));
			base.LoadContent ();
		}
	}
}


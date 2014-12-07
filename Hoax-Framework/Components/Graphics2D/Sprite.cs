using System;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;

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

		#region implemented abstract members of Entity

		protected override void OnTransformationChanged (Matrix transformation2D)
		{

		}

		#endregion

		public override void LoadContent ()
		{
			OnLoadContentStarts (new ReferenceResourceEventArgs (Identifier, AssetName));
			Texture = Game.Content.Load<Texture2D> (AssetName);
			OnLoadContentEnds (new ReferenceEventArg (Identifier));
			base.LoadContent ();
		}
	}
}


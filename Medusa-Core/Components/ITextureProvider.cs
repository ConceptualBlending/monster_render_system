using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public interface ITextureProvider
	{
		Texture2D GetTexture();
		string GetTextualPresentation();
	}

	public class InternalRessourceTextureProvider : ITextureProvider
	{
		public string AssetPath { get; private set; }
		private Game game;

		public InternalRessourceTextureProvider(Game game, string assetPath)
		{
			AssetPath = assetPath;
			this.game = game;
		}

		#region ITextureProvider implementation

		public Texture2D GetTexture ()
		{
			Texture2D tex = game.Content.Load<Texture2D> (AssetPath);
			var result = new Texture2D (game.GraphicsDevice, tex.Width, tex.Height);
			Color[] data = new Color[tex.Width * tex.Height];
			tex.GetData (data);
			result.SetData (data);
			return result;
		}

		public string GetTextualPresentation ()
		{
			return "res://" + AssetPath;
		}

		#endregion


	}

}


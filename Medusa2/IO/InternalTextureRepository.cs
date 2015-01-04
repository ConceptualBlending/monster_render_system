using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Medusa2
{
	public class InternalTextureRepository : ITextureRepository
	{
		Game game;

		public InternalTextureRepository (Game game)
		{
			this.game = game;
		}

		#region ITextureRepository implementation

		public Texture2D GetTexture (string assetName)
		{
			return game.Content.Load<Texture2D> (assetName);
		}

		#endregion
	}
}


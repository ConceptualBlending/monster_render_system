using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO
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


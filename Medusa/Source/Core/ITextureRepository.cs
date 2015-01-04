using System;
using Microsoft.Xna.Framework.Graphics;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public interface ITextureRepository
	{
		Texture2D GetTexture(string assetName);
	}
}


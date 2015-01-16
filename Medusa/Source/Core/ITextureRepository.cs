using System;
using System.Drawing;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public interface ITextureRepository
	{
		Bitmap GetTexture(string assetName);
	}
}


using System;
using Microsoft.Xna.Framework.Graphics;

namespace Medusa2
{
	public interface ITextureRepository
	{
		Texture2D GetTexture(string assetName);
	}
}


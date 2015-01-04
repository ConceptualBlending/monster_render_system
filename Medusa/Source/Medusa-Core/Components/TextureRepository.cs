using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class TextureRepository
	{
		private Dictionary<string, Texture2D> availableTextures;  
		private Dictionary<string, ITextureProvider> textureProvider;

		public TextureRepository ()
		{
			availableTextures = new Dictionary<string, Texture2D> ();
			textureProvider = new Dictionary<string, ITextureProvider> ();
		}

		public void AddSource(string shapeName, ITextureProvider provider)
		{
			checkNameIsUnique (shapeName, provider);
			textureProvider [shapeName] = provider;
		}

		public Texture2D this[string shapeName]
		{
			get {
				checkShapeNameTextureProviderIsKnown (shapeName);
				if (!availableTextures.ContainsKey (shapeName))
					availableTextures [shapeName] = textureProvider [shapeName].GetTexture ();
				return textureProvider [shapeName].GetTexture();
			}
		}

		void checkNameIsUnique (string shapeName, ITextureProvider provider)
		{
			if (textureProvider.ContainsKey (shapeName)) {
				var msg = string.Format ("Shape repository declares source for shape {0} at least twice. " +
					"This is inconsitent. Once it should be loaded from {1} and from {2}.", shapeName, 
					textureProvider[shapeName].GetTextualPresentation(), provider.ToString());
				throw new Exception (msg);
			}
		}

		void checkShapeNameTextureProviderIsKnown (string shapeName)
		{
			if (!textureProvider.ContainsKey (shapeName)) {
				var msg = string.Format ("There is no source defined for shape \"{0}\"", shapeName );
				throw new Exception (msg);
			}
		}
	}
}


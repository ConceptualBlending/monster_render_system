using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO
{ 
	public class FileSystemTextureRepository : ITextureRepository
	{
		private string path;
		private GraphicsDevice graphicsDevice;

		public FileSystemTextureRepository(string path, GraphicsDevice graphicsDevice)
		{
			this.path = (path.EndsWith("/") || path.EndsWith("\\") ? path : path + "/").Replace("\\","/");
			this.graphicsDevice = graphicsDevice;
		}

		#region ITextureRepository implementation
		public Texture2D GetTexture (string assetName)
		{
			FileStream filestream = new FileStream(path + assetName, FileMode.CreateNew);
			return Texture2D.FromStream(graphicsDevice, filestream);
		}
		#endregion
	}
}


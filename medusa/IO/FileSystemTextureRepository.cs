using System;
using System.IO;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils;
using System.Drawing;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO
{ 
	public class FileSystemTextureRepository : ITextureRepository
	{
		private string path;

		public FileSystemTextureRepository(string path)
		{
			this.path = (path.EndsWith("/") || path.EndsWith("\\") ? path : path + "/").Replace("\\","/");
		}

		#region ITextureRepository implementation
		public Bitmap GetTexture (string assetName)
		{
			var filename = path + assetName;
			return (Bitmap) Bitmap.FromFile(filename);
		}
		#endregion
	}
}


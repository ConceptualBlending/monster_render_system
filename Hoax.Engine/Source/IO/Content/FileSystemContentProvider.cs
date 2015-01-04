using System;
using System.IO;
using System.Collections.Generic;

namespace HoaxFramework
{
	public class FileSystemContentProvider : IContentProvider
	{
		public class RootDirectoryNotFoundException : Exception 
		{
			public RootDirectoryNotFoundException(string path) : base(path) { }
		}

		public class RootDirectoryNullOrEmptyException : Exception { }

		public class AssetNotFoundException : Exception 
		{
			public AssetNotFoundException(string rootPath, string assetName) : base("asset=" + assetName + " (root=" + rootPath + ")") { }
		}

		private string rootPath;

		public string RootPath 
		{
			get 
			{
				return rootPath;
			}

			set 
			{
				if (string.IsNullOrEmpty (value))
					throw new RootDirectoryNullOrEmptyException ();
				if (!Directory.Exists(value))
					throw new RootDirectoryNotFoundException (value);
				this.rootPath = (value.EndsWith ("/") || value.EndsWith (@"\")) ? value : value + "/";
			}
		}

		public FileSystemContentProvider(string rootPath) {
			this.RootPath = rootPath;
		}

		#region IContentProvider implementation

		public Stream OpenStream (string assetName)
		{
			string filepath = this.rootPath + assetName.ToLower ().Trim ();

			if (!File.Exists (filepath))
				throw new AssetNotFoundException (this.rootPath, filepath);

			return new FileStream (filepath, FileMode.Open);
		}

		public bool ContainsAsset (string assetName)
		{
			string asset = assetName.ToLower ().Trim ();

			return File.Exists (this.rootPath + asset);
		}

		public IEnumerable<string> ListAssets ()
		{
			return ListFiles (this.rootPath);
		}

		#endregion

		private IEnumerable<string> ListFiles(string path) {
			Queue<string> queue = new Queue<string>();
			queue.Enqueue(path);
			while (queue.Count > 0) {
				path = queue.Dequeue();
				foreach (var subDirectory in Directory.GetDirectories(path)) {
						queue.Enqueue(subDirectory);
					}
				string[] files = Directory.GetFiles(path);

				if (files != null) {
					for(int i = 0 ; i < files.Length ; i++) {
						yield return files[i].Substring(this.rootPath.Length);
					}
				}
			}
		} 
	}
}


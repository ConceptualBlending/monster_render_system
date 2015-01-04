using System;
using HoaxFramework;
using System.IO;

namespace ContentLoader
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string assetRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Assets";

			Console.WriteLine ("Asset root dir=" + assetRoot);

			IContentProvider provider = new FileSystemContentProvider (assetRoot);

			foreach (var asset in provider.ListAssets ()) {
				var reader = new StreamReader (provider.OpenStream (asset));
				var content = reader.ReadToEnd ();
				Console.WriteLine ("Asset=" + asset + ", content=" + content);
			}
				
		}
	}
}

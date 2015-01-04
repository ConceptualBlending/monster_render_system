using System;
using System.IO;
using System.Collections.Generic;

namespace HoaxFramework
{
	public interface IContentProvider
	{
		Stream OpenStream(string assetName);

		bool ContainsAsset(string assetName);

		IEnumerable<string> ListAssets();
	}
}


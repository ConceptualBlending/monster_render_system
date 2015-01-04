using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HoaxFramework
{
	public sealed class ContentLoader : IDisposable
	{
		public class AssetRawStreamNotFoundException : FileNotFoundException 
		{
			public AssetRawStreamNotFoundException (string assetName) : base (assetName) { }
		}

		public class IllegalAssetNameEmptyOrNull : Exception 
		{
			public IllegalAssetNameEmptyOrNull (string assetName) : base (assetName) { }
		}

		public class ContentTypeNotSupportedException : Exception 
		{
			public ContentTypeNotSupportedException (string assetName) : base (assetName) { }
		}

		private List<IContentProvider> contentProviders;
		private Dictionary<string, object> loadedAssets;
		private GraphicsDevice graphicsDevice;

		public ContentLoader(GraphicsDevice graphicsDevice)
		{
			this.contentProviders = new List<IContentProvider> ();
			this.loadedAssets = new Dictionary<string, object> ();
			this.RegisterProvider (new FileSystemContentProvider (GameService.Instance.Configuration.Content.AssetRootFallback));
			this.graphicsDevice = graphicsDevice;
		}

		public void RegisterProvider(IContentProvider provider)
		{
			this.contentProviders.Add (provider);
		}

		public ContentType Load<ContentType> (string assetName)
		{
			Trace.WriteLine("ContentLoader load \"" + assetName + "\"");

			var assetKeyName = assetName.ToLower ().Trim (); 

			object retval = null;

			if (this.loadedAssets.TryGetValue (assetKeyName, out retval) && retval is ContentType) {
				Trace.WriteLine("Return \"" + assetName + "\" from cache");
				return (ContentType)retval;
			}
			else {
				Trace.WriteLine("Read \"" + assetName + "\" from stream");

				Stream rawData = this.Read (assetName);

				ContentType typedData = this.ReadAsType<ContentType> (assetName, rawData);

				this.loadedAssets [assetKeyName] = typedData;
				return typedData;
			}
		}

		public Stream Read (string assetName) 
		{
			foreach (var provider in this.contentProviders) {
				if (provider.ContainsAsset (assetName)) {
					return provider.OpenStream (assetName);
				}
			}

			throw new AssetRawStreamNotFoundException (assetName);
		}

		private ContentType ReadAsType<ContentType> (string assetName, Stream data)
		{
			if (typeof(ContentType) == typeof(Texture2D)) {
				Trace.WriteLine("Read \"" + assetName + "\" as Texture2D");

				if (this.graphicsDevice == null)
					throw new InvalidOperationException ("No Graphics Device");
				else return (ContentType) ((object) Texture2D.FromStream (this.graphicsDevice, data));
			}

			Trace.Fail("FATAL: \"" + assetName + "\" type is not supported!");

			throw new ContentTypeNotSupportedException (assetName);
		}

		private void checkAssetName (string name) 
		{
			if (string.IsNullOrEmpty (name))
				throw new IllegalAssetNameEmptyOrNull (name);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			throw new NotImplementedException ();
		}

		#endregion


	}
}


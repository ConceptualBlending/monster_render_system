using System;
using Newtonsoft.Json;

namespace HoaxFramework
{
	public class Configuration
	{
		public class ContentConfiguration {

			public string AssetRootFallback {
				get;
				set;
			}

			public ContentConfiguration()
			{
				this.AssetRootFallback = "";
			}
		}

		public ContentConfiguration Content {
			get;
			set;
		}

		public Configuration() {
			this.Content = new ContentConfiguration();
		}

		public string Serialize() 
		{
			return JsonConvert.SerializeObject (this, Formatting.Indented);
		}

		public static Configuration Deserialize(string jsonString)
		{
			var retval = JsonConvert.DeserializeObject<Configuration> (jsonString);
			return retval;
		}
	}
}


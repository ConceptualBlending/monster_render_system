using System;
using HoaxFramework.CaptainCash.Core;
using System.IO;
using System.Windows.Forms;

namespace HoaxFramework
{
	public class GameService
	{
		public static readonly string ConfigurationFile = "Configuration/ContentConf.json";

		public class GameServiceConfigurationFileNotFound : FileNotFoundException { }

		#region Thread-Safe Singleton
		private static volatile GameService instance;
		private static object syncRoot = new Object();

		private GameService() {
			this.Initialized = false;
		}

		public bool Initialized {
			get;
			private set;
		}

		public void Initialize()
		{
			this.Initialized = true;

			if (!File.Exists (GameService.ConfigurationFile))
				throw new GameServiceConfigurationFileNotFound ();

			this.Configuration = Configuration.Deserialize (File.ReadAllText (GameService.ConfigurationFile));
		}

		public static GameService Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new GameService();
					}
				}

				return instance;
			}
		}
		#endregion

		public GameWindow GameWindow {
			get;
			internal set;
		}

		public Configuration Configuration {
			get;
			set;
		}
	}
}


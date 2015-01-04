using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Medusa2
{
	public class Repository
	{
		public class Vec2 {

			public int x {
				get;
				set;
			}
			public int y {
				get;
				set;
			}

			public Vec2(int x, int y) {
				this.x = x;
				this.y = y;
			}
		}

		public class TypeItem {

			public string Type {
				get;
				set;
			}

			public string ImageFile {
				get;
				set;
			}
				
			public Dictionary<string, Vec2> Points {
				get;
				set;
			}

			public TypeItem(string typeName, string imageFile) 
			{
				this.Type = typeName;
				this.ImageFile = imageFile;
				this.Points = new Dictionary<string, Vec2>();
			}
		}

		public List<TypeItem> RepositoryContent {
			get;
			set;
		}

		public string RepositoryName {
			get;
			set;
		}

		public string RepositoryDescription {
			get;
			set;
		}

		public int Version {
			get;
			set;
		}

		public string MedusaFormatToken = "REP_TYPE_FORMAT_1";

		public class Autor {

			public string Name {
				get;
				set;
			}

			public string EMail {
				get;
				set;
			}

			public Autor(string name, string eMail) {
				this.Name = name;
				this.EMail = eMail;
			}
		}

		public List<Autor> Autors {
			get;
			set;
		}

		public Repository(string repositoryName, string repositoryDescription, int version) 
		{
			this.RepositoryName = repositoryName;
			this.RepositoryDescription = repositoryDescription;
			this.Version = version;
			this.RepositoryContent = new List<TypeItem> ();
			this.Autors = new List<Autor> ();
		}

		public string Serialize() 
		{
			return JsonConvert.SerializeObject (this, Formatting.Indented);
		}

		public string RootDirectory {
			get;
			set;
		}

		public bool ShouldSerializeRootDirectory() {
			return false;
		}

		public static Repository Deserialize(string rootDirectory, string jsonString)
		{
			var retval = JsonConvert.DeserializeObject<Repository> (jsonString);
			retval.RootDirectory = rootDirectory + "/";
			return retval;
		}
	}
}


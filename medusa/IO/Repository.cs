using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO
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

			public override string ToString ()
			{
				return string.Format ("[Vec2: x={0}, y={1}]", x, y);
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

			public override string ToString ()
			{
				return string.Format ("[TypeItem: Type={0}, ImageFile={1}, Points={2}]", Type, ImageFile, Points);
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

		public override string ToString ()
		{
			return string.Format ("[Repository: RepositoryContent={0}, RepositoryName={1}, RepositoryDescription={2}, Version={3}, Autors={4}, RootDirectory={5}]", RepositoryContent, RepositoryName, RepositoryDescription, Version, Autors, RootDirectory);
		}
	}
}


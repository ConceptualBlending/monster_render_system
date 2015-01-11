using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO
{
	public class MonsterMarkup
	{
		public List<Individual> Definitions {
			get;
			set;
		}

		public MonsterMarkup() {
			Definitions = new List<Individual> ();
			Relations = new List<Relation> ();
		}

		public class Individual {

			public string Identifier {
				get;
				set;
			}

			public string Type {
				get;
				set;
			}

			public Individual(string identifier, string type) 
			{
				this.Identifier = identifier;
				this.Type = type;
			}

			public override string ToString ()
			{
				return string.Format ("[Individual: Identifier={0}, Type={1}]", Identifier, Type);
			}
		}

		public List<Relation> Relations {
			get;
			set;
		}

		public class Relation {

			public string Individual1 {
				get;
				set;
			}

			public string Point1 {
				get;
				set;
			}

			public string Individual2 {
				get;
				set;
			}

			public string Point2 {
				get;
				set;
			}

			public Relation(string individual1, string point1, string individual2, string point2) {
				this.Individual1 = individual1;
				this.Individual2 = individual2;
				this.Point1 = point1;
				this.Point2 = point2;
			}

			public override string ToString ()
			{
				return string.Format ("[Relation: Individual1={0}, Point1={1}, Individual2={2}, Point2={3}]", Individual1, Point1, Individual2, Point2);
			}
		}

		public string Serialize() 
		{
			return JsonConvert.SerializeObject (this, Formatting.Indented);
		}

		public static MonsterMarkup Deserialize(string jsonString)
		{
			return JsonConvert.DeserializeObject<MonsterMarkup> (jsonString);
		}

		public override string ToString ()
		{
			return string.Format ("[MonsterMarkup: Definitions={0}, Relations={1}]", Definitions, Relations);
		}
	}
}


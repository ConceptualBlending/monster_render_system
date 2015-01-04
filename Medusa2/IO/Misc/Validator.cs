using System;
using System.Linq;
using System.IO;

namespace Medusa2
{
	public class Validator
	{
		Repository repository;
		MonsterMarkup markup;

		public Validator (Repository repository, MonsterMarkup markup)
		{
			this.repository = repository;
			this.markup = markup;
		}

		public void validate()
		{
			checkRepository ();
			checkMarkup ();
		}

		void checkRepository ()
		{
			if (repository == null)
				throwException ("Internal error. Reference to repository is null");
			else {
				if (repository.RepositoryName == null || repository.RepositoryName.Trim().Length == 0)
					throwException ("Repository doesn't have a name.");
				if (repository.RepositoryContent == null || repository.RepositoryContent.Count == 0)
					throwException ("Repository \"{0}\" doesn't contain any content", repository.RepositoryName);
				foreach (var content in repository.RepositoryContent) {
					if (content.Type == null || content.Type.Trim ().Length == 0)
						throwException ("Repository contains at least one type which defintion is empty or illegal");
					if (content.Points == null || content.Points.Count == 0)
						throwException ("Type \"{0}\" contains no points or there was a format error", content.Type);
					if (!ExistsAndReadable(repository.RootDirectory + content.ImageFile))
						throwException ("Type \"{0}\" image file \"{1}\" doesn't exists or is not readable.", content.Type, repository.RootDirectory + content.ImageFile);
					foreach (var point in content.Points) {
						if (point.Key == null || point.Key.Trim ().Length == 0)
							throwException ("Type \"{0}\" contains a point which name is unset", content.Type);
						if (point.Value == null)
							throwException ("Point \"{0}\" in type \"{1}\" contains no vector information", point.Key, content.Type);
						if (point.Value.x < 0)
							throwException ("First component of point \"{0}\" in type \"{1}\" is negative", point.Key, content.Type);
						if (point.Value.y < 0)
							throwException ("Second component of point \"{0}\" in type \"{1}\" is negative", point.Key, content.Type);
					}

					var countOfTypeDefs = (from c in repository.RepositoryContent where c.Type == content.Type select c).Count();
					if (countOfTypeDefs != 1)
						throwException ("Type \"{0}\" is multiple defined in repository", content.Type);
				}
			}
		}

		void checkMarkup ()
		{
			if (markup == null)
				throwException ("Internal error. Reference to markup is null");
			else {
				if (markup.Definitions == null || markup.Definitions.Count == 0)
					throwException ("Markup does not contain any definitions");
				foreach (var def in markup.Definitions) {
					if (def.Identifier == null || def.Identifier.Trim().Length == 0)
						throwException ("Markup contains a definition which identifier is empty or unset");
					if (def.Type == null || def.Type.Trim ().Length == 0)
						throwException ("Type definition for \"{0}\" contains no valid type. It is empty or unset.", def.Identifier);
					var typeCountInRep = (from t in repository.RepositoryContent where t.Type == def.Type select t).Count ();
					if (typeCountInRep != 1)
						throwException ("Markup contains individual \"{0}\" which type \"{1}\" is not defined in repository \"{2}\"", def.Identifier, def.Type, repository.RepositoryName);
				}
				foreach (var relations in markup.Relations) {
					if (relations.Individual1 == null || relations.Individual1.Trim ().Length == 0)
						throwException ("Relation in markup contains a tuple which first individual is unset.");
					if (relations.Individual2 == null || relations.Individual2.Trim ().Length == 0)
						throwException ("Relation in markup contains a tuple which second individual is unset.");
					if (relations.Individual1 == relations.Individual2)
						throwException ("Circle relation for \"{0}\" detected.", relations.Individual1);
					if (relations.Point1 == null || relations.Point1.Trim().Length == 0)
						throwException ("Relation in markup contains a tuple which first point is unset.");
					if (relations.Point2 == null || relations.Point2.Trim().Length == 0)
						throwException ("Relation in markup contains a tuple which first point is unset.");

					var defCountIndividual1 = (from typeDefs in markup.Definitions where typeDefs.Identifier == relations.Individual1 select typeDefs).Count();
					var defCountIndividual2 = (from typeDefs in markup.Definitions where typeDefs.Identifier == relations.Individual2 select typeDefs).Count();

					if (defCountIndividual1 == 0)
						throwException ("Relation in markup constains individual \"{0}\" which is not defined.", relations.Individual1);
					if (defCountIndividual2 == 0)
						throwException ("Relation in markup constains individual \"{0}\" which is not defined.", relations.Individual2);

					if (defCountIndividual1 > 1)
						throwException ("Multiple definitions for individual \"{0}\" detected.", relations.Individual1);
					if (defCountIndividual2 > 1)
						throwException ("Multiple definitions for individual \"{0}\" detected.", relations.Individual2);
				}
			}
		}

		void throwException(string message, params object[] args) 
		{
			var fmsg = string.Format(message, args);
				throw new Exception (fmsg);
		}

		bool ExistsAndReadable(string filename) 
		{
			try
			{
				File.Open(filename, FileMode.Open, FileAccess.Read).Dispose();
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}
	}
}


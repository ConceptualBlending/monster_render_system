using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;
using System.IO;
using System;
using System.Collections.Generic;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Utils;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa
{
	public class MedusaRenderer 
	{
		Universe universe;
		Repository repository;
		MonsterMarkup markup;

		public MedusaRenderer (Repository repository, MonsterMarkup markup, string outputFileName)
		{
			this.repository = repository;
			this.markup = markup;
		}

		public Bitmap Run() 
		{
			buildUniverse ();
			return universe.Draw ();
		}

		void buildUniverse() 
		{
			universe = new Universe ();

			foreach (var relation in markup.Relations) {
				ImportIntoUniverse (relation.Individual1);
				ImportIntoUniverse (relation.Individual2);
				universe.Connect (relation.Individual1, relation.Point1, relation.Individual2, relation.Point2);
			}
		}

		public Bitmap GetTexture (string fileName)
		{
			return (Bitmap) Bitmap.FromFile (fileName);
		}

		void ImportIntoUniverse(string individualName) {
			string textureFileName = null;
			var referencePoints = new List<ReferencePoint> ();

			foreach (var def in markup.Definitions) {
				if (def.Identifier == individualName) {
					foreach (var type in repository.RepositoryContent) {
						if (type.Type == def.Type) {
							textureFileName = type.ImageFile;

							foreach (var point in type.Points)
								referencePoints.Add (new ReferencePoint (point.Key, new Vector2 (point.Value.x, point.Value.y), ReferencePoint.PointType.A));
							break;
						}
					}
					break;
				}
			}

			var texture = GetTexture (repository.RootDirectory + textureFileName);
			if (!universe.Contains(individualName))
				universe.Add (new ReferencePointContainer (individualName, texture, referencePoints.ToArray ()));
		}
	}
}

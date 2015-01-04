using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class ConnectionPointRegister
	{
		private Dictionary<string, Dictionary<string, Vector2>> register;

		public ConnectionPointRegister ()
		{
			register = new Dictionary<string, Dictionary<string, Vector2>> ();
		}

		public void AddConnectionPoints(string shapeName, Dictionary<string, Vector2> connectionPoints)
		{
			checkShapeNameIsUnique (shapeName);
			checkConnectionPoints (shapeName, connectionPoints);

			if (!register.ContainsKey (shapeName))
				register [shapeName] = new Dictionary<string, Vector2>();
			foreach (var connectionPointIdentifier in connectionPoints.Keys)
				register [shapeName].Add(connectionPointIdentifier, connectionPoints[connectionPointIdentifier]);
		}

		public Dictionary<string, Vector2> this[string shapeName]
		{
			get {
				checkShapeNameExists (shapeName);
				return register [shapeName];
			}
		}

		void checkShapeNameIsUnique (string shapeName)
		{
			if (shapeName == null || shapeName.Trim().Length == 0) {
				var msg = "Not initialized or illegal name for shape identifier detected. It's not " +
					"allowed to use empty names.";
				throw new Exception (msg);
			}
		}

		void checkConnectionPoints (string shapeName, Dictionary<string, Vector2> connectionPoints)
		{
			if (connectionPoints == null || connectionPoints.Count == 0) {
				var msg = string.Format ("List of connections points for shape \"{0}\" have to be non-empty.",
					          shapeName);
				throw new Exception (msg);
			}
		}

		void checkShapeNameExists (string shapeName)
		{
			if (!register.ContainsKey (shapeName)) {
				var msg = string.Format("Unknown shape name \"{0}\" while defining connection points detected.", 
					shapeName);
				throw new Exception (msg);
			}
		}
	}
}


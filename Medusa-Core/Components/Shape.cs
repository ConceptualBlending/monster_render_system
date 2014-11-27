using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class Shape
	{
		public Texture2D Texture { get; private set; }

		public string Identifier { get; private set; }

		public Dictionary<string, Vector2> ConnectionsPoints;

		public Shape (string identifier, TextureRepository textureRepository, ConnectionPointRegister connectionPointRegister)
		{
			checkIdentifierFormat (identifier);
			checkTexture (textureRepository[identifier]);

			Identifier = identifier;
			Texture = textureRepository[identifier];
			ConnectionsPoints = new Dictionary<string, Vector2> ();

			foreach (var key in connectionPointRegister[identifier])
				DeclareConnectionPoint (key.Key, key.Value);
		}

		private void DeclareConnectionPoint(string connectionPoint, Vector2 point)
		{
			checkIdentifierisUnique (connectionPoint);
			checkPointInsideBounds (connectionPoint, point);
			ConnectionsPoints [connectionPoint] = point;
		}

		public Vector2 this[string connectionPointName]
		{
			get {
				return ConnectionsPoints[connectionPointName];
			}
		}

		void checkIdentifierFormat (string identifier)
		{
			if (identifier == null || identifier.Trim().Length == 0) {
				var msg = "Not initialized or illegal name for shape identifier detected. It's not " +
					"allowed to use empty names.";
				throw new Exception (msg);
			}
		}

		void checkTexture (Texture2D texture)
		{
			if (texture == null) {
				var msg = "Not initialized or illegal texture for shape detected. Check your shape " +
					"repository.";
				throw new Exception (msg);
			}
		}

		private void checkIdentifierisUnique (string identifier)
		{
			if (ConnectionsPoints.ContainsKey (identifier)) {
				var msg = string.Format ("It's not allowed to declare a connection point inside a " +
					"single shape more than once. Shape {0} declares connection point \"{1}\" at " +
					"least two times.", this.Identifier, identifier);
				throw new Exception (msg);
			}
		}

		private void checkPointInsideBounds (string identifier, Vector2 point)
		{
			if (point.X < 0 || point.X > Texture.Width || point.Y < 0 || point.Y > Texture.Height) {
				var msg = string.Format ("It's not allowed to declare a connection point outside the " +
					"bounds of shape. Connection point {0} in shape {1} (wdith={2}, height={3}) is " +
					"declared at point (x={4}, y={5}).", identifier, this.Identifier, Texture.Width, 
					Texture.Height, point.X, point.Y);
				throw new Exception (msg);
			}
		}
	}
}


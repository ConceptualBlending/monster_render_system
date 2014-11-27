using System;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class Individual
	{
		public string Identifier { get; private set; }
		public Shape Shape { get; private set; }

		public Individual (string name, Shape shape)
		{
			checkNameFormat (name);
			checkShape (name, shape);
			Identifier = name;
			Shape = shape;
		}

		public override bool Equals (object obj)
		{
			return obj.GetType() == typeof(Individual) ? this.Identifier.Equals (((Individual)obj).Identifier) : false;
		}

		void checkNameFormat (string name)
		{
			if (name == null || name.Trim().Length == 0) {
				var msg = "Not initialized or illegal name for individual identifier detected. It's not " +
					"allowed to use empty names.";
				throw new Exception (msg);
			}
		}

		void checkShape (string individualName, Shape shape)
		{
			if (shape == null) {
				var msg = string.Format ("Not initialized shape detected for individual \"{0}\". This is an " +
					"internal error.", individualName);
				throw new Exception (msg);
			}
		}
	}
}


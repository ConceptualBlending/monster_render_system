using System;
using ConceptualBlending.Tools.Medusa.Components;

namespace ConceptualBlending.Tools.Medusa.Components
{
	public class Statement
	{
		public Individual First { get; private set; }
		public string FirstPoint { get; private set; }
		public Individual Second { get; private set; }
		public string SecondPoint { get; private set; }

		public Statement (Individual first, string firstPoint, Individual second, string secondPoint)
		{
			First = first;
			FirstPoint = firstPoint;
			Second = second;
			SecondPoint = secondPoint;
		}
	}
}


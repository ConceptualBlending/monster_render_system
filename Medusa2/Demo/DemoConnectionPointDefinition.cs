using System;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;
using System.Collections.Generic;

namespace Medusa2
{
	public class DemoConnectionPointDefinition : IConnectionPointDefinition
	{
		Dictionary<string, ReferencePoint[]> dic = new Dictionary<string, ReferencePoint[]>();


		#region IConnectionPointDefinition implementation

		public ReferencePoint[] GetLocalPoints (string assetName)
		{
			return dic [assetName];
		}

		#endregion


	}
}


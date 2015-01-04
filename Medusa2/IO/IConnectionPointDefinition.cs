using System;
using Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core;

namespace Medusa2
{
	public interface IConnectionPointDefinition
	{
		ReferencePoint[] GetLocalPoints(string assetName);
	}
}


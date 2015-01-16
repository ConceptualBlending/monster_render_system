using System;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public interface IConnectionPointDefinition
	{
		ReferencePoint[] GetLocalPoints(string assetName);
	}
}


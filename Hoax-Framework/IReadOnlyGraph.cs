using System;
using System.Collections.Generic;

namespace Hoax.Engine.Graphics2D
{
	public interface IReadOnlyGraph<Type>
	{
		Type Parent { get; }

		ICollection<Type> GetChildren ();
	}
}


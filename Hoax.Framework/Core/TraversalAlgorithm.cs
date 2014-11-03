using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hoax.Framework.Components.Graphics2D
{
	public abstract class TraversalAlgorithm<Type> : System.Collections.Generic.IEnumerable<Type> where Type : IReadOnlyGraph<Type>
	{
		public Type RootNode { get; set; }

		#region IEnumerable implementation
		public abstract IEnumerator<Type> GetEnumerator ();
		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
	}

}


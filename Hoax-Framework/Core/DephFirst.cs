using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hoax.Framework.Components.Graphics2D
{
	public class DephFirst<Type> : TraversalAlgorithm<Type>, System.Collections.Generic.IEnumerable<Type> where Type : IReadOnlyGraph<Type>
	{

		#region IEnumerable implementation

		public override System.Collections.Generic.IEnumerator<Type> GetEnumerator ()
		{
			visitNext.Push (base.RootNode);

			while (visitNext.Count > 0) {
				Type vertex = visitNext.Pop ();
				labeled.Add (vertex);

				foreach (var child in vertex.GetChildren()) {
					if (!labeled.Contains (child)) {
						visitNext.Push (child);
					}
				}

				yield return vertex;
			}
		}

		#endregion

		List<Type> labeled = new List<Type>();
		Stack<Type> visitNext = new Stack<Type>();
	}

	public class DephFirst : DephFirst<Entity>
	{

	}
}


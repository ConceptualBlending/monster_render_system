using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hoax.Framework.Components.Graphics2D
{
	public class BreadthFirst<Type> : TraversalAlgorithm<Type>, System.Collections.Generic.IEnumerable<Type> where Type : IReadOnlyGraph<Type>
	{
		#region IEnumerable implementation

		public override System.Collections.Generic.IEnumerator<Type> GetEnumerator ()
		{
			visitNext.Enqueue (base.RootNode);

			while (visitNext.Count > 0) {
				Type vertex = visitNext.Dequeue ();
				labeled.Add (vertex);

				foreach (var child in vertex.GetChildren()) {
					if (!labeled.Contains (child)) {
						visitNext.Enqueue (child);
					}
				}

				yield return vertex;
			}
		}

		#endregion

		List<Type> labeled = new List<Type>();
		Queue<Type> visitNext = new Queue<Type>();
	}

	public class BreadthFirst : BreadthFirst<Entity> 
	{

	}
}


using System;
using System.Linq;
using System.Collections.Generic;
using C5;

namespace Ovgu.ComputerScience.KnowledgeAndLanguageEngineering.Blending.Medusa.Core
{
	public class GroupService
	{
		#region Thread-Safe Singleton
		private static volatile GroupService instance;
		private static object syncRoot = new Object();

		private GroupService() {}

		public static GroupService Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new GroupService();
					}
				}

				return instance;
			}
		}
		#endregion

		private Dictionary<ReferencePointContainer, ArrayList<ReferencePointContainer>> groups = new Dictionary<ReferencePointContainer, ArrayList<ReferencePointContainer>>();

		//
		// Methods
		//
		public void Group(ReferencePointContainer a, ReferencePointContainer b) 
		{
			SingleGrouping (a, b);
			SingleGrouping (b, a);
		}

		public void Apply(ReferencePointContainer member, Action<ReferencePointContainer> action, List<ReferencePointContainer> aux = null) {
			if (aux == null)
				aux = new List<ReferencePointContainer> ();

			if (groups.ContainsKey (member)) {
				if (!aux.Contains (member)) {
					action.Invoke (member);
					aux.Add (member);
					foreach (var friend in groups[member])
						Apply (friend, action, aux);
				}
			} else {
				groups.Add (member, new ArrayList<ReferencePointContainer> ());
				action.Invoke (member);
			}
		}

		private void SingleGrouping(ReferencePointContainer a, ReferencePointContainer b) 
		{
			if (!groups.ContainsKey (a))
				groups.Add (a, new ArrayList<ReferencePointContainer> ());
			if (!groups [a].Contains(b))
				groups [a].Add (b);
		}
	}
}


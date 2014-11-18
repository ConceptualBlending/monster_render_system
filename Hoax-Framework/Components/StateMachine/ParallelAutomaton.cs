using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hoax.Framework.Components.StateMachine
{
	public class ParallelAutomaton<Node, Symbol> 
	{
		public ParallelAutomaton(Node[] startNodes, Node[] acceptNodes) 
		{
			Nodes = new List<Node> ();
			ActiveNodes = new List<Node> ();
			Symbols = new List<Symbol> ();
			Transitions = new Dictionary<Node, Dictionary<Symbol, List<Node>>> ();
			ParallelTransitions = new Dictionary<List<Node>, List<Node>> ();
			SyncTransitions = new Dictionary<List<Node>, Dictionary<Symbol, List<Node>>> ();

			StartNodes = new List<Node>();
			AcceptNodes = new List<Node> ();

			StartNodes.AddRange (startNodes);
			AcceptNodes.AddRange (acceptNodes);

			Reset ();
		}

		public List<Node> StartNodes { get; private set; }
		public List<Node> AcceptNodes { get; private set; }
		public List<Node> ActiveNodes { get; private set; }
		public List<Node> Nodes { get; private set; }
		public List<Symbol> Symbols { get; private set; }

		public Dictionary<Node, Dictionary<Symbol, List<Node>>> Transitions { get; private set; }
		public Dictionary<List<Node>, List<Node>> ParallelTransitions { get; private set; }
		public Dictionary<List<Node>, Dictionary<Symbol, List<Node>>> SyncTransitions { get; private set; }

		public ParallelAutomaton<Node, Symbol> AddTransition (Node fromNode, Symbol symbol, params Node[] toNodes) 
		{
			if (!Nodes.Contains(fromNode))
				Nodes.Add(fromNode);
			foreach (var node in toNodes)
				if (!Nodes.Contains(node))
					Nodes.Add(node);
			if (!Symbols.Contains (symbol))
				Symbols.Add (symbol);

			var paths = Transitions.ContainsKey (fromNode) ? Transitions [fromNode] : new Dictionary<Symbol, List<Node>> ();
			var transition = paths.ContainsKey (symbol) ? paths [symbol] : new List<Node> ();
			transition.AddRange (toNodes);
			paths.Add (symbol, transition);
			if (Transitions.ContainsKey (fromNode))
				Transitions.Remove (fromNode);
			Transitions.Add (fromNode, paths);

			return this;
		}

		public ParallelAutomaton<Node, Symbol> AddParallelTransition(List<Node> fromNodes, List<Node> toNodes)
		{
			foreach (var node in fromNodes)
				if (!Nodes.Contains(node))
					Nodes.Add(node);
			foreach (var node in toNodes)
				if (!Nodes.Contains(node))
					Nodes.Add(node);
					
			var transition = ParallelTransitions.ContainsKey (fromNodes) ? ParallelTransitions [fromNodes] : new List<Node> ();
			transition.AddRange (toNodes);
			if (ParallelTransitions.ContainsKey (fromNodes))
				ParallelTransitions.Remove (fromNodes);
			ParallelTransitions.Add (fromNodes, transition);

			return this;
		}

		public ParallelAutomaton<Node, Symbol> AddSynchronizeTransition(List<Node> fromNodes, Symbol symbol, List<Node> toNodes)
		{
			foreach (var node in fromNodes)
				if (!Nodes.Contains(node))
					Nodes.Add(node);
			foreach (var node in toNodes)
				if (!Nodes.Contains(node))
					Nodes.Add(node);
			if (!Symbols.Contains (symbol))
				Symbols.Add (symbol);

			var paths = SyncTransitions.ContainsKey (fromNodes) ? SyncTransitions [fromNodes] : new Dictionary<Symbol, List<Node>> ();
			var transition = paths.ContainsKey (symbol) ? paths [symbol] : new List<Node> ();
			transition.AddRange (toNodes);
			if (paths.ContainsKey (symbol))
				paths.Remove (symbol);
			paths.Add (symbol, transition);
			if (SyncTransitions.ContainsKey (fromNodes))
				SyncTransitions.Remove (fromNodes);
			SyncTransitions.Add (fromNodes, paths);

			return this;
		}

		void parallelStep ()
		{
			if (ParallelTransitions.ContainsKeyDeep<List<Node>, Node, List<Node>> (ActiveNodes)) {
				ActiveNodes = ParallelTransitions.GetDeep<List<Node>, Node, List<Node>>(ActiveNodes);
			} else {
				Debug.WriteLine ("Nö");
			}
		}

		public ParallelAutomaton<Node, Symbol> Consume(Symbol symbol)
		{
			parallelStep ();
			Debug.WriteLine (toString(ActiveNodes) + "\tread\t" + symbol);
			Debug.WriteLine ("\t\tParallel Steps" + ParallelTransitions.ToString());
			if (!Symbols.Contains (symbol))
				throw new IndexOutOfRangeException ("Symbol \"" + symbol + "\" is unkown");
			else {


				if (SyncTransitions.ContainsKeyDeep<List<Node>, Node, Dictionary<Symbol, List<Node>>> (ActiveNodes) && SyncTransitions.GetDeep<List<Node>, Node, Dictionary<Symbol, List<Node>>> (AcceptNodes).ContainsKey (symbol)) {
					ActiveNodes = SyncTransitions.GetDeep<List<Node>, Node, Dictionary<Symbol, List<Node>>>(AcceptNodes)[symbol];
				} else {
					List<Node> newActiveNodes = new List<Node> ();
					List<Node> removeActiveNodes = new List<Node> ();

					foreach (var activeNode in ActiveNodes) {
						if (Transitions.ContainsKey (activeNode)) {
							var path = Transitions [activeNode];
							if (path.ContainsKey (symbol)) {
								newActiveNodes.AddRange (path [symbol]);
								removeActiveNodes.Add (activeNode);
							}
						}
					}

					foreach (var oldNode in removeActiveNodes)
						ActiveNodes.Remove (oldNode);
					ActiveNodes.AddRange (newActiveNodes);
				}
			}

			Debug.WriteLine ("\t\t-->"  + toString(ActiveNodes) + "("+Accept()+")");
			return this;
		}

		private string toString(ICollection<Node> l) {
			string activeNodesString = "";
			foreach (var node in l)
				activeNodesString += " " + node;
			return activeNodesString;
		}

		public bool Accept()
		{
			parallelStep ();
			return ActiveNodes == AcceptNodes;
		}

		public ParallelAutomaton<Node, Symbol> Reset ()
		{
			ActiveNodes.Clear ();
			ActiveNodes.AddRange (StartNodes);
			return this;
		}

	}

	public static class Tools {

		public static bool ContainsKeyDeep<K,T, V> (this Dictionary<K,V> dic, K key) where K : List<T> 
		{
			foreach (var keyInDic in dic.Keys) {
				if (keyInDic.Count != key.Count)
					continue;
				else {
					bool equal = true;
					for (int i = 0; i < keyInDic.Count; i++) {
						if (!keyInDic [i].Equals (key [i])) {
							equal = false;
							break;
						}
					}
					if (equal)
						return true;
				}
			}
			return false;
		}

		public static V GetDeep<K,T, V> (this Dictionary<K,V> dic, K key) where K : List<T>
																		  where V : new()
		{
			foreach (var keyInDic in dic.Keys) {
				if (keyInDic.Count != key.Count)
					continue;
				else {
					bool equal = true;
					V value = new V();
					for (int i = 0; i < keyInDic.Count; i++) {
						if (!keyInDic [i].Equals (key [i])) {
							equal = false;
							value = dic[keyInDic];
							break;
						}
					}
					if (equal)
						return value;
				}
			}
			throw new IndexOutOfRangeException (key + " is not a key");
		}

	}

}


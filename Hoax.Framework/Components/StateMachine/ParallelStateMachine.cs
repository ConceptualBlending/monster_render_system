using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hoax.Framework.Components.StateMachine
{
	class UIntArrayEqualityComparer : IEqualityComparer<uint[]> 
	{
		#region IEqualityComparer implementation
		public bool Equals (uint[] x, uint[] y)
		{
			if (x.Length != y.Length)
				return false;
			else {
				for (int i = 0; i < y.Length; i++)
					if (x [i] != y [i])
						return false;
				return true;
			}
		}
		public int GetHashCode (uint[] obj)
		{
			return obj.GetHashCode ();
		}
		#endregion
	}

	public sealed class ParallelStateMachine
	{ 
		public const uint SYSTEM_SYMBOL_LAMBDA = 0;

		public ParallelStateMachine ()
		{
			NextStateId = 0;
			NextSymbolId = SYSTEM_SYMBOL_LAMBDA + 1;

			_StartStateIds = new HashSet<uint> ();
			_FinalStateIds = new HashSet<uint> ();
			_ActiveStateIds = new HashSet<uint> ();
			_transitions = new Dictionary<uint, Dictionary<uint, ISet<uint>>> ();
			_parallelTransitions = new Dictionary<uint, ISet<uint>> ();
			_syncTransitions = new Dictionary<uint[], uint> (new UIntArrayEqualityComparer());
		}
			
		#region States

		public uint GenerateState ()
		{
			return NextStateId++;
		}

		public uint NextStateId { get; private set; }

		#region Start States
		public void AddStartState (uint id)
		{
			CheckStateValidity (id);
			_StartStateIds.Add (id);
		}

		public void RemoveStartState (uint id)
		{
			CheckStateValidity (id);
			_StartStateIds.Remove (id);
		}

		public bool HasStartState (uint id)
		{
			CheckStateValidity (id);
			return _StartStateIds.Contains (id);
		}

		public uint[] StartStateIds { get { 
				uint[] result = new uint[_StartStateIds.Count];
				_StartStateIds.CopyTo (result, 0);
				return result; } 
		}
		#endregion

		#region Final States
		public void AddFinalState (uint id)
		{
			CheckStateValidity (id);
			_FinalStateIds.Add (id);
		}

		public void RemoveFinalState (uint id)
		{
			CheckStateValidity (id);
			_FinalStateIds.Remove (id);
		}

		public bool HasFinalState (uint id)
		{
			CheckStateValidity (id);
			return _FinalStateIds.Contains (id);
		}

		public uint[] FinalStateIds { get { 
				uint[] result = new uint[_FinalStateIds.Count];
				_FinalStateIds.CopyTo (result, 0);
				return result; } 
		}
		#endregion

		#endregion

		#region Symbols

		public uint GenerateSymbole ()
		{
			return NextSymbolId++;
		}

		public uint NextSymbolId { get; private set; }

		#endregion

		#region Transitions

		private Dictionary<uint, Dictionary<uint, ISet<uint>>> _transitions;

		public void RunSequence (uint sourceNode, uint symbol, uint destNode)
		{
			CheckStateValidity (sourceNode);
			CheckStateValidity (destNode);
			CheckSymbolValidity (symbol);

			if (!_transitions.ContainsKey (sourceNode))
				_transitions.Add (sourceNode, new Dictionary<uint, ISet<uint>> ());
			var symbolsToNodes = _transitions [sourceNode];
			if (!symbolsToNodes.ContainsKey (symbol))
				symbolsToNodes.Add (symbol, new HashSet<uint> ());

			_transitions[sourceNode][symbol].Add (destNode);
		}

		private Dictionary<uint, ISet<uint>> _parallelTransitions;

		public void RunParallel (uint sourceNode, params uint[] destNode)
		{
			checkDestNodes (destNode);
			CheckStateValidity (sourceNode);
			CheckStateValidity (destNode);

			if (!_parallelTransitions.ContainsKey (sourceNode))
				_parallelTransitions.Add (sourceNode, new HashSet<uint> ());
			foreach (var dn in destNode)
				_parallelTransitions[sourceNode].Add (dn);
		}

		private Dictionary<uint[], uint> _syncTransitions;

		public void Synchronize (uint destNode, params uint[] sourceNodes)
		{
			CheckStateValidity (sourceNodes);
			CheckStateValidity (destNode);

			if (_syncTransitions.ContainsKey (sourceNodes))
				throw new InvalidOperationException ("There is already a sync states for this sources.");
			else _syncTransitions.Add (sourceNodes, destNode);
		}

		#endregion

		#region Consuming

		public void Consume (uint symbol)
		{
			initializeActiveStates ();
			runParallelTransitions ();
			if (runSynTransitions (symbol) == ConsumeResult.SymbolConsumed)
				return;
			else
				runRegularTransitions (symbol);
		}

		void initializeActiveStates ()
		{
			if (_ActiveStateIds.Count == 0)
				_ActiveStateIds = _StartStateIds;
		}

		void runParallelTransitions ()
		{
			ISet <uint> additionalActiveStateIds = new HashSet<uint> ();
			ISet <uint> removeActiveStateIds = new HashSet<uint> ();

			foreach (var activeState in _ActiveStateIds)
				if (_parallelTransitions.ContainsKey (activeState)) {
					removeActiveStateIds.Add (activeState);
					additionalActiveStateIds.UnionWith (_parallelTransitions [activeState]);
				}

			if (additionalActiveStateIds.Count != 0) {
				ISet <uint> lastCurrentActiveStates = new HashSet<uint>(_ActiveStateIds);
				_ActiveStateIds.ExceptWith (removeActiveStateIds);
				_ActiveStateIds.UnionWith (additionalActiveStateIds);
				OnStateChanged (new StateEntryEventArgs (StateEntryEventArgs.TransitionType.Parallelization, lastCurrentActiveStates, _ActiveStateIds));
			}
		}

		ConsumeResult runSynTransitions (uint symbol)
		{
			ISet <uint> additionalActiveStateIds = new HashSet<uint> ();
			ISet <uint> removeActiveStateIds = new HashSet<uint> ();

			foreach (var statesToSync in _syncTransitions.Keys) {
				if (_ActiveStateIds.IsSupersetOf (statesToSync)) {
					removeActiveStateIds.UnionWith (statesToSync);
					additionalActiveStateIds.Add (_syncTransitions [statesToSync]);
				}
			}

			if (additionalActiveStateIds.Count == 0)
				return ConsumeResult.SymbolNotConsumed;
			else {
				ISet <uint> lastCurrentActiveStates = new HashSet<uint>(_ActiveStateIds);
				_ActiveStateIds.ExceptWith (removeActiveStateIds);
				_ActiveStateIds.UnionWith (additionalActiveStateIds);
				OnStateChanged (new StateEntryEventArgs (StateEntryEventArgs.TransitionType.Synchronization, lastCurrentActiveStates, _ActiveStateIds, symbol));

				return ConsumeResult.SymbolConsumed;
			}
		}

		void runRegularTransitions (uint symbol)
		{
			ISet <uint> additionalActiveStateIds = new HashSet<uint> ();
			ISet <uint> removeActiveStateIds = new HashSet<uint> ();

			foreach (var activeNode in _ActiveStateIds) {
				if (_transitions.ContainsKey (activeNode)) {
					var path = _transitions [activeNode];
					if (path.ContainsKey (symbol)) {
						additionalActiveStateIds.UnionWith (path [symbol]);
						removeActiveStateIds.Add (activeNode);
					}
				}
			}

			if (additionalActiveStateIds.Count != 0) {
				ISet <uint> lastCurrentActiveStates = new HashSet<uint>(_ActiveStateIds);
				_ActiveStateIds.ExceptWith (removeActiveStateIds);
				_ActiveStateIds.UnionWith (additionalActiveStateIds);
				OnStateChanged (new StateEntryEventArgs (StateEntryEventArgs.TransitionType.RegularTransition, lastCurrentActiveStates, _ActiveStateIds, symbol));
			}
		}

		#endregion

		#region Random

		void RunRandom () {
			throw new NotImplementedException ();
		}

		#endregion

		#region Priority

		void RunPriority () {
			throw new NotImplementedException ();
		}

		#endregion

		#region Skipping and Unskipping

		void MarkSkip () 
		{
			throw new NotImplementedException ();
		}

		void MarkUnskip () 
		{
			throw new NotImplementedException ();
		}

		public enum SkipType { SKIP, RUN }

		SkipType GetSkipType () 
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Others

		public void SelectState()
		{
			throw new NotImplementedException ();
		}

		public uint GetSelectedState()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Others 2

		public void Push()
		{
			throw new NotImplementedException ();
		}

		public uint Pop()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Events

		public delegate void StateChangedHandler(object sender, StateEntryEventArgs e);

		public event StateChangedHandler StateChanged;

		private void OnStateChanged (StateEntryEventArgs e)
		{
			if (StateChanged != null)
				StateChanged (this, e);
		}
		#endregion

		#region Fields

		private ISet<uint> _StartStateIds;
		private ISet<uint> _FinalStateIds;
		private ISet<uint> _ActiveStateIds;

		#endregion

		enum ConsumeResult {
			SymbolConsumed,

			SymbolNotConsumed,
 }

		private void CheckStateValidity (uint id)
		{
			if (id >= NextStateId)
				throw new IndexOutOfRangeException ("State ID " + id + " is out of range");
		}

		private void CheckStateValidity (uint[] ids)
		{
			foreach (var id in ids)
				CheckStateValidity (id);
		}

		private void CheckSymbolValidity (uint id)
		{
			if (id >= NextSymbolId)
				throw new IndexOutOfRangeException ("Symbol ID " + id + " is out of range");
		}


		void checkDestNodes (uint[] destNode)
		{
			if (destNode == null || destNode.Length == 0)
				throw new ArgumentException ("Destintation state have to be non-null and non-empty.");
			foreach (var node in destNode)
				CheckStateValidity (node);
		}


		
	}

	public sealed class StateEntryEventArgs : EventArgs 
	{
		public enum TransitionType { Synchronization, Parallelization, RegularTransition }

		public StateEntryEventArgs(TransitionType transitionType, ISet<uint> lastStates, ISet<uint> currentStates, uint symbol = ParallelStateMachine.SYSTEM_SYMBOL_LAMBDA)
		{
			LastStates = lastStates;
			CurrentStates = currentStates;
			Symbol = symbol;
			Type = transitionType;
		}

		public ISet<uint> LastStates { get; private set; }
		public uint Symbol { get; private set; }
		public ISet<uint> CurrentStates { get; private set; }
		public TransitionType Type  { get; private set; }
	}
}


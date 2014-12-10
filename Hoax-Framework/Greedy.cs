using System;
using System.Collections.Generic;

namespace Hoax.Optimization
{
	/// <summary>
	/// Greedy optimizer.
	/// </summary>
	/// <code>
	/// var optimizer = new GreedyOptimizer<int> (
	/// 	isFeasibleSolution : (solutionSet) => Sum(solutionSet) <= 367,
	/// 	isSatisfyingSolution : (solutionSet) => Sum(solutionSet)  == 367,
	/// 	selectLargestInstance : (searchSpace) => Max(searchSpace)
	/// );
	/// 
	/// foreach (var solution in optimizer.Solve(ElementUsagePolicy.AllowReUse, 
	/// 		100, 25, 10, 5, 1))
	/// 		System.Console.WriteLine (solution);
	/// </code>
	public class GreedyOptimizer<Type>
	{
		public Func<List<Type>, bool> IsSatisfyingSolution 
		{
			get;
			set;
		}

		public Func<List<Type>, bool> IsFeasibleSolution
		{
			get;
			set;
		}

		public Func<ISet<Type>, Type> SelectLargestInstance
		{
			get;
			set;
		}


		public GreedyOptimizer (
			Func<List<Type>, bool> isSatisfyingSolution,
			Func<List<Type>, bool> isFeasibleSolution,
			Func<ISet<Type>, Type> selectLargestInstance)
		{
			this.IsSatisfyingSolution = isSatisfyingSolution;
			this.IsFeasibleSolution = isFeasibleSolution;
			this.SelectLargestInstance = selectLargestInstance;
		}

		public IEnumerable<Type> Solve(ElementUsagePolicy policy, params Type[] searchSpace)
		{
			var searchSet = new HashSet<Type> (searchSpace);
			var solutionSet = new List<Type> ();

			while (!IsSatisfyingSolution (solutionSet)) {
				if (searchSet.Count == 0)
					throw new InvalidOperationException ();

				var next = SelectLargestInstance (searchSet);
				solutionSet.Add (next);

				if (!IsFeasibleSolution (solutionSet)) {
					solutionSet.Remove (next);
					searchSet.Remove (next);
				}

				if (policy == ElementUsagePolicy.DenyReUse)
					searchSet.Remove (next);
			}

			return solutionSet;
		}
	}
}


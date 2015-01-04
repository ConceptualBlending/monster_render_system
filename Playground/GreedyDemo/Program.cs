using System;
using Hoax.Optimization;
using System.Collections.Generic;

namespace Hoax.Examples.Optimization
{
	class MainClass
	{
		public static void Each<T>(IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
				action(item);
		}

		public static int Max (IEnumerable<int> items) 
		{
			int max = int.MinValue;
			foreach (var item in items)
				max = Math.Max (item, max);
			return max;
		}

		public static int Sum (IEnumerable<int> items) 
		{
			int sum = 0;
			foreach (var item in items)
				sum += item;
			return sum;
		}

		public static void Main (string[] args)
		{
			var optimizer = new GreedyOptimizer<int> (
				isFeasibleSolution : (solutionSet) => Sum(solutionSet) <= 367,
				isSatisfyingSolution : (solutionSet) => Sum(solutionSet)  == 367,
				selectLargestInstance : (searchSpace) => Max(searchSpace)
			);

			foreach (var solution in optimizer.Solve(ElementUsagePolicy.AllowReUse, 
													 100, 25, 10, 5, 1))
				System.Console.WriteLine (solution);


			System.Console.WriteLine ("Done ");
		}
	}
}

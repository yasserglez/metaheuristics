using System;

namespace Metaheuristics
{
	public class DiscreteSA4TSP : DiscreteSA
	{
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteSA4TSP(TSPInstance instance, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			return TSPUtils.RandomSolution(Instance);
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

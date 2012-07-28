using System;

namespace Metaheuristics
{
	public class DiscreteSA4TSP : DiscreteSA
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;
		
		public DiscreteSA4TSP(TSPInstance instance, int initialSolutions, 
		                      int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			generatedSolutions = 0;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			int[] solution;
			
			if (generatedSolutions == 0) {
				solution = TSPUtils.GreedySolution(Instance);
			}
			else {
				solution = TSPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

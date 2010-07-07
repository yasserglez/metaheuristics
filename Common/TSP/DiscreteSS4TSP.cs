using System;

namespace Metaheuristics
{
	public class DiscreteSS4TSP : DiscreteSS
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteSS4TSP(TSPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;		
			generatedSolutions = 0;			
		}
		
		protected override double Fitness(int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] RandomSolution()
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
		
		protected override void Repair(int[] solution)
		{
			TSPUtils.Repair(Instance, solution);
		}
		
		protected override double Distance(int[] a, int[] b)
		{
			return TSPUtils.Distance(Instance, a, b);
		}
	}
}

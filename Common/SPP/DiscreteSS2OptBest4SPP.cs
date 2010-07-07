using System;

namespace Metaheuristics
{
	public class DiscreteSS2OptBest4SPP : DiscreteSS
	{
		public SPPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;			
		
		public DiscreteSS2OptBest4SPP(SPPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;
			generatedSolutions = 0;			
		}
		
		protected override double Fitness(int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] RandomSolution()
		{
			int[] solution;
			
			if (generatedSolutions < 2) {
				solution = SPPUtils.GRCSolution(Instance, 1.0);
			}
			else {
				solution = SPPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}
		
		protected override void Repair(int[] solution)
		{
		}
		
		protected override void Improve (int[] solution)
		{
			SPPUtils.LocalSearch2OptBest(Instance, solution);
		}
		
		protected override double Distance(int[] a, int[] b)
		{
			return SPPUtils.Distance(Instance, a, b);
		}
	}
}

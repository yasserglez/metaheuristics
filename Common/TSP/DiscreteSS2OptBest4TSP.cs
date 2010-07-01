using System;

namespace Metaheuristics
{
	public class DiscreteSS2OptBest4TSP : DiscreteSS
	{
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteSS2OptBest4TSP(TSPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] RandomSolution()
		{
			return TSPUtils.RandomSolution(Instance);
		}
		
		protected override void Repair(int[] solution)
		{
			TSPUtils.Repair(Instance, solution);
		}
		
		protected override void Improve (int[] solution)
		{
			TSPUtils.LocalSearch2OptBest(Instance, solution);
		}
		
		protected override double Distance(int[] a, int[] b)
		{
			return TSPUtils.Distance(Instance, a, b);
		}
	}
}

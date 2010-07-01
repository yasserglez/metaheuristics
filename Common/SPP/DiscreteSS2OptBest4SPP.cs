using System;

namespace Metaheuristics
{
	public class DiscreteSS2OptBest4SPP : DiscreteSS
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteSS2OptBest4SPP(SPPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] RandomSolution()
		{
			return SPPUtils.RandomSolution(Instance);
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

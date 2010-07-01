using System;

namespace Metaheuristics
{
	public class DiscreteSS4SPP : DiscreteSS
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteSS4SPP(SPPInstance instance, int poolSize, int refSetSize, double explorationFactor)
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
		
		protected override double Distance(int[] a, int[] b)
		{
			return SPPUtils.Distance(Instance, a, b);
		}
	}
}

using System;

namespace Metaheuristics
{
	public class DiscreteSSNPS42SP : DiscreteSS
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteSSNPS42SP(TwoSPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;			
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, solution));
		}
		
		protected override int[] RandomSolution()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}
		
		protected override void Repair(int[] solution)
		{
			TwoSPUtils.Repair(Instance, solution);
		}
		
		protected override double Distance(int[] a, int[] b)
		{
			return TwoSPUtils.Distance(Instance, a, b);
		}
	}
}

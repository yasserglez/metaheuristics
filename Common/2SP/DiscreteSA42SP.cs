using System;

namespace Metaheuristics
{
	public class DiscreteSA42SP : DiscreteSA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteSA42SP(TwoSPInstance instance)
			: base(2 * instance.NumberItems, 0.95)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TwoSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

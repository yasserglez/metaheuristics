using System;

namespace Metaheuristics
{
	public class DiscreteSABL42SP : DiscreteSA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteSABL42SP(TwoSPInstance instance)
			: base(2 * instance.NumberItems, 0.95)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, solution));
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

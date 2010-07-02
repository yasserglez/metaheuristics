
using System;

namespace Metaheuristics
{
	public class DiscreteTSNPS42SP : DiscreteTS
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteTSNPS42SP (TwoSPInstance instance, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			Instance = instance;
		}
		
		protected override int[] InitialSolution ()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
		}
		
		protected override Tuple<int, int> GetTabu (int[] current, int[] neighbor)
		{
			return TwoSPUtils.GetTabu(current, neighbor);
		}
		
		protected override int[] GetNeighbor (int[] solution)
		{
			return TwoSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

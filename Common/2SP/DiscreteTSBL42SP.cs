
using System;

namespace Metaheuristics
{
	public class DiscreteTSBL42SP : DiscreteTS
	{
		public TwoSPInstance Instance { get; protected set; }

		protected int generatedSolutions;		
		
		public DiscreteTSBL42SP (TwoSPInstance instance, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			Instance = instance;
			generatedSolutions = 0;			
		}
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions == 0) {
				solution = TwoSPUtils.DecreasingArea(Instance);
			}
			else if (generatedSolutions == 1) {
				solution = TwoSPUtils.DecreasingWidth(Instance);
			}
			else if (generatedSolutions == 2) {
				solution = TwoSPUtils.DecreasingHeight(Instance);
			}
			else {
				solution = TwoSPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;			
			return solution;
		}
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, individual));
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

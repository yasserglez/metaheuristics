using System;

namespace Metaheuristics
{
	public class DiscreteSSBL42SP : DiscreteSS
	{
		public TwoSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteSSBL42SP(TwoSPInstance instance, int poolSize, 
		                        int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;		
			generatedSolutions = 0;			
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, solution));
		}
		
		protected override int[] RandomSolution()
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

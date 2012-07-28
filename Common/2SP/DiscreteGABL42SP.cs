using System;

namespace Metaheuristics
{
	public class DiscreteGABL42SP : DiscreteGA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteGABL42SP (TwoSPInstance instance, int popSize, 
		                         double mutationProbability,
		                          int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			generatedSolutions = 0;			
		}
		
		protected override void Repair(int[] individual)
		{
			TwoSPUtils.Repair(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, individual));
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
	}
}

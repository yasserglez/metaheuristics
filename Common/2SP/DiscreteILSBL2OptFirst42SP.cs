using System;

namespace Metaheuristics
{
	public class DiscreteILSBL2OptFirst42SP : DiscreteILS
	{
		public TwoSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteILSBL2OptFirst42SP(TwoSPInstance instance, int restartIterations, 
		                                  int[] lowerBounds, int[] upperBounds) 
			: base ( restartIterations, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			generatedSolutions = 0;			
		}
		
		protected override void Repair(int[] individual)
		{
			TwoSPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			TwoSPUtils.BLLocalSearch2OptFirst(Instance, individual);
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
		
		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			TwoSPUtils.PerturbateSolution(solution, perturbation);
		}

	}
}
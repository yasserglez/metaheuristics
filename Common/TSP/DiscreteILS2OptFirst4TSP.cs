using System;

namespace Metaheuristics
{
	public class DiscreteILS2OptFirst4TSP : DiscreteILS
	{
		protected int generatedSolutions;
		
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteILS2OptFirst4TSP (TSPInstance instance, int restartIterations, 
		                                 int[] lowerBounds, int[] upperBounds) 
			: base ( restartIterations, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			generatedSolutions = 0;
		}
		
		protected override void Repair(int[] individual)
		{
			TSPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			TSPUtils.LocalSearch2OptFirst(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return TSPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions == 0) {
				solution = TSPUtils.GreedySolution(Instance);
			}
			else {
				solution = TSPUtils.GRCSolution(Instance, 0.9);
			}
			
			generatedSolutions++;
			return solution;
		}
		
		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			TSPUtils.PerturbateSolution(solution, perturbation);
		}
	}
}
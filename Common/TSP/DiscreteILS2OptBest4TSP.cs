using System;

namespace Metaheuristics
{
	public class DiscreteILS2OptBest4TSP : DiscreteILS
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;
		
		public DiscreteILS2OptBest4TSP (TSPInstance instance, int restartIterations, 
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
			TSPUtils.LocalSearch2OptBest(Instance, individual);
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
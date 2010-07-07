using System;

namespace Metaheuristics
{
	public class DiscreteMA2OptFirst4TSP : DiscreteMA
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteMA2OptFirst4TSP(TSPInstance instance, int popSize, double mutationProbability,
		                              int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			LocalSearchEnabled = true;
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
				solution = TSPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}

	}
}

using System;

namespace Metaheuristics
{
	public class DiscreteGA4TSP: DiscreteGA
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;
		
		public DiscreteGA4TSP(TSPInstance instance, int popSize, double mutationProbability,
		                      int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			generatedSolutions = 0;
		}
		
		protected override void Repair(int[] individual)
		{
			TSPUtils.Repair(Instance, individual);
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

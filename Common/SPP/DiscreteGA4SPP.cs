using System;

namespace Metaheuristics
{
	public class DiscreteGA4SPP: DiscreteGA
	{
		public SPPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;			
		
		public DiscreteGA4SPP(SPPInstance instance, int popSize, double mutationProbability,
		                      int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			generatedSolutions = 0;			
		}
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions < 2) {
				solution = SPPUtils.GRCSolution(Instance, 1.0);
			}
			else {
				solution = SPPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}

	}
}

using System;

namespace Metaheuristics
{
	public class DiscreteGA4SPP: DiscreteGA
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteGA4SPP(SPPInstance instance, int popSize, double mutationProbability,
		                      int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			return SPPUtils.RandomSolution(Instance);
		}

	}
}

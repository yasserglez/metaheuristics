using System;

namespace Metaheuristics
{
	public class DiscreteGA2OptFirst4SPP : DiscreteGA
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteGA2OptFirst4SPP(SPPInstance instance, int popSize, double mutationProbability,
		                              int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			LocalSearchEnabled = true;
		}
		
		protected override void LocalSearch(int[] individual)
		{
			SPPUtils.LocalSearch2OptFirst(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
	}
}

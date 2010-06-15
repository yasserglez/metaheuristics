using System;

namespace Metaheuristics
{
	public class DiscreteGANPS42SP : DiscreteGA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteGANPS42SP (TwoSPInstance instance, int popSize, double mutationProbability,
		                          int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			TwoSPUtils.Repair(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
		}
	}
}

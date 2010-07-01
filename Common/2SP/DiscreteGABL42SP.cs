using System;

namespace Metaheuristics
{
	public class DiscreteGABL42SP : DiscreteGA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteGABL42SP (TwoSPInstance instance, int popSize, double mutationProbability,
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
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, individual));
		}
		
		protected override int[] InitialSolution ()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}
	}
}

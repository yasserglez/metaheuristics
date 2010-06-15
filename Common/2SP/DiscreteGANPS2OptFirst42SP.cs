using System;

namespace Metaheuristics
{
	public class DiscreteGANPS2OptFirst42SP : DiscreteGA
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteGANPS2OptFirst42SP(TwoSPInstance instance, int popSize, double mutationProbability,
		                                  int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			LocalSearchEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			TwoSPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{		
			TwoSPUtils.NPSLocalSearch2OptFirst(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
		}
	}
}

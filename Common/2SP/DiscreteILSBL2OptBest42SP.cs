
using System;

namespace Metaheuristics
{


	public class DiscreteILSBL2OptBest42SP : DiscreteILS
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteILSBL2OptBest42SP (TwoSPInstance instance, int restartIterations, 
		                                 int perturbationPoints, int[] lowerBounds, 
		                                 int[] upperBounds) 
			: base ( restartIterations, perturbationPoints, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		
		protected override void Repair(int[] individual)
		{
			TwoSPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			TwoSPUtils.BLLocalSearch2OptBest(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, individual));
		}
		
		protected override int[] InitialSolution ()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}

		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			TwoSPUtils.PerturbateSolution(solution, perturbation);
		}

	}
}
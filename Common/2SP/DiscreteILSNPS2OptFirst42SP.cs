
using System;

namespace Metaheuristics
{


	public class DiscreteILSNPS2OptFirst42SP : DiscreteILS
	{
		
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteILSNPS2OptFirst42SP (TwoSPInstance instance, int restartIterations, 
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
			TwoSPUtils.NPSLocalSearch2OptFirst(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
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
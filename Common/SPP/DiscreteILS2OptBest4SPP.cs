
using System;

namespace Metaheuristics
{


	public class DiscreteILS2OptBest4SPP : DiscreteILS
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteILS2OptBest4SPP (SPPInstance instance, int restartIterations, 
		                                 int perturbationPoints, int[] lowerBounds, 
		                                 int[] upperBounds) 
			: base ( restartIterations, perturbationPoints, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = false;
		}
		
		protected override void LocalSearch(int[] individual)
		{
			SPPUtils.LocalSearch2OptBest(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			return SPPUtils.RandomSolution(Instance);
		}
		
		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			SPPUtils.PerturbateSolution(Instance, solution, perturbation);
		}
	}
}
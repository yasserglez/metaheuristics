
using System;

namespace Metaheuristics
{


	public class DiscreteILS2OptBest4QAP : DiscreteILS
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteILS2OptBest4QAP (QAPInstance instance, int restartIterations, 
		                                 int perturbationPoints, int[] lowerBounds, 
		                                 int[] upperBounds) 
			: base ( restartIterations, perturbationPoints, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		
		protected override void Repair(int[] individual)
		{
			QAPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			QAPUtils.LocalSearch2OptBest(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			return QAPUtils.RandomSolution(Instance);
		}
		
		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			QAPUtils.PerturbateSolution(solution, perturbation);
		}
	}
}
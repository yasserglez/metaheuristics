using System;

namespace Metaheuristics
{
	public class DiscreteILS2OptFirst4QAP : DiscreteILS
	{
		public QAPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteILS2OptFirst4QAP(QAPInstance instance, int restartIterations, 
		                                int[] lowerBounds, int[] upperBounds) 
			: base ( restartIterations, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			generatedSolutions = 0;			
		}
		
		protected override void Repair(int[] individual)
		{
			QAPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			QAPUtils.LocalSearch2OptFirst(Instance, individual);
		}		
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions < 2) {
				solution = QAPUtils.GRCSolution(Instance, 1.0);
			}
			else {
				solution = QAPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}
		
		protected override void PerturbateSolution (int[] solution, int perturbation)
		{
			QAPUtils.PerturbateSolution(solution, perturbation);
		}

	}
}
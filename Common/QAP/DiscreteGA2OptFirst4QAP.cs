using System;

namespace Metaheuristics
{
	public class DiscreteGA2OptFirst4QAP : DiscreteGA
	{
		public QAPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteGA2OptFirst4QAP (QAPInstance instance, int popSize, double mutationProbability,
		                                int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			LocalSearchEnabled = true;			
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
	}
}

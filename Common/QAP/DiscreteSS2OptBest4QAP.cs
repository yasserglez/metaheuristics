using System;

namespace Metaheuristics
{
	public class DiscreteSS2OptBest4QAP : DiscreteSS
	{
		public QAPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteSS2OptBest4QAP(QAPInstance instance, int poolSize, int refSetSize, double explorationFactor)
			: base(poolSize, refSetSize, explorationFactor)
		{
			Instance = instance;
			generatedSolutions = 0;			
		}
		
		protected override double Fitness(int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] RandomSolution()
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
		
		protected override void Repair(int[] solution)
		{
			QAPUtils.Repair(Instance, solution);
		}
		
		protected override void Improve (int[] solution)
		{
			QAPUtils.LocalSearch2OptBest(Instance, solution);
		}
		
		protected override double Distance(int[] a, int[] b)
		{
			return QAPUtils.Distance(Instance, a, b);
		}
	}
}

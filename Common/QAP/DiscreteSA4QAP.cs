using System;

namespace Metaheuristics
{
	public class DiscreteSA4QAP : DiscreteSA
	{
		public QAPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;
		
		public DiscreteSA4QAP(QAPInstance instance, int initialSolutions, 
		                      int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			generatedSolutions = 0;
		}
		
		protected override double Fitness(int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
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
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return QAPUtils.GetNeighbor(Instance, solution);
		}
	}
}

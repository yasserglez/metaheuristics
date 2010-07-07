using System;

namespace Metaheuristics
{
	public class DiscreteSA4SPP : DiscreteSA
	{
		public SPPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		
		
		public DiscreteSA4SPP(SPPInstance instance, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			generatedSolutions = 0;
		}
		
		protected override double Fitness(int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			int[] solution;
			
			if (generatedSolutions < 2) {
				solution = SPPUtils.GRCSolution(Instance, 1.0);
			}
			else {
				solution = SPPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return SPPUtils.GetNeighbor(Instance, solution);
		}
	}
}

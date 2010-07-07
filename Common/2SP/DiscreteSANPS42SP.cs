using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteSANPS42SP : DiscreteSA
	{
		protected int generatedSolutions;
			
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteSANPS42SP(TwoSPInstance instance, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			generatedSolutions = 0;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, solution));
		}
		
		protected override int[] InitialSolution()
		{
			int[] solution;
			
			if (generatedSolutions == 0) {
				solution = TwoSPUtils.DecreasingArea(Instance);
			}
			else {
				solution = TwoSPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;			
			return solution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TwoSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

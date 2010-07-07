using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscretePSO2OptFirst4TSP : DiscretePSO
	{
		public TSPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		

        public DiscretePSO2OptFirst4TSP(TSPInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
			LocalSearchEnabled = true;
			generatedSolutions = 0;			
        }

        protected override double Fitness(int[] individual)
        {
            return TSPUtils.Fitness(Instance, individual);
        }
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions == 0) {
				solution = TSPUtils.GreedySolution(Instance);
			}
			else {
				solution = TSPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}

		protected override void LocalSearch (int[] solution)
		{
			TSPUtils.LocalSearch2OptFirst(Instance, solution);
		}
	}
}

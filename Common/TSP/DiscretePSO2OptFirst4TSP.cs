using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscretePSO2OptFirst4TSP : DiscretePSO
	{
		public TSPInstance Instance { get; protected set; }

        public DiscretePSO2OptFirst4TSP(TSPInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
			LocalSearchEnabled = true;
        }

        protected override double Fitness(int[] individual)
        {
            return TSPUtils.Fitness(Instance, individual);
        }
		
		protected override int[] InitialSolution ()
		{
			return TSPUtils.RandomSolution(Instance);
		}

		protected override void LocalSearch (int[] solution)
		{
			TSPUtils.LocalSearch2OptFirst(Instance, solution);
		}
	}
}

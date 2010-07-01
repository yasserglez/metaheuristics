using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscretePSOBL2OptFirst42SP : DiscretePSO
	{
		public TwoSPInstance Instance { get; protected set; }

        public DiscretePSOBL2OptFirst42SP(TwoSPInstance instance, int partsCount, double prevConf,
		                                   double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
			LocalSearchEnabled = true;
        }

        protected override double Fitness(int[] individual)
        {
            return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, individual));
        }
		
		protected override int[] InitialSolution ()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}

		
		protected override void LocalSearch (int[] solution)
		{
			TwoSPUtils.BLLocalSearch2OptFirst(Instance, solution);
		}
	}
}

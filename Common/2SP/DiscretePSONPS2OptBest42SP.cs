using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscretePSONPS2OptBest42SP : DiscretePSO
	{
		public TwoSPInstance Instance { get; protected set; }

		public DiscretePSONPS2OptBest42SP(TwoSPInstance instance, int partsCount, double prevConf,
		                                  double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
            RepairEnabled = true;
			LocalSearchEnabled = true;
        }

        protected override void Repair(int[] individual)
        {
            TwoSPUtils.Repair(Instance, individual);
        }

        protected override double Fitness(int[] individual)
        {
            return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
        }
		
		protected override void LocalSearch (int[] solution)
		{
			TwoSPUtils.NPSLocalSearch2OptBest(Instance, solution);
		}
	}
}

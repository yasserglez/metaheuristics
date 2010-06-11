using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscretePSO2OptBest42SP : DiscretePSO
	{
		public TwoSPInstance Instance { get; protected set; }

        public DiscretePSO2OptBest42SP(TwoSPInstance instance, int partsCount, double prevConf,
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
            return TwoSPUtils.Fitness(Instance, individual);
        }
		
		protected override void LocalSearch (int[] solution)
		{
			TwoSPUtils.LocalSearch2OptBest(Instance, solution);
		}
	}
}

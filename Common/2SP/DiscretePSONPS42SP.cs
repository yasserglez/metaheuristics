using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class DiscretePSONPS42SP : DiscretePSO
    {
        public TwoSPInstance Instance { get; protected set; }

        public DiscretePSONPS42SP(TwoSPInstance instance, int partsCount, double prevConf,
		                          double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
        }

        protected override double Fitness(int[] individual)
        {
            return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, individual));
        }
		
		protected override int[] InitialSolution ()
		{
			return TwoSPUtils.RandomSolution(Instance);
		}

    }
}
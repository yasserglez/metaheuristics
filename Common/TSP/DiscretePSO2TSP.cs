using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class DiscretePSO4TSP : DiscretePSO
    {
        public TSPInstance Instance { get; protected set; }

        public DiscretePSO4TSP(TSPInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
            RepairEnabled = true;
        }

        protected override void Repair(int[] individual)
        {
            TSPUtils.Repair(Instance, individual);
        }

        protected override double Fitness(int[] individual)
        {
            return TSPUtils.Fitness(Instance, individual);
        }
    }
}

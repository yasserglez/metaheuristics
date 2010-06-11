using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class DiscretePSO4QAP : DiscretePSO
    {
        public QAPInstance Instance { get; protected set; }

        public DiscretePSO4QAP(QAPInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
            RepairEnabled = true;
        }

        protected override void Repair(int[] individual)
        {
            QAPUtils.Repair(Instance, individual);
        }

        protected override double Fitness(int[] individual)
        {
            return QAPUtils.Fitness(Instance, individual);
        }
    }
}

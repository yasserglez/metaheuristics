using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class DiscretePSO4QAP : DiscretePSO
    {
        public QAPInstance Instance { get; protected set; }
		
		protected int generatedSolutions;		

        public DiscretePSO4QAP(QAPInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
			generatedSolutions = 0;			
        }

        protected override double Fitness(int[] individual)
        {
            return QAPUtils.Fitness(Instance, individual);
        }
		
		protected override int[] InitialSolution ()
		{
			int[] solution;
			
			if (generatedSolutions < 2) {
				solution = QAPUtils.GRCSolution(Instance, 1.0);
			}
			else {
				solution = QAPUtils.RandomSolution(Instance);
			}
			
			generatedSolutions++;
			return solution;
		}
    }
}

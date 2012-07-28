using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class SSTuner : Tuner
	{
		public SSTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250 };
			double[] poolSizes = new double[] { 20, 40, 100, 200 };
			double[] refSetSizes = new double[] { 4, 10 };
			double[] explorationFactors = new double[] { 0.25, 0.5, 0.75 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double poolSize in poolSizes) {
					foreach (double refSetSize in refSetSizes) {
						foreach (double explorationFactor in explorationFactors) {
							yield return new double[] { 
								timePenalty, poolSize, 
								refSetSize, explorationFactor 
							};
						}
					}
				}
			}
		}
	}
}

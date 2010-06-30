using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class GATuner : Tuner
	{
		public GATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 100, 250};
			List<double> popFactors = new List<double>();
//			popFactors.Add(0.02);
//			popFactors.Add(0.04);
//			popFactors.Add(0.06);
//			popFactors.Add(0.08);
			popFactors.Add(0.10);
			popFactors.Add(0.15);
			popFactors.Add(0.20);
	        popFactors.Add(0.25);
			popFactors.Add(0.50);
			popFactors.Add(0.75);
			popFactors.Add(1);
			double[] mutProbabilities = new double[] { 0.1, 0.2, 0.3 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double mutProbability in mutProbabilities) {
					foreach (double popFactor in popFactors) {
						yield return new double[] { timePenalty, popFactor, mutProbability };
					}
				}
			}
		}
	}
}

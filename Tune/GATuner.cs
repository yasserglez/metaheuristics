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
			double[] popFactors = new double[25];
			for (int i = 0; i < popFactors.Length; i++) {
				popFactors[i] = (2 * i) + 1;
			}
			double[] mutProbabilities = new double[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6 };
			
			foreach (double popFactor in popFactors) {
				foreach (double mutProbability in mutProbabilities) {
					yield return new double[] { popFactor, mutProbability };
				}
			}
		}
	}
}

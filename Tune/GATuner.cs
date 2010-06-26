using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class GATuner : Tuner
	{
		public GATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 3, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
			double[] timePenalties = new double[] { 250, 500 };
			List<double> popFactors = new List<double>();
			for (int i = 10; i <= 40; i += 3) {
				popFactors.Add(i);
			}
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

using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class HMTuner : Tuner
	{
		public HMTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250, 500 };
			double[] mutProbabilities = new double[] { 0.1, 0.2, 0.3 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double mutProbability in mutProbabilities) {
						yield return new double[] { timePenalty, mutProbability };
				}
			}
		}
	}
}

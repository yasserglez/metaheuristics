using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class GRASPTuner : Tuner
	{
		public GRASPTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
			double[] rclThresholds = new double[] { 0.25, 0.30, 0.50, 0.60, 0.75, 0.90 };
			double[] timePenalties = new double[] { 0, 250, 500};
			
			foreach (double timePenalty in timePenalties) {
				foreach (double rclThreshold in rclThresholds) {
					yield return new double[] { timePenalty, rclThreshold, };
				}
			}
		}
	}
}

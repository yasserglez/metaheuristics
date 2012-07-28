using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class TSTuner : Tuner
	{
		public TSTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 100, 250 };
			double[] tabuListFactors = new double[] { 0.20, 0.50 };
			double[] neighborCheckFactors = new double[] { 0.10, 0.25, 0.50};
			double[] rclThresholds = new double[] { 0.75, 0.9};
			
			foreach (double rclThreshold in rclThresholds) {
				foreach (double timePenalty in timePenalties) {
					foreach (double tabuListFactor in tabuListFactors) {
						foreach (double neighborCheckFactor in neighborCheckFactors) {
							yield return new double[] { 
								timePenalty, neighborCheckFactor, 
								tabuListFactor, rclThreshold
							};
						}
					}
				}
			}
		}
	}
}

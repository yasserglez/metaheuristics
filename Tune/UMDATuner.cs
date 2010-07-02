using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class UMDATuner : Tuner
	{
		public UMDATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
            : base(metaheuristic, dirInstances, 6, new int[] { 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250, 500, 750 };
			List<double> popFactors = new List<double>();
            popFactors.Add(0.02);
			popFactors.Add(0.03);
			popFactors.Add(0.06);
			popFactors.Add(0.1);
			popFactors.Add(0.25);
			popFactors.Add(0.5);
			popFactors.Add(0.75);
			popFactors.Add(1.0);
			popFactors.Add(1.5);
			for (int i = 2; i <= 2; i += 2) {
				popFactors.Add(i);
			}
			double[] truncFactors = new double[] { 0.2, 0.3, 0.4 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double truncFactor in truncFactors) {
					foreach (double popFactor in popFactors) {
						yield return new double[] { timePenalty, popFactor, truncFactor };
					}
				}
			}
		}
	}
}

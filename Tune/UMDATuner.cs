using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class UMDATuner : Tuner
	{
		public UMDATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 3, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
			double[] popFactors = new double[25];
			for (int i = 0; i < popFactors.Length; i++) {
				popFactors[i] = (2 * i) + 1;
			}
			double[] truncFactors = new double[] { 0.2, 0.4, 0.6, 0.8 };
			
			foreach (double popFactor in popFactors) {
				foreach (double truncFactor in truncFactors) {
					yield return new double[] { popFactor, truncFactor };
				}
			}
		}
	}
}

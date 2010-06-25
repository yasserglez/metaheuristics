using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class UMDATuner : Tuner
	{
		public UMDATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
			List<double> popFactors = new List<double>();
			for (int i = 5; i <= 51; i += 2) {
				popFactors.Add(i);
			}
			double[] truncFactors = new double[] { 0.2, 0.3, 0.4, 0.5 };
			
			foreach (double popFactor in popFactors) {
				foreach (double truncFactor in truncFactors) {
					yield return new double[] { popFactor, truncFactor };
				}
			}
		}
	}
}

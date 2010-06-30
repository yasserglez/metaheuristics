using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class PSOTuner:Tuner
	{
		public PSOTuner (ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250, 500 };
			List<double> popFactors = new List<double>();
//			popFactors.Add(0.02);
//			popFactors.Add(0.04);
//			popFactors.Add(0.06);
//			popFactors.Add(0.08);
//			popFactors.Add(0.10);
//			popFactors.Add(0.15);
	        popFactors.Add(0.25);
			popFactors.Add(0.50);
			popFactors.Add(0.50);
			popFactors.Add(0.60);
			popFactors.Add(0.75);
			popFactors.Add(1);
			popFactors.Add(1.5);
			double[] previousConfidence = new double[] { 0.25, 0.5, 0.75 };
			double[] neighbourConfidence = new double[] { 0.25, 0.5, 0.75 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double prevConf in previousConfidence) {
					foreach (double neighConf in neighbourConfidence) {
						foreach (double popFactor in popFactors) {
							yield return new double[] { timePenalty, popFactor, prevConf, neighConf };
						}	
					}
				}
			}
		}
	}
}
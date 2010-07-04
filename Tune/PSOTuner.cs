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
            double[] timePenalties = new double[] { 100, 250 };
			List<double> popSizes = new List<double>();
//			popSizes.Add(2);
//			popSizes.Add(4);
			popSizes.Add(6);
			popSizes.Add(8);
			popSizes.Add(10);
			popSizes.Add(16);
			popSizes.Add(20);
	        popSizes.Add(30);
//			popSizes.Add(40);
//			popSizes.Add(50);
//			popSizes.Add(60);
//			popSizes.Add(70);
//			popSizes.Add(80);
			double[] previousConfidence = new double[] { 0.6, 0.75 };
			double[] neighbourConfidence = new double[] { 0.6, 0.75 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double prevConf in previousConfidence) {
					foreach (double neighConf in neighbourConfidence) {
						foreach (double popFactor in popSizes) {
							yield return new double[] { timePenalty, popFactor, prevConf, neighConf };
						}	
					}
				}
			}
		}
	}
}

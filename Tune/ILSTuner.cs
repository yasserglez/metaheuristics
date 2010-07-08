using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class ILSTuner : Tuner
	{
		public ILSTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 50, 100 };
			double[] graspIterations = new double[] { 50, 100 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double graspIteration in graspIterations) {
						yield return new double[] { timePenalty, graspIteration };
				}
			}
		}
	}
}

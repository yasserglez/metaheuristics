using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class SATuner : Tuner
	{
		public SATuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250 };		
			double[] initialSolutions = new double[] { 2, 4, 8, 10 };
			double[] levelLengthFactors = new double[] { 0.05, 0.25, 0.75, 1.0 };
			double[] tempReductions = new double[] { 0.75, 0.85, 0.95 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double initialSolution in initialSolutions) {
					foreach (double levelLengthFactor in levelLengthFactors) {
						foreach (double tempReduction in tempReductions) {
							yield return new double[] { timePenalty, initialSolution, levelLengthFactor, tempReduction };
						}
					}
				}
			}
		}
	}
}

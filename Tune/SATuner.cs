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
			double[] initialSolutions = new double[] { 5, 6, 7, 8, 9, 10 };
			double[] levelLengthFactors = new double[] { 0.5, 0.7, 0.9, 1.0 };
			double[] tempReductions = new double[] { 0.75, 0.80, 0.85, 0.90, 0.95 };
			
			foreach (double initialSolution in initialSolutions) {
				foreach (double levelLengthFactor in levelLengthFactors) {
					foreach (double tempReduction in tempReductions) {
						yield return new double[] { initialSolution, levelLengthFactor, tempReduction };
					}
				}
			}
		}
	}
}

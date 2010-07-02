using System;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public class ACOTuner : Tuner
	{
		public ACOTuner(ITunableMetaheuristic metaheuristic, string dirInstances)
			: base(metaheuristic, dirInstances, 6, new int[] { 2000, 10000 }, 5)
		{
		}
		
		protected override IEnumerable<double[]> EnumerateParameters()
		{
            double[] timePenalties = new double[] { 250 };		
			double[] rhos = new double[] { 0.02 };
			double[] alphas = new double[] { 1 };
			double[] betas = new double[] { 2 };
			double[] maxReinits = new double[] { 5 };
			
			foreach (double timePenalty in timePenalties) {
				foreach (double rho in rhos) {
					foreach (double alpha in alphas) {
						foreach (double beta in betas) {
							foreach (double maxReinit in maxReinits) {
								yield return new double[] { timePenalty, rho, alpha, beta, maxReinit };
							}
						}
					}
				}
			}
		}
	}
}

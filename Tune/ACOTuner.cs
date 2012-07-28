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
			double[] rhos = new double[] { 0.05, 0.1, 0.2 };
			double[] alphas = new double[] { 0, 1, 2, 5 };
			double[] betas = new double[] { 0, 1, 2, 5 };
			double[] maxReinits = new double[] { 2, 5, 10, 50 };
			double[] numberAnts = new double[] { 2, 5, 10 };
			double[] candidateLengths = new double[] { 15, 20, 25, 30, 35, 40 };
			double[] candidateWeights = new double[] { 0.25, 0.5, 0.75, 0.875, 1 };

			foreach (double timePenalty in timePenalties) {
				foreach (double rho in rhos) {
					foreach (double alpha in alphas) {
						foreach (double beta in betas) {
							foreach (double numberAnt in numberAnts) {
								foreach (double maxReinit in maxReinits) {
									foreach (double candidateLength in candidateLengths) {
										foreach (double candidateWeight in candidateWeights) {
											yield return new double[] { 
												timePenalty, rho, alpha, beta, maxReinit, 
												numberAnt, candidateLength, candidateWeight 
											};
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}

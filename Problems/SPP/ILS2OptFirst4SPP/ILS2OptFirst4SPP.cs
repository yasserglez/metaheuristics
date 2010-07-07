using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class ILS2OptFirst4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int restartIterations = 100;
		public int perturbations = 2;
		
		public void Start (string inputFile, string outputFile, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(inputFile);
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteILS ils = new DiscreteILS2OptFirst4SPP(instance, restartIterations, perturbations, lowerBounds, upperBounds);
			ils.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, ils.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "ILS with 2-opt (first improvement) local search for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ILS;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.SPP;
			}
		}

		public string[] Team {
			get {
				return About.Team;
			}
		}
		
		public void UpdateParameters (double[] parameters)
		{
			timePenalty = (int)parameters[0];
		}
	}
}

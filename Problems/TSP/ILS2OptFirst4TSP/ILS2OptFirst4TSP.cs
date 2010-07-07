using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class ILS2OptFirst4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 50;
		public int restartIterations = 100;
		public int perturbations = 2;
		
		
		public void Start (string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
			DiscreteILS ils = new DiscreteILS2OptFirst4TSP(instance, restartIterations, perturbations, lowerBounds, upperBounds);
			ils.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, ils.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "ILS with 2-opt (best improvement) local search for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ILS;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TSP;
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

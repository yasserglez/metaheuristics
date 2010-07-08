using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class ILS2OptBest4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int restartIterations = 50;
		
		public void Start (string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
			DiscreteILS ils = new DiscreteILS2OptBest4TSP(instance, restartIterations, lowerBounds, upperBounds);
			ils.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, ils.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "ILS with 2-opt (first improvement) local search for TSP";
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
			restartIterations = (int)parameters[1];
		}
	}
}

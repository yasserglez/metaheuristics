using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class ILSBL2OptFirst42SP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int restartIterations = 50;
		
		public void Start (string inputFile, string outputFile, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(inputFile);
			int[] lowerBounds = new int[instance.NumberItems];
            int[] upperBounds = new int[instance.NumberItems];
            for (int i = 0; i < instance.NumberItems; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberItems - 1;
            }
			DiscreteILS ils = new DiscreteILSBL2OptFirst42SP(instance, restartIterations, lowerBounds, upperBounds);
			ils.Run(timeLimit - timePenalty);
			int[,] coordinates = TwoSPUtils.BLCoordinates(instance, ils.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "ILS using the BL heuristic with 2-opt (first improvement) local search for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ILS;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
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

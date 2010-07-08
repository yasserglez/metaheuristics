using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class ILS2OptBest4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 50;
		public int restartIterations = 50;
		
		public void Start (string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
			int[] lowerBounds = new int[instance.NumberFacilities];
            int[] upperBounds = new int[instance.NumberFacilities];
            for (int i = 0; i < instance.NumberFacilities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberFacilities - 1;
            }
			DiscreteILS ils = new DiscreteILS2OptBest4QAP(instance, restartIterations, lowerBounds, upperBounds);
			ils.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, ils.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "ILS with 2-opt (best improvement) local search for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ILS;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.QAP;
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

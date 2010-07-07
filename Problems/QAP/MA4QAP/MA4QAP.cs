using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MA4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 500;
		protected double popSize = 8;
		protected double mutProbability = 0.3;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the MA for this instance of the problem.
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteMA memetic = new DiscreteMA4QAP(instance, (int)popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			memetic.Run(timeLimit - (int)timePenalty);
			QAPSolution solution = new QAPSolution(instance, memetic.BestIndividual);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "MA for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.MA;
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

		public void UpdateParameters(double[] parameters)
		{
			timePenalty = parameters[0];
			popSize = parameters[1];
			mutProbability = parameters[2];
		}
	}
}

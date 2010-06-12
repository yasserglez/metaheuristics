using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GA4QAP : IMetaheuristic
	{		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberFacilities;
			double mutProbability = 0.3;
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteGA genetic = new DiscreteGA4QAP(instance, popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			genetic.Run(timeLimit);
			QAPSolution solution = new QAPSolution(instance, genetic.BestIndividual);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GA for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GA;
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
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class UMDA4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double popFactor = 50;
		protected double truncFactor = 0.3;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberFacilities);
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4QAP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			umda.Run(timeLimit);
			QAPSolution solution = new QAPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "UMDA for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.EDA;
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
			popFactor = parameters[0];
			truncFactor = parameters[1];
		}		
	}
}

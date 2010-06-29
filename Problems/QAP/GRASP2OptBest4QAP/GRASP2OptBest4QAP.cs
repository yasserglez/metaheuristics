using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptBest4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 250;
		protected double rclThreshold = 0.25;
			
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the GRASP for this instance of the problem.
			// Bigger Threshold benefits greedy over random.
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteGRASP grasp = new DiscreteGRASP2OptBest4QAP(instance, rclThreshold, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			grasp.Run(timeLimit - (int)timePenalty, RunType.TimeLimit);
			QAPSolution solution = new QAPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GRASP with 2-opt (best improvement) local search for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GRASP;
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
		
		public void UpdateParameters (double[] parameters) {
			timePenalty = parameters[0];
			rclThreshold = parameters[1];
		}
	}
}

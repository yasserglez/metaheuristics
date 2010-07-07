using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptBest4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 50;
		protected double rclThreshold = 0.9;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the GRASP for this instance of the problem.
			DiscreteGRASP grasp = new DiscreteGRASP2OptBest4TSP(instance, rclThreshold);
			
			// Solving the problem and writing the best solution found.
			grasp.Run(timeLimit - (int)timePenalty, RunType.TimeLimit);
			TSPSolution solution = new TSPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GRASP with 2-opt (best improvement) local search for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GRASP;
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
		
		public void UpdateParameters (double[] parameters) {
			timePenalty = parameters[0];
			rclThreshold = parameters[1];
		}
	}
}

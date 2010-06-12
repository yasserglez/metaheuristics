using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptFirst4SPP: IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the GRASP for this instance of the problem.
			// Bigger Threshold benefits greedy over random.
			double rclThreshold = 0.6;
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteGRASP grasp = new DiscreteGRASP2OptFirst4SPP(instance, rclThreshold, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			grasp.Run(timeLimit, RunType.TimeLimit);
			SPPSolution solution = new SPPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GRASP with 2-opt (first improvement) local search for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GRASP;
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
	}
}

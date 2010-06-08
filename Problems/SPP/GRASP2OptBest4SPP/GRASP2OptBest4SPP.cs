using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptBest4SPP
	{
		public static string Algoritmo = "GRASP with 2-opt (best improvement) local search for SPP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
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
			DiscreteGRASP grasp = new DiscreteGRASP2OptBest4SPP(instance, rclThreshold, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = grasp.Run(timeLimit, RunType.TimeLimit);
			SPPSolution solution = new SPPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

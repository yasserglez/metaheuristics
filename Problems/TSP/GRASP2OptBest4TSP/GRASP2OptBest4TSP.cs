using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptBest4TSP
	{
		public static string Algoritmo = "GRASP with 2-opt (best improvement) local search for TSP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the GRASP for this instance of the problem.
			// Bigger Threshold benefits greedy over random.
			double rclThreshold = 0.6;
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteGRASP grasp = new DiscreteGRASP2OptBest4TSP(instance, rclThreshold, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = grasp.Run(timeLimit, RunType.TimeLimit);
			TSPSolution solution = new TSPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

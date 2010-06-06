using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMDA2OptFirst42SP
	{
		public static string Algoritmo = "UMDA using the NPS heuristic with 2-opt (first improvement) local search for 2SP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = (int) Math.Max(10, instance.NumberItems / 3.0);
			double truncFactor = 0.5;
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberItems - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA2OptFirst42SP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			int[,] coordinates = TwoSPUtils.NPS2Coordinates(instance, umda.BestIndividual);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

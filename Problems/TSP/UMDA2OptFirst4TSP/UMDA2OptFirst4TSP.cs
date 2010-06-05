using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMDA2OptFirst4TSP
	{
		public static string Algoritmo = "UMDA with 2-opt (first improvement) local search for TSP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberCities;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA2OptFirst4TSP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			TSPSolution solution = new TSPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

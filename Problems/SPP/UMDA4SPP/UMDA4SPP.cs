using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMDA4SPP
	{
		public static string Algoritmo = "UMDA for SPP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberItems;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4SPP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class GA2OptFirst4SPP
	{
		public static string Algoritmo = "GA with 2-opt (first improvement) local search for SPP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the GA for this instance of the problem.
			int popSize = 50 * instance.NumberItems;
			double mutProbability = 0.3;
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteGA genetic = new DiscreteGA2OptFirst4SPP(instance, popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = genetic.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, genetic.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

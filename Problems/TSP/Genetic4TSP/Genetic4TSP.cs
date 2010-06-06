using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class Genetic4TSP
	{
		public static string Algoritmo = "Genetic Algorithm for TSP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the Genetic Algorithm for this instance of the problem.
			int popSize = 50 * instance.NumberCities;
			double mutProbability = 0.3;
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteGenetic genetic = new DiscreteGenetic4TSP(instance, popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = genetic.Run(timeLimit);
			TSPSolution solution = new TSPSolution(instance, genetic.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

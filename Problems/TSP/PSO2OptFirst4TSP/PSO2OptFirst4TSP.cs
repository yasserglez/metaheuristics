using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class PSO2OptFirst4TSP
	{
		public static string Algoritmo = "PSO with 2-opt (first improvement) local search for TSP";

        public static string[] Integrantes = TeamInfo.Members;

        public static string Nombre_equipo = TeamInfo.Name;

        public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
        {
            TSPInstance instance = new TSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = 50 * instance.NumberCities;
            double prevConf = 0.5;
            double neighConf = 0.8;
            int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
            DiscretePSO pso = new DiscretePSO2OptFirst4TSP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);
            
            // Solving the problem and writing the best solution found.
            List<double> solutions = pso.Run(timeLimit);
            TSPSolution solution = new TSPSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);

            return solutions;
        }
	}
}

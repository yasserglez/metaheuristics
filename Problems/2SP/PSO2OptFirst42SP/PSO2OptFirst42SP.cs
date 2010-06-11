using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class PSO2OptFirst42SP
	{

		public static string Algoritmo = "PSO using the NPS heuristic for 2SP";

        public static string[] Integrantes = TeamInfo.Members;

        public static string Nombre_equipo = TeamInfo.Name;

        public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
        {
            TwoSPInstance instance = new TwoSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = (int)Math.Max(10, instance.NumberItems / 3.0);
            double prevConf = 0.5;
            double neighConf = 0.8;
            int[] lowerBounds = new int[instance.NumberItems];
            int[] upperBounds = new int[instance.NumberItems];
            for (int i = 0; i < instance.NumberItems; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberItems - 1;
            }
            DiscretePSO pso = new DiscretePSO2OptFirst42SP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            List<double> solutions = pso.Run(timeLimit);
            int[,] coordinates = TwoSPUtils.NPS2Coordinates(instance, pso.BestPosition);
            TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
            solution.Write(fileOutput);

            return solutions;
        }
    }
}

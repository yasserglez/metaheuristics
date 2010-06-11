using System;
using System.Collections.Generic;

namespace Metaheuristics
{
    public static class PSO42OptBestQAP
    {
        public static string Algoritmo = "PSO with 2-opt (best improvement) local search for QAP";

        public static string[] Integrantes = TeamInfo.Members;

        public static string Nombre_equipo = TeamInfo.Name;

        public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
        {
            QAPInstance instance = new QAPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = 50 * instance.NumberFacilities;
            double prevConf = 0.5;
            double neighConf = 0.8;
            int[] lowerBounds = new int[instance.NumberFacilities];
            int[] upperBounds = new int[instance.NumberFacilities];
            for (int i = 0; i < instance.NumberFacilities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberFacilities - 1;
            }
            DiscretePSO pso = new DiscretePSO2OptBest4QAP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            List<double> solutions = pso.Run(timeLimit);
            QAPSolution solution = new QAPSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);

            return solutions;
        }
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class SA4TSP
	{
		public static string Algoritmo = "SA for TSP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4TSP(instance);
			List<double> solutions = sa.Run(timeLimit);
			TSPSolution solution = new TSPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

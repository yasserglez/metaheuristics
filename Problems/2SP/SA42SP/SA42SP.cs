using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class SA42SP
	{
		public static string Algoritmo = "SA for 2SP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA42SP(instance);
			List<double> solutions = sa.Run(timeLimit);
			int[,] coordinates = TwoSPUtils.NPS2Coordinates(instance, sa.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

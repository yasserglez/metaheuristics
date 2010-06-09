using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class SA4SPP
	{
		public static string Algoritmo = "SA for SPP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4SPP(instance);
			List<double> solutions = sa.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

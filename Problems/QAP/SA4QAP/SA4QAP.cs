using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class SA4QAP
	{
		public static string Algoritmo = "SA for QAP";
		
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4QAP(instance);
			List<double> solutions = sa.Run(timeLimit);
			QAPSolution solution = new QAPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
}

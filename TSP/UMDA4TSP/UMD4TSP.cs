using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMD4TSP
	{
		public static string Algoritmo = "UMDA4TSP";
		
		public static string[] Integrantes = new string[] { "Ariel Hernández", "Yasser González" };
		
		public static string Nombre_equipo = "";
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			return new List<double>();
		}
	}
	
	class DiscreteUMDA4TSP : DiscreteUMDA
	{
		protected override double Fitness(int[] individual)
		{
			return 0;
		}
	}
}

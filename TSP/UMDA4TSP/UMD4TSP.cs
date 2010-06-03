using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMD4TSP
	{
		public static string Algoritmo = "UMDA4TSP";
		
		public static string[] Integrantes = Team.Members;
		
		public static string Nombre_equipo = Team.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberCities;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4TSP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			TSPSolution solution = new TSPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
	
	class DiscreteUMDA4TSP : DiscreteUMDA
	{
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4TSP(TSPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			TSPUtils.Repair(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return TSPUtils.Cost(Instance, individual);
		}
	}
}

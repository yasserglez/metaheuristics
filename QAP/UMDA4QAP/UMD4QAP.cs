using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMD4QAP
	{
		public static string Algoritmo = "UMDA4QAP";
		
		public static string[] Integrantes = Team.Members;
		
		public static string Nombre_equipo = Team.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberFacilities;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4QAP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			QAPSolution solution = new QAPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
	
	class DiscreteUMDA4QAP : DiscreteUMDA
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4QAP(QAPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			QAPUtils.Repair(Instance, individual);		
		}
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Cost(Instance, individual);
		}
	}
}

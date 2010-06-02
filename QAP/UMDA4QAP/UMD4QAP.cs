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
			int popSize = 50 * instance.Dimension;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.Dimension];
			int[] upperBounds = new int[instance.Dimension];
			for (int i = 0; i < instance.Dimension; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.Dimension - 1;
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
			int facilitiesCount = 0;
			bool[] facilitiesUsed = new bool[Instance.Dimension];
			bool[] facilitiesRepeated = new bool[Instance.Dimension];
				
			// Get information to decide if the individual is valid.
			for (int i = 0; i < Instance.Dimension; i++) {
				if (!facilitiesUsed[individual[i]]) {
					facilitiesCount += 1;
					facilitiesUsed[individual[i]] = true;
				}
				else {
					facilitiesRepeated[i] = true;
				}
			}
				
			// If the individual is invalid, make it valid.
			if (facilitiesCount != Instance.Dimension) {
				for (int i = 0; i < facilitiesRepeated.Length; i++) {
					if (facilitiesRepeated[i]) {
						int count = Statistics.RandomDiscreteUniform(1, Instance.Dimension - facilitiesCount);
						for (int f = 0; f < facilitiesUsed.Length; f++) {
							if (!facilitiesUsed[f]) {
								count -= 1;
								if (count == 0) {
									individual[i] = f;
									facilitiesRepeated[i] = false;
									facilitiesUsed[f] = true;
									facilitiesCount += 1;
									break;
								}
							}
						}							
					}
				}
			}			
		}
		
		protected override double Fitness(int[] individual)
		{
			double fitness = 0;
			
			for (int i = 1; i < individual.Length; i++) {
				for (int j = 0; j < individual.Length; j++) {
					fitness += Instance.Distances[i,j] * Instance.Flows[individual[i],individual[j]];
				}
			}
			
			return fitness;
		}
	}
}

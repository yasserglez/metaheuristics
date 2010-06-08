using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GRASP2OptBest4QAP
	{
		public static string Algoritmo = "GRASP with 2-opt (best improvement) local search for QAP";
	
		public static string[] Integrantes = TeamInfo.Members;
		
		public static string Nombre_equipo = TeamInfo.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the GRASP for this instance of the problem.
			// Bigger Threshold benefits greedy over random.
			double rclThreshold = 0.6;
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteGRASP grasp = new DiscreteGRASP2OptBest4QAP(instance, rclThreshold, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = grasp.Run(timeLimit, RunType.TimeLimit);
			QAPSolution solution = new QAPSolution(instance, grasp.BestSolution);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public enum RunType 
	{
		IterationsLimit, TimeLimit
	}

	public abstract class DiscreteGRASP
	{
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public double RCLThreshold { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		
		public int[] BestIndividual { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteGRASP (double rclThreshold, int[] lowerBounds, int[] upperBounds)
		{
			LowerBounds = lowerBounds;
			UpperBounds = upperBounds;
			RCLThreshold = rclThreshold;			
			RepairEnabled = false;
			BestIndividual = null;
			BestFitness = 0;
		}
		
		protected abstract double Fitness(int[] solution);
		
		// Greedy Randomized Construction.
		protected abstract void GRCSolution(int[] solution);
		
		// Local search method.
		protected virtual void LocalSearch(int[] individual)
		{
		}
		
		public List<double> Run(int limit, RunType runType)
		{
			switch (runType) {
			case RunType.IterationsLimit:
				return Run(int.MaxValue, limit);
			case RunType.TimeLimit:
				return Run(limit, int.MaxValue);
			default:
				return null;
			}
		}
		
		public List<double> Run(int timeLimit, int iterationsLimit)
		{
			int startTime = Environment.TickCount;
			int numVariables = LowerBounds.Length;
			List<double> solutions = new List<double>();
			int[] newSolution = new int[numVariables];
			double newFitness = 0;
			int iteration = 0;
			
			GRCSolution(newSolution);
			
			// Run a local search method for each individual in the population.
			LocalSearch(newSolution);
			
			newFitness = Fitness(newSolution);
			
			BestIndividual = newSolution;
			BestFitness = newFitness;		
			
			while (Environment.TickCount - startTime < timeLimit && iteration < iterationsLimit) {
				GRCSolution(newSolution);
				
				// Run a local search method for each individual in the population.
				LocalSearch(newSolution);
				newFitness = Fitness(newSolution);
				
				if (newFitness < BestFitness) {
					BestIndividual = newSolution;
					BestFitness = newFitness;		
				}
				solutions.Add(newFitness);
				iteration++;
			}

			return solutions;
		}
	}
}

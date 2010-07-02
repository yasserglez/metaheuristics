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
		public double RCLThreshold { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		
		public int[] BestSolution { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteGRASP (double rclThreshold)
		{
			RCLThreshold = rclThreshold;			
			RepairEnabled = false;
			BestSolution = null;
			BestFitness = 0;
		}
		
		protected abstract double Fitness(int[] solution);
		
		// Greedy Randomized Construction.
		protected abstract int[] GRCSolution();
		
		// Local search method.
		protected abstract void LocalSearch(int[] solution);
		
		public void Run(int limit, RunType runType)
		{
			switch (runType) {
			case RunType.IterationsLimit:
				Run(int.MaxValue, limit);
				break;
			case RunType.TimeLimit:
				Run(limit, int.MaxValue);
				break;
			default:
				break;
			}
		}
		
		public void Run(int timeLimit, int iterationsLimit)
		{
			int startTime = Environment.TickCount;
			int iterationStartTime = 0;
			int iterationTime = 0;
			int maxIterationTime = 0;			
			int[] newSolution = GRCSolution();
			double newFitness = 0;
			int iteration = 0;
			
			
			
			// Run a local search method for each individual in the population.
			LocalSearch(newSolution);
			
			newFitness = Fitness(newSolution);
			
			BestSolution = newSolution;
			BestFitness = newFitness;		
			
			maxIterationTime = Environment.TickCount - startTime;
			
			while (Environment.TickCount - startTime < timeLimit - maxIterationTime && iteration < iterationsLimit) {
				iterationStartTime = Environment.TickCount;
				newSolution = GRCSolution();
				
				// Run a local search method for each individual in the population.
				LocalSearch(newSolution);
				newFitness = Fitness(newSolution);
				
				if (newFitness < BestFitness) {
					BestSolution = newSolution;
					BestFitness = newFitness;		
				}
				iteration++;
				
				iterationTime = Environment.TickCount - iterationStartTime;
				maxIterationTime = (maxIterationTime < iterationTime) ? iterationTime : maxIterationTime;				
			}
		}
	}
}

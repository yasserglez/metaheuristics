using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteILS
	{
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		public int RestartIterations { get; protected set; }

		public int[] BestSolution { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteILS (int restartIterations, int[] lowerBounds, 
		                    int[] upperBounds)
		{
			RepairEnabled = false;
			RestartIterations = restartIterations;
			UpperBounds = upperBounds;
			LowerBounds = lowerBounds;
			BestSolution = null;
			BestFitness = 0;
		}

		// Evaluate an individual of the population.
		protected abstract double Fitness(int[] individual);

		// Generate the initial solution.
		protected abstract int[] InitialSolution();

		// Perturbate the solution.
		protected abstract void PerturbateSolution(int[] solution, int perturbation);

		// Repairing method to handle constraints.
		protected virtual void Repair(int[] individual)
		{
		}

		// Local search method.
		protected virtual void LocalSearch(int[] individual)
		{
		}
		
		public void Run(int timeLimit)
		{
			int startTime = Environment.TickCount;
			int iterationStartTime = 0;
			int iterationTime = 0;
			int maxIterationTime = 0;
			int perturbation = 2;
			int numVariables = UpperBounds.Length;
			
			double fitness = 0;
	        int lastImprovement = 0;
			int[] newSolution = InitialSolution();
			int[] solution = new int[numVariables];
			
			LocalSearch(newSolution);
			
			fitness = Fitness(newSolution);
			
			BestFitness = fitness;
			BestSolution = new int[numVariables];
			newSolution.CopyTo(BestSolution, 0);
			newSolution.CopyTo(solution, 0);
			
			maxIterationTime = Environment.TickCount - startTime;
			
			while (Environment.TickCount - startTime < timeLimit - maxIterationTime) {
				iterationStartTime = Environment.TickCount;
				
				PerturbateSolution(solution, perturbation);
				
				LocalSearch(solution);
				fitness = Fitness(solution);
				
				if (fitness <= BestFitness) {
					BestFitness = fitness;
					solution.CopyTo(BestSolution, 0);
					lastImprovement = 0;
					perturbation = 2;
				}
				else if (lastImprovement + 1 == RestartIterations) {
					// Restart the algorithm.
					newSolution = InitialSolution();
					newSolution.CopyTo(solution, 0);
					lastImprovement = 0;
					perturbation = 2;
				}
				else {
					perturbation++;
					lastImprovement++;
				}
				
				iterationTime = Environment.TickCount - iterationStartTime;
				maxIterationTime = (maxIterationTime < iterationTime) ? iterationTime : maxIterationTime;				
			}
		}
	}
}

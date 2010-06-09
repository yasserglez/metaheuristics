using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteSA
	{
		public int LevelLength { get; protected set; }
		public double TempReduction { get; protected set; }
		
		public int[] BestSolution { get; protected set; }
		public double BestFitness { get; protected set; }
		
		public DiscreteSA(int levelLength, double tempReduction)
		{
			LevelLength = levelLength;
			TempReduction = tempReduction;
			BestSolution = null;
			BestFitness = 0;
		}
		
		// Generate the initial solution.
		protected abstract int[] InitialSolution();
		
		// Evaluate a solution.
		protected abstract double Fitness(int[] solution);
		
		// Get a new solution in the neighborhood of the given solution.
		protected abstract int[] GetNeighbor(int[] solution);
		
		public List<double> Run(int timeLimit)
		{	
			List<double> solutions = new List<double>();
			int startTime = Environment.TickCount;			
			
			// Generate initial solutions and select the one with the best fitness.
			// This is also used to choose an initial temperature valuel.
			int[] currentSolution = null;
			double currentFitness = 0;
			for (int i = 0; i <= 5; i++) {
				int[] solution = InitialSolution();
				double solutionFitness = Fitness(solution);
				
				if (currentSolution == null || solutionFitness < currentFitness) {
					currentSolution = solution;
					currentFitness = solutionFitness;
				}
				
				solutions.Add(solutionFitness);				
			}
			
			double temperature = Statistics.StandardDeviation(solutions);

			BestSolution = currentSolution;
			BestFitness = currentFitness;

			while (Environment.TickCount - startTime < timeLimit) {
				for (int level = 0; level < LevelLength; level++) {
					int[] newSolution = GetNeighbor(currentSolution);
					double newFitness = Fitness(newSolution);
					double fitnessDiff = newFitness - currentFitness;
					
					if (fitnessDiff <= 0) {
						if (newFitness < BestFitness) {
							BestSolution = newSolution;
							BestFitness = newFitness;
							solutions.Add(BestFitness);
						}
						currentSolution = newSolution;
						currentFitness = newFitness;
					}
					else {
						double u =  Statistics.RandomUniform();
						if (u <= Math.Exp(-fitnessDiff / temperature)) {
							if (newFitness < BestFitness) {
								BestSolution = newSolution;
								BestFitness = newFitness;
								solutions.Add(BestFitness);
							}
							currentSolution = newSolution;
							currentFitness = newFitness;
						}
					}
				}
				
				// Apply a geometric schema by default.
				temperature = TempReduction * temperature;
			}

			return solutions;
		}
	}
}

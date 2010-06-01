using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteUMDA
	{
		public int[] BestIndividual { get; protected set; }
		
		private double bestIndividualFitness;
		
		protected abstract double Fitness(int[] individual);
		
		public List<double> Start(int dimension, int timeLimit, int popSize, double truncFactor)
		{
			int currentTime;
			int[][] population = new int[popSize][];
			int selectedSize = (int) Math.Round(popSize * truncFactor);
			double[] model = new double[dimension];
			double[] evaluation = new double[popSize];
			List<double> solutions = new List<double>();
			
			currentTime = 0;
			
			// Generate the initial random population.
			for (int k = 0; k < popSize; k++) {
				int[] individual = new int[dimension];
				for (int i = 0; i < dimension; i++) {
					individual[i] = Statistics.RandomDiscreteUniform(0, 1);
				}
				population[k] = individual;
			}

			BestIndividual = null;
			while (currentTime < timeLimit) {
				// Evaluate the population.
				for (int k = 0; k < popSize; k++) {
					evaluation[k] = Fitness(population[k]);
				}
				
				// Apply the selection method.
				Array.Sort(evaluation, population);
				if (BestIndividual == null || bestIndividualFitness < evaluation[0]) {
					BestIndividual = population[0];
					bestIndividualFitness = evaluation[0];
					solutions.Add(evaluation[0]);
				}
				
				// Learn the probabilistic model from the selected population
				// (Probability of xi = 1 for all 0 <= i <= dimension - 1).
				for (int i = 0; i < dimension; i++) {
					double numOnes = 0;
					for (int k = 0; k < selectedSize; k++) {
						numOnes += population[k][i];
					}
					model[i] = numOnes / selectedSize;
				}
				
				// Sample the population for the next generation.
				for (int k = 0; k < popSize; k++) {
					int[] individual = new int[dimension];
					for (int i = 0; i < dimension; i++) {
						individual[i] = Statistics.RandomUniform() <= model[i] ? 1 : 0;
					}
					population[k] = individual;
				}
				
				// Prepare for the next generation.
				break;
			}
			
			return solutions;
		}
	}
}

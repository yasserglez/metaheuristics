using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteUMDA
	{
		public int PopulationSize { get; protected set; }
		public double TruncationFactor {get; protected set; }
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		public bool LocalSearchEnabled { get; protected set; }
		
		public int[] BestIndividual { get; protected set; }
		public double BestFitness { get; protected set; }
		
		public DiscreteUMDA(int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
		{
			PopulationSize = popSize;
			TruncationFactor = truncFactor;
			LowerBounds = lowerBounds;
			UpperBounds = upperBounds;
			RepairEnabled = false;
			LocalSearchEnabled = false;
			BestIndividual = null;
			BestFitness = 0;
		}
		
		// Evaluate an individual of the population.
		protected abstract double Fitness(int[] individual);
		
		// Repairing method to handle constraints.
		protected virtual void Repair(int[] individual)
		{
		}
		
		// Local search method.
		protected virtual void LocalSearch(int[] individual)
		{
		}
		
		public List<double> Run(int timeLimit)
		{	
			int startTime = Environment.TickCount;
			int numVariables = LowerBounds.Length;
			int selectedSize = Math.Max(1, (int) Math.Round(TruncationFactor * PopulationSize));
			List<double> solutions = new List<double>();
			int[][] population = new int[PopulationSize][];
			double[] evaluation = new double[PopulationSize];
			double[][] model = new double[numVariables][];
			for (int i = 0; i < numVariables; i++) {
				model[i] = new double[(UpperBounds[i] - LowerBounds[i]) + 1];
			}
			
			// Generate the initial random population.
			for (int k = 0; k < PopulationSize; k++) {
				population[k] = new int[numVariables];
				for (int i = 0; i < numVariables; i++) {
					population[k][i] = Statistics.RandomDiscreteUniform(LowerBounds[i], UpperBounds[i]);
				}
			}

			BestIndividual = null;
			while (Environment.TickCount - startTime < timeLimit) {
				// Handle constraints using a repairing method.
				if (RepairEnabled) {
					for (int k = 0; k < PopulationSize; k++) {
						Repair(population[k]);
					}
				}
				
				// Run a local search method for each individual in the population.
				if (LocalSearchEnabled) {
					for (int k = 0; k < PopulationSize; k++) {
						LocalSearch(population[k]);
					}
				}				
				
				// Evaluate the population.
				for (int k = 0; k < PopulationSize; k++) {
					evaluation[k] = Fitness(population[k]);
				}

				// Apply the selection method.
				Array.Sort(evaluation, population);
				if (BestIndividual == null || evaluation[0] < BestFitness) {
					BestIndividual = population[0];
					BestFitness = evaluation[0];
					solutions.Add(BestFitness);
				}

				// Learn the probabilistic model from the selected population.
				for (int i = 0; i < numVariables; i++) {
					// Set the counters to zero.
					for (int k = 0; k <= UpperBounds[i] - LowerBounds[i]; k++) {
						model[i][k] = 0;
					}
					// Count the values of the variable.
					for (int k = 0; k < selectedSize; k++) {
						model[i][population[k][i] - LowerBounds[i]] += 1;
					}
					// Calculate the frequency of each value of the variable.
					for (int k = 0; k <= UpperBounds[i] - LowerBounds[i]; k++) {
						model[i][k] /= selectedSize;
					}
				}

				// Sample the population for the next generation.
				for (int k = 0; k < PopulationSize; k++) {
					population[k] = new int[numVariables];
					for (int i = 0; i < numVariables; i++) {
						population[k][i] = LowerBounds[i] + Statistics.SampleRoulette(model[i]);
					}
				}
			}

			return solutions;
		}
	}
}

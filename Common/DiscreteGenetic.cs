using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteGenetic
	{
		public int PopulationSize { get; protected set; }
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		public bool LocalSearchEnabled { get; protected set; }
		public double MutationProbability { get; protected set; } 
		
		public int[] BestIndividual { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteGenetic (int popSize, int[] lowerBounds, int[] upperBounds, 
		                        double mutatuionProbability)
		{
			PopulationSize = popSize;
			LowerBounds = lowerBounds;
			UpperBounds = upperBounds;
			RepairEnabled = false;
			LocalSearchEnabled = false;
			BestIndividual = null;
			BestFitness = 0;
			MutationProbability = mutatuionProbability;
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
			List<double> solutions = new List<double>();
			int[][] population = new int[PopulationSize][];
			double[] evaluation = new double[PopulationSize];
			
			int[] parent1 = null;
			int[] parent2 = null;
			int[] descend1 = null;
			int[] descend2 = null;
			bool[] crossMask = new bool[numVariables];
			bool[] mutMask = new bool[numVariables];
			
			// Generate the initial random population.
			for (int k = 0; k < PopulationSize; k++) {
				population[k] = new int[numVariables];
				for (int i = 0; i < numVariables; i++) {
					population[k][i] = Statistics.RandomDiscreteUniform(LowerBounds[i], UpperBounds[i]);
				}
			}

			BestIndividual = null;
			while (Environment.TickCount - startTime < timeLimit) {
				int[][] newPopulation = new int[PopulationSize][];
				
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
				
				// Crossover's and Mutation's masks.
				double mutMaskProbability = 1/numVariables;
				for (int i = 0; i < numVariables; i++) {
					crossMask[i] = Statistics.RandomUniform() < 0.5;
					mutMask[i] = Statistics.RandomUniform() < mutMaskProbability;
				}
				// Ensures that, at least, it will make a 1X 
				// crossover and mutation.
				crossMask[Statistics.RandomDiscreteUniform(0, numVariables-1)] = true;
				mutMask[Statistics.RandomDiscreteUniform(0, numVariables-1)] = true;
				
				for (int i = 0; i < PopulationSize/2; i++) {
					// Select by four individual's Tournament.
					parent1 = population[Math.Min(Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1), 
				 	                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)),
				                                  Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1),
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)))];
					parent2 = population[Math.Min(Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1), 
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)),
					                              Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1),
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)))];
					// Crossover UX.
					for (int j = 0; j < numVariables; j++) {
						if (crossMask[j]) {
							descend1[j] = parent2[j];
							descend2[j] = parent1[j];
						}
						else {
							descend1[j] = parent1[j];
							descend2[j] = parent2[j];
						}
					}
					
					// Mutation.
					if (Statistics.RandomUniform() < MutationProbability) {
						for (int j = 0; j < numVariables; j++) {
							if (mutMask[j]) {
								descend1[j] = Statistics.RandomDiscreteUniform(LowerBounds[j], UpperBounds[j]);									
								descend2[j] = Statistics.RandomDiscreteUniform(LowerBounds[j], UpperBounds[j]);
							}
						}
					}
					newPopulation[i] = descend1;
					newPopulation[i+PopulationSize/2] = descend2;
				}
				
				population = newPopulation;
			}

			return solutions;
		}
	}
}

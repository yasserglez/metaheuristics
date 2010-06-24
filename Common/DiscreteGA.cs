using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteGA
	{
		public int PopulationSize { get; protected set; }
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		public bool LocalSearchEnabled { get; protected set; }
		public double MutationProbability { get; protected set; } 
		
		public int[] BestIndividual { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteGA (int popSize, double mutationProbability, int[] lowerBounds, int[] upperBounds)
		{
			PopulationSize = popSize;
			LowerBounds = lowerBounds;
			UpperBounds = upperBounds;
			RepairEnabled = false;
			LocalSearchEnabled = false;
			BestIndividual = null;
			BestFitness = 0;
			MutationProbability = mutationProbability;
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
		
		public void Run(int timeLimit)
		{	
			int startTime = Environment.TickCount;
			int numVariables = LowerBounds.Length;
			int[][] population = new int[PopulationSize][];
			double[] evaluation = new double[PopulationSize];
			
			int[] parent1 = null;
			int[] parent2 = null;
			int[] descend1 = null;
			int[] descend2 = null;
			bool[] crossMask = new bool[numVariables];
			bool[] mutMask = new bool[numVariables];
			int[][] iterationPopulation = new int[PopulationSize][];
			double[] iterationEvaluation = new double[PopulationSize];
			int[][] newPopulation = null;
			double[] newEvaluation = null;
			
			// Generate the initial random population.
			for (int k = 0; k < PopulationSize; k++) {
				population[k] = new int[numVariables];
				for (int i = 0; i < numVariables; i++) {
					population[k][i] = Statistics.RandomDiscreteUniform(LowerBounds[i], UpperBounds[i]);
				}
			}
			
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
			Array.Sort(evaluation, population);
			
			BestIndividual = population[0];
			BestFitness = evaluation[0];
			
			while (Environment.TickCount - startTime < timeLimit) {
				newPopulation = new int[PopulationSize][];
				newEvaluation = new double[PopulationSize];

				// Apply the selection method.
				if (BestIndividual == null || evaluation[0] < BestFitness) {
					BestIndividual = population[0];
					BestFitness = evaluation[0];
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
					descend1 = new int[numVariables];
					descend2 = new int[numVariables];
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
					iterationPopulation[i] = descend1;
					iterationPopulation[i+PopulationSize/2] = descend2;
				}
				
				// Handle constraints using a repairing method.
				if (RepairEnabled) {
					for (int k = 0; k < PopulationSize; k++) {
						Repair(iterationPopulation[k]);
					}
				}
				
				// Run a local search method for each individual in the population.
				if (LocalSearchEnabled) {
					for (int k = 0; k < PopulationSize; k++) {
						LocalSearch(iterationPopulation[k]);
					}
				}				
				
				// Evaluate the population.
				for (int k = 0; k < PopulationSize; k++) {
					iterationEvaluation[k] = Fitness(iterationPopulation[k]);
				}
				Array.Sort(iterationEvaluation, iterationPopulation);
				
				// Merge the new population with the existing.
				int iterationIndex = 0;
				int existingIndex = 0;
				for (int k = 0; k < PopulationSize; k++) {
					if (evaluation[existingIndex] < iterationEvaluation[iterationIndex]) {
						newPopulation[k] = population[existingIndex];
						newEvaluation[k] = evaluation[existingIndex];
						existingIndex++;
					}
					else {
						newPopulation[k] = iterationPopulation[iterationIndex];
						newEvaluation[k] = iterationEvaluation[iterationIndex];
						iterationIndex++;
					}
				}
				
				population = newPopulation;
				evaluation = newEvaluation;
			}
		}
	}
}

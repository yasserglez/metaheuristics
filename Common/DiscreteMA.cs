using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteMA
	{
		public int PopulationSize { get; protected set; }
		public int[] LowerBounds { get; protected set; }
		public int[] UpperBounds { get; protected set; }
		public bool RepairEnabled { get; protected set; }
		public bool LocalSearchEnabled { get; protected set; }
		public double MutationProbability { get; protected set; } 
		
		public int[] BestIndividual { get; protected set; }
		public double BestFitness { get; protected set; }

		public DiscreteMA (int popSize, double mutationProbability, int[] lowerBounds, int[] upperBounds)
		{
			PopulationSize = popSize + (popSize % 2);
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
		
		// Generate the initial solution.
		protected abstract int[] InitialSolution();
		
		// Generate the initial meme.
		protected bool[] InitialMeme()
		{
			bool[] meme = new bool[LowerBounds.Length];
			int points = Statistics.RandomDiscreteUniform(LowerBounds.Length / 2, (2*LowerBounds.Length) / 3);
			for (int i = 0; i < points; i++) {
				meme[Statistics.RandomDiscreteUniform(0, LowerBounds.Length)] = true;
			}
			return meme;
		}
		
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
			int numVariables = LowerBounds.Length;
			KeyValuePair<int[], bool[]>[] population = new KeyValuePair<int[], bool[]>[PopulationSize];
			double[] evaluation = new double[PopulationSize];
			
			int parent1 = 0;
			int parent2 = 0;
			KeyValuePair<int[], bool[]> descend = new KeyValuePair<int[], bool[]>(null,null);
			KeyValuePair<int[], bool[]>[] iterationPopulation = new KeyValuePair<int[], bool[]>[PopulationSize];
			double[] iterationEvaluation = new double[PopulationSize];
			KeyValuePair<int[], bool[]>[] newPopulation = null;
			double[] newEvaluation = null;
			
			// Generate the initial random population.
			for (int k = 0; k < PopulationSize; k++) {
				population[k] = new KeyValuePair<int[], bool[]>(InitialSolution(),InitialMeme());
			}
			
			// Run a local search method for each individual in the population.
			if (LocalSearchEnabled) {
				for (int k = 0; k < PopulationSize; k++) {
					LocalSearch(population[k].Key);
				}
			}				
			
			// Evaluate the population.
			for (int k = 0; k < PopulationSize; k++) {
				evaluation[k] = Fitness(population[k].Key);
			}
			Array.Sort(evaluation, population);
			
			BestIndividual = population[0].Key;
			BestFitness = evaluation[0];
			
			maxIterationTime = Environment.TickCount - startTime;				
			while (Environment.TickCount - startTime < timeLimit - maxIterationTime) {
				
				iterationStartTime = Environment.TickCount;
				newPopulation = new KeyValuePair<int[], bool[]>[PopulationSize];
				newEvaluation = new double[PopulationSize];

				// Apply the selection method.
				if (BestIndividual == null || evaluation[0] < BestFitness) {
					BestIndividual = population[0].Key;
					BestFitness = evaluation[0];
				}
				
				// Mutation's points.
				int mut1stPoint = Statistics.RandomDiscreteUniform(0, numVariables - 1);		
				int mut2ndPoint = Statistics.RandomDiscreteUniform(0, numVariables - 1);		
				 
				for (int i = 0; i < PopulationSize; i++) {
					// Select by four individual's Tournament.
					parent1 = Math.Min(Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1), 
				 	                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)),
				                                  Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1),
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)));
					parent2 = Math.Min(Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1), 
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)),
					                              Math.Min(Statistics.RandomDiscreteUniform(0,PopulationSize-1),
					                                       Statistics.RandomDiscreteUniform(0,PopulationSize-1)));
					if (parent1 > parent2) {
						int tmp = parent1;
						parent1 = parent2;
						parent2 = tmp;
					}
					// Crossover with the meme of the best parent.
					descend = new KeyValuePair<int[], bool[]>(new int[numVariables], population[parent1].Value);
					for (int j = 0; j < numVariables; j++) {
						if (descend.Value[j]) {
							descend.Key[j] = population[parent1].Key[j];
						}
						else {
							descend.Key[j] = population[parent1].Key[j];
						}
					}
					
					// Mutation.
					if (Statistics.RandomUniform() < MutationProbability) {
						descend.Key[mut1stPoint] = Statistics.RandomDiscreteUniform(LowerBounds[mut1stPoint], UpperBounds[mut1stPoint]);									
						descend.Key[mut2ndPoint] = Statistics.RandomDiscreteUniform(LowerBounds[mut2ndPoint], UpperBounds[mut2ndPoint]);									
					}
					iterationPopulation[i] = descend;
				}
				
				// Handle constraints using a repairing method.
				if (RepairEnabled) {
					for (int k = 0; k < PopulationSize; k++) {
						Repair(iterationPopulation[k].Key);
					}
				}
				
				// Run a local search method for each individual in the population.
				if (LocalSearchEnabled && 
				    Environment.TickCount - startTime < timeLimit) {
					for (int k = 0; k < PopulationSize; k++) {
						LocalSearch(iterationPopulation[k].Key);
					}
				}				
				
				// Evaluate the population.
				for (int k = 0; k < PopulationSize; k++) {
					iterationEvaluation[k] = Fitness(iterationPopulation[k].Key);
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
				
				iterationTime = Environment.TickCount - iterationStartTime;
				maxIterationTime = (maxIterationTime < iterationTime) ? iterationTime : maxIterationTime;				
			}
		}
		
	}
}

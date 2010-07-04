using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class MaxMinAntSystem
	{
		protected int tourLength;
		protected double tauMin;
		protected double tauMax;
		protected int numAnts;
		protected double rho;
		protected double alpha;
		protected double beta;
		protected int maxReinit;
		
		public int[] BestSolution { get; protected set; }
		public double BestFitness { get; protected set; }		
		
		public MaxMinAntSystem(int tourLength, double tau, int numAnts, double rho, double alpha, double beta, int maxReinit)
		{
			this.tourLength = tourLength;
			this.tauMax = tau;
			this.tauMin = (this.tauMax * (1 - Math.Pow(0.05, 1.0 / tourLength))) / (tourLength - 1);
			this.numAnts = numAnts;
			this.rho = rho;
			this.alpha = alpha;
			this.beta = beta;
			this.maxReinit = maxReinit;
		}
		
		protected abstract double Fitness(int[] solution);
		
		protected abstract void InitializeHeuristic(double[,] heuristic);
		
		protected abstract List<int> FactibleNeighbors(int i, bool[] visited);
		
		public virtual void LocalSearch(int[] solution)
		{
		}
		
		public void Run(int timeLimit)
		{
			int startTime = Environment.TickCount;
			int iterStartTime = 0;
			int iterTime = 0;
			int maxIterTime = 0;
			
			BestSolution = null;
			BestFitness = double.MaxValue;
			
			// Initialization.
			int iter = 0;
			int reinitCount = 0;
			bool bestImproved = false;
			int[] bestTour = null;
			double bestTourFitness = double.MaxValue;
			int[] bestIterTour = null;
			double bestIterTourFitness = double.MaxValue;
			double[,] pheromone = new double[tourLength,tourLength];
			double[,] heuristic = new double[tourLength,tourLength];
			
			InitializeHeuristic(heuristic);
			InitializePheromone(pheromone);
			
			while (Environment.TickCount - startTime < timeLimit - maxIterTime) {
				iterStartTime = Environment.TickCount;
				
				// Reinitialization of pheromone.
				if (reinitCount >= maxReinit) {
					InitializePheromone(pheromone);
				}
				
				// Moving the ants to generate solutions.
				bestImproved = false;
				bestIterTourFitness = double.MaxValue;
				for (int i = 0; i < numAnts; i++) {
					int[] antTour = AntActivity(pheromone, heuristic);
					LocalSearch(antTour);
					double antTourFitness = Fitness(antTour);
					if (antTourFitness < bestTourFitness) {
						bestImproved = true;
						bestTour = antTour;
						bestTourFitness = antTourFitness;
						tauMax = bestTourFitness;
						tauMin = (tauMax * (1 - Math.Pow(0.05, 1.0 / tourLength))) / (tourLength - 1);
					}
					if (antTourFitness < bestIterTourFitness) {
						bestIterTour = antTour;
						bestIterTourFitness = antTourFitness;
					}
				}
				reinitCount = bestImproved ? 0 : (reinitCount + 1);
				PheromoneUpdate(iter, pheromone, bestTour, bestIterTourFitness, bestIterTour, bestIterTourFitness);
				
				iterTime = Environment.TickCount - iterStartTime;
				maxIterTime = (int) Math.Max(maxIterTime, iterTime);				
				iter++;
			}
			
			BestSolution = bestTour;
			BestFitness = bestTourFitness;
		}
		
		protected void InitializePheromone(double[,] pheromone)
		{
			for (int i = 0; i < tourLength; i++) {
				for (int j = 0; j < tourLength; j++) {
					pheromone[i,j] = (i == j) ? 0 : tauMax;
				}
			}
		}
		
		protected void PheromoneUpdate(int iter, double[,] pheromone, int[] bestTour, double bestTourFitness, int[] bestIterTour, double bestIterTourFitness)
		{
			// Evaporate.
			for (int i = 0; i < tourLength; i++) {
				for (int j = 0; j < tourLength; j++) {
					pheromone[i,j] = Math.Max((1 - rho) * pheromone[i,j], tauMin);;
					pheromone[j,i] = pheromone[i,j];
				}
			}
			// Select the where the pheromone will be deposited.
			int[] selectedTour = null;
			double selectedTourFitness = double.MaxValue;
			if (iter > 250 ||
			    (iter >= 26 && iter <= 75 && (iter - 26) % 5 == 0) ||
			    (iter >= 76 && iter <= 125 && (iter - 76) % 3 == 0) ||
			    (iter >= 126 && iter <= 250 && (iter - 126) % 2 == 0)) {
				selectedTour = bestTour;
				selectedTourFitness = bestIterTourFitness;
			}
			else {
				selectedTour = bestIterTour;
				selectedTourFitness = bestIterTourFitness;
			}
			// Deposit pheromone on the selected tour.
			double delthaTau = (1.0 / selectedTourFitness);
			for (int k = 0; k < tourLength - 1; k++) {
				int i = selectedTour[k];
				int j = selectedTour[k + 1];
				pheromone[i,j] = Math.Min(pheromone[i,j] + delthaTau, tauMax);
				pheromone[j,i] = pheromone[i,j];
			}
		}
		
		protected int[] AntActivity(double[,] pheromone, double[,] heuristic)
		{
			int[] tour = new int[tourLength];
			bool[] visited = new bool[tourLength];
			int selectedNeighbor;
			double[] probabilities;
			List<int> neighbors;
			double denominator;
			
			// Select the initial vertex randomly.
			tour[0] = Statistics.RandomDiscreteUniform(0, tourLength-1);
			visited[tour[0]] = true;
			
			// Complete the rest of the tour.
			for (int i = 1; i < tourLength; i++) {
				neighbors = FactibleNeighbors(tour[i-1], visited);
				
				denominator = 0;
				for (int j = 0; j < neighbors.Count; j++) {
					denominator +=  (Math.Pow(pheromone[tour[i-1],neighbors[j]], alpha) * 
					                 Math.Pow(heuristic[tour[i-1],neighbors[j]], beta));
				}
				
				probabilities = new double[neighbors.Count];
				for (int j = 0; j < neighbors.Count; j++) {
					probabilities[j] = (Math.Pow(pheromone[tour[i-1],neighbors[j]], alpha) *
					                    Math.Pow(heuristic[tour[i-1],neighbors[j]], beta)) / denominator;
				}

				selectedNeighbor = neighbors[Statistics.SampleRoulette(probabilities)];

				visited[selectedNeighbor] = true;
				tour[i] = selectedNeighbor;
			}
			
			return tour;
		}
	}
}

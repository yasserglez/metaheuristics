using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public abstract class DiscreteSS
	{
		public int PoolSize { get; protected set; }
		public int RefSetSize { get; protected set; }
		public double ExplorationFactor { get; protected set; }
		
		public int[] BestSolution { get; protected set; }
		public double BestFitness { get; protected set; }		

		public DiscreteSS(int poolSize, int refSetSize, double explorationFactor)
		{
			PoolSize = poolSize;
			RefSetSize = refSetSize;
			ExplorationFactor = explorationFactor;
			BestSolution = null;
			BestFitness = 0;
		}

		// Calculate the distance between two given solutions. This distance
		// can be any non-negative value but it should be zero if and only
		// if the both are the same solution.
		protected abstract double Distance(int[] a, int[] b);

		// Generate a random solution.
		protected abstract int[] RandomSolution();

		// Improvement methods (repairing or local optimization)
		protected abstract void Repair(int[] solution);
		
		protected virtual void Improve(int[] solution)
		{
		}
		
		protected abstract double Fitness(int[] solution);		
		
		protected double Distance(int[] solution, int[][] others)
		{
			List<double> dists = new List<double>();
			
			for (int i = 0; i < others.Length; i++) {
				dists.Add(Distance(solution, others[i]));
			}
			
			return dists.Sum() / dists.Count();
		}
		
		protected int[] Combine(int[] a, int[] b)
		{
			int point = Statistics.RandomDiscreteUniform(0, a.Length - 1);
			int[] result = new int[a.Length];
			for (int i = 0; i < a.Length; i++) {
				result[i] = (i <= point) ? a[i] : b[i];
			}
			Repair(result);
			return result;
		}
		
		protected int[][] GenerateP()
		{
			int[][] P = new int[PoolSize][];
			bool generated;
			int[] currentSolution = null;
			int currentIndex;
			
			currentIndex = 0;
			while (currentIndex < PoolSize) {
				generated = false;
				while (!generated) {
					currentSolution = RandomSolution();
					Improve(currentSolution);
					if (currentIndex > 0) {
						for (int i = 0; i < currentIndex; i++) {
							if (Distance(currentSolution, P[i]) == 0) {
								generated = false;
								break;
							}
							generated = true;
						}
					}
					else {
						generated = true;
					}
				}
				P[currentIndex] = currentSolution;
				currentIndex++;
			}
			
			return P;
		}
		
		public void Run(int timeLimit)
		{
			int startTime = Environment.TickCount;
			int iterationStartTime = 0;
			int iterationTime = 0;
			int maxIterationTime = 0;			
			int[][] P, R1, R2;
			double[] PFitness, PDistance, R1Fitness, R2Distance;
			List<int[]> combinations = new List<int[]>();
			int[] a, b;

			// Generating the pool of initial solutions.
			P = GenerateP();
			PFitness = new double[P.Length];
			PDistance = new double[P.Length];
			
			// Build the reference set.
			R1 = new int[(int) Math.Ceiling((1.0 - ExplorationFactor) * RefSetSize)][];
			R1Fitness = new double[R1.Length];
			for (int i = 0; i < PFitness.Length; i++) {
				PFitness[i] = Fitness(P[i]);
			}
			Array.Sort(PFitness, P);
			for (int i = 0; i < R1.Length; i++) {
				R1[i] = P[i];
				R1Fitness[i] = PFitness[i];
			}			
			BestFitness = R1Fitness[0];
			BestSolution = R1[0];
			R2 = new int[(int) Math.Ceiling(ExplorationFactor * RefSetSize)][];
			R2Distance = new double[R2.Length];
			for (int i = 0; i < PDistance.Length; i++) {
				PDistance[i] = -Distance(P[i], R1);
			}
			Array.Sort(PDistance, P);
			for (int i = 0; i < R2.Length; i++) {
				R2[i] = P[i];
				R2Distance[i] = PDistance[i];
			}
				
			while (Environment.TickCount - startTime < timeLimit - maxIterationTime) {
				iterationStartTime = Environment.TickCount;
				
				// Update the reference set (static).
				combinations.Clear();
				for (int i = 0; i < R1.Length + R2.Length; i++) {
					if (i < R1.Length) {
						a = R1[i];
					}
					else {
						a = R2[i - R1.Length];
					}
					for (int j = 0; j < i; j++) {
						if (j < R1.Length) {
							b = R1[j];
						}
						else {
							b = R2[j - R1.Length];
						}
						combinations.Add(Combine(a, b));
					}
				}
				foreach (int[] combined in combinations) {
					int[] improved = combined.ToArray();
					Improve(improved);
					double improvedFitness = Fitness(improved);
					if (improvedFitness < R1Fitness[R1Fitness.Length - 1]) {
						R1[R1Fitness.Length - 1] = improved;
						R1Fitness[R1Fitness.Length  - 1] = improvedFitness;
						Array.Sort(R1Fitness, R1);
						if (R1Fitness[0] < BestFitness) {
							BestFitness = R1Fitness[0];
							BestSolution = R1[0];
						}
					}
				}
				foreach (int[] combined in combinations) {
					double combinedDistance = -Distance(combined, R1);
					if (combinedDistance < R2Distance[R2Distance.Length - 1]) {
						R2[R2Distance.Length - 1] = combined;
						R2Distance[R2Distance.Length - 1] = combinedDistance;
						Array.Sort(R2Distance, R2);
					}
				}				
				
				iterationTime = Environment.TickCount - iterationStartTime;
				maxIterationTime = (int) Math.Max(maxIterationTime, iterationTime);
			}
		}
	}
}

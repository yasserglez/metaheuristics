using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class Tuple
    {
        public Tuple(int val1, int val2)
        {
            Val1 = val1;
            Val2 = val2;
        }
        public int Val1 { get; set; }
        public int Val2 { get; set; }
    }

    public abstract class DiscretePSO
    {
        public int ParticlesCount { get; protected set; }
        public int[] LowerBounds { get; protected set; }
        public int[] UpperBounds { get; protected set; }
        // Best Previous Position Confidence.
        public double PreviousConfidence { get; protected set; }
        // Best Neighbour Position Confidence.
        public double NeighbourConfidence { get; protected set; }

        public bool RepairEnabled { get; protected set; }
        public bool LocalSearchEnabled { get; protected set; }

        public int[] BestPosition { get; protected set; }
        public double BestFitness { get; protected set; }

        public DiscretePSO(int partsCount, double prevConf, double neighConf, int[] lowerBounds, int[] upperBounds)
		{
            ParticlesCount = partsCount;
			LowerBounds = lowerBounds;
			UpperBounds = upperBounds;
            PreviousConfidence = prevConf;
            NeighbourConfidence = neighConf;
			RepairEnabled = false;
			LocalSearchEnabled = false;
			BestPosition = null;
			BestFitness = 0;
		}

        // Evaluate an individual of the population.
        protected abstract double Fitness(int[] individual);

        // Velocity "plus" Velocity -> Velocity
        protected virtual List<Tuple> PlusVelocity(List<Tuple> vel1, List<Tuple> vel2)
        {
            List<Tuple> result = new List<Tuple>(vel1);
            result.AddRange(vel2);
            return result;
        }

        // Position "minus" Position -> Velocity
        protected virtual List<Tuple> MinusPosition(int[] pos1, int[] pos2)
        {
            List<Tuple> result = new List<Tuple>();

            for (int i = 0; i < pos1.Length; i++) {
                if (pos1[i] != pos2[i]) {
                    result.Add(new Tuple(pos1[i], pos2[i]));
                }
            }

            return result;
        }

        // Position "plus" Velocity -> Position
        protected virtual int[] Move(int[] pos, List<Tuple> vel)
        {
            int[] result = new int[pos.Length];
            pos.CopyTo(result, 0);
            foreach (Tuple item in vel) {
                for (int i = 0; i < pos.Length; i++) {
                    if (result[i] == item.Val1) {
                        result[i] = item.Val2;
                    }
                    else if (result[i] == item.Val2) {
                        result[i] = item.Val1;
                    }
                }
            }
            return result;
        }

        // Scalar "times" Velocity -> Velocity
        protected virtual List<Tuple> Times( double scalar, List<Tuple> vel)
        {
            List<Tuple> result = new List<Tuple>();
            if (scalar == 0) {
                return result;
            }
            else if (scalar <= 1) {
                for (int i = 0; i < (int)(scalar*(double)(vel.Count)); i++) {
                    result.Add(vel[i]);
			    }
        	}
            else {
                int floor = (int)(Math.Floor(scalar));
                for (int i = 0; i < floor; i++) {
                    result.AddRange(vel);
			    }
                for (int i = 0; i < (int)((scalar - (double)floor)*(double)(vel.Count)); i++) {
			        result.Add(vel[i]);
			    }              
            }
            return result;
        }

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
            int[][] partBestPrevPosition = new int[ParticlesCount][];
            int[][] partPosition = new int[ParticlesCount][];
            List<Tuple>[] partVelocitys = new List<Tuple>[ParticlesCount];
            double[] partBestPrevFitness = new double[ParticlesCount];

            double rp = 1;
            double rg = 1;

            // Generate the initial random positions.
            for (int k = 0; k < ParticlesCount; k++) {
                partPosition[k] = new int[numVariables];
                partVelocitys[k] = new List<Tuple>();
                partVelocitys[k].Add(new Tuple(Statistics.RandomDiscreteUniform(LowerBounds[0], UpperBounds[0]),
                                               Statistics.RandomDiscreteUniform(LowerBounds[1], UpperBounds[1])));
                for (int i = 0; i < numVariables; i++) {
                    partPosition[k][i] = Statistics.RandomDiscreteUniform(LowerBounds[i], UpperBounds[i]);
                }
                partBestPrevPosition[k] = partPosition[k];
            }

            BestPosition = null;

            // Handle constraints using a repairing method.
            if (RepairEnabled) {
                for (int k = 0; k < ParticlesCount; k++) {
                    Repair(partPosition[k]);
                }
            }

            // Run a local search method for each individual in the population.
            if (LocalSearchEnabled) {
                for (int k = 0; k < ParticlesCount; k++) {
                    LocalSearch(partPosition[k]);
                }
            }

            // Evaluate the population.
            partBestPrevFitness[0] = Fitness(partPosition[0]);
            BestFitness = partBestPrevFitness[0];
            BestPosition = partPosition[0];
            for (int k = 1; k < ParticlesCount; k++) {
                partBestPrevFitness[k] = Fitness(partPosition[k]);
                if (partBestPrevFitness[k] < BestFitness) {
                    BestFitness = partBestPrevFitness[k];
                    BestPosition = partPosition[k];
                }
            }

            while (Environment.TickCount - startTime < timeLimit) {
                for (int i = 0; i < ParticlesCount; i++) {
                    rg = Statistics.RandomUniform();
                    rp = Statistics.RandomUniform();

                    List<Tuple> vel = PlusVelocity(partVelocitys[i], 
                                                   PlusVelocity(Times(rp*PreviousConfidence, MinusPosition(partBestPrevPosition[i], partPosition[i])), 
                                                                Times(rg*NeighbourConfidence, MinusPosition(BestPosition, partPosition[i]))));
                    partPosition[i] = Move(partPosition[i], vel);
                }

                // Run a local search method for the position of each particle.
                if (LocalSearchEnabled) {
                    for (int k = 0; k < ParticlesCount; k++) {
                        LocalSearch(partPosition[k]);
                    }
                }

                for (int k = 0; k < ParticlesCount; k++) {
                    double fitness = Fitness(partPosition[k]);
                    if (partBestPrevFitness[k] > fitness ) {
                        partBestPrevFitness[k] = fitness;
                        partBestPrevPosition[k] = partPosition[k];
                    }
                    if (BestFitness > fitness) {
                        BestFitness = fitness;
                        BestPosition = partPosition[k];
                    }
                }
            }

            return solutions;
        }
    }
}

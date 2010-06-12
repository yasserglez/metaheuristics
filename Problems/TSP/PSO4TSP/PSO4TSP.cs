using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class PSO4TSP : IMetaheuristic
	{
        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            TSPInstance instance = new TSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = 50 * instance.NumberCities;
            double prevConf = 0.5;
            double neighConf = 0.8;
            int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
            DiscretePSO pso = new DiscretePSO4TSP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);
            
            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit);
            TSPSolution solution = new TSPSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);
        }
        
        public string Name {
        	get {
        		return "PSO for TSP";
        	}
        }
        
        public MetaheuristicType Type {
        	get {
        		return MetaheuristicType.PSO;
        	}
        }
        
        public ProblemType Problem {
        	get {
        		return ProblemType.TSP;
        	}
        }
        
        public string[] Team {
        	get {
        		return About.Team;
        	}
        }
	}
}

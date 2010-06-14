using System;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class PSO42SP : IMetaheuristic
    {
        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            TwoSPInstance instance = new TwoSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = (int)Math.Max(10, instance.NumberItems / 3.0);
            double prevConf = 0.5;
            double neighConf = 0.8;
            int[] lowerBounds = new int[instance.NumberItems];
            int[] upperBounds = new int[instance.NumberItems];
            for (int i = 0; i < instance.NumberItems; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberItems - 1;
            }
            DiscretePSO pso = new DiscretePSONPS42SP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit);
            int[,] coordinates = TwoSPUtils.NPS2Coordinates(instance, pso.BestPosition);
            TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
            solution.Write(fileOutput);
        }
        
        public string Name {
        	get {
        		return "PSO using the NPS heuristic for 2SP";
        	}
        }
        
        public MetaheuristicType Type {
        	get {
        		return MetaheuristicType.PSO;
        	}
        }
        
        public ProblemType Problem {
        	get {
        		return ProblemType.TwoSP;
        	}
        }

        public string[] Team {
        	get {
        		return About.Team;
        	}
        }
    }
}
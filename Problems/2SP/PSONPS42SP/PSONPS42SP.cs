using System;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class PSONPS42SP : IMetaheuristic, ITunableMetaheuristic
    {
		protected double timePenalty = 250;
		protected double particlesCount = 50;
		protected double prevConf = 0.5;
        protected double neighConf = 0.8;
		
        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            TwoSPInstance instance = new TwoSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int[] lowerBounds = new int[instance.NumberItems];
            int[] upperBounds = new int[instance.NumberItems];
            for (int i = 0; i < instance.NumberItems; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberItems - 1;
            }
            DiscretePSO pso = new DiscretePSONPS42SP(instance, (int)particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit - (int)timePenalty);
            int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, pso.BestPosition);
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
		
		public void UpdateParameters (double[] parameters)
        {
        	timePenalty = parameters[0];
			particlesCount = parameters[1];
			prevConf = parameters[2];
			neighConf = parameters[3];
        }
    }
}
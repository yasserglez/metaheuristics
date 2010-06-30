using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class PSO2OptFirst4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 250;
		protected double popFactor = 0.25;
		protected double prevConf = 0.75;
        	protected double neighConf = 0.75;
		
        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            TSPInstance instance = new TSPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int particlesCount = (int)(popFactor * instance.NumberCities);
            int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
            DiscretePSO pso = new DiscretePSO2OptFirst4TSP(instance, particlesCount, prevConf, neighConf, lowerBounds, upperBounds);
            
            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit - (int)timePenalty);
            TSPSolution solution = new TSPSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);
        }
        
        public string Name {
        	get {
        		return "PSO with 2-opt (first improvement) local search for TSP";
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
		
		public void UpdateParameters (double[] parameters)
        {
        	timePenalty = parameters[0];
			popFactor = parameters[1];
			prevConf = parameters[2];
			neighConf = parameters[3];
        }
	}
}

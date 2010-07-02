using System;
using System.Collections.Generic;

namespace Metaheuristics
{
    public class PSO4QAP : IMetaheuristic, ITunableMetaheuristic
    {
	protected double timePenalty = 250;
	protected double particlesCount = 1.5;
	protected double prevConf = 0.75;
        protected double neighConf = 0.75;
		
        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            QAPInstance instance = new QAPInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int[] lowerBounds = new int[instance.NumberFacilities];
            int[] upperBounds = new int[instance.NumberFacilities];
            for (int i = 0; i < instance.NumberFacilities; i++) {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberFacilities - 1;
            }
            DiscretePSO pso = new DiscretePSO4QAP(instance, (int)particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit - (int)timePenalty);
            QAPSolution solution = new QAPSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);
        }
        
        public string Name {
        	get {
        		return "PSO for QAP";
        	}
        }
        
        public MetaheuristicType Type {
        	get {
        		return MetaheuristicType.PSO;
        	}
        }
        
        public ProblemType Problem {
        	get {
        		return ProblemType.QAP;
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

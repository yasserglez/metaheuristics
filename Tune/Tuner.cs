using System;
using System.IO;
using System.Collections.Generic;

using Metaheuristics;

namespace Tune
{
	public abstract class Tuner
	{
		public ITunableMetaheuristic Metaheuristic { get; protected set; }
		
		public string[] InputFiles { get; protected set; }
		
		public int RunsPerProblem { get; protected set; }
		
		public int[] TimeLimits { get; protected set; }
		
		public int TimeTolerance { get; protected set; }
		 
		public Tuner(ITunableMetaheuristic metaheuristic, string dirInstances, int runsPerProblem, int[] timeLimits, int timeTolerance)
		{
			Metaheuristic = metaheuristic;
			InputFiles = Directory.GetFiles(dirInstances, "*.in", SearchOption.AllDirectories);
			RunsPerProblem = runsPerProblem;
			TimeLimits = timeLimits;
			TimeTolerance = timeTolerance;
		}
		
		public void Start(string logFile)
		{
			StreamWriter log = File.CreateText(logFile);
			double[] bestParameters = null;
			double bestCost = double.PositiveInfinity;
			double currentCost;
			double currentFitness;
			List<double> means = new List<double>();
			List<double> fitness = new List<double>();
			
			log.AutoFlush = true;
			log.WriteLine("Parameters tuning for " + Metaheuristic.Name + ".");
			log.WriteLine();
			
			foreach (double[] currentParameters in EnumerateParameters()) {
				means.Clear();
				Metaheuristic.UpdateParameters(currentParameters);
				log.WriteLine("Current parameters: " + ParametersToString(currentParameters));
				
				foreach (string inputFile in InputFiles) {
					foreach (int timeLimit in TimeLimits) {
						fitness.Clear();
						for (int run = 0; run < RunsPerProblem; run++) {
							currentFitness = Run(inputFile, timeLimit);
							if (double.IsPositiveInfinity(currentFitness)) break;
							fitness.Add(currentFitness);
						}
						if (fitness.Count == 0) break;
						means.Add(Statistics.Mean(fitness));
					}
					if (means.Count == 0) break;
				}
				if (means.Count == 0) continue;
				
				currentCost = 0.0;
				foreach (double mean in means) {
					currentCost += mean;
				}
				if (currentCost < bestCost) {
					bestCost = currentCost;
					bestParameters = currentParameters;
					log.WriteLine("Best parameters: " + ParametersToString(bestParameters));
				}
			}
			
			if (bestParameters != null) {
				log.WriteLine("Best parameters: " + ParametersToString(bestParameters));
			}
			else {
				log.WriteLine("Not feasible parameters found.");
			}
			log.WriteLine("Parameters tuning for " + Metaheuristic.Name + " finished.");
			
			log.Close();
		}
		
		protected double Run(string inputFile, int timeLimit)
		{
			int startTime, elapsedTime;
			string outputFile = Path.GetTempFileName();
			int maxTime = timeLimit + (int) Math.Round((TimeTolerance / 100.0) * timeLimit);
			
			startTime = Environment.TickCount;
			Metaheuristic.Start(inputFile, outputFile, timeLimit);
			elapsedTime = Environment.TickCount - startTime;
			if (elapsedTime > maxTime) {
				return double.PositiveInfinity;
			}
			else {
				using (StreamReader reader = File.OpenText(outputFile)) {
					string line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}
					return double.Parse(line);
				}
			}
		}
		
		protected string ParametersToString(double[] parameters)
		{
			string[] stringParameters = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++) {
				stringParameters[i] = parameters[i].ToString();
			}
			return string.Join(", ", stringParameters);
		}
		
		protected abstract IEnumerable<double[]> EnumerateParameters();
	}
}

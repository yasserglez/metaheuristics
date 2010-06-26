using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
			List<double> meanFitness = new List<double>();
			List<double> runFitness = new List<double>();
			
			log.AutoFlush = true;
			log.WriteLine("Parameters tuning for: " + Metaheuristic.Name);
			log.WriteLine("Estimated run time: " + ApproxRunningTime());
			
			foreach (double[] currentParameters in EnumerateParameters()) {
				log.WriteLine();
				log.WriteLine("Current parameters: " + ParametersToString(currentParameters));
				
				Metaheuristic.UpdateParameters(currentParameters);
				meanFitness.Clear();
				foreach (string inputFile in InputFiles) {
					runFitness.Clear();
					foreach (int timeLimit in TimeLimits) {
						for (int run = 0; run < RunsPerProblem; run++) {
							currentFitness = Run(log, inputFile, timeLimit);
							if (double.IsPositiveInfinity(currentFitness)) {
								runFitness.Clear();
								break;
							}
							runFitness.Add(currentFitness);
						}
						if (runFitness.Count == 0) {
							meanFitness.Clear();
							break;
						}
					}
					if (runFitness.Count == 0) {
						meanFitness.Clear();
						break;
					}
					meanFitness.Add(Statistics.Mean(runFitness));
					log.WriteLine("Mean fitness for " + Path.GetFileName(inputFile) + ": " + meanFitness[meanFitness.Count-1]);
				}
				if (meanFitness.Count == 0) {
					log.WriteLine("Skiping this parameters.");
					continue;
				}
				
				currentCost = meanFitness.Sum();
				log.WriteLine("Current cost: " + currentCost);
				if (currentCost < bestCost) {
					bestCost = currentCost;
					bestParameters = currentParameters;
					log.WriteLine("This is the best cost until now.");
				}
			}
			
			log.WriteLine();
			if (bestParameters != null) {
				log.WriteLine("Final best parameters: " + ParametersToString(bestParameters));
			}
			else {
				log.WriteLine("Not feasible parameters found.");
			}
			log.Close();
		}
		
		protected double Run(StreamWriter log, string inputFile, int timeLimit)
		{
			int startTime, elapsedTime;
			string outputFile = Path.GetTempFileName();
			int maxTime = timeLimit + (int) Math.Round((TimeTolerance / 100.0) * timeLimit);
			
			startTime = Environment.TickCount;
			Metaheuristic.Start(inputFile, outputFile, timeLimit);
			elapsedTime = Environment.TickCount - startTime;
			log.WriteLine("Elapsed time for " + Path.GetFileName(inputFile) + ": " + elapsedTime + " of " + maxTime);
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
		
		protected string ApproxRunningTime()
		{
			int miliseconds = 0;
			int runsPerTime = EnumerateParameters().Count() * InputFiles.Length * RunsPerProblem;
			foreach (int timeLimit in TimeLimits) {
				miliseconds += timeLimit * runsPerTime;
			}
			TimeSpan time = new TimeSpan(0, 0, 0, 0, miliseconds);
			return time.ToString();			
		}
		
		protected abstract IEnumerable<double[]> EnumerateParameters();
	}
}

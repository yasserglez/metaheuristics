using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		#region Problem Instances
		
		protected static readonly string TSPDir = "../../../Problems/TSP/";
		protected static readonly string SPPDir = "../../../Problems/SPP/";
		protected static readonly string QAPDir = "../../../Problems/QAP/";
		protected static readonly string TwoSPDir = "../../../Problems/2SP/";
		
		#endregion

		public static void Main(string[] args)
		{
			TuneGA4TSP();
		}

		#region UMDA Tuners
		
		public static void TuneUMDA4TSP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new UMDA4TSP(),
				new UMDA2OptFirst4TSP(),
				new UMDA2OptBest4TSP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				UMDATuner tuner = new UMDATuner(algorithm, TSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneUMDA4SPP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new UMDA4SPP(),
				new UMDA2OptFirst4SPP(),
				new UMDA2OptBest4SPP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				UMDATuner tuner = new UMDATuner(algorithm, SPPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}		
		
		public static void TuneUMDA4QAP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new UMDA4QAP(),
				new UMDA2OptFirst4QAP(),
				new UMDA2OptBest4QAP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				UMDATuner tuner = new UMDATuner(algorithm, QAPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneUMDA42SP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new UMDANPS42SP(),
				new UMDABL42SP(),
				new UMDABL2OptFirst42SP(),
				new UMDABL2OptBest42SP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				UMDATuner tuner = new UMDATuner(algorithm, TwoSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		#endregion
		
		#region GA Tuners
		
		public static void TuneGA4TSP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GA4TSP(),
				new GA2OptFirst4TSP(),
				new GA2OptBest4TSP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				GATuner tuner = new GATuner(algorithm, TSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneGA4SPP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GA4SPP(),
				new GA2OptFirst4SPP(),
				new GA2OptBest4SPP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				GATuner tuner = new GATuner(algorithm, SPPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}		
		
		public static void TuneGA4QAP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GA4QAP(),
				new GA2OptFirst4QAP(),
				new GA2OptBest4QAP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				GATuner tuner = new GATuner(algorithm, QAPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneGA42SP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GANPS42SP(),
				new GABL42SP(),
				new GABL2OptFirst42SP(),
				new GABL2OptBest42SP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				GATuner tuner = new GATuner(algorithm, TwoSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		#endregion		
		
		#region SA Tuners
		
		public static void TuneSA4TSP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new SA4TSP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				SATuner tuner = new SATuner(algorithm, TSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneSA4QAP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new SA4QAP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				SATuner tuner = new SATuner(algorithm, QAPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneSA4SPP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new SA4SPP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				SATuner tuner = new SATuner(algorithm, SPPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneSA42SP()
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new SANPS42SP(),
				new SABL42SP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				SATuner tuner = new SATuner(algorithm, TwoSPDir);
				string logFile = algorithm.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		#endregion
	}
}

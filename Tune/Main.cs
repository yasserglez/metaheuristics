using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			TuneUMDA4TSP();
		}
		
		public static void TuneUMDA4TSP()
		{
			ITunableMetaheuristic[] umdas = new ITunableMetaheuristic[] {
				new UMDA4TSP(),
				new UMDA2OptFirst4TSP(),
				new UMDA2OptBest4TSP(),
			};
			
			umdas.ParallelForEach(umda => {
				UMDATuner tuner = new UMDATuner(umda, "../../../Problems/TSP/");
				string logFile = umda.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneUMDA4SPP()
		{
			ITunableMetaheuristic[] umdas = new ITunableMetaheuristic[] {
				new UMDA4SPP(),
				new UMDA2OptFirst4SPP(),
				new UMDA2OptBest4SPP(),
			};
			
			umdas.ParallelForEach(umda => {
				UMDATuner tuner = new UMDATuner(umda, "../../../Problems/SPP/");
				string logFile = umda.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}		
		
		public static void TuneUMDA4QAP()
		{
			ITunableMetaheuristic[] umdas = new ITunableMetaheuristic[] {
				new UMDA4QAP(),
				new UMDA2OptFirst4QAP(),
				new UMDA2OptBest4QAP(),
			};
			
			umdas.ParallelForEach(umda => {
				UMDATuner tuner = new UMDATuner(umda, "../../../Problems/QAP/");
				string logFile = umda.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}
		
		public static void TuneUMDA42SP()
		{
			ITunableMetaheuristic[] umdas = new ITunableMetaheuristic[] {
				new UMDANPS42SP(),
				new UMDABL42SP(),
				new UMDABL2OptFirst42SP(),
				new UMDABL2OptBest42SP(),
			};
			
			umdas.ParallelForEach(umda => {
				UMDATuner tuner = new UMDATuner(umda, "../../../Problems/2SP/");
				string logFile = umda.GetType().Name + ".txt";
				tuner.Start(logFile);
			});
		}		
	}
}

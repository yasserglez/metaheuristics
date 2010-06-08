using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/TSP/Instances/att48.in";
			string fileOutputGA = "../../../Problems/TSP/Instances/att48_GA.out";
			string fileOutputGRASP2B = "../../../Problems/TSP/Instances/att48_GRASP2B.out";
			string fileOutputGRASP2F = "../../../Problems/TSP/Instances/att48_GRASP2F.out";
			string fileOutputUMDA = "../../../Problems/TSP/Instances/att48_UMDA.out";
			string fileOutputUMDA2B = "../../../Problems/TSP/Instances/att48_UMDA2B.out";
			string fileOutputUMDA2F = "../../../Problems/TSP/Instances/att48_UMDA2F.out";
			GA4TSP.Start(fileInput, fileOutputGA, 3000);
			GRASP2OptBest4TSP.Start(fileInput, fileOutputGRASP2B, 3000);
			GRASP2OptFirst4TSP.Start(fileInput, fileOutputGRASP2F, 3000);
			UMDA4TSP.Start(fileInput, fileOutputUMDA, 6000);
			UMDA2OptBest4TSP.Start(fileInput, fileOutputUMDA2B, 6000);
			UMDA2OptFirst4TSP.Start(fileInput, fileOutputUMDA2F, 6000);
		}
	}
}
using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/TSP/Instances/att48.in";
			string fileOutputUMDA = "../../../Problems/TSP/Instances/att48_UMDA.out";
			string fileOutputGA = "../../../Problems/TSP/Instances/att48_GA.out";
			UMD4TSP.Start(fileInput, fileOutputUMDA, 10000);
			Genetic4TSP.Start(fileInput, fileOutputGA, 10000);
		}
	}
}

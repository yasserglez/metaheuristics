using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/TSP/Instances/berlin52.in";
			string fileOutputGA = "../../../Problems/TSP/Instances/berlin52_GA.out";
			string fileOutputUMDA = "../../../Problems/TSP/Instances/berlin52_UMDA.out";
			GA4TSP.Start(fileInput, fileOutputGA, 3000);
			//UMDA2OptFirst4TSP.Start(fileInput, fileOutputUMDA, 10000);
		}
	}
}
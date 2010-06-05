using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/TSP/Instances/att48.in";
			string fileOutput = "../../../Problems/TSP/Instances/att48.out";
			UMDA4TSP.Start(fileInput, fileOutput, 50000);
		}
	}
}

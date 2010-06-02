using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../TSP/Instances/att48.in";
			string fileOutput = "../../../TSP/Instances/att48.out";
			UMD4TSP.Start(fileInput, fileOutput, 10000);
		}
	}
}

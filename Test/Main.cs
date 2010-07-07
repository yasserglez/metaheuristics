using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   	int time = 10000;
			string input = "../../../Problems/TSP/Instances/rat195.in";
			string output = "../../../Problems/TSP/Instances/rat195.out";
			
			(new GA4TSP()).Start(input, output+"ga", time);
			(new SA4TSP()).Start(input, output+"sa", time);
			(new TS4TSP()).Start(input, output+"ts", time);
			(new ILS2OptFirst4TSP()).Start(input, output+"ils2of", time);
			(new MA4TSP()).Start(input, output+"ma", time);
			
		}
	}
}


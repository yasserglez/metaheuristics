using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   	int time = 10000;
			string input = "../../../Problems/TSP/Instances/bier127.in";
			string output = "../../../Problems/TSP/Instances/bier127.out";
			
//			(new SA4TSP()).Start(input, output+".SA", time);
			(new TS4TSP()).Start(input, output+".TS", time);
			(new ILS2OptBest4TSP()).Start(input, output+".ILS2ob", time);
			(new ILS2OptFirst4TSP()).Start(input, output+".ILS2of", time);
		}
	}
}


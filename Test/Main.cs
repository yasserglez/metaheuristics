using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int time = 10000;
			string input = "../../../Problems/2SP/Instances/inst1.in";
			string output = "../../../Problems/2SP/Instances/inst1.out";
			(new BL2OptFirst42SP()).Start(input, output + ".BL2OptFirst", time);
			(new BL2OptBest42SP()).Start(input, output + ".BL2OptBest", time);
		}
	}
}

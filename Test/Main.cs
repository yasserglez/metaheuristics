using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   	int time = 10000;
			string input = "../../../Problems/2SP/Instances/inst3.in";
			string output = "../../../Problems/2SP/Instances/inst3.out";
			(new TSBL42SP()).Start(input, output+".TSBL", time);
			(new TSNPS42SP()).Start(input, output+".TSNPS", time);
			(new SABL42SP()).Start(input, output+".SABL", time);
			(new SANPS42SP()).Start(input, output+".SANPS", time);
			
		}
	}
}

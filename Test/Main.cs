using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   	int time = 10000;
			string input = "../../../Problems/QAP/Instances/sko72.in";
			string output = "../../../Problems/QAP/Instances/sko72.out";
			(new GA4QAP()).Start(input, output, time);
		}
	}
}


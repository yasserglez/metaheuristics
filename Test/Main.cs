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
			
//			(new GA4QAP()).Start(input, output+"ga", time);
//			(new SA4QAP()).Start(input, output+"sa", time);
//			(new TS4QAP()).Start(input, output+"ts", time);
//			(new ILS2OptFirst4QAP()).Start(input, output+"ils2of", time);
			(new MA4QAP()).Start(input, output+"ma", time);
			
		}
	}
}


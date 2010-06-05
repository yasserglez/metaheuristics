using System;
using System.IO;

namespace Metaheuristics
{	
	public class SPPSolution
	{
		public SPPInstance Instance { get; protected set; }
		
		public int[] Assignment { get; protected set; }
		
		public SPPSolution(SPPInstance instance, int[] assignment)
		{
			Instance = instance;
			Assignment = assignment;
		}
		
		public void Write(string file)
		{
			double deviation = SPPUtils.Fitness(Instance, Assignment);
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(deviation);
				writer.WriteLine(Instance.NumberItems);
				writer.WriteLine(Instance.NumberSubsets);
				writer.WriteLine();
				for (int i = 0; i < Instance.NumberItems; i++) {
					writer.WriteLine(Assignment[i] + 1);
				}
			}		
		}
	}
}

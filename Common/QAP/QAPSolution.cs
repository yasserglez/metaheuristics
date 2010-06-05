using System;
using System.IO;

namespace Metaheuristics
{	
	// We encode a solution of the QAP as an integer vector that in each position (representing
	// the number of the location) contains the number of the facility assigned to the location.
	public class QAPSolution
	{
		public QAPInstance Instance { get; protected set; }
		
		public int[] Assignment { get; protected set; }
		
		public QAPSolution(QAPInstance instance, int[] assignment)
		{
			Instance = instance;
			Assignment = assignment;
		}
		
		public void Write(string file)
		{
			double cost = QAPUtils.Fitness(Instance, Assignment);
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Instance.NumberFacilities);
				for (int i = 0; i < Instance.NumberFacilities; i++) {
					writer.WriteLine(Assignment[i] + 1);
				}
			}			
		}
	}
}

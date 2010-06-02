using System;
using System.IO;

namespace Metaheuristics
{
	public class TSPSolution
	{
		public TSPInstance Instance { get; protected set; }
		
		public int[] Path { get; protected set; }
		
		public TSPSolution(TSPInstance instance, int[] path)
		{
			Instance = instance;
			Path = path;
		}
		
		public void Write(string file)
		{
			double cost = 0;
			
			for (int i = 1; i < Path.Length; i++) {
				cost += Instance.Costs[Path[i-1],Path[i]];
			}
			cost += Instance.Costs[Path[Path.Length-1],Path[0]];
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Path.Length);
				for (int i = 0; i < Path.Length; i++) {
					writer.WriteLine(Path[i] + 1);
				}
			}
		}
	}
	
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
			double cost = 0;
			
			for (int i = 1; i < Assignment.Length; i++) {
				for (int j = 0; j < Assignment.Length; j++) {
					cost += Instance.Distances[i,j] * Instance.Flows[Assignment[i],Assignment[j]];
				}
			}
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Assignment.Length);
				for (int i = 0; i < Assignment.Length; i++) {
					writer.WriteLine(Assignment[i] + 1);
				}
			}			
		}
	}
	
	public class TwoSPSolution
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public TwoSPSolution(TwoSPInstance instance)
		{
			Instance = instance;
		}		
		
		public void Write(string file)
		{
		}
	}	
	
	public class SPPSolution
	{
		public SPPInstance Instance { get; protected set; }
		
		public SPPSolution(SPPInstance instance)
		{
			Instance = instance;
		}
		
		public void Write(string file)
		{
		}
	}
}

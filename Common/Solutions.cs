using System;
using System.IO;

namespace Metaheuristics
{
	// We encode a solution of the TSP as an integer vector describing the ordering
	// that the traveling salesman should follow visiting the cities.
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
			double cost = TSPUtils.Cost(Instance, Path);
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Instance.NumberCities);
				for (int i = 0; i < Instance.NumberCities; i++) {
					writer.WriteLine(Path[i] + 1);
				}
			}
		}
	}
	
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
			double cost = QAPUtils.Cost(Instance, Assignment);
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Instance.NumberFacilities);
				for (int i = 0; i < Instance.NumberFacilities; i++) {
					writer.WriteLine(Assignment[i] + 1);
				}
			}			
		}
	}
	
	// We encode a solution of the 2SP as an integer vector describing an ordering of the items. 
	// This ordering is used to place the items in the strip following the Normal Pattern 
	// Shifting (NPS) as described in Boschetti, M. A. and Montaletti, L. An Exact Algorithm for 
	// the Two-Dimensional Strip Packing Problem.
	public class TwoSPSolution
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public int[][] Coordinates { get; protected set; }
		
		public TwoSPSolution(TwoSPInstance instance, int[] ordering)
		{
			Instance = instance;
			Coordinates = TwoSPUtils.NPS2Coordinates(Instance, ordering);
		}		
		
		public void Write(string file)
		{
			int totalHeight = TwoSPUtils.TotalHeight(Instance, Coordinates);

			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(totalHeight);
				writer.WriteLine(Instance.NumberItems);
				for (int i = 0; i < Instance.NumberItems; i++) {
					writer.WriteLine(Coordinates[0][i] + " " + Coordinates[1][i]);
				}
			}
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

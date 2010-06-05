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
			double cost = TSPUtils.Fitness(Instance, Path);
			
			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(cost);
				writer.WriteLine(Instance.NumberCities);
				for (int i = 0; i < Instance.NumberCities; i++) {
					writer.WriteLine(Path[i] + 1);
				}
			}
		}
	}
}

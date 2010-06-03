using System;
using System.IO;

namespace Metaheuristics
{	
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
}

using System;
using System.IO;

namespace Metaheuristics
{
	// We encode a solution of the 2SP as a bidimensional array with the first dimension 
	// corresponding to the items and the second dimension corresponding to the x and y
	// coordinates of the location of the item in the strip.
	

	public class TwoSPSolution
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public int[,] Coordinates { get; protected set; }
		
		public TwoSPSolution(TwoSPInstance instance, int[,] coordinates)
		{
			Instance = instance;
			Coordinates = coordinates;
		}		
		
		public void Write(string file)
		{
			int totalHeight = TwoSPUtils.TotalHeight(Instance, Coordinates);

			using (StreamWriter writer = File.CreateText(file)) {
				writer.WriteLine(totalHeight);
				writer.WriteLine(Instance.NumberItems);
				for (int i = 0; i < Instance.NumberItems; i++) {
					writer.WriteLine(Coordinates[i,0] + " " + Coordinates[i,1]);
				}
			}
		}
	}	
}

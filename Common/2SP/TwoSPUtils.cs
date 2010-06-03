using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class TwoSPUtils
	{
		public static int[][] NPS2Coordinates(TwoSPInstance Instance, int[] ordering)
		{
			// TODO: Implement this!
			return null;
		}
		
		public static int TotalHeight(TwoSPInstance instance, int[][] coordinates)
		{
			// TODO: Implement this!
			return 0;
		}
		
		public static int TotalHeight(TwoSPInstance instance, int[] ordering)
		{
			return TotalHeight(instance, NPS2Coordinates(instance, ordering));
		}
	}
}

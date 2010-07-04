using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteSANPS42SP : DiscreteSA
	{
		protected bool orderedSolution;
			
		public TwoSPInstance Instance { get; protected set; }
		
		public DiscreteSANPS42SP(TwoSPInstance instance, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			orderedSolution = true;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.NPSCoordinates(Instance, solution));
		}
		
		protected override int[] InitialSolution()
		{
			if (orderedSolution) {
				List<Tuple<int,int>> sorting = new List<Tuple<int,int>>();
				for (int i = 0; i < Instance.NumberItems; i++) {
					int area = Instance.ItemsHeight[i] * Instance.ItemsWidth[i];
					sorting.Add(new Tuple<int,int>(-area, i));
				}
				sorting.Sort();
				int[] ordering = new int[Instance.NumberItems];
				for (int i = 0; i < Instance.NumberItems; i++) {
					ordering[i] = sorting[i].Val2;
				}
				orderedSolution = false;
				return ordering;
			}
			else {
				return TwoSPUtils.RandomSolution(Instance);
			}
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TwoSPUtils.GetNeighbor(Instance, solution);
		}
	}
}

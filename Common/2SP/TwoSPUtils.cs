using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class TwoSPUtils
	{
		// We use the Normal Pattern Shifting (NPS) heuristic to encode a solution of the 2SP as an
		// integer vector describing an ordering of the items. This method calculates the coordinates 
		// of all items in the strip from an ordering of the items following the NPS heuristic.
		public static int[,] NPS2Coordinates(TwoSPInstance instance, int[] ordering)
		{
			int x = 0, y = 1;
			int[,] coordinates = new int[instance.NumberItems,2];
			List<int[]> topLeftSeeds = new List<int[]>();
			List<int[]> bottomRightSeeds = new List<int[]>();
			List<int[]> candidatePositions = new List<int[]>();
			bool[] allocatedItem = new bool[instance.NumberItems];
			
			for (int i = 0; i < instance.NumberItems; i++) {
				int item = ordering[i];
				int[] finalPosition = new int[2];
				
				if (i == 0) {
					// The first item uses the starting position.
					finalPosition[x] = 0;
					finalPosition[y] = 0;
				}
				else {
					candidatePositions.Clear();
					
					foreach (int[] position in topLeftSeeds) {
						// Move left the item as much as possible.
						position[x] = int.MaxValue;
						
						// Check if we can put the item next to the left border of the strip.
						coordinates[item,x] = 0;
						coordinates[item,y] = position[y];
						if (IsFeasible(instance, coordinates, allocatedItem, item) && 
						    SatisfiesNPP(instance, coordinates, allocatedItem, item)) {
							// If this is valid, it's the best position.
							position[x] = coordinates[item,x];
							position[y] = coordinates[item,y];
							candidatePositions.Add(position);
						}
						else {
							// Try to put the item next to other items.
							for (int otherItem = 0; otherItem < instance.NumberItems; otherItem++) {
								coordinates[item,x] = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
								if (allocatedItem[otherItem] && coordinates[item,x] < position[x]) {
									if (IsFeasible(instance, coordinates, allocatedItem, item) && 
									    SatisfiesNPP(instance, coordinates, allocatedItem, item)) {
										position[x] = coordinates[item,x];
										position[y] = coordinates[item,y];
										candidatePositions.Add(position);
									}
								}
							}
						}						
					}
					
					foreach (int[] position in bottomRightSeeds) {
						// Move down the item as much as possible.
						position[y] = int.MaxValue;

						// Check if we can put the item at the bottom of the strip.
						coordinates[item,x] = position[x];
						coordinates[item,y] = 0;
						if (IsFeasible(instance, coordinates, allocatedItem, item) && 
						    SatisfiesNPP(instance, coordinates, allocatedItem, item)) {
							// If this is valid, it's the best position.
							position[x] = coordinates[item,x];
							position[y] = coordinates[item,y];
							candidatePositions.Add(position);
						}
						else {
							// Try to put the item on top of other item.
							for (int otherItem = 0; otherItem < instance.NumberItems; otherItem++) {
								coordinates[item,y] = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
								if (allocatedItem[otherItem] && coordinates[item,y] < position[y]) {
									if (IsFeasible(instance, coordinates, allocatedItem, item) && 
									    SatisfiesNPP(instance, coordinates, allocatedItem, item)) {
										position[x] = coordinates[item,x];
										position[y] = coordinates[item,y];
										candidatePositions.Add(position);
									}
								}
							}
						}
					}

					// Choose the lowest and leftmost position that maximizes the quality.
					double bestQuality = 0;
					foreach (int[] position in candidatePositions) {
						double currentQuality = NPSQuality(instance, coordinates, allocatedItem, item);
						
						if (currentQuality > bestQuality) {
							finalPosition[x] = position[x];
							finalPosition[y] = position[y];
						}
						else if (currentQuality == bestQuality && 
						         position[x] <= finalPosition[x] && 
						         position[x] <= finalPosition[x]) {
							finalPosition[x] = position[x];
							finalPosition[y] = position[y];
						}
					}
				}

				// Set the position of the current item.
				allocatedItem[item] = true;
				coordinates[item,x] = finalPosition[x];
				coordinates[item,y] = finalPosition[y];
				
				// Update the lists with the seed positions.
				topLeftSeeds.Add(new int[] {coordinates[item,x], coordinates[item,y] + instance.ItemsHeight[item]});
				bottomRightSeeds.Add(new int[] {coordinates[item,x] + instance.ItemsWidth[item], coordinates[item,y]});
			}
				
			return coordinates;
		}
		
		// Check if a coordinates assigment is valid.
		public static bool IsFeasible(TwoSPInstance instance, int[,] coordinates)
		{
			bool[] itemAllocated = new bool[instance.NumberItems];
			
			for (int item = 0; item < itemAllocated.Length; item++) {
				itemAllocated[item] = true;
			}			
			for (int item = 0; item < coordinates.GetLength(0); item++) {
				itemAllocated[item] = false;
				if (!IsFeasible(instance, coordinates, itemAllocated, item)) {
					return false;
				}
				itemAllocated[item] = true;
			}
			return true;
		}
		
		public static int Fitness(TwoSPInstance instance, int[,] coordinates)
		{
			int totalHeight = 0;
			
			for (int item = 0; item < coordinates.GetLength(0); item++) {
				int itemHeight = coordinates[item,1] + instance.ItemsHeight[item];
				if (itemHeight > totalHeight) {
					totalHeight = itemHeight;
				}
			}
			
			return totalHeight;
		}
		
		public static int Fitness(TwoSPInstance instance, int[] ordering)
		{
			return Fitness(instance, NPS2Coordinates(instance, ordering));
		}
		
		private static double NPSQuality(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItem, int item)
		{
			int y = 1;
			double numerator = 0, denominator;			
			double currentItemHeight, maxHeight = 0;

			for (int currentItem = 0; currentItem < instance.NumberItems; currentItem++) {
				if (allocatedItem[currentItem] || currentItem == item) {
					currentItemHeight = coordinates[currentItem,y] + instance.ItemsHeight[currentItem];		
					numerator += instance.ItemsWidth[currentItem] * currentItemHeight;
					if (currentItemHeight > maxHeight) {
						maxHeight = currentItemHeight;
					}
				}
			}			
			denominator = instance.StripWidth * maxHeight;
			
			return numerator / denominator;
		}		
		
		// Check if the location given in the coordinates array for the given item is 
		// valid with respect to the location of the rest of the items.
		private static bool IsFeasible(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItem, int item)
		{
			int x = 0, y = 1;
			int itemXStart = coordinates[item,x];
			int itemXEnd = coordinates[item,x] + instance.ItemsWidth[item];
			int itemYStart = coordinates[item,y];
			int itemYEnd = coordinates[item,y] + instance.ItemsHeight[item];
			
			// Checking if the item is located inside the strip.
			if (itemXStart < 0 || itemXEnd > instance.StripWidth) {
				return false;
			}
			
			// Check if the item collapses with other item.
			for (int otherItem = 0; otherItem < coordinates.GetLength(0); otherItem++) {
				if (allocatedItem[otherItem]) {
					int otherItemXStart = coordinates[otherItem,x];
					int otherItemXEnd = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
					int otherItemYStart = coordinates[otherItem,y];
					int otherItemYEnd = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
					
					if (((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
					     (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd)) &&
					    ((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
					     (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd))) {
						return false;
					}
				}
			}
			
			return true;
		}

		// Check if the assigment of a location to an item satisfies the normal pattern principle.
		private static bool SatisfiesNPP(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItem, int item)
		{
			// The left-hand edge and the bottom edges should be both adjacent to other
			// items or to the edges of the strip.
			int x = 0, y = 1;
			int itemXStart = coordinates[item,x];
			int itemXEnd = coordinates[item,x] + instance.ItemsWidth[item];
			int itemYStart = coordinates[item,y];
			int itemYEnd = coordinates[item,y] + instance.ItemsHeight[item];
			bool leftAdjacent = false;
			bool bottomAdjacent = false;		
			
			if (itemXStart == 0) leftAdjacent = true;
			if (itemYStart == 0) bottomAdjacent = true;
			
			for (int otherItem = 0; otherItem < coordinates.GetLength(0); otherItem++) {
				if (allocatedItem[otherItem]) {
					int otherItemXStart = coordinates[otherItem,x];
					int otherItemXEnd = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
					int otherItemYStart = coordinates[otherItem,y];
					int otherItemYEnd = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
					
					if (((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
					     (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd)) &&
					    (itemYStart == otherItemYEnd)) {
						bottomAdjacent = true;
					}
					
					if (((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
					     (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd)) &&
					    (itemXStart == otherItemXEnd)) {
						leftAdjacent = true;
					}
				}
			}
			
			return (leftAdjacent && bottomAdjacent);
		}
		
		public static void Repair(TwoSPInstance instance, int[] individual)
		{			
			int itemsAllocatedCount = 0;
			bool[] itemsAllocated = new bool[instance.NumberItems];
			bool[] itemsRepeated = new bool[instance.NumberItems];
				
			// Get information to decide if the individual is valid.
			for (int item = 0; item < instance.NumberItems; item++) {
				if (!itemsAllocated[individual[item]]) {
					itemsAllocatedCount += 1;
					itemsAllocated[individual[item]] = true;
				}
				else {
					itemsRepeated[item] = true;
				}
			}
				
			// If the individual is invalid, make it valid.
			if (itemsAllocatedCount != instance.NumberItems) {
				for (int item = 0; item < itemsRepeated.Length; item++) {
					if (itemsRepeated[item]) {
						int count = Statistics.RandomDiscreteUniform(1, instance.NumberItems - itemsAllocatedCount);
						for (int i = 0; i < itemsAllocated.Length; i++) {
							if (!itemsAllocated[i]) {
								count -= 1;
								if (count == 0) {
									individual[item] = i;
									itemsRepeated[item] = false;
									itemsAllocated[i] = true;
									itemsAllocatedCount += 1;
									break;
								}
							}
						}
					}
				}
			}
		}
		
		// Implementation of the 2-opt (first improvement) local search algorithm.
		public static void LocalSearch2OptFirst(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, ordering);			
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, ordering);
					if (currentFitness < bestFitness) {
						return;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
		}
		
		// Implementation of the 2-opt (best improvement) local search algorithm.
		public static void LocalSearch2OptBest(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, ordering);			
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, ordering);
					if (currentFitness < bestFitness) {
						firstSwapItem = j;
						secondSwapItem = i;
						bestFitness = currentFitness;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = ordering[firstSwapItem];
				ordering[firstSwapItem] = ordering[secondSwapItem];
				ordering[secondSwapItem] = tmp;
			}
		}		
	}
}

using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Tune
{
	public static class ParallelUtils 
	{
		public static void ParallelForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items.Count() == 0) {
				return;
			}
			else if (items.Count() == 1) {
				action(items.First());
			}
			else {
				var chunkSize = Environment.ProcessorCount;
				for (var offset = 0; offset <= items.Count() / chunkSize; offset++) {
					var chunk = items.Skip(offset * chunkSize).Take(chunkSize);
					var resetEvents = new ManualResetEvent[chunk.Count()];
					var currentItem = 0;
					foreach (var item in chunk) {
						resetEvents[currentItem] = new ManualResetEvent(false);
						ThreadPool.QueueUserWorkItem(new WaitCallback((object data) => {
							int methodIndex = (int) ((object[]) data)[0];
							action((T) ((object[]) data)[1]);
							resetEvents[methodIndex].Set();
						}), new object[] { currentItem, item });
						currentItem++;
					}
	
					WaitHandle.WaitAll(resetEvents);
				}
			}
		}
	}
}

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
				int itemNumber = 0;
				ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
				ManualResetEvent[] resetEvents = new ManualResetEvent[items.Count()];
				foreach (T item in items) {
					resetEvents[itemNumber] = new ManualResetEvent(false);
					ThreadPool.QueueUserWorkItem(new WaitCallback((object data) => {
						int index = (int) ((object[]) data)[0];
						action((T) ((object[]) data)[1]);
						resetEvents[index].Set();
					}), new object[] { itemNumber, item });
					itemNumber++;
				}
				WaitHandle.WaitAll(resetEvents);
			}
		}
	}
}

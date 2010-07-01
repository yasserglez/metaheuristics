using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class Tuple <T1, T2>
    {
        public Tuple(T1 val1, T2 val2)
        {
            Val1 = val1;
            Val2 = val2;
        }
        public T1 Val1 { get; set; }
        public T2 Val2 { get; set; }
		
		public override bool Equals (object obj)
		{
			if (obj is Tuple<T1, T2>) {
				return ((T1)((Tuple<T1, T2>) obj).Val1).Equals(this.Val1) && 
					((T2)((Tuple<T1, T2>) obj).Val2).Equals(this.Val2);
			}
			else
				return base.Equals(obj);
		}

    }
	
    public class LimitedQueue<T> : IEnumerable<T>
    {
        protected int limit;
        protected Queue<T> queue;

        public bool IsFull {
            get {
                return (queue.Count == limit);
            }
        }

        public LimitedQueue(int limit)
        {
            this.limit =  limit;
            this.queue = new Queue<T>(limit);
        }

        public void Enqueue(T item)
        {
            if (IsFull) {
                queue.Dequeue();
            }
            queue.Enqueue(item);
        }

        public void Clear()
        {
            queue.Clear();
        }
		
		public bool Contains(T item)
		{
			return queue.Contains(item);
		}
		
        public IEnumerator<T> GetEnumerator ()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return this.queue.GetEnumerator();
        }
    }
}

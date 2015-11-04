using System;
using System.Collections.Generic;

namespace Framework
{
	public class Pool<T>
	{

		private readonly List<T> freeObjects;
		private readonly int maxSize;
		public delegate T ObjectCreater();
		private ObjectCreater objCreater;

		public Pool (ObjectCreater objCreater, int maxSize)
		{
			this.objCreater = objCreater;
			this.maxSize = maxSize;
			this.freeObjects = new List<T>(maxSize);
		}

		public T NewObject() {
			T obj = default(T);

			if (freeObjects.Count == 0)
				obj = objCreater();
			else {
				obj = freeObjects [freeObjects.Count - 1];
				freeObjects.RemoveAt (freeObjects.Count - 1);
			}

			return obj;
		}

		public void Free(T obj){
			if (freeObjects.Count < maxSize)
				freeObjects.Add (obj);
		}
	}
}


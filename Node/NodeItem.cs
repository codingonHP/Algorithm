using System.Collections.Generic;

namespace Pluralsight.Algorithm
{
    public class NodeItem<T> 
    {
        public T Item { get; set; }
        private NodeItem<T> NextPtr { get;  set; }
        private NodeItem<T> HeadPtr { get; set; } 

        public void AddItem(T item)
        {
            var newNode = new NodeItem<T>
            {
                Item = item,
                NextPtr = null
            };

            if (HeadPtr == null)
            {
                HeadPtr = newNode;
                return;
            }

            NodeItem<T> tempNodeItem = HeadPtr;
            while (tempNodeItem.NextPtr != null)
            {
                tempNodeItem = tempNodeItem.NextPtr;
            }

            tempNodeItem.NextPtr = newNode;
        }

        public IEnumerable<T> GetAll()
        {
            NodeItem<T> tempNodeItem = HeadPtr;
            while ( tempNodeItem != null )
            {
                yield return tempNodeItem.Item;
                tempNodeItem = tempNodeItem.NextPtr;
            }
        }
    }
}

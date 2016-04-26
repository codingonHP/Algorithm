using System;
using System.Collections;
using System.Collections.Generic;

namespace Pluralsight.Algorithm
{


    public class LinkedList<T> : ICollection<T>
    {
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        private Node NodeList { get; set; }

        public void Add(T item)
        {
            ++Count;
            var newNode = new Node
            {
                Item = item,
                Last = true,
                Next = null
            };

            if (NodeList == null)
            {
                newNode.First = true;
                NodeList = newNode;
                return;
            }


            var tempNode = NodeList;
            while (tempNode.Next != null)
            {
                tempNode.Last = false;
                tempNode = tempNode.Next;
            }

            tempNode.Next = newNode;
        }

        public bool Remove(T item)
        {
            int removedAt = Remove(item, Comparer<T>.Default);
            return removedAt != -1;
        }

        public T Remove(int position)
        {
            if (NodeList == null)
            {
                throw new InvalidOperationException("List is empty");
            }

            if (position < 0 || position > Count - 1)
            {
                throw new ArgumentOutOfRangeException("position");
            }

            if (position == 0)
            {
                --Count;
                var removedItem = NodeList;
                NodeList = NodeList.Next;

                if (NodeList != null)
                {
                    NodeList.First = true;
                }

                return removedItem.Item;
            }

            var tempNodeItem = NodeList;
            Node prevNodeItem = null;

            while (position != 0)
            {
                position--;
                prevNodeItem = tempNodeItem;
                tempNodeItem = tempNodeItem.Next;
            }

            if (prevNodeItem != null && tempNodeItem != null)
            {
                --Count;
                prevNodeItem.Next = tempNodeItem.Next;
                return tempNodeItem.Item;

            }

            --Count;
            return prevNodeItem.Item;

        }

        public int Remove(T itemToRemove, IComparer<T> comparer)
        {
            if (NodeList == null)
            {
                throw new InvalidOperationException("Node list is empty");
            }

            var tempNode = NodeList;
            Node tempPrevNode = null;

            int postion = -1;

            while (tempNode != null)
            {
                ++postion;

                if (comparer != null)
                {
                    int compareResult = comparer.Compare(tempNode.Item, itemToRemove);
                    if (compareResult == 0)
                    {
                        --Count;
                        if (tempPrevNode != null)
                        {
                            tempPrevNode.Next = tempNode.Next;
                        }
                        else
                        {
                            NodeList = tempNode.Next;
                            NodeList.First = true;
                        }

                        return postion;
                    }
                }
                else if (tempNode.Item.Equals(itemToRemove))
                {
                    --Count;
                    if (tempPrevNode != null)
                    {
                        tempPrevNode.Next = tempNode.Next;
                    }
                    else
                    {
                        NodeList = tempNode.Next;
                        NodeList.First = true;
                    }

                    return postion;
                }

                tempPrevNode = tempNode;
                tempNode = tempPrevNode.Next;

            }

            return -1;
        }

        public IEnumerable<T> GetAll()
        {
            var tempNodeItem = NodeList;
            while (tempNodeItem != null)
            {
                yield return tempNodeItem.Item;
                tempNodeItem = tempNodeItem.Next;
            }
        }

        public void Clear()
        {
            NodeList = null;
        }

        public bool Contains(T item)
        {
            if (typeof(T) == typeof(object) && item == null)
            {
                throw new ArgumentNullException("item is null");
            }

            var tempHead = NodeList;
            while (tempHead != null)
            {
                if (tempHead.Item.Equals(item))
                {
                    return true;
                }
                tempHead = tempHead.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("array index cannot be negative");
            }

            if (array.Length - arrayIndex < Count || arrayIndex >= Count)
            {
                throw new ArgumentOutOfRangeException("invalid array index");
            }

            var tempHead = NodeList;
            while (tempHead != null)
            {
                array[arrayIndex++] = tempHead.Item;
                tempHead = tempHead.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetAll() as IEnumerator<T>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public T Item { get; set; }
            public bool First { get; set; }
            public bool Last { get; set; }
            public Node Next { get; set; }

        }
    }

}

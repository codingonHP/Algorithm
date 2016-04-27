using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm_unit_test_project
{
    [TestClass]
    public class NodeUnitTest
    {
        [TestMethod]
        public void CreateNewNodeTest()
        {
            var nodeItem = new Pluralsight.Algorithm.LinkedList<int>();

            Assert.AreEqual(nodeItem.Count, 0);
        }

        [TestMethod]
        public void AddItemToNodeListTest()
        {
            var nodeList = AddItemToNodeListAndReturn();
            var allItemList = nodeList.GetAll();

            Assert.AreEqual(nodeList.Count, allItemList.Count());
        }

        [TestMethod]
        public void GetAllItemFromNodeList()
        {
            var nodeList = AddItemToNodeListAndReturn();
            var allItemList = nodeList.GetAll();

            var itemList = allItemList as int[] ?? allItemList.ToArray();

            Assert.AreEqual(itemList.Count(), nodeList.Count);
            Assert.AreEqual(itemList[0], 24);
            Assert.AreEqual(itemList[1], 87);
            Assert.AreEqual(itemList[2], 0);
            Assert.AreEqual(itemList[3], -1);
            Assert.AreEqual(itemList[4], -88);

            try
            {
                Assert.AreEqual(itemList[5], 77);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(IndexOutOfRangeException) );
            }

        }

        [TestMethod]
        public void RemoveItemFromNodeListByPosition()
        {
            var nodeList = AddItemToNodeListAndReturn();

            var allItems = nodeList.GetAll().ToArray();

            var removeItem = nodeList.Remove(0);

            Assert.AreEqual(removeItem, allItems[0]);
            Assert.AreEqual(nodeList.Count, allItems.Length - 1);

            removeItem = nodeList.Remove(0);
            Assert.AreEqual(removeItem, allItems[1]);
            Assert.AreEqual(nodeList.Count, allItems.Length - 2);

            removeItem = nodeList.Remove(2);
            Assert.AreEqual(removeItem, allItems[4]);
            Assert.AreEqual(nodeList.Count, allItems.Length - 3);


        }

        [TestMethod]
        public void RemoveLastItemFromNodeList()
        {
            var nodeList = AddItemToNodeListAndReturn();

            var allItems = nodeList.GetAll().ToArray();

            var removeItem = nodeList.Remove(allItems.Length - 1 );

            Assert.AreEqual(removeItem, allItems[allItems.Length - 1]);
            Assert.AreEqual(nodeList.Count, allItems.Length - 1);
            
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveItemFromEmptyNodeList()
        {
            var emptyNodeList = new Pluralsight.Algorithm.LinkedList<int>();
           emptyNodeList.Remove(0);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveItemFromInvalidPositionInNodeList()
        {
            var nodeList = AddItemToNodeListAndReturn();
            nodeList.Remove(-1);
            nodeList.Remove(5);
            nodeList.Remove(6);
            nodeList.Remove(10);

        }

        [TestMethod]
        public void RemoveItemFromNodeListByValue()
        {
            var nodeList = AddItemToNodeListAndReturn();
            var allItems = nodeList.GetAll().ToArray();

            var removedItemAt = nodeList.Remove(24,null);

            Assert.AreEqual(removedItemAt, 0);
            Assert.AreEqual(nodeList.Count, allItems.Length - 1);

            allItems = nodeList.GetAll().ToArray();
            Assert.AreEqual(allItems[0], 87);

            removedItemAt = nodeList.Remove(-1,Comparer<int>.Default);
            Assert.AreEqual(removedItemAt, 2);
            Assert.AreEqual(nodeList.Count, allItems.Length - 1);

            allItems = nodeList.GetAll().ToArray();

            removedItemAt = nodeList.Remove(-88,null);
            Assert.AreEqual(removedItemAt, 2);
            Assert.AreEqual(nodeList.Count, allItems.Length - 1);

            allItems = nodeList.GetAll().ToArray();
            removedItemAt = nodeList.Remove(1000, null);

            Assert.AreEqual(removedItemAt, -1);
            Assert.AreEqual(nodeList.Count, allItems.Length);


        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            var items = AddItemToNodeListAndReturn();
            var enumerator = items.GetEnumerator();

            Assert.IsNotNull(enumerator);
            
        }

        [TestMethod]
        public void LinkedListContainsItemTest()
        {
            var items = AddItemToNodeListAndReturn();
            bool contains = items.Contains(23);
            Assert.IsFalse(contains);

            contains = items.Contains(-88);
            Assert.IsTrue(contains);

            contains = items.Contains(24);
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void CopyToLinkedListTest()
        {
            var list = AddItemToNodeListAndReturn();
            var listArry = list.ToArray();
            var len = list.Count;

            var intArry = new int[len];

            list.CopyTo(intArry,0);

            var equals = ArrayEquals(listArry, intArry,0);
            Assert.IsTrue(equals);
        }


        [TestMethod]
        public void CopyLinkedListToArryOfMoreSizeStartingAtZeroIndex()
        {
            var list = AddItemToNodeListAndReturn();
            var listArry = list.ToArray();
            var len = list.Count;

            var intArry = new int[len + 10];

            list.CopyTo(intArry, 0);

            var equals = ArrayEquals(listArry, intArry,0);
            Assert.IsTrue(equals);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyLinkedListToArryOfMoreSizeStartingAtWrongIndex()
        {
            var list = AddItemToNodeListAndReturn();
            var len = list.Count;

            var intArry = new int[len + 10];

            list.CopyTo(intArry, 11);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyLinkedListToNullArray()
        {
            var list = AddItemToNodeListAndReturn();

            int[] intArry = null;

            list.CopyTo(intArry, 11);

        }


        [TestMethod]
        public void CopyLinkedListToArryOfMoreSizeStartingAtXIndex()
        {
            var list = AddItemToNodeListAndReturn();
            var listArry = list.ToArray();
            var len = list.Count;

            var intArry = new int[len + 10];

            list.CopyTo(intArry, 3);

            var equals = ArrayEquals(listArry, intArry, 3);
            Assert.IsTrue(equals);
        }




        private bool ArrayEquals<T>(T[] src, T[] dest, int startAt)
        {
           
            if (src.Length == dest.Length && startAt != 0)
            {
                return false;
            }

            if (src.Length > dest.Length)
            {
                return false;
            }

            if (src.Length == dest.Length && startAt == 0)
            {
                if (src.Where((t, i) => !t.Equals(dest[i])).Any())
                {
                    return false;
                }

                return true;
            }

            if (src.Length < dest.Length)
            {
                int index = 0;
                for (int i = startAt; i < src.Length; i++)
                {
                    if (!src[index++].Equals(dest[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private Pluralsight.Algorithm.LinkedList<int> AddItemToNodeListAndReturn()
        {
            var nodeList = new Pluralsight.Algorithm.LinkedList<int>();
            nodeList.Add(24);
            nodeList.Add(87);
            nodeList.Add(0);
            nodeList.Add(-1);
            nodeList.Add(-88);

            return nodeList;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Pluralsight.Algorithm
{
    class ConsoleApplication
    {
        static void Main(string[] args)
        {
            var nodeItem = new LinkedList<int>();

            nodeItem.Add(23);
            nodeItem.Add(34);
            nodeItem.Add(1);
            nodeItem.Add(45);

           PrintNode(nodeItem);

           int removedItem = nodeItem.Remove(0);

            PrintNode(nodeItem);
            Console.ReadKey();
        }

        private static void PrintNode(LinkedList<int> nodeList )
        {
            foreach (var i in nodeList.GetAll())
            {
                Console.WriteLine(i);
            }
        }
    }
}

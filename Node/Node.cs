using System;

namespace Pluralsight.Algorithm
{
    class Node
    {
        static void Main(string[] args)
        {
            var nodeItem = new NodeItem<int>();

            nodeItem.AddItem(23);
            nodeItem.AddItem(34);
            nodeItem.AddItem(1);
            nodeItem.AddItem(45);

            foreach (var i in nodeItem.GetAll())
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }
    }
}

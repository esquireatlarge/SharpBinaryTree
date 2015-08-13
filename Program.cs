//A binary tree in C#.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSequence
{
    //Node object.
    class Node
    {
        public Node()
        {
            left = null;
            right = null;
            data = 0;
        }

        public Node(int data, Node left = null, Node right = null)
        {
            this.data = data;
            this.left = left;
            this.right = right;
        }

        public void Print()
        {
            Console.Write(data);
        }
        
        public int Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public Node Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        public Node Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

        private int data;
        private Node left;
        private Node right;
    }

    class BinaryTree
    {
        public BinaryTree()
        {
            root = null;
        }

        public BinaryTree(int rootValue)
        {
            root = new Node(rootValue);
        }

        public void Insert(int val)
        {
            //Breadth First Traversal insertion.  This is a binary tree,
            //not a binary search tree.  Insertion will be done in left to right
            //breadth first order.
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

            //Continue enqueueing until an empty spot is found.
            //Then insert the node and stop traversing.
            bool inserted = false;
            while (q.Count > 0 && !inserted)
            {
                Node s = q.Dequeue();
                if (s.Left == null)
                {
                    s.Left = new Node(val);
                    inserted = true;
                }
                else if (s.Right == null)
                {
                    s.Right = new Node(val);
                    inserted = true;
                }
                else
                {
                    q.Enqueue(s.Left);
                    q.Enqueue(s.Right);
                }
            }
        }

        //DFS print of tree.
        public void Print()
        {
            DoPrint(root);
        }

        //Static helper function to create a random tree of random depth.
        public static BinaryTree CreateRandomTree(int max = 15)
        {
            Random r = new Random();
            int numToInsert = r.Next(max * 2);
            BinaryTree bt = new BinaryTree(r.Next(max));

            for (int i = 0; i < numToInsert; i++)
                bt.Insert(r.Next(max));

            return bt;
        }

        //A static helper function which creates a tree guaranteed to have
        //several sequences adding to the number 6.
        public static BinaryTree CreateTest1()
        {
            BinaryTree bt = new BinaryTree(6);
            bt.Insert(8);
            bt.Insert(-5);
            bt.Insert(-3);
            bt.Insert(7);
            bt.Insert(3);
            bt.Insert(1);
            bt.Insert(4);
            bt.Insert(3);

            return bt;
        }

        //Retrieves all paths within the tree (not necessarily root to leaf) 
        //which add up to a target number.
        public List<List<Node>> GetSequences(int target)
        {
            //The list of lists of Nodes.
            List<List<Node>> sequenceList = new List<List<Node>>();
            List<Node> intermediate = new List<Node>();

            DoDFS(root, target, sequenceList, intermediate);

            Console.WriteLine();
            for (int i = 0; i < sequenceList.Count; i++)
            {
                for (int j = 0; j < sequenceList[i].Count; j++)
                {
                    Console.Write(sequenceList[i].ElementAt(j).Data + ",");
                }
                Console.WriteLine();
            }
            return sequenceList;
        }

        private void DoPrint(Node r)
        {
            r.Print();
            if (r.Left != null)
                DoPrint(r.Left);
            if (r.Right != null)
                DoPrint(r.Right);
        }

        /// <summary>
        /// This function scans the tree in a DFS fashion.
        /// </summary>
        /// <param name="curr">The current node to look at.</param>
        /// <param name="target">The number that the branch should add up to.</param>
        /// <param name="ls">The list to place qualifying sequences into.</param>
        /// <param name="l">A list containing all previously visited items.</param>
        private void DoDFS(Node curr, int target, List<List<Node>> ls, List<Node> l)
        {
            //First add the current node to the list.
            if(curr != null)
                l.Add(curr);

            //Next, iterate through the list backwards, adding all numbers.
            //Should the sum equal to the target at any point, we will add
            //the subset of the list "l" to the sequence list "ls" and continue scanning.
            //We must always scan the entire list, making the worst case a O(depth).
            int sum = 0;
            for (int i = l.Count - 1; i >= 0; i--)
            {
                sum += l[i].Data;
                if (sum == target)
                {
                    //add sequence to list of sequences
                    ls.Add(l.GetRange(i, (l.Count - i)));
                }
            }
            if (curr.Left != null)
                DoDFS(curr.Left, target, ls, l);
            if (curr.Right != null)
                DoDFS(curr.Right, target, ls, l);
        }

        private Node root;
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = BinaryTree.CreateTest1();
            tree.Print();

            tree.GetSequences(6);

            Console.ReadLine();
        }
    }
}

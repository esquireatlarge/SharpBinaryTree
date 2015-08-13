using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSequence
{
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
            //BFS insertion
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

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

        public void Print()
        {
            DoPrint(root);
        }

        public static BinaryTree CreateRandomTree(int max = 15)
        {
            Random r = new Random();
            int numToInsert = r.Next(max * 2);
            BinaryTree bt = new BinaryTree(r.Next(max));

            for (int i = 0; i < numToInsert; i++)
                bt.Insert(r.Next(max));

            return bt;
        }

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

        public List<List<Node>> GetSequences(int target)
        {
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

        private void DoDFS(Node curr, int target, List<List<Node>> ls, List<Node> l)
        {
            if(curr != null)
                l.Add(curr);

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

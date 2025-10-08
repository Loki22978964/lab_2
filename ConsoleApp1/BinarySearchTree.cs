using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : class
    {
        private class Node
        {
            public T Value { get; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public Node(T value) { Value = value; }
        }

        private readonly IComparer<T> _comparer;
        private Node? _root;
        public int Count { get; private set; }

        public BinarySearchTree() : this(Comparer<T>.Default) { }

        public BinarySearchTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public void Insert(T value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            _root = InsertRecursive(_root, value);
        }

        private Node InsertRecursive(Node? node, T value)
        {
            if (node is null)
            {
                Count++;
                return new Node(value);
            }

            int cmp = _comparer.Compare(value, node.Value);
            if (cmp <= 0) node.Left = InsertRecursive(node.Left, value);
            else node.Right = InsertRecursive(node.Right, value);
            return node;
        }

        public bool Contains(T value)
        {
            if (value is null) return false;
            Node? current = _root;
            while (current is not null)
            {
                int cmp = _comparer.Compare(value, current.Value);
                if (cmp == 0) return true;
                current = cmp < 0 ? current.Left : current.Right;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator() => PostOrderTraversal(_root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static IEnumerable<T> PostOrderTraversal(Node? node)
        {
            if (node is null) yield break;
            foreach (var v in PostOrderTraversal(node.Left)) yield return v;
            foreach (var v in PostOrderTraversal(node.Right)) yield return v;
            yield return node.Value;
        }
    }
}



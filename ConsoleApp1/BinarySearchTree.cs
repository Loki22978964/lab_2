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
            if (_root is null)
            {
                _root = new Node(value);
                Count = 1;
                return;
            }

            Node current = _root;
            while (true)
            {
                int cmp = _comparer.Compare(value, current.Value);
                if (cmp <= 0)
                {
                    if (current.Left is null)
                    {
                        current.Left = new Node(value);
                        Count++;
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right is null)
                    {
                        current.Right = new Node(value);
                        Count++;
                        return;
                    }
                    current = current.Right;
                }
            }
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

        public IEnumerator<T> GetEnumerator()
        {
            return new PostOrderEnumerator(_root);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class PostOrderEnumerator : IEnumerator<T>
        {
            private readonly Stack<(Node node, bool visited)> _stack = new();
            private T? _current;

            public PostOrderEnumerator(Node? root)
            {
                if (root is not null)
                    _stack.Push((root, false));
            }

            public T Current => _current!;
            object IEnumerator.Current => Current!;

            public bool MoveNext()
            {
                while (_stack.Count > 0)
                {
                    var (node, visited) = _stack.Pop();
                    if (visited)
                    {
                        _current = node.Value;
                        return true;
                    }

                    _stack.Push((node, true));
                    if (node.Right is not null) _stack.Push((node.Right, false));
                    if (node.Left is not null) _stack.Push((node.Left, false));
                }
                return false;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
            }
        }
    }
}



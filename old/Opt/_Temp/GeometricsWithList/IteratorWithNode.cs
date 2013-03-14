using System;

namespace Opt.Geometrics.GeometricContainers
{
    public class IteratorWithNode<Type>
    {
        #region Скрытые поля и свойства.
        protected Node<Type> node_elements;
        protected Node<IteratorWithNode<Type>> node_iterators;
        protected bool is_closeable;
        #endregion

        #region Открытые поля и свойства.
        public Type Data
        {
            get
            {
                return node_elements.data;
            }
            set
            {
                node_elements.data = value;
            }
        }
        #endregion

        #region IteratorOfPolygonWithNode(...)
        public IteratorWithNode(Node<IteratorWithNode<Type>> node_iterators, Node<Type> node_elements, bool is_closeable)
        {
            if (is_closeable)
            {
                this.node_iterators = new Node<IteratorWithNode<Type>>() { prev = node_iterators, next = node_iterators.next, data = this };
                this.node_iterators.prev.next = this.node_iterators;
                this.node_iterators.next.prev = this.node_iterators;
            }
            else
                this.node_iterators = node_iterators;

            this.is_closeable = is_closeable;

            this.node_elements = node_elements;
        }
        #endregion

        #region Методы.
        public IteratorWithNode<Type> CreateIterator()
        {
            return new IteratorWithNode<Type>(node_iterators, node_elements, true);
        }

        public void Add(int k, Type element)
        {
            Node<Type> node_temp = null;
            if (k > 0)
                node_temp = new Node<Type>() { prev = node_elements, next = node_elements.next, data = element };
            if (k < 0)
                node_temp = new Node<Type>() { prev = node_elements.prev, next = node_elements, data = element };
            if (k != 0)
            {
                node_temp.prev.next = node_temp;
                node_temp.next.prev = node_temp;
            }
        }
        public void Del(int k)
        {
            Node<Type> node_temp = null;
            if (k > 0)
                node_temp = node_elements.next;
            if (k < 0)
                node_temp = node_elements.prev;
            if (k != 0)
            {
                bool is_free = true;
                Node<IteratorWithNode<Type>> node_iterator_temp = node_iterators;
                do
                {
                    if (node_iterator_temp.data.node_elements == node_temp)
                        is_free = false;
                    node_iterator_temp = node_iterator_temp.next;
                } while (node_iterator_temp != node_iterators && is_free);

                if (is_free)
                {
                    node_temp.prev = node_temp.next;
                    node_temp.next = node_temp.prev;
                }
            }
        }

        public IteratorWithNode<Type> Move(int k)
        {
            if (k > 0)
                node_elements = node_elements.next;
            if (k < 0)
                node_elements = node_elements.prev;
            return this;
        }

        public void Close()
        {
            if (is_closeable)
            {
                node_elements = null;
                node_iterators.prev = node_iterators.next;
                node_iterators.next = node_iterators.prev;
                node_iterators = null;
            }
        }
        public bool IsClose()
        {
            return node_iterators == null;
        }
        #endregion
    }
}

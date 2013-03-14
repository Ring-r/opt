using System;

namespace Opt.Geometrics.GeometricContainers
{
    [Serializable]
    public class GeometricContainerWithNode<Type> : GeometricWithPole
    {
        #region Скрытые поля и свойства.
        protected Node<Type> node_points;
        protected Node<IteratorWithNode<Type>> node_iterators;
        #endregion

        #region Открытые поля и свойства.
        public IteratorWithNode<Type> Iterator
        {
            get
            {
                return node_iterators.data;
            }
        }
        #endregion

        #region GeometricContainerWithNode(...)
        public GeometricContainerWithNode()
        {
            node_points = new Node<Type>();
            node_points.prev = node_points;
            node_points.next = node_points;

            node_iterators = new Node<IteratorWithNode<Type>>();
            node_iterators.prev = node_iterators;
            node_iterators.next = node_iterators;
            node_iterators.data = new IteratorWithNode<Type>(node_iterators, node_points, false);
        }
        #endregion
    }
}
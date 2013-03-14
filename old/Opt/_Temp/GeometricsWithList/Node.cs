using System;

namespace Opt.Geometrics.GeometricContainers
{
    [Serializable]
    public class Node<Type>
    {
        public Node<Type> prev;
        public Node<Type> next;
        public Type data;
    }
}

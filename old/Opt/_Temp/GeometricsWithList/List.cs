using System;

namespace Opt.Geometrics.List
{
    public partial class List<TypeInList>
    {
        #region Необходимые классы.
        public class NodeTwoWay<TypeInNode>
        {
            protected List<TypeInNode> list;
            protected TypeInNode data;
            protected NodeTwoWay<TypeInNode> next;
            protected NodeTwoWay<TypeInNode> prev;

            public TypeInNode Data
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
            public NodeTwoWay<TypeInNode> Next
            {
                get
                {
                    return next;
                }
            }
            public NodeTwoWay<TypeInNode> Prev
            {
                get
                {
                    return prev;
                }
            }

            public NodeTwoWay(List<TypeInNode> list)
            {
                this.list = list;
            }

            public void Add(int way_index, TypeInNode data)
            {
                NodeTwoWay<TypeInNode> node_temp = new NodeTwoWay<TypeInNode>(list);
                node_temp.data = data;
                if (way_index > 0)
                {
                    node_temp.next = this.next;
                    node_temp.prev = this;
                }
                if (way_index < 0)
                {
                    node_temp.next = this;
                    node_temp.prev = this.prev;
                }
                if (way_index != 0)
                {
                    node_temp.next.prev = node_temp;
                    node_temp.prev.next = node_temp;
                    list.count++;
                }
            }
            public void Del(int way_index)
            {
                NodeTwoWay<TypeInNode> node_temp = this;
                if (way_index > 0)
                    node_temp = this.next;
                if (way_index < 0)
                    node_temp = this.prev;
                if (node_temp != this)
                {
                    node_temp.next.prev = node_temp.prev;
                    node_temp.prev.next = node_temp.next;
                    list.count--;
                }
            }
        }
        #endregion

        #region Скрытые поля и свойства.
        protected NodeTwoWay<TypeInList> node;
        protected int count;
        #endregion

        #region Открытые поля и свойства.
        public NodeTwoWay<TypeInList> Node
        {
            get
            {
                return node;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
            protected set
            {
            }
        }
        #endregion

        #region List(...)
        public List()
        {
            node = new NodeTwoWay<TypeInList>(this);
            count = 1;
        }
        #endregion
    }
}

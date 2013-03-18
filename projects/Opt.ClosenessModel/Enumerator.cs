using System;

namespace Opt.ClosenessModel
{
    public partial class Vertex<DataType>
    {
        public class EnumeratorClass
        {
            #region Скрытые поля и свойства.
            /// <summary>
            /// Начальная вершина.
            /// </summary>
            protected Vertex<DataType> start;
            /// <summary>
            /// Текущая вершина.
            /// </summary>
            protected Vertex<DataType> current;
            #endregion

            #region Открытые поля и свойства.
            /// <summary>
            /// Получить и текущую вершину.
            /// </summary>
            public Vertex<DataType> Current
            {
                get
                {
                    return current;
                }
            }
            #endregion

            #region EnumeratorClass(...)
            public EnumeratorClass(Vertex<DataType> vertex)
            {
                start = vertex;
                current = vertex;
            }
            #endregion

            public void Reset()
            {
                current = start;
            }
            public bool MoveNext()
            {
                throw new NotImplementedException();
                return current != start;
            }
            public bool MoveNextInNode()
            {
                current = current.next.cros.next;
                return current != start;
            }
            public bool MoveNextInTriple()
            {
                current = current.next;
                return current != start;
            }
        }
    }
}

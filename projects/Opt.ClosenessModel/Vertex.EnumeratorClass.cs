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
                    return this.current;
                }
            }
            #endregion

            #region EnumeratorClass(...)
            public EnumeratorClass(Vertex<DataType> vertex)
            {
                this.start = vertex;
                this.current = vertex;
            }
            #endregion

            public void Reset()
            {
                this.current = this.start;
            }
            public bool MoveNext()
            {
                // TODO: Использовать алгоритм поиска в ширину или в глубину.
                throw new NotImplementedException();
                return current != start;
            }
            public bool MoveNextInNode()
            {
                this.current = this.current.next.cros.next;
                return this.current != this.start;
            }
            public bool MoveNextInTriple()
            {
                this.current = this.current.next;
                return this.current != this.start;
            }
        }
    }
}

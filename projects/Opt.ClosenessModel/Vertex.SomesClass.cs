using System;

// TODO: Использовать CircleDelone.
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPointScalar;

namespace Opt.ClosenessModel
{
    public partial class Vertex<DataType>
    {
        /// <summary>
        /// Класс вспомогательных данных для вершины модели близости.
        /// </summary>
        [Serializable]
        public class SomesClass
        {
            #region Скрытые поля и свойства.
            /// <summary>
            /// Вершина, связанная со вспомогательными данными.
            /// </summary>
            protected Vertex<DataType> vertex;
            /// <summary>
            /// Круг Делоне.
            /// </summary>
            protected Circle circle_delone;
            /// <summary>
            /// Отметка-время вершины.
            /// </summary>
            protected DateTime last_checked;
            #endregion

            #region Открытые поля и свойства.
            /// <summary>
            /// Получает связанную вершину.
            /// </summary>
            public Vertex<DataType> Vertex
            {
                get
                {
                    return this.vertex;
                }
            }
            /// <summary>
            /// Задаёт и получает круг Делоне.
            /// </summary>
            public Circle CircleDelone
            {
                get
                {
                    return this.circle_delone;
                }
                set
                {
                    this.circle_delone = value;
                }
            }
            /// <summary>
            /// Задаёт или получает отметку-время вершины.
            /// </summary>
            public DateTime LastChecked
            {
                get
                {
                    return this.last_checked;
                }
                set
                {
                    this.last_checked = value;
                }
            }
            #endregion

            #region SomesClass(...)
            public SomesClass(Vertex<DataType> vertex)
            {
                this.vertex = vertex;
            }
            #endregion
        }
    }
}

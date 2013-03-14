using System;
using System.Collections.Generic;

namespace Opt
{
    namespace VD
    {
        public class Vertex<Object, DeloneCircle>
            //where Object : IObject<Object>
            where DeloneCircle : IDeloneCircle<Object>, new()
        {
            /// <summary>
            /// Ссылка на предыдущий узел в тройке.
            /// </summary>
            internal Vertex<Object, DeloneCircle> prev;
            public Vertex<Object, DeloneCircle> Prev
            {
                get
                {
                    return prev;
                }
            }
            /// <summary>
            /// Ссылка на перекрёстный узел.
            /// </summary>
            internal Vertex<Object, DeloneCircle> cros;
            public Vertex<Object, DeloneCircle> Cros
            {
                get
                {
                    return cros;
                }
            }
            /// <summary>
            /// Ссылка на следующий узел.
            /// </summary>
            internal Vertex<Object, DeloneCircle> next;
            public Vertex<Object, DeloneCircle> Next
            {
                get
                {
                    return next;
                }
            }
            /// <summary>
            /// Ссылка на объект.
            /// </summary>
            internal Object data;
            public Object Data
            {
                get
                {
                    return data;
                }
            }
            /// <summary>
            /// Вспомогательная ссылка.
            /// </summary>
            /// <remarks>
            /// Можно использовать для хранения ссылки на текущую тройку либо для хранения ссылки на круг Делоне связанный с данным узлом.
            /// </remarks>
            internal Triple<Object, DeloneCircle> triple;
            public Triple<Object, DeloneCircle> Triple
            {
                get
                {
                    return triple;
                }
            }
            /// <summary>
            /// Ссылка на круг Делоне.
            /// </summary>
            public DeloneCircle Delone_Circle
            {
                get
                {
                    return triple.delone_circle;
                }
            }

            ///// <summary>
            ///// Конструктор.
            ///// </summary>
            internal Vertex()
            {
            }

            /// <summary>
            /// Установление связи с перекрёстным узлом (и наоборот).
            /// </summary>
            /// <param name="cros_vertex">Ссылка на перекрёстный узел.</param>
            internal void SetCros(Vertex<Object, DeloneCircle> cros_vertex)
            {
                cros = cros_vertex;
                cros.cros = this;
            }

            /// <summary>
            /// Разбиение противоположного ребра (вставка двух новых троек).
            /// </summary>
            /// <param name="data">Вставляемый объект.</param>
            internal void Break(Object data)
            {
                // Создаём две противоположнонаправленные тройки.
                Triple<Object, DeloneCircle> triple_n = new Triple<Object, DeloneCircle>(next.data, data, prev.data);
                Triple<Object, DeloneCircle> triple_f = new Triple<Object, DeloneCircle>(prev.data, data, next.data);

                // Устанавливаем связи между созданными тройками.
                triple_n.vertex.prev.SetCros(triple_f.vertex.next);
                triple_n.vertex.next.SetCros(triple_f.vertex.prev);

                // Устанавливаем связи со всем графом.
                triple_f.vertex.SetCros(cros);
                triple_n.vertex.SetCros(this);
            }

            /// <summary>
            /// Переразбиение смежных троек.
            /// </summary>
            internal void Rebuild()
            {
                Vertex<Object, DeloneCircle> vertex = cros;

                prev.data = vertex.data;
                vertex.prev.data = data;
                SetCros(vertex.next.cros);
                vertex.SetCros(next.cros);
                next.SetCros(vertex.next);
            }

            #region Необходимые алгоритмы
            public List<Vertex<Object, DeloneCircle>> Веер_вершин()
            {
                List<Vertex<Object, DeloneCircle>> res = new List<Vertex<Object, DeloneCircle>>();
                res.Add(this);
                Vertex<Object, DeloneCircle> temp_vertex = this.next.cros.next;
                while (temp_vertex != this)
                {
                    res.Add(temp_vertex);
                    temp_vertex = temp_vertex.Next.Cros.Next;
                }
                return res;
            }
            #endregion
        }
    }
}
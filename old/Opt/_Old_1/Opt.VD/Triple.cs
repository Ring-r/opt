using System;

namespace Opt
{
    namespace VD
    {
        public class Triple<Object, DeloneCircle>
            //where Object : IObject<Object>
            where DeloneCircle : IDeloneCircle<Object>, new()
        {
            internal Triple<Object, DeloneCircle> prev;
            internal Triple<Object, DeloneCircle> next;

            /// <summary>
            /// Ссылка на вершину в текущей тройке.
            /// </summary>
            internal Vertex<Object, DeloneCircle> vertex;
            public Vertex<Object, DeloneCircle> Vertex
            {
                get
                {
                    return vertex;
                }
            }
            /// <summary>
            /// Ссылка на круг Делоне.
            /// </summary>
            internal DeloneCircle delone_circle;
            public DeloneCircle Delone_Circle
            {
                get
                {
                    return delone_circle;
                }
            }

            /// <summary>
            /// Конструктор (вершины не создаются).
            /// </summary>
            internal Triple()
            {
                prev = this;
                next = this;
            }
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="prev_data">Ссылка на предыдущий объект.</param>
            /// <param name="curr_data">Ссылка на текущий объект.</param>
            /// <param name="next_data">Ссылка на следующий объект.</param>
            internal Triple(Object prev_data, Object curr_data, Object next_data)
            {
                vertex = new Vertex<Object, DeloneCircle>() { data = curr_data, triple = this };

                vertex.prev = new Vertex<Object, DeloneCircle>() { data = prev_data, triple = this, next = vertex };
                vertex.next = new Vertex<Object, DeloneCircle>() { data = next_data, triple = this, prev = vertex };

                vertex.prev.prev = vertex.next;
                vertex.next.next = vertex.prev;

                delone_circle = new DeloneCircle();
            }

            #region Старый вариант
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="triple">Тройка.</param>
            internal Triple(Triple<Object, DeloneCircle> triple)
            {
                vertex = new Vertex<Object, DeloneCircle>() { cros = triple.vertex.cros, data = triple.vertex.data, triple = this };

                vertex.prev = new Vertex<Object, DeloneCircle>() { cros = triple.vertex.prev.cros, data = triple.vertex.prev.data, triple = this, next = vertex };
                vertex.next = new Vertex<Object, DeloneCircle>() { cros = triple.vertex.next.cros, data = triple.vertex.next.data, triple = this, prev = vertex };

                vertex.prev.prev = vertex.next;
                vertex.next.next = vertex.prev;

                delone_circle = new DeloneCircle();
            }

            /// <summary>
            /// Разбиение тройки на три новые.
            /// </summary>
            /// <param name="data">Ссылка на объект, который является общим для новых троек.</param>
            /// <remarks>Новые тройки связаны между собой.</remarks>
            internal void Break(Object data)
            {
                Triple<Object, DeloneCircle> triple_pcp = new Triple<Object, DeloneCircle>(this);
                Triple<Object, DeloneCircle> triple_ncn = new Triple<Object, DeloneCircle>(this);

                // Получаем разбиение текущей тройки.
                triple_pcp.vertex.prev.data = data;
                triple_pcp.vertex.cros = vertex.prev;
                triple_pcp.vertex.next.cros = triple_ncn.vertex.prev;
                vertex.prev.cros = triple_pcp.vertex;
                vertex.data = data;
                vertex.next.cros = triple_ncn.vertex;
                triple_ncn.vertex.prev.cros = triple_pcp.vertex.next;
                triple_ncn.vertex.cros = vertex.next;
                triple_ncn.vertex.next.data = data;

                // Создаем ссылки из соседних троек.
                triple_pcp.vertex.prev.cros.cros = triple_pcp.vertex.prev;
                vertex.cros.cros = vertex;
                triple_ncn.vertex.next.cros.cros = triple_ncn.vertex.next;
            }
            #endregion

            internal void CalculateDeloneCircle()
            {
                Vertex<Object, DeloneCircle> temp_vertex = vertex.next;
                while (temp_vertex.data != null && temp_vertex != vertex)
                    temp_vertex = temp_vertex.next;
                if (temp_vertex.data == null)
                    delone_circle.Calculate(temp_vertex.next.data, temp_vertex.prev.data);
                else
                    delone_circle.Calculate(temp_vertex.prev.data, temp_vertex.data, temp_vertex.next.data);
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace Opt
{
    namespace VD
    {
        public class VD<Object, DeloneCircle>
            //where Object : IObject<Object>
            where DeloneCircle : IDeloneCircle<Object>, new()
        {
            protected Triple<Object, DeloneCircle> triple;
            protected void AddTriple(Triple<Object, DeloneCircle> triple)
            {
                triple.prev = this.triple;
                triple.next = this.triple.next;
                triple.prev.next = triple;
                triple.next.prev = triple;
            }
            protected void DelTriple(Triple<Object, DeloneCircle> triple)
            {
                triple.prev.next = triple.next;
                triple.next.prev = triple.prev;
            }
            public Triple<Object, DeloneCircle> NullTriple
            {
                get
                {
                    return triple;
                }
            }
            public Triple<Object, DeloneCircle> PrevTriple(Triple<Object, DeloneCircle> triple)
            {
                return triple.prev;
            }
            public Triple<Object, DeloneCircle> NextTriple(Triple<Object, DeloneCircle> triple)
            {
                return triple.next;
            }

            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="prev_data">Геометрический объект.</param>
            /// <param name="infinity">Ссылка на внешний объект.</param>
            /// <param name="next_data">Геометрический объект.</param>
            public VD(Object prev_data, Object infinity, Object next_data)
            {
                // Создание двух начальных троек.
                Triple<Object, DeloneCircle> triple_prev = new Triple<Object, DeloneCircle>(prev_data, infinity, next_data);
                Triple<Object, DeloneCircle> triple_next = new Triple<Object, DeloneCircle>(next_data, infinity, prev_data);

                // Установление связей между начальными тройками.
                triple_prev.vertex.prev.SetCros(triple_next.vertex.next);
                triple_prev.vertex.SetCros(triple_next.vertex);
                triple_prev.vertex.next.SetCros(triple_next.vertex.prev);

                // Расчёт кругов Делоне для начальных троек.
                triple_prev.CalculateDeloneCircle();
                triple_next.CalculateDeloneCircle();

                // Сохранение всех троек в список (либо другую структуру).
                this.triple = new Triple<Object, DeloneCircle>();
                AddTriple(triple_prev);
                AddTriple(triple_next);
            }

            public void Insert(Object data)
            {
                Insert(Близжайшая_тройка(data, false)[0].vertex, data);
                // Додумать if.
            } // Додумать.
            public void Insert(Vertex<Object, DeloneCircle> vertex, Object data)
            {
                // Вставка двух новых троек напротив текущей вершины.
                vertex.Break(data);

                // Изменение связей в смежных тройка при невыполнении условия Делоне.
                Vertex<Object, DeloneCircle> start = vertex.cros;
                Vertex<Object, DeloneCircle> node = start;
                do
                {
                    while (node.cros.triple.delone_circle.ExtendedDistance(node.data) < 0) // Можно использовать разные условия проверки.
                    {
                        DelTriple(node.cros.triple);
                        node.Rebuild();
                    }
                    node.triple.CalculateDeloneCircle();
                    AddTriple(node.triple);
                    node = node.next.cros.next;
                } while (node != start);
            }

            #region Необходимые алгоритмы
            public List<Triple<Object, DeloneCircle>> Близжайшая_тройка(Object data, bool is_full_path)
            {
                return Близжайшая_тройка(triple.next, data, is_full_path);
            }
            public static List<Triple<Object, DeloneCircle>> Близжайшая_тройка(Triple<Object, DeloneCircle> triple, Object data, bool is_full_path)
            {
                List<Triple<Object, DeloneCircle>> res = new List<Triple<Object, DeloneCircle>>(); // Изменить при поиске только последней тройки.

                Vertex<Object, DeloneCircle> minim_vertex = triple.Vertex;
                double minim_dist = minim_vertex.Triple.Delone_Circle.ExtendedDistance(data);

                if (!is_full_path)
                    res.Clear();
                res.Add(minim_vertex.Triple); // Изменить при поиске только последней тройки.

                double temp_dist = minim_vertex.Cros.Triple.Delone_Circle.ExtendedDistance(data);
                if (minim_dist > temp_dist)
                {
                    minim_vertex = minim_vertex.Cros;
                    minim_dist = temp_dist;

                    if (!is_full_path)
                        res.Clear();
                    res.Add(minim_vertex.Triple);  // Изменить при поиске только последней тройки.
                }
                bool is_end = false;
                while (!is_end)
                {
                    is_end = true;
                    double prev_dist = minim_vertex.Prev.Cros.Triple.Delone_Circle.ExtendedDistance(data);
                    double next_dist = minim_vertex.Next.Cros.Triple.Delone_Circle.ExtendedDistance(data);
                    if (prev_dist < next_dist && prev_dist < minim_dist)
                    {
                        minim_dist = prev_dist;
                        minim_vertex = minim_vertex.Prev.Cros;

                        if (!is_full_path)
                            res.Clear();
                        res.Add(minim_vertex.Triple);  // Изменить при поиске только последней тройки.

                        is_end = false;
                    }
                    if (next_dist < prev_dist && next_dist < minim_dist)
                    {
                        minim_dist = next_dist;
                        minim_vertex = minim_vertex.Next.Cros;

                        if (!is_full_path)
                            res.Clear();
                        res.Add(minim_vertex.Triple);  // Изменить при поиске только последней тройки.

                        is_end = false;
                    }
                }
                return res;
            }
            #endregion
        }
    }
}
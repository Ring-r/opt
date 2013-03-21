using System;
using System.Collections;
using System.Collections.Generic;

namespace Opt
{
    namespace GeometricObjects
    {
        /// <summary>
        /// Многоугольник в двухмерном пространстве.
        /// </summary>
        [Serializable]
        public class Polygon : IEnumerable<Point>
        {
            protected List<Point> points = new List<Point>(); // Минус: медленная вставка и удаление вершин. Минус: необходимость следить за границами индекса.

            #region Polygon(...)
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <remarks>Количество вершин равно нулю.</remarks>
            public Polygon()
            {
            } // Многоугольник без вершин не имеет смысла в реальности.
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="count">Количество вершин в многоугольнике.</param>
            /// <remarks>Все вершины являются разными объектами, но имеют одинаковое значение - координаты точек равны нулю.</remarks>
            public Polygon(int count)
            {
                for (int i = 0; i < count; i++)
                    points.Add(new Point());
            } // Многоугольник с нулевым количеством вершин не имеет смысла в реальности. Имеет ли смысл многоугольник с одной или двумя вершинами?
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="points">Набор вершин многоугольника.</param>
            /// <remarks>Так как точка является классом, то точки добавляются по ссылке.</remarks>
            public Polygon(IEnumerable<Point> points)
            {
                this.points.InsertRange(0, points);
            } // Обрабатывается набор с любым количеством вершин. А значит возникают те же вопросы, что и в остальных конструкторах.
            #endregion

            /// <summary>
            /// Количество вершин многоугольника.
            /// </summary>
            public int Count
            {
                get
                {
                    return points.Count;
                }
            }
            /// <summary>
            /// Получить или установить ссылку на вершину.
            /// </summary>
            /// <param name="index">Номер вершины.</param>
            /// <returns>Точка, определяющая вершину.</returns>
            /// <remarks>Так как точка является классом, то происходит возвращение (get) и изменение (set) ссылки на точку.</remarks>
            public Point this[int index]
            {
                get
                {
                    return points[index];
                }
                set
                {
                    points[index] = value;
                }
            } // Минус: выход индекса за границы.

            /// <summary>
            /// Удалить все вершины.
            /// </summary>
            public void Clear()
            {
                points.Clear();
            }
            /// <summary>
            /// Добавить вершину.
            /// </summary>
            /// <param name="index">Номер позиции, в которую происходит вставка вершины.</param>
            /// <param name="point">Добавляемая вершина.</param>
            /// <remarks>Так как точка является классом, то точка добавляются по ссылке.</remarks>
            public void Insert(int index, Point point)
            {
                points.Insert(index, point);
            } // Минус: выход индекса за границы.
            /// <summary>
            /// Добавить набор вершин.
            /// </summary>
            /// <param name="index">Номер позиции, с которой начинается вставка вершин.</param>
            /// <param name="points">Набор, добавляемых вершин.</param>
            /// <remarks>Так как точка являются классом, то точки добавляются по ссылке.</remarks>
            public void Insert(int index, IEnumerable<Point> points)
            {
                this.points.InsertRange(index, points);
            } // Минус: выход индекса за границы.
            /// <summary>
            /// Удалить вершину.
            /// </summary>
            /// <param name="index">Номер позиции, с которой происходит удаление вершины.</param>
            public void Remove(int index)
            {
                points.RemoveAt(index);
            } // Минус: выход индекса за границы.
            /// <summary>
            /// Удалить вершины.
            /// </summary>
            /// <param name="index">Номер позиции, с которой начинается удаление вершин.</param>
            /// <param name="count">Количество удаляемых вершин.</param>
            /// <remarks>Набор вершин рассматривается, как кольцевой список.</remarks>
            public void Remove(int index, int count)
            {
                if (count > points.Count)
                    points.Clear();
                else
                {
                    if (index + count > points.Count)
                    {
                        int temp_count = points.Count - index;
                        points.RemoveRange(index, temp_count);
                        points.RemoveRange(0, count - temp_count);
                    }
                    else
                        points.RemoveRange(index, count);
                }

            } // Минус: выход индекса за границы. Минус: дополнительные проверки для реализации аналога кольцевого списка.

            #region Set(...)
            #endregion

            #region Get(...)
            /// <summary>
            /// Получить вершину.
            /// </summary>
            /// <param name="index">Номер вершины.</param>
            /// <returns>Точка, определяющая вершину.</returns>
            /// <remarks>Происходит возвращение копии точки, задающей вершину, а не её ссылка.</remarks>
            public Point GetVertex(int index)
            {
                return new Point(points[index]);
            } // Минус: выход индекса за границы.
            #endregion

            #region IEnumerable<Point>...
            /// <summary>
            /// Метод необходимый для наследования от интерфейса IEnumerable.
            /// </summary>
            /// <returns>Перечислитель, осуществляющий перебор вершин.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return points.GetEnumerator();
            }
            /// <summary>
            /// Возвращает перечислитель, осуществляющий перебор вершин.
            /// </summary>
            /// <returns>Перечислитель, осуществляющий перебор вершин.</returns>
            /// <remarks>Превращает многоугольник в список вершин (однонаправленный, некольцевой).</remarks>
            public IEnumerator<Point> GetEnumerator()
            {
                return points.GetEnumerator();
            }
            #endregion
        }
    }
}
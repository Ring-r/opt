using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Многоугольник в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Polygon : GeometricsWithList<Point>
    {
        #region Необходимые классы.
        /// <summary>
        /// Класс, который хранит информацию о определённом многоугольнике и определённой вершине.
        /// </summary>
        public class Iterator
        {
            #region Скрытые поля и свойства.
            /// <summary>
            /// Многоугольник, с которым связан итератор. 
            /// </summary>
            protected Polygon polygon;
            /// <summary>
            /// Индекс вершины, с которой связан итератор.
            /// </summary>
            protected int index;
            protected int index_polygon;
            #endregion

            #region Открытые поля и свойства.
            /// <summary>
            /// Возвращает индекс, с которым связан итератор.
            /// </summary>
            public int Index
            {
                get
                {
                    if (polygon.Count >= 0)
                        return index;
                    else
                        return -1;
                }
            }
            public int IndexPolygon
            {
                get
                {
                    return index_polygon;
                }
            }
            /// <summary>
            /// Возвращает многоугольник, с которым связан итератор.
            /// </summary>
            public Polygon Polygon
            {
                get
                {
                    return polygon;
                }
            }
            /// <summary>
            /// Возвращает количество элементов в многоугольнике, с которым связан итератор.
            /// </summary>
            public int Count
            {
                get
                {
                    return polygon.Count;
                }
            }
            #endregion

            #region Iterator(...)
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="polygon">Многоугольник, с которым связан итератор. Многоугольник не может иметь значение null!</param>
            /// <param name="index">Многоугольник.</param>
            public Iterator(int index_polygon, Polygon polygon, int index)
            {
                if (polygon == null)
                    throw new Exception("Значение переменной polygon не может быть null!");

                this.index_polygon = index_polygon;
                this.polygon = polygon;
                this.index = index;
            }
            #endregion

            #region Открытые методы.
            /// <summary>
            /// Операция сдвига итератора на определённое количество элементов.
            /// </summary>
            /// <param name="cor">Количество элементов (может быть отрицательным).</param>
            public void Move(int cor)
            {
                index += cor;                
                polygon.CheckedIndex(ref index);
            }
            /// <summary>
            /// Получить значение точки многоугольника, с которым связан итератор. Значение координат точки в глобальной системе координат.
            /// </summary>
            /// <param name="cor">Относительный сдвиг индекса относительно текущего положения.</param>
            /// <returns>Точка.</returns>
            public Point Point(int cor)
            {
                if (polygon.Count >= 0)
                {
                    return polygon[index + cor] + polygon.Pole.Vector;
                }
                else
                    return null;
            }
            /// <summary>
            /// Получить значение полуплоскости многоугольника, с которым связан итератор. Значение координат полуплоскости в глобальной системе координат.
            /// </summary>
            /// <param name="cor">Относительный сдвиг индекса относительно текущего положения.</param>
            /// <returns>Полуплоскость (вектор нормали направлен от многоугольника).</returns>
            public Plane Plane(int cor)
            {
                if (polygon.Count >= 0)
                {
                    Vector vector = polygon[index + cor + 1].Vector - polygon[index + cor].Vector;
                    return new Plane() { Pole = polygon[index + cor] + polygon.Pole.Vector, Normal = new Vector { X = vector.Y, Y = -vector.X } };
                }
                else
                    return null;
            }
            #endregion
        }
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Polygon Copy
        {
            get
            {
                Polygon rez = new Polygon { Pole = this.Pole.Copy };
                for (int i = 0; i < Count; i++)
                    rez.Add(this[i].Copy);
                return rez;
            }
            set
            {
                Pole.Copy = value.Pole;
                base.list_elements.Clear();
                for (int i = 0; i < value.Count; i++)
                    this.Add(value[i].Copy);
            }
        } // !!!Потом возможно переделать!!!
        #endregion

        #region Polygon(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Polygon()
            : base()
        {
        }
        #endregion


    }
}
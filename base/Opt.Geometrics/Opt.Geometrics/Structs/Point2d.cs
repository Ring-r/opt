using System;

namespace Opt.Geometrics.Struct
{
    /// <summary>
    /// Точка-структура в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public struct Point2d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор, задающий точку.
        /// </summary>
        private Vector2d vector;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт координату X.
        /// </summary>
        public double X
        {
            get
            {
                return vector.X;
            }
            set
            {
                vector.X = value;
            }
        }
        /// <summary>
        /// Получает или задаёт координату Y.
        /// </summary>
        public double Y
        {
            get
            {
                return vector.Y;
            }
            set
            {
                vector.Y = value;
            }
        }
        /// <summary>
        /// Получает или задаёт координаты точки по их номеру. Нулевая координата возвращает 1, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y.
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return 1;
                    case 1: return vector.X;
                    case 2: return vector.Y;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: vector.X = value; break;
                    case 2: vector.Y = value; break;
                }
            }
        }
        /// <summary>
        /// Получить или установить вектор, задающий точку.
        /// </summary>
        public Vector2d Vector
        {
            get
            {
                return vector;
            }
            set
            {
                vector = value;
            }
        }
        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Point2d Copy
        {
            get
            {
                return new Point2d { vector = vector.Copy };
            }
            set
            {
                vector.Copy = value.vector;
            }
        }
        #endregion

        #region Point2d(...)
        #endregion

        #region Операции над точками и векторами.
        /// <summary>
        /// Сложение точки и вектора.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом сложения заданной точки на заданный вектор (начальная точка не изменяется).</returns>
        public static Point2d operator +(Point2d point, Vector2d vector)
        {
            return new Point2d { vector = point.vector + vector };
        }
        /// <summary>
        /// Вычитание вектора из точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки (начальная точка не изменяется).</returns>
        public static Point2d operator -(Point2d point, Vector2d vector)
        {
            return new Point2d { vector = point.vector - vector };
        }

        /// <summary>
        /// Вычитание двух точек.
        /// </summary>
        /// <param name="point_this">Точка.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
        public static Vector2d operator -(Point2d point_this, Point2d point)
        {
            return point_this.vector - point.vector;
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return X.ToString() + ", " + Y.ToString();
        }
    }
}

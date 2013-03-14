using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Точка в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Point : Geometric
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор, задающий точку.
        /// </summary>
        protected Vector vector;
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
        public Vector Vector
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
        public Point Copy
        {
            get
            {
                return new Point() { vector = this.vector.Copy };
            }
            set
            {
                vector.Copy = value.vector;  
            }
        }
        #endregion

        #region Point2d(...)
        /// <summary>
        /// Создание точки с нулевыми координатами.
        /// </summary>
        public Point()
        {
            vector = new Vector();
        }
        #endregion

        #region Операции над точками и векторами.
        /// <summary>
        /// Сложение точки и вектора.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Точка, которая является результатом сложения заданной точки на заданный вектор.</returns>
        public static Point operator +(Point left, Vector right)
        {
            return new Point{ vector = left.vector + right };
        }
        /// <summary>
        /// Вычитание вектора из точки.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки.</returns>
        public static Point operator -(Point left, Vector right)
        {
            return new Point{ vector = left.vector - right };
        }
        /// <summary>
        /// Вычитание двух точек.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Точка.</param>
        /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
        public static Vector operator -(Point left, Point right)
        {
            return left.vector - right.vector;
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
using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Точка-класс в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Point3d : Geometric3d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор, задающий точку.
        /// </summary>
        protected Vector3d vector;
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
        /// Получает или задаёт координату Z.
        /// </summary>
        public double Z
        {
            get
            {
                return vector.Z;
            }
            set
            {
                vector.Z = value;
            }
        }
        /// <summary>
        /// Получает или задаёт координаты точки по их номеру. Нулевая координата возвращает 1, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y, третья координата получает или задаёт координату Z.
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
                    case 3: return vector.Z;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: vector.X = value; break;
                    case 2: vector.Y = value; break;
                    case 3: vector.Z = value; break;
                }
            }
        }
        /// <summary>
        /// Получить или установить вектор, задающий точку.
        /// </summary>
        public Vector3d Vector
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
        public Point3d Copy
        {
            get
            {
                return new Point3d { vector = vector.Copy };
            }
            set
            {
                vector.Copy = value.vector;
            }
        }
        #endregion

        #region Point3d(...)
        /// <summary>
        /// Создание точки с нулевыми координатами.
        /// </summary>
        public Point3d()
        {
            vector = new Vector3d();
        }
        #endregion

        #region Операции над точками и векторами.
        /// <summary>
        /// Сложение точки и вектора.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом сложения заданной точки на заданный вектор (начальная точка не изменяется).</returns>
        public static Point3d operator +(Point3d point, Vector3d vector)
        {
            return new Point3d { vector = point.vector + vector };
        }
        /// <summary>
        /// Вычитание вектора из точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки (начальная точка не изменяется).</returns>
        public static Point3d operator -(Point3d point, Vector3d vector)
        {
            return new Point3d { vector = point.vector - vector };
        }

        /// <summary>
        /// Вычитание двух точек.
        /// </summary>
        /// <param name="point_this">Точка.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
        public static Vector3d operator -(Point3d point_this, Point3d point)
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
            return X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();
        }
    }
}

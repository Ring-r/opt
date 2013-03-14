using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Точка-класс.
    /// </summary>
    [Serializable]
    public class Point<VectorType> : Geometric
        where VectorType : IVector, new()
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор, задающий точку.
        /// </summary>
        protected VectorType vector;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получить или установить вектор, задающий точку.
        /// </summary>
        public VectorType Vector
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
        public Point<VectorType> Copy
        {
            get
            {
                throw new NotImplementedException();
                //return new Point<VectorType> { vector = vector };
            }
            set
            {
                throw new NotImplementedException();
                //vector.Copy = value.vector;
            }
        }
        #endregion

        #region Point(...)
        /// <summary>
        /// Создание точки с нулевыми координатами.
        /// </summary>
        public Point()
        {
            vector = new VectorType();
        }
        #endregion

        #region Операции над точками и векторами.
        /// <summary>
        /// Сложение точки и вектора.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом сложения заданной точки на заданный вектор (начальная точка не изменяется).</returns>
        public static Point<VectorType> operator +(Point<VectorType> point, VectorType vector)
        {
            VectorType vt = (VectorType)((ICloneable)point.vector).Clone();
            vt._Add(vector);
            return new Point<VectorType> { vector = vt };
        }
        /// <summary>
        /// Вычитание вектора из точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки (начальная точка не изменяется).</returns>
        public static Point<VectorType> operator -(Point<VectorType> point, VectorType vector)
        {
            VectorType vt = (VectorType)((ICloneable)point.vector).Clone();
            vt._Deduct(vector);
            return new Point<VectorType> { vector = vt };
        }

        /// <summary>
        /// Вычитание двух точек.
        /// </summary>
        /// <param name="point_this">Точка.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
        public static VectorType operator -(Point<VectorType> point_this, Point<VectorType> point)
        {
            VectorType vt = (VectorType)((ICloneable)point_this.vector).Clone();
            vt._Deduct(point.vector);
            return vt;
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return vector.ToString();
        }
    }
}

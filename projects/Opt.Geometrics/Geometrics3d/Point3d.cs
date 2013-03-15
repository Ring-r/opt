using System;

namespace Opt.Geometrics.Geometrics3d
{
    /// <summary>
    /// Точка в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Point3d : Geometric3d
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Вектор, задающий точку.
        /// </summary>
        protected Vector3d vector = new Vector3d();

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Получает или задаёт координату X.
        /// </summary>
        public double X
        {
            get
            {
                return this.vector.X;
            }
            set
            {
                this.vector.X = value;
            }
        }

        /// <summary>
        /// Получает или задаёт координату Y.
        /// </summary>
        public double Y
        {
            get
            {
                return this.vector.Y;
            }
            set
            {
                this.vector.Y = value;
            }
        }

        /// <summary>
        /// Получает или задаёт координату Z.
        /// </summary>
        public double Z
        {
            get
            {
                return this.vector.Z;
            }
            set
            {
                this.vector.Z = value;
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
                    case 1: return this.vector.X;
                    case 2: return this.vector.Y;
                    case 3: return this.vector.Z;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: this.vector.X = value; break;
                    case 2: this.vector.Y = value; break;
                    case 3: this.vector.Z = value; break;
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
                return this.vector;
            }
            set
            {
                this.vector = value;
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Point3d Copy
        {
            get
            {
                return new Point3d() { vector = this.vector.Copy };
            }
            set
            {
                this.vector.Copy = value.vector;
            }
        }

        #endregion

        #region Операции над точками и векторами.

        /// <summary>
        /// Сложение точки и вектора.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Точка, которая является результатом сложения заданной точки на заданный вектор.</returns>
        public static Point3d operator +(Point3d left, Vector3d right)
        {
            return new Point3d { vector = left.vector + right };
        }

        /// <summary>
        /// Вычитание вектора из точки.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки.</returns>
        public static Point3d operator -(Point3d left, Vector3d right)
        {
            return new Point3d { vector = left.vector - right };
        }

        /// <summary>
        /// Вычитание двух точек.
        /// </summary>
        /// <param name="left">Точка.</param>
        /// <param name="right">Точка.</param>
        /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
        public static Vector3d operator -(Point3d left, Point3d right)
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
            return this.vector.ToString();
        }
    }
}
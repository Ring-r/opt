using System;

namespace Opt
{
    namespace GeometricObjects
    {
        /// <summary>
        /// Точка в двухмерном пространстве.
        /// </summary>
        [Serializable]
        public class Point : IEquatable<Point>
        {
            #region Поля и свойства.
            /// <summary>
            /// Координата X.
            /// </summary>
            protected double x;
            /// <summary>
            /// Получает или задаёт координату X.
            /// </summary>
            public double X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }
            /// <summary>
            /// Координата Y.
            /// </summary>
            protected double y;
            /// <summary>
            /// Получает или задаёт координату Y.
            /// </summary>
            public double Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
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
                        case 1: return x;
                        case 2: return y;
                    }
                    return 0;
                }
                set
                {
                    switch (index)
                    {
                        case 1: x = value; break;
                        case 2: y = value; break;
                    }
                }
            }
            /// <summary>
            /// Получает и задаёт вектор, определяющий точку.
            /// </summary>
            public Vector Vector
            {
                get
                {
                    return new Vector(x, y);
                }
                set
                {
                    x = value.X;
                    y = value.Y;
                }
            }
            #endregion

            #region Point2d(...)
            /// <summary>
            /// Создание точки с нулевыми координатами.
            /// </summary>
            public Point()
            {
                this.x = 0;
                this.y = 0;
            }
            /// <summary>
            /// Создаине точки с заданными координатами.
            /// </summary>
            /// <param name="x">Координата X.</param>
            /// <param name="y">Координата Y.</param>
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            /// <summary>
            /// Создаине точки с заданными координатами.
            /// </summary>
            /// <param name="vector">Координаты, заданные в виде вектора.</param>
            public Point(Vector vector)
            {
                this.x = vector.X;
                this.y = vector.Y;
            }
            /// <summary>
            /// Создание точки с заданными координатами.
            /// </summary>
            /// <param name="point">Координаты, заданные в виде точки.</param>
            public Point(Point point)
            {
                this.x = point.X;
                this.y = point.Y;
            }
            #endregion

            #region Get(...)
            #endregion

            #region Set(...)
            /// <summary>
            /// Заполнение координат нулями.
            /// </summary>
            public void Set()
            {
                this.x = 0;
                this.y = 0;
            }
            /// <summary>
            /// Заполнение координат заданными числами.
            /// </summary>
            /// <param name="x">Координата X.</param>
            /// <param name="y">Координата Y.</param>
            public void Set(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            /// <summary>
            /// Заполнение координат заданными числами.
            /// </summary>
            /// <param name="vector">Координаты, заданные в виде вектора.</param>
            public void Set(Vector vector)
            {
                this.x = vector.X;
                this.y = vector.Y;
            }
            /// <summary>
            /// Заполнение координат заданными числами.
            /// </summary>
            /// <param name="point">Координаты, заданные в виде точки.</param>
            public void Set(Point point)
            {
                this.x = point.x;
                this.y = point.y;
            }
            #endregion

            #region Операции сравнения (отношений).
            /// <summary>
            /// Сравнение двух точек.
            /// </summary>
            /// <param name="point">Точка для сравнения.</param>
            /// <returns>True - если координаты равны между собой.</returns>
            public bool Equals(Point point)
            {
                return x == point.x && y == point.y;
            }
            /// <summary>
            /// Сравнение двух точек.
            /// </summary>
            /// <param name="point">Точка для сравнения.</param>
            /// <param name="eps">Погрешность.</param>
            /// <returns>True - если эвклидовое расстояние меньше заданной погрешности.</returns>
            public bool Equals(Point point, double eps)
            {
                return Math.Sqrt((x - point.x) * (x - point.x) + (y - point.y) * (y - point.y)) < eps;
            }

            // public int Compare(Point point)
            // public int Compare(Point point, double eps)

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
                return new Point() { x = left.x + right.X, y = left.y + right.Y };
            } // !Не нравится такой подход!
            /// <summary>
            /// Вычитание вектора из точки.
            /// </summary>
            /// <param name="left">Точка.</param>
            /// <param name="right">Вектор.</param>
            /// <returns>Точка, которая является результатом вычитание заданного вектора из заданной точки.</returns>
            public static Point operator -(Point left, Vector right)
            {
                return new Point() { x = left.x - right.X, y = left.y - right.Y };
            } // !Не нравится такой подход!
            /// <summary>
            /// Вычитание двух точек.
            /// </summary>
            /// <param name="left">Точка.</param>
            /// <param name="right">Точка.</param>
            /// <returns>Вектор, который является результатом вычитания заданной точки из заданной точки.</returns>
            public static Vector operator -(Point left, Point right)
            {
                return new Vector() { X = left.x - right.x, Y = left.y - right.Y };
            }
            /// <summary>
            /// Прибавление вектора.
            /// </summary>
            /// <param name="vector">Вектор.</param>
            public void Add(Vector vector)
            {
                x += vector.X;
                y += vector.Y;
            }
            /// <summary>
            /// Вычитание вектора.
            /// </summary>
            /// <param name="vector">Вектор.</param>
            public void Deduct(Vector vector)
            {
                x -= vector.X;
                y -= vector.Y;
            }
            #endregion
        }
    }
}
using System;

namespace Opt
{
    namespace GeometricObjects
    {
        /// <summary>
        /// Вектор в двухмерном пространстве.
        /// </summary>
        [Serializable]
        public class Vector : IEquatable<Vector>
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
            /// Получает или задаёт координаты вектора по их номеру. Нулевая координата возвращает 0, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y.
            /// </summary>
            public double this[int index]
            {
                get
                {
                    switch (index)
                    {
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
            /// Получает или задаёт перпендикулярный вектор (поворот против часовой стрелки).
            /// </summary>
            public Vector PVectorRL
            {
                get
                {
                    return new Vector(-y, x);
                }
                set
                {
                    x = value.y;
                    y = -value.x;
                }
            }
            /// <summary>
            /// Получает или задаёт перпендикулярный вектор (поворот по часовой стрелке).
            /// </summary>
            public Vector PVectorRR
            {
                get
                {
                    return new Vector(y, -x);
                }
                set
                {
                    x = -value.y;
                    y = value.x;
                }
            }
            #endregion

            #region Vector2d(...)
            /// <summary>
            /// Создание вектора с нулевыми координатами.
            /// </summary>
            public Vector()
            {
                this.x = 0;
                this.y = 0;
            }
            /// <summary>
            /// Создаине вектора с заданными координатами.
            /// </summary>
            /// <param name="x">Координата X.</param>
            /// <param name="y">Координата Y.</param>
            public Vector(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            /// <summary>
            /// Создание вектора с заданными координатами.
            /// </summary>
            /// <param name="vector">Координаты, заданные в виде вектора.</param>
            public Vector(Vector vector)
            {
                this.x = vector.X;
                this.y = vector.Y;
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
                this.x = vector.x;
                this.y = vector.y;
            }
            #endregion

            #region Операции сравнения (отношений).
            /// <summary>
            /// Сравнение двух векторов.
            /// </summary>
            /// <param name="vector">Вектор для сравнения.</param>
            /// <returns>True - если координаты равны между собой.</returns>
            public bool Equals(Vector vector)
            {
                return x == vector.x && y == vector.y;
            }
            /// <summary>
            /// Сравнение двух векторов.
            /// </summary>
            /// <param name="vector">Вектор для сравнения.</param>
            /// <param name="eps">Погрешность.</param>
            /// <returns>True - если эвклидовое расстояние меньше заданной погрешности.</returns>
            public bool Equals(Vector vector, double eps)
            {
                return Math.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y)) < eps;
            }

            // public int Compare(Vector vector)
            // public int Compare(Vector vector, double eps)

            #endregion

            #region Операции над точками и векторами.
            /// <summary>
            /// Прибавление вектора.
            /// </summary>
            /// <param name="vector">Вектор.</param>
            public void Add(Vector vector)
            {
                x += vector.x;
                y += vector.y;
            }
            /// <summary>
            /// Суммирование двух векторов.
            /// </summary>
            /// <param name="left">Вектор.</param>
            /// <param name="right">Вектор.</param>
            /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
            public static Vector operator +(Vector left, Vector right)
            {
                return new Vector(left.x + right.x, left.y + right.y);
            } // !Не нравится такой подход!
            /// <summary>
            /// Вычитание вектора.
            /// </summary>
            /// <param name="vector">Вектор.</param>
            public void Deduct(Vector vector)
            {
                x -= vector.x;
                y -= vector.y;
            }
            /// <summary>
            /// Разница двух векторов.
            /// </summary>
            /// <param name="left">Вектор.</param>
            /// <param name="right">Вектор.</param>
            /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
            public static Vector operator -(Vector left, Vector right)
            {
                return new Vector(left.x - right.x, left.y - right.y);
            } // !Не нравится такой подход!
            /// <summary>
            /// Вычисление скалярного произведения двух векторов.
            /// </summary>
            /// <param name="left">Вектор.</param>
            /// <param name="right">Вектор.</param>
            /// <returns>Скалярное произведение двух векторов.</returns>
            public static double operator *(Vector left, Vector right)
            {
                return left.x * right.x + left.y * right.y;
            }
            /// <summary>
            /// Произведение на число.
            /// </summary>
            /// <param name="value">Скаляр.</param>
            public void Multiply(double value)
            {
                x *= value;
                y *= value;
            }
            /// <summary>
            /// Произведение вектора на число.
            /// </summary>
            /// <param name="left">Вектор.</param>
            /// <param name="right">Скаляр.</param>
            /// <returns>Вектор, который является произведением вектора на число (начальный вектор не изменяется).</returns>
            public static Vector operator *(Vector left, double right)
            {
                return new Vector(left.x * right, left.y * right);
            } // !Не нравится такой подход!
            /// <summary>
            /// Деление на число.
            /// </summary>
            /// <param name="value">Скаляр.</param>
            public void Divide(double value)
            {
                x /= value;
                y /= value;
            }
            /// <summary>
            /// Деление вектора на число.
            /// </summary>
            /// <param name="left">Вектор.</param>
            /// <param name="right">Скаляр.</param>
            /// <returns>Вектор, который является результатом деления вектора на число (начальный вектор не изменяется).</returns>
            public static Vector operator /(Vector left, double right)
            {
                return new Vector(left.x / right, left.y / right);
            } // !Не нравится такой подход!
            #endregion
        }
    }
}
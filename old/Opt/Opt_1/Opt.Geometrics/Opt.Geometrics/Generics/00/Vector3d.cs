using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Вектор-класс в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector3d : Vector2d, ICloneable
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Координата Z.
        /// </summary>
        protected double z;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Возвращает размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public override int Dim
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Получает или задаёт координату Z.
        /// </summary>
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        /// <summary>
        /// Получает или задаёт координаты вектора по их номеру. Нулевая координата возвращает 0, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y, третья координата получает или задаёт координату Z.
        /// </summary>
        public override double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1: return x;
                    case 2: return y;
                    case 3: return z;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: x = value; break;
                    case 2: y = value; break;
                    case 3: z = value; break;
                }
            }
        }
        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public new Vector3d Copy
        {
            get
            {
                return new Vector3d { x = this.x, y = this.y, z = this.z };
            }
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }
        #endregion

        #region Vector3d(...)
        /// <summary>
        /// Создание вектора с нулевыми координатами.
        /// </summary>
        public Vector3d()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }
        #endregion

        Object ICloneable.Clone()
        {
            return Copy;
        }

        #region Операции над векторами.
        /// <summary>
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Add(Vector3d vector)
        {
            x += vector.x;
            y += vector.y;
            z += vector.z;
        }
        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Deduct(Vector3d vector)
        {
            x -= vector.x;
            y -= vector.y;
            z -= vector.z;
        }

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public virtual void _Multiply(double scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
        }

        /// <summary>
        /// Произведение скаляра на вектор.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является произведением вектора на скаляр (начальный вектор не изменяется).</returns>
        public static Vector3d operator *(double scalar, Vector3d vector)
        {
            return new Vector3d { x = vector.x * scalar, y = vector.y * scalar, z = vector.z * scalar };
        }
        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на скаляр (начальный вектор не изменяется).</returns>
        public static Vector3d operator /(Vector3d vector, double scalar)
        {
            return (1 / scalar) * vector;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector3d operator +(Vector3d vector_this, Vector3d vector)
        {
            return new Vector3d { x = vector_this.x + vector.x, y = vector_this.y + vector.y, z = vector_this.z + vector.z };
        }
        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector3d operator -(Vector3d vector_this, Vector3d vector)
        {
            //return vector_this + (-1) * vector;
            return new Vector3d { x = vector_this.x - vector.x, y = vector_this.y - vector.y, z = vector_this.z + vector.z };
        }

        /// <summary>
        /// Вычисление скалярного произведения двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение двух векторов.</returns>
        public static double operator *(Vector3d vector_this, Vector3d vector)
        {
            return vector_this.x * vector.x + vector_this.y * vector.y + vector_this.z * vector.z;
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns>Строка-информация об объекте.</returns>
        public override string ToString()
        {
            return X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();
        }
    }
}

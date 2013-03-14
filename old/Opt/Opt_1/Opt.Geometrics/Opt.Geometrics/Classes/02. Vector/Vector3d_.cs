using System;

namespace Opt.Geometrics_
{
    /// <summary>
    /// Вектор-класс в трёхмерном пространстве (альтернативная версия).
    /// </summary>
    [Serializable]
    public class Vector3d : Opt.Geometrics.Geometric3d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Координата X.
        /// </summary>
        protected double x;
        /// <summary>
        /// Координата Y.
        /// </summary>
        protected double y;
        /// <summary>
        /// Координата Z.
        /// </summary>
        protected double z;
        #endregion

        #region Открытые поля и свойства.
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
        public double this[int index]
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
        public Vector3d Copy
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

        #region Операции над векторами.
        /// <summary>
        /// Произведение скаляра на вектор.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        /// <param name="vector">Вектор (изменяется).</param>
        /// <returns>Вектор, который является произведением вектора на скаляр.</returns>
        public static Vector3d operator *(double scalar, Vector3d vector)
        {
            vector.x *= scalar;
            vector.y *= scalar;
            vector.z *= scalar;
            return vector;
        }
        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор (изменяется).</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на скаляр.</returns>
        public static Vector3d operator /(Vector3d vector, double scalar)
        {
            return (1 / scalar) * vector;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор (изменяется).</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов.</returns>
        public static Vector3d operator +(Vector3d vector_this, Vector3d vector)
        {
            vector_this.x += vector.x;
            vector_this.y += vector.y;
            vector_this.z += vector.z;
            return vector_this;
        }
        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор (изменяется).</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов.</returns>
        public static Vector3d operator -(Vector3d vector_this, Vector3d vector)
        {
            //return vector_this + (-1) * vector.Copy;
            vector_this.x -= vector.x;
            vector_this.y -= vector.y;
            vector_this.z -= vector.z;
            return vector_this;
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

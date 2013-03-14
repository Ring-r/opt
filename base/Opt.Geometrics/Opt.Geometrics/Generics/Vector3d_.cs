using System;

namespace Opt.Geometrics_.Generic
{
    /// <summary>
    /// Вектор-класс в трёхмерном пространстве (альтернативный вариант).
    /// </summary>
    [Serializable]
    public class Vector3d : Opt.Geometrics.Generic.Vector3d, ICloneable
    {
        #region Открытые поля и свойства.
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
    }
}

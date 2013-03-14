using System;

namespace Opt.Geometrics_.Generic
{
    /// <summary>
    /// Вектор-класс в двухмерном пространстве (альтернативный вариант).
    /// </summary>
    [Serializable]
    public class Vector2d : Opt.Geometrics.Generic.Vector2d, ICloneable
    {
        #region Открытые поля и свойства.
        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public new Vector2d Copy
        {
            get
            {
                return new Vector2d { x = this.x, y = this.y };
            }
            set
            {
                x = value.x;
                y = value.y;
            }
        }
        #endregion

        #region Vector2d(...)
        /// <summary>
        /// Создание вектора с нулевыми координатами.
        /// </summary>
        public Vector2d()
        {
            this.x = 0;
            this.y = 0;
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
        public static Vector2d operator *(double scalar, Vector2d vector)
        {
            vector.x *= scalar;
            vector.y *= scalar;
            return vector;
        }
        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор (изменяется).</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на скаляр.</returns>
        public static Vector2d operator /(Vector2d vector, double scalar)
        {
            return (1 / scalar) * vector;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор (изменяется).</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов.</returns>
        public static Vector2d operator +(Vector2d vector_this, Vector2d vector)
        {
            vector_this.x += vector.x;
            vector_this.y += vector.y;
            return vector_this;
        }
        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор (изменяется).</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов.</returns>
        public static Vector2d operator -(Vector2d vector_this, Vector2d vector)
        {
            //return vector_this + (-1) * vector.Copy;
            vector_this.x -= vector.x;
            vector_this.y -= vector.y;
            return vector_this;
        }

        /// <summary>
        /// Вычисление скалярного произведения двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение двух векторов.</returns>
        public static double operator *(Vector2d vector_this, Vector2d vector)
        {
            return vector_this.x * vector.x + vector_this.y * vector.y;
        }
        #endregion
    }
}

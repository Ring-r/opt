using System;

namespace Opt.Geometrics_.Struct
{
    /// <summary>
    /// Вектор-структура в двухмерном пространстве (альтернативная версия).
    /// </summary>
    [Serializable]
    public struct Vector2d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Координата X.
        /// </summary>
        private double x;
        /// <summary>
        /// Координата Y.
        /// </summary>
        private double y;
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
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Vector2d Copy
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
        #endregion

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

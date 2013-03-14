using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Вектор-класс в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector2d : Vector, ICloneable
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
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Возвращает размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public override int Dim
        {
            get
            {
                return 2;
            }
        }

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
        public override double this[int index]
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
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Add(Vector2d vector)
        {
            x += vector.x;
            y += vector.y;
        }
        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Deduct(Vector2d vector)
        {
            x -= vector.x;
            y -= vector.y;
        }

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public virtual void _Multiply(double scalar)
        {
            x *= scalar;
            y *= scalar;
        }

        /// <summary>
        /// Произведение скаляра на вектор.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является произведением вектора на скаляр (начальный вектор не изменяется).</returns>
        public static Vector2d operator *(double scalar, Vector2d vector)
        {
            return new Vector2d { x = vector.x * scalar, y = vector.y * scalar };
        }
        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на скаляр (начальный вектор не изменяется).</returns>
        public static Vector2d operator /(Vector2d vector, double scalar)
        {
            return (1 / scalar) * vector;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector2d operator +(Vector2d vector_this, Vector2d vector)
        {            
            return new Vector2d { x = vector_this.x + vector.x, y = vector_this.y + vector.y };
        }
        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector2d operator -(Vector2d vector_this, Vector2d vector)
        {
            //return vector_this + (-1) * vector;
            return new Vector2d { x = vector_this.x - vector.x, y = vector_this.y - vector.y };
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

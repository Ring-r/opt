using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Вектор в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector2d
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
        /// Получает или задаёт координату X.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Получает или задаёт координату Y.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Получает или задаёт координаты вектора по их номеру. Нулевая координата возвращает 0, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y, третья координата получает или задаёт координату Z
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1: return this.x;
                    case 2: return this.y;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: this.x = value; break;
                    case 2: this.y = value; break;
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
                return new Vector2d() { x = this.x, y = this.y };
            }
            set
            {
                this.x = value.x;
                this.y = value.y;
            }
        }

        #endregion

        #region Операции над точками и векторами.

        /// <summary>
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public void _Add(Vector2d vector)
        {
            this.x += vector.x;
            this.y += vector.y;
        }

        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public void _Deduct(Vector2d vector)
        {
            this.x -= vector.x;
            this.y -= vector.y;
        }

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public void _Multiply(double scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
        }

        /// <summary>
        /// Разделить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public void _Divide(double scalar)
        {
            this.x /= scalar;
            this.y /= scalar;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector2d operator +(Vector2d left, Vector2d right)
        {
            return new Vector2d() { x = left.x + right.x, y = left.y + right.y };
        }

        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector2d operator -(Vector2d left, Vector2d right)
        {
            return new Vector2d() { x = left.x - right.x, y = left.y - right.y };
        }

        /// <summary>
        /// Вычисление скалярного произведения двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Скалярное произведение двух векторов.</returns>
        public static double operator *(Vector2d left, Vector2d right)
        {
            return left.x * right.x + left.y * right.y;
        }

        /// <summary>
        /// Произведение вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является произведением вектора на число (начальный вектор не изменяется).</returns>
        public static Vector2d operator *(Vector2d left, double right)
        {
            return new Vector2d() { x = left.x * right, y = left.y * right };
        }

        /// <summary>
        /// Деление вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на число (начальный вектор не изменяется).</returns>
        public static Vector2d operator /(Vector2d left, double right)
        {
            return new Vector2d() { x = left.x / right, y = left.y / right };
        }

        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", this.x, this.y);
        }
    }
}
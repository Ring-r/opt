using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Вектор в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector : Geometric
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
        public Vector Copy
        {
            get
            {
                return new Vector() { X = x, Y = y };
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
        public Vector()
        {
            this.x = 0;
            this.y = 0;
        }
        #endregion

        #region Операции над точками и векторами.
        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector() { x = left.x + right.x, y = left.y + right.y };
        }
        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector() { x = left.x - right.x, y = left.y - right.y };
        }
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
        /// Произведение вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является произведением вектора на число (начальный вектор не изменяется).</returns>
        public static Vector operator *(Vector left, double right)
        {
            return new Vector() { x = left.x * right, y = left.y * right };
        }
        /// <summary>
        /// Деление вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на число (начальный вектор не изменяется).</returns>
        public static Vector operator /(Vector left, double right)
        {
            return new Vector() { x = left.x / right, y = left.y / right };
        }                
        #endregion

        #region Дополнительные операции над векторами.
        public Vector Normalize()
        {
            double length = Math.Sqrt(x * x + y * y);
            x /= length;
            y /= length;
            return this;
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
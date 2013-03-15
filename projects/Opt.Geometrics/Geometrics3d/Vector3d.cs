using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Geometrics3d
{
    /// <summary>
    /// Вектор в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector3d : Vector2d
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Координата Z.
        /// </summary>
        protected double z;

        #endregion

        #region Открытые поля и свойства.

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
                    case 3: return this.z;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: this.x = value; break;
                    case 2: this.y = value; break;
                    case 3: this.z = value; break;
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
                return new Vector3d() { x = this.x, y = this.y, z = this.z };
            }
            set
            {
                this.x = value.x;
                this.y = value.y;
                this.z = value.z;
            }
        }

        #endregion

        #region Операции над точками и векторами.

        /// <summary>
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public void _Add(Vector3d vector)
        {
            this.x += vector.x;
            this.y += vector.y;
            this.z += vector.z;
        }

        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public void _Deduct(Vector3d vector)
        {
            this.x -= vector.x;
            this.y -= vector.y;
            this.z -= vector.z;
        }

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public void _Multiply(double scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
            this.z *= scalar;
        }

        /// <summary>
        /// Разделить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public void _Divide(double scalar)
        {
            this.x /= scalar;
            this.y /= scalar;
            this.z /= scalar;
        }

        /// <summary>
        /// Суммирование двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является суммой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector3d operator +(Vector3d left, Vector3d right)
        {
            return new Vector3d() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
        }

        /// <summary>
        /// Разница двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Вектор, который является разницой двух векторов (начальные вектора не изменяются).</returns>
        public static Vector3d operator -(Vector3d left, Vector3d right)
        {
            return new Vector3d() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }

        /// <summary>
        /// Вычисление скалярного произведения двух векторов.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Вектор.</param>
        /// <returns>Скалярное произведение двух векторов.</returns>
        public static double operator *(Vector3d left, Vector3d right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }

        /// <summary>
        /// Произведение вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является произведением вектора на число (начальный вектор не изменяется).</returns>
        public static Vector3d operator *(Vector3d left, double right)
        {
            return new Vector3d() { x = left.x * right, y = left.y * right, z = left.z * right };
        }

        /// <summary>
        /// Деление вектора на число.
        /// </summary>
        /// <param name="left">Вектор.</param>
        /// <param name="right">Скаляр.</param>
        /// <returns>Вектор, который является результатом деления вектора на число (начальный вектор не изменяется).</returns>
        public static Vector3d operator /(Vector3d left, double right)
        {
            return new Vector3d() { x = left.x / right, y = left.y / right, z = left.z / right };
        }

        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.x, this.y, this.z);
        }
    }
}
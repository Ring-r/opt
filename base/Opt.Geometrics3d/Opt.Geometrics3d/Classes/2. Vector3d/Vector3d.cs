using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Вектор в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Vector3d : Geometric3d
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
        /// Получает или задаёт координаты вектора по их номеру. Нулевая координата возвращает 0, первая координата получает или задаёт координату X, вторая координата получает или задаёт координату Y, третья координата получает или задаёт координату Z
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
                return new Vector3d() { X = x, Y = y, Z = z };
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

        #region Операции над точками и векторами.
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
            return X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();
        }
    }
}
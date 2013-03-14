using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Вектор-класс.
    /// </summary>
    [Serializable]
    public abstract class Vector
    {
        #region Открытые поля и свойства.
        /// <summary>
        /// Возвращает размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public virtual int Dim
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Получает или задаёт координаты вектора по их номеру.
        /// </summary>
        public virtual double this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Vector(...)
        #endregion

        #region Операции над векторами.
        /// <summary>
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Add(Vector vector)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        public virtual void _Deduct(Vector vector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        public virtual void _Multiply(double scalar)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

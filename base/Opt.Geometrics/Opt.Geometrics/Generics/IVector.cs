using System;

namespace Opt.Geometrics.Generic
{
    public interface IVector
    {
        #region Открытые поля и свойства.
        /// <summary>
        /// Возвращает размерность пространства, в котором задан вектор.
        /// </summary>
        int Dim { get; set; }

        /// <summary>
        /// Получает или задаёт координаты вектора по их номеру.
        /// </summary>
        double this[int index] { get; set; }
        #endregion

        #region Операции над векторами.
        /// <summary>
        /// Добавить вектор к текущему.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        void _Add(IVector vector);
        /// <summary>
        /// Вычесть вектор из текущего.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        void _Deduct(IVector vector);

        /// <summary>
        /// Умножить текущий вектор на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        void _Multiply(double scalar);
        #endregion
    }
}

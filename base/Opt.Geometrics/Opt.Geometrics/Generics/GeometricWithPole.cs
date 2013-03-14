using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Геометрический объект с полюсом (начало связанной системы координат).
    /// </summary>
    [Serializable]
    public abstract class GeometricWithPole<VectorType> : Geometric
        where VectorType : IVector, new()
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Получить или установить полюс.
        /// </summary>
        protected Point<VectorType> pole;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point<VectorType> Pole
        {
            get
            {
                return pole;
            }
            set
            {
                pole = value;
            }
        }
        #endregion

        #region GeometricWithPole(...)
        /// <summary>
        /// Создание геометрического объекта с полюсом, координаты которого равны нулю.
        /// </summary>
        public GeometricWithPole()
        {
            pole = new Point<VectorType>();
        }
        #endregion
    }
}

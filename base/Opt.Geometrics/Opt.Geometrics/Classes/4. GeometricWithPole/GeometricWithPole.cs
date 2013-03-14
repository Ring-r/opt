using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Геометрический объект с полюсом (начало связанной системы координат).
    /// </summary>
    [Serializable]
    public abstract class GeometricWithPole : Geometric, IGeometricWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        protected Point pole;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point Pole
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
        public GeometricWithPole()
        {
            pole = new Point();
        }
        #endregion
    }
}

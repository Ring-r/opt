using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Геометрический объект с полюсом (начало связанной системы координат).
    /// </summary>
    public abstract class GeometricWithPole3d : Geometric3d, IGeometricWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        protected Point3d pole;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point3d Pole
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

        #region GeometricWithPole3d(...)
        public GeometricWithPole3d()
        {
            pole = new Point3d();
        }
        #endregion
    }
}

using System;

namespace Opt.Geometrics.Geometrics3d
{
    /// <summary>
    /// Геометрический объект с полюсом (начало связанной системы координат).
    /// </summary>
    public abstract class Geometric3dWithPole : Geometric3d
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        protected Point3d pole = new Point3d();

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point3d Pole
        {
            get
            {
                return this.pole;
            }
            set
            {
                this.pole = value;
            }
        }

        #endregion
    }
}

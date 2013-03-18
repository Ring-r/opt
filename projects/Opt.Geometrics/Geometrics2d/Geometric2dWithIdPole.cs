using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Геометрический объект с идентификатором и полюсом (начало связанной системы координат).
    /// </summary>
    [Serializable]
    public abstract class Geometric2dWithIdPole : Geometric2dWithId
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        protected Point2d pole = new Point2d();

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point2d Pole
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

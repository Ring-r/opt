using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.SpecialGeometrics
{
    /// <summary>
    /// Круг Делоне в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class DeloneCircle2d : Geometric2dWithPoint
    {
        #region Скрытые поля и свойства.

        protected Double scalar = 0.0;
        protected Vector2d vector = new Vector2d();

        #endregion

        #region Открытые поля и свойства.

        public Double Scalar
        {
            get
            {
                return this.scalar;
            }
            set
            {
                this.scalar = value;
            }
        }

        public Vector2d Vector
        {
            get
            {
                return this.vector;
            }
            set
            {
                this.vector = value;
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public DeloneCircle2d Copy
        {
            get
            {
                return new DeloneCircle2d() { point = this.point.Copy, scalar = this.scalar, vector = this.vector.Copy };
            }
            set
            {
                this.point.Copy = value.point;
                this.scalar = value.scalar;
                this.vector.Copy = value.vector;
            }
        }

        #endregion
    }
}

using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Сфера (круг) в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric2dWithPointScalar : Geometric2dWithPoint
    {
        protected double scalar = 0.0;
        public double Scalar
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

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Geometric2dWithPointScalar Copy
        {
            get
            {
                return new Geometric2dWithPointScalar() { scalar = this.scalar, point = this.point.Copy };
            }
            set
            {
                this.scalar = value.scalar;
                this.point.Copy = value.point;
            }
        }

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", this.point, this.scalar);
        }
    }
}
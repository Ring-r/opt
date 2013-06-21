using System;

namespace Opt.Geometrics.Geometrics2d
{
    [Serializable]
    public abstract class Geometric2dWithPoint : Geometric2d
    {
        protected Point2d point = new Point2d();
        public Point2d Point
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }

        /// <summary>
        /// Хранит значение полюса (начала связанной системы координат).
        /// </summary>
        public Point2d Pole
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }
    }
}

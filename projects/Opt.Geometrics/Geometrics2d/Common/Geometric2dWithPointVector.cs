using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Прямоугольник в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric2dWithPointVector : Geometric2dWithPoint
    {
        protected Vector2d vector = new Vector2d();
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
        public Geometric2dWithPointVector Copy
        {
            get
            {
                return new Geometric2dWithPointVector() { vector = this.vector.Copy, point = this.Pole.Copy };
            }
            set
            {
                this.vector.Copy = value.vector;
                this.point.Copy = value.point;
            }
        }
    }
}

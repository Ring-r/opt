using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Полуплоскость в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Plane2d : Geometric2dWithPointVector
    {
        /// <summary>
        /// Получает или задаёт вектор нормали.
        /// </summary>
        public Vector2d Normal
        {
            get
            {
                return this.vector;
            }
            set
            {
                double length = value * value;
                if (length != 0)
                {
                    this.vector = value;
                    if (length != 1)
                    {
                        length = Math.Sqrt(length);
                        this.vector.Copy /= length;
                    }
                }
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Plane2d Copy
        {
            get
            {
                return new Plane2d() { vector = this.vector.Copy, point = this.Pole.Copy };
            }
            set
            {
                this.vector.Copy = value.vector;
                this.point.Copy = value.point;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Plane2d()
        {
            this.vector = new Vector2d() { X = 1, Y = 0 };
        }

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}; {2}", base.ToString(), Pole, Normal);
        }
    }
}

using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Прямая линия.
    /// </summary>
    [Serializable]
    public class StripLine2d : Geometric2dWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор направления.
        /// </summary>
        protected Vector2d vector;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Установить или получить вектор направления.
        /// </summary>
        public Vector2d Vector
        {
            get
            {
                return vector;
            }
            set
            {
                double length = value * value;
                if (length != 0)
                {
                    vector = value;
                    if (length != 1)
                    {
                        length = Math.Sqrt(length);
                        vector.Copy /= length;
                    }
                }
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public StripLine2d Copy
        {
            get
            {
                return new StripLine2d() { vector = this.vector.Copy, pole = this.Pole.Copy };
            }
            set
            {
                vector.Copy = value.vector;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region StripLine(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public StripLine2d()
        {
            this.vector = new Vector2d() { X = 1, Y = 0 };
            this.pole = new Point2d();
        }
        #endregion
    }
}

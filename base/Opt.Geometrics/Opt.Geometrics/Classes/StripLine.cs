using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Прямая линия.
    /// </summary>
    [Serializable]
    public class StripLine : GeometricWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор направления.
        /// </summary>
        protected Vector vector;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Установить или получить вектор направления.
        /// </summary>
        public Vector Vector
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
        public StripLine Copy
        {
            get
            {
                return new StripLine() { vector = this.vector.Copy, pole = this.Pole.Copy };
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
        public StripLine()
        {
            this.vector = new Vector() { X = 1, Y = 0 };
            this.pole = new Point();
        }
        #endregion
    }
}

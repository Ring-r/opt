using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Прямоугольник в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Rectangle : GeometricWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Размер.
        /// </summary>
        protected Vector size;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт размер.
        /// </summary>
        public Vector Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Rectangle Copy
        {
            get
            {
                return new Rectangle() { size = this.size.Copy, pole = this.Pole.Copy };
            }
            set
            {
                size.Copy = value.size;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region Rectangle(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Rectangle()
        {
            this.size = new Vector();
            this.pole = new Point();
        }
        #endregion
    }
}

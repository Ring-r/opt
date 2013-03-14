using System;

namespace Opt.Geometrics.Generic
{
    /// <summary>
    /// Прямоугольник.
    /// </summary>
    [Serializable]
    public class Rectangle<VectorType> : GeometricWithPole<VectorType>
        where VectorType : IVector, new()
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Размер.
        /// </summary>
        protected VectorType size;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт размер.
        /// </summary>
        public VectorType Size
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
        public Rectangle<VectorType> Copy
        {
            get
            {
                throw new NotImplementedException();
                //return new Rectangle<Vector> { size = this.size.Copy, pole = this.Pole.Copy };
            }
            set
            {
                throw new NotImplementedException();
                //size.Copy = value.size;
                //pole.Copy = value.pole;
            }
        }
        #endregion

        #region Rectangle(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Rectangle()
        {
            this.size = new VectorType();
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return pole.ToString() + "; " + size.ToString();
        }
    }
}

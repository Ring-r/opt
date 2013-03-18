using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Сфера (круг) в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric2dWithIdPoleValue : Geometric2dWithIdPole
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Радиус.
        /// </summary>
        protected double value;

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Получает или задаёт радиус.
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Geometric2dWithIdPoleValue Copy
        {
            get
            {
                return new Geometric2dWithIdPoleValue() { value = this.value, pole = this.pole.Copy };
            }
            set
            {
                this.value = value.value;
                this.pole.Copy = value.pole;
            }
        }

        #endregion

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", this.pole, this.value);
        }
    }
}

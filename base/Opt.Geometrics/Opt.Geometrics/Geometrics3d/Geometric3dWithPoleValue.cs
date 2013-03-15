using System;

namespace Opt.Geometrics.Geometrics3d
{
    /// <summary>
    /// Сфера в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric3dWithPoleValue : Geometric3dWithPole
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
        public Geometric3dWithPoleValue Copy
        {
            get
            {
                return new Geometric3dWithPoleValue() { value = this.value, pole = this.pole.Copy };
            }
            set
            {
                this.value = value.value;
                this.pole.Copy = value.pole;
            }
        }

        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", this.pole, this.value);
        }
    }
}
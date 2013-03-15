using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Прямоугольник в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric2dWithPoleVector : Geometric2dWithPole
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Размер.
        /// </summary>
        protected Vector2d vector = new Vector2d();

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Получает или задаёт размер.
        /// </summary>
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
        public Geometric2dWithPoleVector Copy
        {
            get
            {
                return new Geometric2dWithPoleVector() { vector = this.vector.Copy, pole = this.Pole.Copy };
            }
            set
            {
                this.vector.Copy = value.vector;
                this.pole.Copy = value.pole;
            }
        }

        #endregion
    }
}

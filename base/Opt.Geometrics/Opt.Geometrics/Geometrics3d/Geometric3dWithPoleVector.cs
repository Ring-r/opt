using System;

namespace Opt.Geometrics.Geometrics3d
{
    /// <summary>
    /// Параллелепипед в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Geometric3dWithPoleVector : Geometric3dWithPole
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Размер.
        /// </summary>
        protected Vector3d vector = new Vector3d();

        #endregion

        #region Открытые поля и свойства.

        /// <summary>
        /// Получает или задаёт размер.
        /// </summary>
        public Vector3d Vector
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
        public Geometric3dWithPoleVector Copy
        {
            get
            {
                return new Geometric3dWithPoleVector() { vector = this.vector.Copy, pole = this.Pole.Copy };
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
using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Сфера в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public class Sphera3d : GeometricWithPole3d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Радиус.
        /// </summary>
        protected double radius;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт радиус.
        /// </summary>
        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Sphera3d Copy
        {
            get
            {
                return new Sphera3d() { radius = this.radius, pole = this.pole.Copy };
            }
            set
            {
                radius = value.radius;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region Sphera3d(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Sphera3d()
            : base()
        {
            this.radius = 0;
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информаицю об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Pole.ToString() + "; " + Radius.ToString();
        }
    }
}
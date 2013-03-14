using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Круг в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Circle : GeometricWithPole
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
        public Circle Copy
        {
            get
            {
                return new Circle() { radius = this.radius, pole = this.pole.Copy };
            }
            set
            {
                radius = value.radius;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region Circle(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Circle()
            :base()
        {
            this.radius = 0;            
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}; {2}", base.ToString(), Pole, Radius);
        }
    }
}
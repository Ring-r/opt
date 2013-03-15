using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Полуплоскость в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Plane2d : Geometric2dWithPole
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор нормали.
        /// </summary>
        protected Vector2d normal;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт вектор нормали.
        /// </summary>
        public Vector2d Normal
        {
            get
            {
                return normal;
            }
            set
            {
                double length = value * value;
                if (length != 0)
                {
                    normal = value;
                    if (length != 1)
                    {
                        length = Math.Sqrt(length);
                        normal.Copy /= length;
                    }
                }
            }
        }

        /// <summary>
        /// Получить копию объекта или установить значения свойств, не изменяя ссылку на объект.
        /// </summary>
        public Plane2d Copy
        {
            get
            {
                return new Plane2d() { normal = this.normal.Copy, pole = this.Pole.Copy };
            }
            set
            {
                normal.Copy = value.normal;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region Plane(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Plane2d()
        {
            this.normal = new Vector2d() { X = 1, Y = 0 };
            this.pole = new Point2d();
        }
        #endregion

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}; {2}", base.ToString(), Pole, Normal);
        }
    }
}

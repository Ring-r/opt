using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Прямоугольник в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class Parallelepiped3d : GeometricWithPole3d
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Размер.
        /// </summary>
        protected Vector3d size;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или задаёт размер.
        /// </summary>
        public Vector3d Size
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
        public Parallelepiped3d Copy
        {
            get
            {
                return new Parallelepiped3d() { size = this.size.Copy, pole = this.Pole.Copy };
            }
            set
            {
                size.Copy = value.size;
                pole.Copy = value.pole;
            }
        }
        #endregion

        #region Parallelepiped3d(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Parallelepiped3d()
        {
            this.size = new Vector3d();
            this.pole = new Point3d();
        }
        #endregion
    }
}
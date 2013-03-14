using System;

namespace Opt.Geometrics3d
{
    /// <summary>
    /// Интерфейс для геометрического объекта с полюсом.
    /// </summary>
    public interface IGeometricWithPole
    {
        /// <summary>
        /// Получает или задаёт полюс.
        /// </summary>
        Point3d Pole { get; set; }
    }
}

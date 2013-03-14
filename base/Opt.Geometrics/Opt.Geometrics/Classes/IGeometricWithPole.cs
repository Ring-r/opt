using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Интерфейс для геометрического объекта с полюсом.
    /// </summary>
    public interface IGeometricWithPole
    {
        /// <summary>
        /// Получает или задаёт полюс.
        /// </summary>
        Point Pole { get; set; }
    }
}

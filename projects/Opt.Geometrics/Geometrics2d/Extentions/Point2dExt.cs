using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Geometrics2d.Extentions
{
    /// <summary>
    /// Расширения для полуплоскости.
    /// </summary>
    public static class Point2dExt
    {
        #region Расширенное расстояние.
        /// <summary>
        /// Получить расширенное расстояние от точки до точки.
        /// </summary>
        /// <param name="point_this">Точка.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(this Point2d point_this, Point2d point)
        {
            Vector2d vector = point - point_this;
            return Math.Sqrt(vector * vector);
        }
        #endregion
    }
}

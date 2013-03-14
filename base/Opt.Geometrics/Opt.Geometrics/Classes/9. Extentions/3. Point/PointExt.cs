using System;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для полуплоскости.
    /// </summary>
    public static class PointExt
    {
        #region Расширенное расстояние.
        /// <summary>
        /// Получить расширенное расстояние от точки до точки.
        /// </summary>
        /// <param name="point_this">Точка.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(this Point point_this, Point point)
        {
            Vector vector = point - point_this;
            return Math.Sqrt(vector * vector);
        }
        #endregion
    }
}

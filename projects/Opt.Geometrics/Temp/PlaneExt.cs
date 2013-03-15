using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для точки.
    /// </summary>
    public static class PlaneExt
    {
        #region Расширенное расстояние.
        /// <summary>
        /// Получить расширенное расстояние от полуплоскости до точки.
        /// </summary>
        /// <param name="plane_this">Полуплоскость.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Plane2d plane_this, Point2d point)
        {
            return (point - plane_this.Pole) * plane_this.Normal;
        }
        /// <summary>
        /// Получить расширенное расстояние от полуплоскости до круга.
        /// </summary>
        /// <param name="plane_this">Полуплоскость.</param>
        /// <param name="circle">Круг.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Plane2d plane_this, Geometric2dWithPoleValue circle)
        {
            return Расширенное_расстояние(plane_this, circle.Pole) - circle.Value;
        }
        /// <summary>
        /// Получить расширенное расстояние от полуплоскости до полуплоскости.
        /// </summary>
        /// <param name="plane_this">Полуплоскость.</param>
        /// <param name="plane">Полуплоскость.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Plane2d plane_this, Plane2d plane)
        {
            if (plane_this.Normal * plane.Normal == -1)
                return Расширенное_расстояние(plane_this, plane.Pole);
            else
                return double.PositiveInfinity;
        }
        #endregion

        #region Годограф функции плотного размещения.
        /// <summary>
        /// Получить годограф функции плотного размещения круга возле полуплоскости.
        /// </summary>
        /// <param name="plane_this">Полуплоскость.</param>
        /// <param name="circle">Круг.</param>
        /// <returns>Годограф функции плотного размещения.</returns>
        public static Plane2d Годограф_функции_плотного_размещения(Plane2d plane_this, Geometric2dWithPoleValue circle)
        {
            return new Plane2d() { Pole = plane_this.Pole + plane_this.Normal * circle.Value, Normal = plane_this.Normal };
        }
        #endregion

        #region Точка пересечения границ.
        /// <summary>
        /// Получить точку пересечения границ полуплоскости и круга.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Точка пересечения.</returns>
        public static Point2d Точка_пересечения_границ(Plane2d plane_prev, Geometric2dWithPoleValue circle_next)
        {
            Vector2d vector_prev = plane_prev.Normal._I_(false);
            Vector2d vector = plane_prev.Pole - circle_next.Pole;

            double b = vector_prev * vector;
            double c = vector * vector - circle_next.Value * circle_next.Value;
            double d = b * b - c;
            if (d < 0)
                return null;
            else
            {
                double t = -b + Math.Sqrt(d);
                return plane_prev.Pole + vector_prev * t;
            }
        }
        /// <summary>
        /// Получить точку пересечения полуплоскости и полуплоскости.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Точка пересечения.</returns>
        public static Point2d Точка_пересечения_границ(Plane2d plane_prev, Plane2d plane_next)
        {
            Vector2d plane_prev_vector = plane_prev.Normal._I_(false);
            double t = plane_prev_vector * plane_next.Normal;
            if (t == 0)
                return null;
            else
            {
                t = (plane_next.Pole - plane_prev.Pole) * plane_next.Normal / t;
                return plane_prev.Pole + plane_prev_vector * t;
            }
        }
        #endregion

        #region Серединная полуплоскость.
        /// <summary>
        /// Получить серединную полуплоскость полуплоскости и круга.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Серединная полуплоскость.</returns>
        public static Plane2d Серединная_полуплоскость(Plane2d plane_prev, Geometric2dWithPoleValue circle_next)
        {
            return new Plane2d { Pole = circle_next.Pole.Copy, Normal = plane_prev.Normal._I_(false) };
        }
        /// <summary>
        /// Получить серединную полуплоскость полуплоскости и полуплоскости.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Серединная полуплоскость.</returns>
        public static Plane2d Серединная_полуплоскость(Plane2d plane_prev, Plane2d plane_next)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

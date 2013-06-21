using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Geometrics2d.Extentions
{
    /// <summary>
    /// Расширения для круга.
    /// </summary>
    public static class Circle2dExt
    {
        #region Расширенное расстояние.
        /// <summary>
        /// Получить расширенное расстояние от круга до точки.
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="point">Точка.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Geometric2dWithPointScalar circle_this, Point2d point)
        {
            return Point2dExt.Расширенное_расстояние(circle_this.Pole, point) - circle_this.Scalar;
        }
        /// <summary>
        /// Получить расширенное расстояние от круга до круга.
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="circle">Круг.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Geometric2dWithPointScalar circle_this, Geometric2dWithPointScalar circle)
        {
            return Расширенное_расстояние(circle_this, circle.Pole) - circle.Scalar;
        }
        #endregion

        #region Годограф функции плотного размещения.
        /// <summary>
        /// Получить годограф функции плотного размещения круга возле круга.
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="circle">Круг.</param>
        /// <returns>Годограф функции плотного размещения.</returns>
        public static Geometric2dWithPointScalar Годограф_функции_плотного_размещения(Geometric2dWithPointScalar circle_this, Geometric2dWithPointScalar circle)
        {
            return new Geometric2dWithPointScalar { Pole = circle_this.Pole.Copy, Scalar = circle_this.Scalar + circle.Scalar };
        }
        #endregion

        #region Точка пересечения границ.
        /// <summary>
        /// Получить точку пересечения границ круга и круга.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Точка пересечения (одна из двух).</returns>
        public static Point2d Точка_пересечения_границ(Geometric2dWithPointScalar circle_prev, Geometric2dWithPointScalar circle_next)
        {
            Vector2d vector = circle_next.Pole - circle_prev.Pole;
            Vector2d vector_ = vector._I_(false);
            double vector_vector = vector * vector;

            double pr = circle_prev.Scalar * circle_prev.Scalar / vector_vector;
            double nr = circle_next.Scalar * circle_next.Scalar / vector_vector;

            double vector_length = -(nr - pr - 1) / 2;
            double vector_length_ = Math.Sqrt(pr - vector_length * vector_length);

            return circle_prev.Pole + vector * vector_length + vector_ * vector_length_;
        }
        /// <summary>
        /// Получить точку пересечения границ круга и полуплоскости.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Точка пересечения.</returns>
        public static Point2d Точка_пересечения_границ(Geometric2dWithPointScalar circle_prev, Plane2d plane_next)
        {
            Vector2d vector_prev = plane_next.Normal._I_(false);
            Vector2d vector = plane_next.Pole - circle_prev.Pole;

            double b = vector_prev * vector;
            double c = vector * vector - circle_prev.Scalar * circle_prev.Scalar;
            double d = b * b - c;
            if (d < 0)
                return null;
            else
            {
                double t = -b - Math.Sqrt(d);
                return plane_next.Pole + vector_prev * t;
            }
        }
        #endregion

        #region Точка близости второго рода.
        /// <summary>
        /// Получить точку близости второго рода для круга и множества (круг, круг).
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Точка близости (одна из двух).</returns>
        public static Point2d Точка_близости_второго_рода(this Geometric2dWithPointScalar circle_this, Geometric2dWithPointScalar circle_prev, Geometric2dWithPointScalar circle_next)
        {
            return Точка_пересечения_границ(Годограф_функции_плотного_размещения(circle_prev, circle_this), Годограф_функции_плотного_размещения(circle_next, circle_this));
        }
        /// <summary>
        /// Получить точку близости второго рода для круга и множества (полуплоскость, круг).
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Точка близости (одна из двух).</returns>
        public static Point2d Точка_близости_второго_рода(this Geometric2dWithPointScalar circle_this, Geometric2dWithPointScalar circle_prev, Plane2d plane_next)
        {
            return Точка_пересечения_границ(Годограф_функции_плотного_размещения(circle_prev, circle_this), Plane2dExt.Годограф_функции_плотного_размещения(plane_next, circle_this));
        }
        /// <summary>
        /// Получить точку близости второго рода для круга и множества (полуплоскость, круг).
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Точка близости (одна из двух).</returns>
        public static Point2d Точка_близости_второго_рода(this Geometric2dWithPointScalar circle_this, Plane2d plane_prev, Geometric2dWithPointScalar circle_next)
        {
            return Plane2dExt.Точка_пересечения_границ(Plane2dExt.Годограф_функции_плотного_размещения(plane_prev, circle_this), Годограф_функции_плотного_размещения(circle_next, circle_this));
        }
        /// <summary>
        /// Получить точку близости второго рода для круга и множества (полуплоскость, полуплоскость).
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Точка близости.</returns>
        public static Point2d Точка_близости_второго_рода(this Geometric2dWithPointScalar circle_this, Plane2d plane_prev, Plane2d plane_next)
        {
            return Plane2dExt.Точка_пересечения_границ(Plane2dExt.Годограф_функции_плотного_размещения(plane_prev, circle_this), Plane2dExt.Годограф_функции_плотного_размещения(plane_next, circle_this));
        }
        #endregion

        #region Серединная полуплоскость.
        /// <summary>
        /// Получить серединную полуплоскость круга и круга.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Серединная полуплоскость.</returns>
        public static Plane2d Серединная_полуплоскость(Geometric2dWithPointScalar circle_prev, Geometric2dWithPointScalar circle_next)
        {
            return new Plane2d { Pole = circle_prev.Pole.Copy, Normal = (circle_next.Pole - circle_prev.Pole)._I_(false) };
        }
        /// <summary>
        /// Получить серединную полуплоскость круга и полуплоскости.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Серединная полуплоскость.</returns>
        public static Plane2d Серединная_полуплоскость(Geometric2dWithPointScalar circle_prev, Plane2d plane_next)
        {
            return new Plane2d { Pole = circle_prev.Pole.Copy, Normal = plane_next.Normal._I_(true) };
        }
        #endregion
    }
}

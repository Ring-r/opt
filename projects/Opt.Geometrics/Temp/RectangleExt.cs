using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для прямоугольника в двухмерном пространстве.
    /// </summary>
    public static class RectangleExt
    {
        /// <summary>
        /// Преобразование прямоугольника в многоугольник.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Polygon2d ToPolygon(this Geometric2dWithPoleVector rectangle)
        {
            Polygon2d polygon = new Polygon2d { Pole = rectangle.Pole.Copy };
            polygon.Add(new Point2d());
            polygon.Add(new Point2d { X = rectangle.Vector.X });
            polygon.Add(new Point2d { X = rectangle.Vector.X, Y = rectangle.Vector.Y });
            polygon.Add(new Point2d { Y = rectangle.Vector.Y });
            return polygon;
        }
    }
}

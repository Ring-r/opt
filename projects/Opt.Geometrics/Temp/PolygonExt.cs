using System;
using System.Collections.Generic;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для многоугольника в двухмерном пространстве.
    /// </summary>
    public static class PolygonExt
    {
        /// <summary>
        /// Преобразование многоугольника в прямоугольник.
        /// </summary>
        /// <param name="polygon">Многоугольник.</param>
        /// <returns>Прямоугольник.</returns>
        public static Geometric2dWithPoleVector ToRectangle(this Polygon2d polygon)
        {
            Geometric2dWithPoleVector rectangle = new Geometric2dWithPoleVector();
            Vector2d size_min = new Vector2d { X = double.PositiveInfinity, Y = double.PositiveInfinity };
            Vector2d size_max = new Vector2d { X = double.NegativeInfinity, Y = double.NegativeInfinity };

            for (int i = 0; i < polygon.Count; i++)
            {
                if (size_min.X > polygon[i].X)
                    size_min.X = polygon[i].X;
                if (size_min.Y > polygon[i].Y)
                    size_min.Y = polygon[i].Y;

                if (size_max.X < polygon[i].X)
                    size_max.X = polygon[i].X;
                if (size_max.Y < polygon[i].Y)
                    size_max.Y = polygon[i].Y;
            }

            rectangle.Vector.Copy = size_max - size_min;

            return rectangle;
        }
    }
}

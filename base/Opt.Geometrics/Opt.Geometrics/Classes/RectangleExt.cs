using System;

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
        public static Polygon ToPolygon(this Rectangle rectangle)
        {
            Polygon polygon = new Polygon { Pole = rectangle.Pole.Copy };
            polygon.Add(new Point());
            polygon.Add(new Point { X = rectangle.Size.X });
            polygon.Add(new Point { X = rectangle.Size.X, Y = rectangle.Size.Y });
            polygon.Add(new Point { Y = rectangle.Size.Y });
            return polygon;
        }
    }
}

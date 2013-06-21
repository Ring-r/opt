namespace Opt.Geometrics.Geometrics2d.Extentions
{
    /// <summary>
    /// Расширения для прямоугольника в двухмерном пространстве.
    /// </summary>
    public static class Rectangle2dExt
    {
        /// <summary>
        /// Преобразование прямоугольника в многоугольник.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Polygon2d ToPolygon(this Geometric2dWithPointVector rectangle)
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

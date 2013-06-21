namespace Opt.Geometrics.Geometrics2d.Extentions
{
    /// <summary>
    /// Расширения для многоугольника в двухмерном пространстве.
    /// </summary>
    public static class Polygon2dExt
    {
        /// <summary>
        /// Преобразование многоугольника в прямоугольник.
        /// </summary>
        /// <param name="polygon">Многоугольник.</param>
        /// <returns>Прямоугольник.</returns>
        public static Geometric2dWithPointVector ToRectangle(this Polygon2d polygon)
        {
            Geometric2dWithPointVector rectangle = new Geometric2dWithPointVector();
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

        //public static void Read(Polygon polygon, TextReader tr)
        //{
        //    polygon.Clear();
        //    int n = int.Parse(tr.ReadLine());
        //    string[] s = (tr.ReadLine()).Split(' ');
        //    for (int i = 0; i < n; i++)
        //        polygon.Insert(polygon.Count, new Point(double.Parse(s[2 * i]), double.Parse(s[2 * i + 1])));
        //}
        //public static void Write(Polygon polygon, TextWriter tw)
        //{
        //    tw.WriteLine(polygon.Count);
        //    for (int i = 0; i < polygon.Count; i++)
        //    {
        //        tw.Write(polygon[i].X);
        //        tw.Write(" ");
        //        tw.Write(polygon[i].Y);
        //        tw.Write(" ");
        //    }
        //    tw.WriteLine();
        //}
    }
}

using System;
using System.IO;
using System.Drawing;

namespace Opt
{
    namespace GeometricObjects
    {
        namespace Extending
        {
            public class PolygonExtending
            {
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

                //public static PointF[] ToArrayOfPointF(Polygon polygon)
                //{
                //    PointF[] points = new PointF[polygon.Count];
                //    for (int i = 0; i < polygon.Count; i++)
                //        points[i] = new PointF((float)polygon[i].X, (float)polygon[i].Y);
                //    return points;
                //}
                //public static void Draw(Polygon polygon, Graphics graphics, Pen pen)
                //{
                //    PointF[] points = ToArrayOfPointF(polygon);
                //    if (points.Length > 1)
                //        graphics.DrawPolygon(pen, points);
                //}
                //public static void Fill(Polygon polygon, Graphics graphics, Brush brush)
                //{
                //    PointF[] points = ToArrayOfPointF(polygon);
                //    if (points.Length > 1)
                //        graphics.FillPolygon(brush, points);
                //}
                //public static void DrawFill(Polygon polygon, Graphics graphics, Pen pen, Brush brush)
                //{
                //    PointF[] points = ToArrayOfPointF(polygon);
                //    if (points.Length > 1)
                //    {
                //        graphics.DrawPolygon(pen, points);
                //        graphics.FillPolygon(brush, points);
                //    }
                //}
            }
        }
    }
}

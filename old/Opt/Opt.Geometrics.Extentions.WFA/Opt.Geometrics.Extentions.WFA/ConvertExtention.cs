using System;
using Opt.Geometrics;

namespace Opt.Geometrics.Extentions.WFA
{
    public static class ConvertExtention
    {
        public static System.Drawing.Point ToSystemDrawingPoint(this Point point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }
        public static System.Drawing.PointF ToSystemDrawingPointF(this Point point)
        {
            return new System.Drawing.PointF((float)point.X, (float)point.Y);
        }

        public static System.Drawing.Rectangle ToSystemDrawingRectangle(this Circle circle)
        {
            return new System.Drawing.Rectangle((int)(circle.Pole.X - circle.Radius), (int)(circle.Pole.Y - circle.Radius), 2 * (int)circle.Radius, 2 * (int)circle.Radius);
        }
        public static System.Drawing.RectangleF ToSystemDrawingRectangleF(this Circle circle)
        {
            return new System.Drawing.RectangleF((float)(circle.Pole.X - circle.Radius), (float)(circle.Pole.Y - circle.Radius), 2 * (float)circle.Radius, 2 * (float)circle.Radius);
        }

        public static System.Drawing.Rectangle ToSystemDrawingRectangle(this Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int)rectangle.Pole.X, (int)rectangle.Pole.Y, (int)rectangle.Size.X, (int)rectangle.Size.Y);
        }
        public static System.Drawing.RectangleF ToSystemDrawingRectangleF(this Rectangle rectangle)
        {
            return new System.Drawing.RectangleF((float)rectangle.Pole.X, (float)rectangle.Pole.Y, (float)rectangle.Size.X, (float)rectangle.Size.Y);
        }

        public static System.Drawing.Point[] ToSystemDrawingPoints(this Polygon polygon)
        {
            System.Drawing.Point[] points = new System.Drawing.Point[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToSystemDrawingPoint();
            return points;
        }
        public static System.Drawing.PointF[] ToSystemDrawingPointFs(this Polygon polygon)
        {
            System.Drawing.PointF[] points = new System.Drawing.PointF[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToSystemDrawingPointF();
            return points;
        }
    }
}

using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;
using Rectangle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleVector;

namespace Opt.Geometrics.Extentions.WFA
{
    public static class ConvertExtention
    {
        public static System.Drawing.Point ToSystemDrawingPoint(this Point2d point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }
        public static System.Drawing.PointF ToSystemDrawingPointF(this Point2d point)
        {
            return new System.Drawing.PointF((float)point.X, (float)point.Y);
        }

        public static System.Drawing.Rectangle ToSystemDrawingRectangle(this Circle circle)
        {
            return new System.Drawing.Rectangle((int)(circle.Pole.X - circle.Value), (int)(circle.Pole.Y - circle.Value), 2 * (int)circle.Value, 2 * (int)circle.Value);
        }
        public static System.Drawing.RectangleF ToSystemDrawingRectangleF(this Circle circle)
        {
            return new System.Drawing.RectangleF((float)(circle.Pole.X - circle.Value), (float)(circle.Pole.Y - circle.Value), 2 * (float)circle.Value, 2 * (float)circle.Value);
        }

        public static System.Drawing.Rectangle ToSystemDrawingRectangle(this Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int)rectangle.Pole.X, (int)rectangle.Pole.Y, (int)rectangle.Vector.X, (int)rectangle.Vector.Y);
        }
        public static System.Drawing.RectangleF ToSystemDrawingRectangleF(this Rectangle rectangle)
        {
            return new System.Drawing.RectangleF((float)rectangle.Pole.X, (float)rectangle.Pole.Y, (float)rectangle.Vector.X, (float)rectangle.Vector.Y);
        }

        public static System.Drawing.Point[] ToSystemDrawingPoints(this Polygon2d polygon)
        {
            System.Drawing.Point[] points = new System.Drawing.Point[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToSystemDrawingPoint();
            return points;
        }
        public static System.Drawing.PointF[] ToSystemDrawingPointFs(this Polygon2d polygon)
        {
            System.Drawing.PointF[] points = new System.Drawing.PointF[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToSystemDrawingPointF();
            return points;
        }
    }
}

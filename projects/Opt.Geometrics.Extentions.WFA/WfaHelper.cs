using System.Drawing;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Extentions.Wfa
{
    public static class WfaHelper
    {
        public static Point ToPoint(this Point2d point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }
        public static PointF ToPointF(this Point2d point)
        {
            return new System.Drawing.PointF((float)point.X, (float)point.Y);
        }

        public static Rectangle ToRectangle(this Geometric2dWithPointScalar circle)
        {
            return new System.Drawing.Rectangle((int)(circle.Pole.X - circle.Scalar), (int)(circle.Pole.Y - circle.Scalar), 2 * (int)circle.Scalar, 2 * (int)circle.Scalar);
        }
        public static RectangleF ToRectangleF(this Geometric2dWithPointScalar circle)
        {
            return new System.Drawing.RectangleF((float)(circle.Pole.X - circle.Scalar), (float)(circle.Pole.Y - circle.Scalar), 2 * (float)circle.Scalar, 2 * (float)circle.Scalar);
        }

        public static Rectangle ToRectangle(this Geometric2dWithPointVector rectangle)
        {
            return new System.Drawing.Rectangle((int)rectangle.Pole.X, (int)rectangle.Pole.Y, (int)rectangle.Vector.X, (int)rectangle.Vector.Y);
        }
        public static RectangleF ToRectangleF(this Geometric2dWithPointVector rectangle)
        {
            return new System.Drawing.RectangleF((float)rectangle.Pole.X, (float)rectangle.Pole.Y, (float)rectangle.Vector.X, (float)rectangle.Vector.Y);
        }

        public static Point[] ToArrayOfPoint(this Polygon2d polygon)
        {
            System.Drawing.Point[] points = new System.Drawing.Point[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToPoint();
            return points;
        }
        public static PointF[] ToArrayOfPointF(this Polygon2d polygon)
        {
            PointF[] points = new PointF[polygon.Count];
            for (int i = 0; i < polygon.Count; i++)
                points[i] = (polygon[i] + polygon.Pole.Vector).ToPointF();
            return points;
        }

        public static void FillAndDraw(this Graphics graphics, Brush brush, Pen pen, Point2d point, float halfSize = 2)
        {
            graphics.FillEllipse(brush, (float)point.X - 1, (float)point.Y - 1, halfSize, halfSize);
            graphics.DrawEllipse(pen, (float)point.X - 1, (float)point.Y - 1, halfSize, halfSize);
        }

        public static void FillAndDraw(this Graphics graphics, Brush brush, Pen pen, Geometric2dWithPointScalar circle)
        {
            graphics.FillEllipse(brush, circle.ToRectangleF());
            graphics.DrawEllipse(pen, circle.ToRectangleF());
        }

        public static void FillAndDraw(this Graphics graphics, Brush brush, Pen pen, Geometric2dWithPointVector rectangle)
        {
            graphics.FillRectangle(brush, rectangle.ToRectangleF());
            graphics.DrawRectangle(pen, rectangle.ToRectangle());
        }

        public static void Draw(this Graphics graphics, Pen pen, Polygon2d polygon)
        {
            if (polygon.Count == 2)
            {
                graphics.DrawLine(pen, polygon[0].ToPointF(), polygon[1].ToPointF());
            }
            if (polygon.Count > 2)
            {
                PointF[] points = polygon.ToArrayOfPointF();
                if (points.Length > 1)
                    graphics.DrawPolygon(pen, points);
            }
        }
        public static void Fill(this Graphics graphics, Brush brush, Polygon2d polygon)
        {
            if (polygon.Count > 2)
            {
                PointF[] points = polygon.ToArrayOfPointF();
                if (points.Length > 1)
                    graphics.FillPolygon(brush, points);
            }
        }
        public static void FillAndDraw(this Graphics graphics, Brush brush, Pen pen, Polygon2d polygon)
        {
            if (polygon.Count == 2)
            {
                graphics.DrawLine(pen, polygon[0].ToPointF(), polygon[1].ToPointF());
            }
            if (polygon.Count > 2)
            {
                PointF[] points = polygon.ToArrayOfPointF();
                graphics.FillPolygon(brush, points);
                graphics.DrawPolygon(pen, points);
            }
        }

        public static void FillAndDraw(this Graphics graphics, Polygon2d region, Brush brush, Pen pen, Plane2d plane)
        {
            Polygon2d polygon = new Polygon2d();
            for (int i = 0; i < region.Count; i++)
            {
                double ed = ((region[i] + region.Pole.Vector) - plane.Pole) * plane.Normal;
                if (ed < 0)
                    polygon.Add(region[i] + region.Pole.Vector);

                double ed_temp = (region[i + 1] - region[i]) * plane.Normal;
                if (ed_temp != 0)
                {
                    double t = -(((region[i] + region.Pole.Vector) - plane.Pole) * plane.Normal) / ed_temp;
                    if (0 < t && t <= 1)
                        polygon.Add(region[i] + region.Pole.Vector + (region[i + 1] - region[i]) * t);
                }
            }

            graphics.FillAndDraw(brush, pen, polygon);
        } // !!!Переделать!!!

        public static void FillAndDraw_(this Graphics graphics, Polygon2d region, Brush brush, Pen pen, Geometric2d geometric)
        {
            if (geometric is Point2d)
                graphics.FillAndDraw(brush, pen, geometric as Point2d);
            if (geometric is Geometric2dWithPointScalar)
                graphics.FillAndDraw(brush, pen, geometric as Geometric2dWithPointScalar);
            if (geometric is Geometric2dWithPointVector)
                graphics.FillAndDraw(brush, pen, geometric as Geometric2dWithPointVector);
            if (geometric is Polygon2d)
                graphics.FillAndDraw(brush, pen, geometric as Polygon2d);
            if (geometric is Plane2d)
                graphics.FillAndDraw(region, brush, pen, geometric as Plane2d);
        }
    }
}

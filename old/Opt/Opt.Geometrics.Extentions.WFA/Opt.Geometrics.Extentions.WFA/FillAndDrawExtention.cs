using System;

namespace Opt.Geometrics.Extentions.WFA
{
    public static class FillAndDrawExtention
    {
        public static void FillAndDraw(this System.Drawing.Graphics graphics, System.Drawing.Brush brush, System.Drawing.Pen pen, Point point)
        {
            graphics.FillEllipse(brush, (float)point.X - 1, (float)point.Y - 1, 2, 2);
            graphics.DrawEllipse(pen, (float)point.X - 1, (float)point.Y - 1, 2, 2);
        }
        public static void FillAndDraw(this System.Drawing.Graphics graphics, System.Drawing.Brush brush, System.Drawing.Pen pen, Circle circle)
        {
            graphics.FillEllipse(brush, circle.ToSystemDrawingRectangleF());
            graphics.DrawEllipse(pen, circle.ToSystemDrawingRectangleF());
        }
        public static void FillAndDraw(this System.Drawing.Graphics graphics, Polygon region, System.Drawing.Brush brush, System.Drawing.Pen pen, Plane plane)
        {
            Polygon polygon = new Polygon();
            for (int i = 0; i < region.Count; i++)
            {
                double ed = ((region[i] + region.Pole.Vector) - plane.Pole) * plane.Normal;
                if (ed < 0)
                    polygon.Add(region[i] + region.Pole.Vector);

                double ed_temp = (region[i + 1] - region[i]) * plane.Normal;
                if (ed_temp != 0)
                {
                    double t = -(((region[i]+ region.Pole.Vector) - plane.Pole) * plane.Normal) / ed_temp;
                    if (0 < t && t <= 1)
                        polygon.Add(region[i] + region.Pole.Vector + (region[i + 1] - region[i]) * t);
                }
            }

            graphics.FillAndDraw(brush, pen, polygon);
        } // !!!Перделать!!!
        public static void FillAndDraw(this System.Drawing.Graphics graphics, System.Drawing.Brush brush, System.Drawing.Pen pen, Polygon polygon)
        {
            if (polygon.Count == 2)
            {
                graphics.DrawLine(pen, polygon[0].ToSystemDrawingPointF(), polygon[1].ToSystemDrawingPointF());
            }
            if (polygon.Count > 2)
            {
                System.Drawing.PointF[] points = polygon.ToSystemDrawingPointFs();
                graphics.FillPolygon(brush, points);
                graphics.DrawPolygon(pen, points);
            }
        }
        public static void FillAndDraw(this System.Drawing.Graphics graphics, System.Drawing.Brush brush, System.Drawing.Pen pen, Rectangle rectangle)
        {
            graphics.FillRectangle(brush, rectangle.ToSystemDrawingRectangleF());
            graphics.DrawRectangle(pen, rectangle.ToSystemDrawingRectangle());
        }

        public static void FillAndDraw_(this System.Drawing.Graphics graphics, Polygon region, System.Drawing.Brush brush, System.Drawing.Pen pen, Geometric geometric)
        {
            if (geometric is Point)
                graphics.FillAndDraw(brush, pen, geometric as Point);
            if (geometric is Circle)
                graphics.FillAndDraw(brush, pen, geometric as Circle);
            if (geometric is Plane)
                graphics.FillAndDraw(region, brush, pen, geometric as Plane);
            if (geometric is Polygon)
                graphics.FillAndDraw(brush, pen, geometric as Polygon);
            if (geometric is Rectangle)
                graphics.FillAndDraw(brush, pen, geometric as Rectangle);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Extentions.WFA;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.SpecialGeometrics;

namespace Opt.Geometrics.WFAT
{
    public partial class FormMain : Form
    {
        private List<Polygon2d> polygon_list;
        private Polygon2d polygon;
        private Point2d pole;
        private bool is_edit;

        public FormMain()
        {
            InitializeComponent();

            polygon_list = new List<Polygon2d>();
            polygon = null;

            is_edit = false;

            #region Тестовый вариант.
            pole = new Point2d { X = 0, Y = 30 };

            Polygon2d polygon_temp = new Polygon2d { Pole = new Point2d { X = 30, Y = 10 } };
            polygon_temp.Add(new Point2d { X = 0, Y = 0 });
            polygon_temp.Add(new Point2d { X = 100, Y = 0 });
            polygon_temp.Add(new Point2d { X = 100, Y = 100 });
            polygon_list.Add(polygon_temp);

            polygon_temp = new Polygon2d { Pole = new Point2d { X = 150, Y = 90 } };
            polygon_temp.Add(new Point2d { X = 50, Y = 0 });
            polygon_temp.Add(new Point2d { X = 100, Y = 50 });
            polygon_temp.Add(new Point2d { X = 50, Y = 150 });
            polygon_temp.Add(new Point2d { X = 0, Y = 50 });
            polygon_list.Add(polygon_temp);
            #endregion
        }

        private Point2d point_prev = new Point2d();
        private Point2d point_curr = new Point2d();
        private bool is_mouse_down = false;
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            point_curr.X = e.X;
            point_curr.Y = e.Y;
            is_mouse_down = true;

            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2)
            {
                is_edit = !is_edit;
                if (is_edit && polygon == null)
                {
                    polygon = new Polygon2d { Pole = new Point2d { Vector = point_curr - pole } };
                    polygon_list.Add(polygon);
                }
                if (!is_edit && !polygon.IsRightPolygon())
                    polygon_list.Remove(polygon);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
            {
                Point2d point = new Point2d { Vector = point_curr - pole };
                if (is_edit)
                {
                    point.Vector -= polygon.Pole.Vector;
                    polygon.Add(point);
                }
                else
                {
                    polygon = null;
                    for (int i = 0; i < polygon_list.Count && polygon == null; i++)
                        if (polygon_list[i].IsContain(point))
                            polygon = polygon_list[i];
                }
            }

            Invalidate();
        }
        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            point_prev.Copy = point_curr;
            point_curr.X = e.X;
            point_curr.Y = e.Y;

            statusLabel.Text = "Координаты мышки: " + (point_curr - pole).ToString();

            if (is_mouse_down)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    pole.Copy += point_curr - point_prev;
                if (e.Button == System.Windows.Forms.MouseButtons.Left && polygon != null)
                    polygon.Pole.Copy += point_curr - point_prev;

                Invalidate();
            }
        }
        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            is_mouse_down = false;
        }

        public void DrawGrid(System.Drawing.Graphics g, Point2d pole)
        {
            int step = 30;
            for (int x = (int)pole.X; x >= 0; x -= step)
                g.DrawLine(System.Drawing.Pens.Silver, x, 0, x, ClientRectangle.Height);
            for (int x = (int)pole.X; x <= ClientRectangle.Width; x += step)
                g.DrawLine(System.Drawing.Pens.Silver, x, 0, x, ClientRectangle.Height);

            for (int y = (int)pole.Y; y >= 0; y -= step)
                g.DrawLine(System.Drawing.Pens.Silver, 0, y, ClientRectangle.Width, y);
            for (int y = (int)pole.Y; y <= ClientRectangle.Width; y += step)
                g.DrawLine(System.Drawing.Pens.Silver, 0, y, ClientRectangle.Width, y);

            g.DrawLine(System.Drawing.Pens.Black, (int)pole.X, 0, (int)pole.X, ClientRectangle.Height);
            g.DrawLine(System.Drawing.Pens.Black, 0, (int)pole.Y, ClientRectangle.Width, (int)pole.Y);
        }
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //e.Graphics.ScaleTransform(1, -1);
            //e.Graphics.TranslateTransform(0, -ClientSize.Height);

            if (is_edit)
            {
                e.Graphics.Clear(System.Drawing.Color.FromArgb(200, 200, 200));

                Point2d pole_temp = polygon.Pole;
                polygon.Pole += pole.Vector;

                DrawGrid(e.Graphics, polygon.Pole);
                e.Graphics.FillAndDraw(System.Drawing.Brushes.Green, System.Drawing.Pens.Black, polygon);

                polygon.Pole = pole_temp;
            }
            else
            {
                e.Graphics.Clear(System.Drawing.Color.White);

                DrawGrid(e.Graphics, pole);
                for (int i = 0; i < polygon_list.Count; i++)
                {
                    Point2d pole_temp = polygon_list[i].Pole;
                    polygon_list[i].Pole += pole.Vector;

                    if (polygon_list[i] == polygon)
                        e.Graphics.FillAndDraw(System.Drawing.Brushes.Blue, System.Drawing.Pens.Black, polygon_list[i]);
                    else
                        e.Graphics.FillAndDraw(System.Drawing.Brushes.Yellow, System.Drawing.Pens.Black, polygon_list[i]);

                    polygon_list[i].Pole = pole_temp;
                }
            }
            Polygon2d region = new Polygon2d { Pole = new Point2d() };
            region.Add(new Point2d());
            region.Add(new Point2d { X = ClientSize.Width });
            region.Add(new Point2d { X = ClientSize.Width, Y = ClientSize.Height });
            region.Add(new Point2d { Y = ClientSize.Height });
            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 100, 100, 100));
            if (plane_dividing_list.Count > 0)
            {
                for (int i = 0; i < plane_dividing_list.Count; i++)
                {
                    Plane2d plane = plane_dividing_list[i].IteratorPlane.Plane(0);
                    Point2d pole_temp = plane.Pole;
                    plane.Pole += pole.Vector;

                    e.Graphics.FillAndDraw(region, brush, System.Drawing.Pens.Black, plane);

                    plane.Pole = pole_temp;
                }
            }
        }

        private List<PlaneDividing> plane_dividing_list = new List<PlaneDividing>();
        private void toolStripMenuItemCreateDividePlane_Click(object sender, EventArgs e)
        {
            if (plane_dividing_list.Count == 0)
                for (int i = 0; i < polygon_list.Count - 1; i++)
                {
                    for (int j = i + 1; j < polygon_list.Count; j++)
                    {
                        PlaneDividing plane_dividing = new PlaneDividing(new Polygon2d.Iterator(0, polygon_list[i], 0), new Polygon2d.Iterator(0, polygon_list[j], 0));
                        plane_dividing.Find();
                        plane_dividing_list.Add(plane_dividing);
                    }
                }
            else
                plane_dividing_list.Clear();

            Invalidate();
        }
    }

    public static class Assistant
    {
        public static bool IsContain(this Polygon2d polygon, Point2d point)
        {
            if (polygon.Count <= 1)
                return false;
            bool is_contain = true;
            Polygon2d.Iterator iterator = new Polygon2d.Iterator(0, polygon, 0);
            for (int i = 0; i < iterator.Polygon.Count && is_contain; i++)
                is_contain = PlaneExt.Расширенное_расстояние(iterator.Plane(i), point) <= 0;
            return is_contain;
        }

        public static bool IsRightPolygon(this Polygon2d polygon)
        {
            Polygon2d.Iterator iterator;
            iterator = new Polygon2d.Iterator(0, polygon, 0);
            bool is_right_polygon = true;
            for (int i = 0; i < iterator.Polygon.Count && is_right_polygon; i++)
                is_right_polygon = PlaneExt.Расширенное_расстояние(iterator.Plane(i), iterator.Point(i + 2)) < 0;
            return is_right_polygon;
        }
    }
}

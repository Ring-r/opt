using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Opt.ClosenessModel.Extentions;
using Opt.Geometrics;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Extentions.Wfa;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Extentions;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPointScalar;

namespace Opt.ClosenessModel.WFAT
{
    public partial class MainForm : Form
    {
        private Random rand;
        private Vertex<Geometric> vertex;
        private List<Vertex<Geometric>> triples;
        private List<Vertex<Geometric>> path = null;

        private Polygon2d region;

        private bool is_delone_circles_show = true;
        private bool is_path_show = false;
        private bool is_circles_show = true;

        public MainForm()
        {
            InitializeComponent();

            region = new Polygon2d();
            region.Add(new Point2d());
            region.Add(new Point2d { X = ClientRectangle.Width });
            region.Add(new Point2d { X = ClientRectangle.Width, Y = ClientRectangle.Height });
            region.Add(new Point2d { Y = ClientRectangle.Height });

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоугольника.
            // TODO: Переделать на многольник.
            List<Plane2d> borders = new List<Plane2d>();
            for (int i = 0; i < region.Count; i++)
                borders.Add(new Plane2d { Pole = region[i].Copy, Normal = (region[i + 1] - region[i])._I_(false) });
            vertex = Vertex<Geometric>.CreateClosenessModel(borders[0], borders[1], borders[2]);

            vertex.BreakCrosBy(borders[3]);
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать?!!
            vertex.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Width - ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Scalar = ClientRectangle.Height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Scalar = ClientRectangle.Height / 2 });

            //vertex.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[0], borders[1], borders[2]));
            //vertex.Cros.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[2], borders[3], borders[0]));

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion

            rand = new Random();

            triples = ClosenessModelExt<Geometric, Circle>.GetTriples(this.vertex);
        }

        private bool PointInTriple(Circle circle, Vertex<Geometric> vertex)
        {
            if (vertex.Somes.CircleDelone == null)
                return false;

            double ed = Circle2dExt.Расширенное_расстояние(circle, vertex.Somes.CircleDelone);
            bool point_in_triple = ed < 0;
            if (point_in_triple)
            {
                Vertex<Geometric> vertex_temp = vertex;
                do
                {
                    point_in_triple = point_in_triple && Circle2dExt.Расширенное_расстояние(circle, vertex_temp.Cros.Somes.CircleDelone) > ed;

                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
            return point_in_triple;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            for (int index = 0; index < 10; ++index)
            {
                Vertex<Geometric> vertex = null;
                Vertex<Geometric> vertex_temp;

                #region Заполнение круга.
                //Circle circle = new Circle() { Pole = new Point2d { X = e.X, Y = e.Y } };
                Circle circle = new Circle() { Pole = new Point2d { X = this.rand.Next(10, this.ClientSize.Width - 10), Y = this.rand.Next(10, this.ClientSize.Height - 10) } };

                int k = -1;
                for (int i = 0; i < triples.Count && k == -1; i++)
                    if (PointInTriple(circle, triples[i]))
                        k = i;
                if (k > -1)
                {
                    vertex = triples[k];

                    double radius_max = double.PositiveInfinity;

                    vertex_temp = vertex;
                    do
                    {
                        radius_max = Math.Min(Geometric2dExt.Расширенное_расстояние(circle, vertex_temp.DataInVertex as Geometric2d), radius_max);
                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != vertex);
                    circle.Scalar = radius_max * rand.NextDouble();
                }
                #endregion

                if (vertex != null)
                {
                    vertex.BreakCrosBy(circle);
                    vertex = vertex.Cros;

                    vertex_temp = vertex;
                    do
                    {
                        while (Circle2dExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                        {
                            vertex_temp.Rebuild();
                        }

                        vertex_temp.SetCircleDelone(Geometric2dExt.Круг_Делоне(vertex_temp.Prev.DataInVertex as Geometric2d, vertex_temp.DataInVertex as Geometric2d, vertex_temp.Next.DataInVertex as Geometric2d));

                        vertex_temp = vertex_temp.Next.Cros.Next;
                    } while (vertex_temp != vertex);

                    triples = ClosenessModelExt<Geometric, Circle>.GetTriples(this.vertex);
                }

                Invalidate();
                this.Refresh();
                System.Threading.Thread.Sleep(100);
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (vertex != null)
            {
                e.Graphics.Clear(System.Drawing.Color.White);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                for (int i = 0; i < triples.Count; i++)
                {
                    if (is_delone_circles_show)
                        e.Graphics.FillAndDraw(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 255, 255, 0)), System.Drawing.Pens.Gray, triples[i].Somes.CircleDelone);

                    Vertex<Geometric> vertex_temp = triples[i];
                    do
                    {
                        //e.Graphics.FillAndDraw(System.Drawing.Brushes.Red, System.Drawing.Pens.Red, AssistantExt.Отрезок_(vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                        if (vertex_temp.Somes.CircleDelone.Scalar != 0 && vertex_temp.Cros.Somes.CircleDelone.Scalar != 0)
                            e.Graphics.DrawCurve(System.Drawing.Pens.Red, AssistantExt.Отрезок(vertex_temp).ToArray());

                        if (is_circles_show)
                            e.Graphics.FillAndDraw_(region, System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, vertex_temp.DataInVertex as Geometric2d);

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }

            if (is_path_show && path != null && path.Count > 2)
            {
                System.Drawing.PointF[] points = new System.Drawing.PointF[path.Count];
                for (int i = 0; i < path.Count; i++)
                    points[i] = new System.Drawing.PointF((float)path[i].Somes.CircleDelone.Pole.X, (float)path[i].Somes.CircleDelone.Pole.Y);
                e.Graphics.DrawLines(System.Drawing.Pens.Green, points);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            region[1].X = ClientRectangle.Width;
            region[2].X = ClientRectangle.Width;
            region[2].Y = ClientRectangle.Height;
            region[3].Y = ClientRectangle.Height;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if (e.KeyCode == Keys.F2)
            {
                is_delone_circles_show = !is_delone_circles_show;
                Invalidate();
            }
            if (e.KeyCode == Keys.F3)
            {
                is_path_show = !is_path_show;
                Invalidate();
            }
            if (e.KeyCode == Keys.F4)
            {
                is_circles_show = !is_circles_show;
                Invalidate();
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.vertex != null)
            {
                this.path = ClosenessModelExt<Geometric, Circle>.NearestFullPath(this.vertex, new Circle() { Pole = new Point2d() { X = e.X, Y = e.Y } });
            }

            this.Invalidate();
        }
    }

    public static class VertexExtention
    {
        public static void SetCircleDelone(this Vertex<Geometric> vertex, Circle circle_delone)
        {
            vertex.Prev.Somes.CircleDelone = circle_delone;
            vertex.Somes.CircleDelone = circle_delone;
            vertex.Next.Somes.CircleDelone = circle_delone;
        }
    }

    public static class AssistantExt
    {
        public static Polygon2d Отрезок(Circle circle_this, Circle circle)
        {
            Polygon2d polygon = new Polygon2d();
            Vector2d vector = circle.Pole - circle_this.Pole;
            double length = Math.Sqrt(vector * vector);
            vector /= length;
            polygon.Add(circle_this.Pole + vector * circle_this.Scalar);
            polygon.Add(circle.Pole - vector * circle.Scalar);
            return polygon;
        }
        public static Polygon2d Отрезок(Circle circle_this, Plane2d plane)
        {
            Polygon2d polygon = new Polygon2d();
            polygon.Add(circle_this.Pole - plane.Normal * circle_this.Scalar);
            polygon.Add(circle_this.Pole - plane.Normal * (circle_this.Scalar + Plane2dExt.Расширенное_расстояние(plane, circle_this)));
            return polygon;
        }

        public static Polygon2d Отрезок_(Geometric geometric_this, Geometric geometric)
        {
            if (geometric_this is Circle)
            {
                if (geometric is Circle)
                    return Отрезок(geometric_this as Circle, geometric as Circle);
                if (geometric is Plane2d)
                    return Отрезок(geometric_this as Circle, geometric as Plane2d);
            }
            if (geometric_this is Plane2d)
            {
                if (geometric is Circle)
                    return Отрезок(geometric as Circle, geometric_this as Plane2d);
            }
            return new Polygon2d();
        }

        public static List<System.Drawing.PointF> Отрезок(Vertex<Geometric> vertex)
        {
            List<System.Drawing.PointF> points = new List<System.Drawing.PointF>();
            double ed = Circle2dExt.Расширенное_расстояние(vertex.Somes.CircleDelone, vertex.Cros.Somes.CircleDelone) / 2;
            double k = 0.1;
            Point2d point;
            Circle circle = new Circle { Pole = vertex.Somes.CircleDelone.Pole.Copy, Scalar = vertex.Somes.CircleDelone.Scalar + ed };
            Circle circle_cros = new Circle { Pole = vertex.Cros.Somes.CircleDelone.Pole.Copy, Scalar = vertex.Cros.Somes.CircleDelone.Scalar + ed };
            do
            {
                circle.Scalar += k;
                circle_cros.Scalar += k;
                point = Circle2dExt.Точка_пересечения_границ(circle, circle_cros);
                if (point != null)
                    points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (Geometric2dExt.Расширенное_расстояние(vertex.Prev.DataInVertex as Geometric2d, point) > 0 && points.Count < 10000);

            circle.Scalar = vertex.Somes.CircleDelone.Scalar + ed;
            circle_cros.Scalar = vertex.Cros.Somes.CircleDelone.Scalar + ed;
            do
            {
                circle.Scalar += k;
                circle_cros.Scalar += k;
                point = Circle2dExt.Точка_пересечения_границ(circle_cros, circle);
                if (point != null)
                    points.Insert(0, new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (Geometric2dExt.Расширенное_расстояние(vertex.Next.DataInVertex as Geometric2d, point) > 0 && points.Count < 10000);

            return points;
        }
    }
}

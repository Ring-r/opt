using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Extentions.WFA;
using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.ClosenessModel.WFAT
{
    public partial class Form1 : Form
    {
        private Random rand;
        private Vertex<Geometric2d> vertex;
        private List<Vertex<Geometric2d>> triples;

        private Polygon2d region;

        public Form1()
        {
            InitializeComponent();

            region = new Polygon2d();
            region.Add(new Point2d());
            region.Add(new Point2d { X = ClientRectangle.Width });
            region.Add(new Point2d { X = ClientRectangle.Width, Y = ClientRectangle.Height });
            region.Add(new Point2d { Y = ClientRectangle.Height });

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоугольника. !!!Потом переделать на многольник!!!
            List<Plane2d> borders = new List<Plane2d>();
            for (int i = 0; i < region.Count; i++)
                borders.Add(new Plane2d { Pole = region[i].Copy, Normal = (region[i + 1] - region[i])._I_(false) });
            vertex = Vertex<Geometric2d>.CreateClosenessModel(borders[0], borders[1], borders[2]);

            vertex.BreakCrosBy(borders[3]);
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать?!!
            vertex.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Width - ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Value = ClientRectangle.Height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Value = ClientRectangle.Height / 2 });

            //vertex.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[0], borders[1], borders[2]));
            //vertex.Cros.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[2], borders[3], borders[0]));

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion

            rand = new Random();

            triples = vertex.GetTriples();
        }

        private bool PointInTriple(Circle circle, Vertex<Geometric2d> vertex)
        {
            if (vertex.Somes.CircleDelone == null)
                return false;

            double ed = CircleExt.Расширенное_расстояние(circle, vertex.Somes.CircleDelone);
            bool point_in_triple = ed < 0;
            if (point_in_triple)
            {
                Vertex<Geometric2d> vertex_temp = vertex;
                do
                {
                    point_in_triple = point_in_triple && CircleExt.Расширенное_расстояние(circle, vertex_temp.Cros.Somes.CircleDelone) > ed;

                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
            return point_in_triple;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Circle circle = new Circle() { Pole = new Point2d { X = e.X, Y = e.Y } };
            int k = -1;
            for (int i = 0; i < triples.Count; i++)
                if (PointInTriple(circle, triples[i]))
                    k = i;
            if (k > -1)
            {
                Vertex<Geometric2d> vertex = triples[k];

                #region Определение радиуса круга.
                double radius_max = double.PositiveInfinity;
                Vertex<Geometric2d> vertex_temp = vertex;
                do
                {
                    double ed = GeometricExt.Расширенное_расстояние(circle, vertex_temp.DataInVertex);
                    if (radius_max > ed)
                        radius_max = ed;

                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
                circle.Value = radius_max * rand.NextDouble();
                #endregion

                vertex.BreakCrosBy(circle);
                vertex = vertex.Cros;

                #region Шаг 5.7. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
                vertex_temp = vertex;
                do
                {
                    while (CircleExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                        vertex_temp.Rebuild();

                    vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                    vertex_temp = vertex_temp.Next.Cros.Next;
                } while (vertex_temp != vertex);
                #endregion

                triples = this.vertex.GetTriples();
                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (vertex != null)
            {
                e.Graphics.Clear(System.Drawing.Color.White);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                for (int i = 0; i < triples.Count; i++)
                {
                    e.Graphics.FillAndDraw(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 255, 255, 0)), System.Drawing.Pens.Gray, triples[i].Somes.CircleDelone);

                    Vertex<Geometric2d> vertex_temp = triples[i];
                    do
                    {
                        //e.Graphics.FillAndDraw(System.Drawing.Brushes.Red, System.Drawing.Pens.Red, AssistantExt.Отрезок_(vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                        if (vertex_temp.Somes.CircleDelone.Value != 0 && vertex_temp.Cros.Somes.CircleDelone.Value != 0)
                            e.Graphics.DrawCurve(System.Drawing.Pens.Red, AssistantExt.Отрезок(vertex_temp).ToArray());

                        e.Graphics.FillAndDraw_(region, System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, vertex_temp.DataInVertex);

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            region[1].X = ClientRectangle.Width;
            region[2].X = ClientRectangle.Width;
            region[2].Y = ClientRectangle.Height;
            region[3].Y = ClientRectangle.Height;
        }
    }

    public static class VertexExtention
    {
        public static void SetCircleDelone(this Vertex<Geometric2d> vertex, Circle circle_delone)
        {
            vertex.Prev.Somes.CircleDelone = circle_delone;
            vertex.Somes.CircleDelone = circle_delone;
            vertex.Next.Somes.CircleDelone = circle_delone;
        }
        public static List<Vertex<Geometric2d>> GetTriples(this Vertex<Geometric2d> vertex)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<Geometric2d>> list = new List<Vertex<Geometric2d>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            GetTriples(list, vertex.Cros, dt);
            return list;
        }
        private static void GetTriples(List<Vertex<Geometric2d>> list, Vertex<Geometric2d> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                list.Add(vertex);

                // Отмечем все тройки.
                Vertex<Geometric2d> vertex_temp = vertex;
                do
                {
                    vertex_temp.Somes.LastChecked = dt;
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);

                // Запускаем для отмеченных.
                do
                {
                    GetTriples(list, vertex_temp.Cros, dt);
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
        }

        public static List<Vertex<Geometric2d>> GetTriples_1(this Vertex<Geometric2d> vertex)
        {
            // Обход в ширину.
            throw new NotImplementedException();
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
            polygon.Add(circle_this.Pole + vector * circle_this.Value);
            polygon.Add(circle.Pole - vector * circle.Value);
            return polygon;
        }
        public static Polygon2d Отрезок(Circle circle_this, Plane2d plane)
        {
            Polygon2d polygon = new Polygon2d();
            polygon.Add(circle_this.Pole - plane.Normal * circle_this.Value);
            polygon.Add(circle_this.Pole - plane.Normal * (circle_this.Value + PlaneExt.Расширенное_расстояние(plane, circle_this)));
            return polygon;
        }

        public static Polygon2d Отрезок_(Geometric2d geometric_this, Geometric2d geometric)
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

        public static List<System.Drawing.PointF> Отрезок(Vertex<Geometric2d> vertex)
        {
            List<System.Drawing.PointF> points = new List<System.Drawing.PointF>();
            double ed = CircleExt.Расширенное_расстояние(vertex.Somes.CircleDelone, vertex.Cros.Somes.CircleDelone) / 2;
            double k = 0.1;
            Point2d point;
            Circle circle = new Circle { Pole = vertex.Somes.CircleDelone.Pole.Copy, Value = vertex.Somes.CircleDelone.Value + ed };
            Circle circle_cros = new Circle { Pole = vertex.Cros.Somes.CircleDelone.Pole.Copy, Value = vertex.Cros.Somes.CircleDelone.Value + ed };
            do
            {
                circle.Value += k;
                circle_cros.Value += k;
                point = CircleExt.Точка_пересечения_границ(circle, circle_cros);
                if (point != null)
                    points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (GeometricExt.Расширенное_расстояние(vertex.Prev.DataInVertex, point) > 0 && points.Count < 10000);

            circle.Value = vertex.Somes.CircleDelone.Value + ed;
            circle_cros.Value = vertex.Cros.Somes.CircleDelone.Value + ed;
            do
            {
                circle.Value += k;
                circle_cros.Value += k;
                point = CircleExt.Точка_пересечения_границ(circle_cros, circle);
                if (point != null)
                    points.Insert(0, new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (GeometricExt.Расширенное_расстояние(vertex.Next.DataInVertex, point) > 0 && points.Count < 10000);

            return points;
        }
    }
}

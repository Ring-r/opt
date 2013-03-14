using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Opt.Geometrics;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Extentions.WFA;

namespace Opt.ClosenessModel.WFAT
{
    public partial class Form1 : Form
    {
        private Random rand;
        private Vertex<Geometric> vertex;
        private List<Vertex<Geometric>> triples;

        private Polygon region;

        public Form1()
        {
            InitializeComponent();

            region = new Polygon();
            region.Add(new Point());
            region.Add(new Point { X = ClientRectangle.Width });
            region.Add(new Point { X = ClientRectangle.Width, Y = ClientRectangle.Height });
            region.Add(new Point { Y = ClientRectangle.Height });

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоугольника. !!!Потом переделать на многольник!!!
            List<Plane> borders = new List<Plane>();
            for (int i = 0; i < region.Count; i++)
                borders.Add(new Plane { Pole = region[i].Copy, Normal = (region[i + 1] - region[i])._I_(false) });
            vertex = Vertex<Geometric>.CreateClosenessModel(borders[0],borders[1],borders[2]);

            vertex.BreakCrosBy(borders[3]);
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать?!!
            vertex.SetCircleDelone(new Circle { Pole = new Point { X = ClientRectangle.Width - ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Radius = ClientRectangle.Height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point { X = ClientRectangle.Height / 2, Y = ClientRectangle.Height / 2 }, Radius = ClientRectangle.Height / 2 });

            //vertex.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[0], borders[1], borders[2]));
            //vertex.Cros.SetCircleDelone(Assistant.КРУГ_ДЕЛОНЕ(borders[2], borders[3], borders[0]));

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion

            rand = new Random();

            triples = vertex.GetTriples();
        }

        private bool PointInTriple(Circle circle, Vertex<Geometric> vertex)
        {
            if (vertex.Somes.CircleDelone == null)
                return false;

            double ed = CircleExt.Расширенное_расстояние(circle, vertex.Somes.CircleDelone);
            bool point_in_triple = ed < 0;
            if (point_in_triple)
            {
                Vertex<Geometric> vertex_temp = vertex;
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
            Circle circle = new Circle() { Pole = new Point { X = e.X, Y = e.Y } };

            #region Поиск тройки.
            int k = -1;
            for (int i = 0; i < triples.Count; i++)
                if (PointInTriple(circle, triples[i]))
                    k = i;
            #endregion

            if (k > -1)
            {
                Vertex<Geometric> vertex = triples[k];

                #region Определение радиуса круга.
                double radius_max = double.PositiveInfinity;
                Vertex<Geometric> vertex_temp = vertex;
                do
                {
                    double ed = GeometricExt.Расширенное_расстояние(circle, vertex_temp.DataInVertex);
                    if (radius_max > ed)
                        radius_max = ed;

                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
                if (radius_max > 20)
                    radius_max = 20;
                circle.Radius = radius_max * rand.NextDouble();
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
                    if (is_delone_circles_show)
                        e.Graphics.FillAndDraw(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 255, 255, 0)), System.Drawing.Pens.Gray, triples[i].Somes.CircleDelone);

                    Vertex<Geometric> vertex_temp = triples[i];
                    do
                    {
                        //e.Graphics.FillAndDraw(System.Drawing.Brushes.Red, System.Drawing.Pens.Red, AssistantExt.Отрезок_(vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                        if (vertex_temp.Somes.CircleDelone.Radius != 0 && vertex_temp.Cros.Somes.CircleDelone.Radius != 0)
                            e.Graphics.DrawCurve(System.Drawing.Pens.Red, AssistantExt.Отрезок(vertex_temp).ToArray());

                        if (is_circles_show)
                            e.Graphics.FillAndDraw_(region, System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, vertex_temp.DataInVertex);

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }

            if (is_path_show && vertexes != null && vertexes.Count > 2)
            {
                System.Drawing.PointF[] points = new System.Drawing.PointF[vertexes.Count];
                for (int i = 0; i < vertexes.Count; i++)
                    points[i] = new System.Drawing.PointF((float)vertexes[i].Somes.CircleDelone.Pole.X, (float)vertexes[i].Somes.CircleDelone.Pole.Y);
                e.Graphics.DrawLines(System.Drawing.Pens.Green, points);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            region[1].X = ClientRectangle.Width;
            region[2].X = ClientRectangle.Width;
            region[2].Y = ClientRectangle.Height;
            region[3].Y = ClientRectangle.Height;
        }

        private bool is_delone_circles_show = true;
        private bool is_path_show = false;
        private bool is_circles_show = true;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
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

        private List<Vertex<Geometric>> vertexes = null;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (vertex != null)
                vertexes = VertexExtention.Близжайшая_тройка(vertex, new Circle() { Pole = new Point() { X = e.X, Y = e.Y } }, true);

            Invalidate();
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
        public static List<Vertex<Geometric>> GetTriples(this Vertex<Geometric> vertex)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<Geometric>> list = new List<Vertex<Geometric>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            GetTriples(list, vertex.Cros, dt);
            return list;
        }
        private static void GetTriples(List<Vertex<Geometric>> list, Vertex<Geometric> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                list.Add(vertex);

                // Отмечем все тройки.
                Vertex<Geometric> vertex_temp = vertex;
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

        public static List<Vertex<Geometric>> GetTriples_1(this Vertex<Geometric> vertex)
        {
            // Обход в ширину.
            throw new NotImplementedException();
        }

        public static List<Vertex<Geometric>> Близжайшая_тройка(Vertex<Geometric> vertex, Circle data, bool is_full_path)
        {
            List<Vertex<Geometric>> res = new List<Vertex<Geometric>>(); // Изменить при поиске только последней тройки.

            Vertex<Geometric> minim_vertex = vertex;
            double minim_dist = CircleExt.Расширенное_расстояние(minim_vertex.Somes.CircleDelone, data);

            if (!is_full_path)
                res.Clear();
            res.Add(minim_vertex); // Изменить при поиске только последней тройки.

            double temp_dist = CircleExt.Расширенное_расстояние(minim_vertex.Cros.Somes.CircleDelone, data);             
            if (minim_dist > temp_dist)
            {
                minim_vertex = minim_vertex.Cros;
                minim_dist = temp_dist;

                if (!is_full_path)
                    res.Clear();
                res.Add(minim_vertex);  // Изменить при поиске только последней тройки.
            }
            bool is_end = false;
            while (!is_end)
            {
                is_end = true;
                double prev_dist = CircleExt.Расширенное_расстояние(minim_vertex.Prev.Cros.Somes.CircleDelone, data);
                double next_dist = CircleExt.Расширенное_расстояние(minim_vertex.Next.Cros.Somes.CircleDelone, data);                
                if (prev_dist < next_dist && prev_dist < minim_dist)
                {
                    minim_dist = prev_dist;
                    minim_vertex = minim_vertex.Prev.Cros;

                    if (!is_full_path)
                        res.Clear();
                    res.Add(minim_vertex);  // Изменить при поиске только последней тройки.

                    is_end = false;
                }
                if (next_dist < prev_dist && next_dist < minim_dist)
                {
                    minim_dist = next_dist;
                    minim_vertex = minim_vertex.Next.Cros;

                    if (!is_full_path)
                        res.Clear();
                    res.Add(minim_vertex);  // Изменить при поиске только последней тройки.

                    is_end = false;
                }
            }
            return res;
        }
    }

    public static class AssistantExt
    {
        public static Polygon Отрезок(Circle circle_this, Circle circle)
        {
            Polygon polygon = new Polygon();
            Vector vector = circle.Pole - circle_this.Pole;
            double length = Math.Sqrt(vector * vector);
            vector /= length;
            polygon.Add(circle_this.Pole + vector * circle_this.Radius);
            polygon.Add(circle.Pole - vector * circle.Radius);
            return polygon;
        }
        public static Polygon Отрезок(Circle circle_this, Plane plane)
        {
            Polygon polygon = new Polygon();
            polygon.Add(circle_this.Pole - plane.Normal * circle_this.Radius);
            polygon.Add(circle_this.Pole - plane.Normal * (circle_this.Radius + PlaneExt.Расширенное_расстояние(plane, circle_this)));
            return polygon;
        }

        public static Polygon Отрезок_(Geometric geometric_this, Geometric geometric)
        {
            if (geometric_this is Circle)
            {
                if (geometric is Circle)
                    return Отрезок(geometric_this as Circle, geometric as Circle);
                if (geometric is Plane)
                    return Отрезок(geometric_this as Circle, geometric as Plane);
            }
            if (geometric_this is Plane)
            {
                if (geometric is Circle)
                    return Отрезок(geometric as Circle, geometric_this as Plane);
            }
            return new Polygon();
        }

        public static List<System.Drawing.PointF> Отрезок(Vertex<Geometric> vertex)
        {
            List<System.Drawing.PointF> points = new List<System.Drawing.PointF>();
            double ed = CircleExt.Расширенное_расстояние(vertex.Somes.CircleDelone, vertex.Cros.Somes.CircleDelone) / 2;
            double k = 0.1;
            Point point;
            Circle circle = new Circle { Pole = vertex.Somes.CircleDelone.Pole.Copy, Radius = vertex.Somes.CircleDelone.Radius + ed };
            Circle circle_cros = new Circle { Pole = vertex.Cros.Somes.CircleDelone.Pole.Copy, Radius = vertex.Cros.Somes.CircleDelone.Radius + ed };
            do
            {
                circle.Radius += k;
                circle_cros.Radius += k;
                point = CircleExt.Точка_пересечения_границ(circle, circle_cros);
                if (point != null)
                    points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (GeometricExt.Расширенное_расстояние(vertex.Prev.DataInVertex, point) > 0 && points.Count<10000);

            circle.Radius = vertex.Somes.CircleDelone.Radius + ed;
            circle_cros.Radius = vertex.Cros.Somes.CircleDelone.Radius + ed;
            do
            {
                circle.Radius += k;
                circle_cros.Radius += k;
                point = CircleExt.Точка_пересечения_границ(circle_cros, circle);
                if (point != null)
                    points.Insert(0, new System.Drawing.PointF((float)point.X, (float)point.Y));
            } while (GeometricExt.Расширенное_расстояние(vertex.Next.DataInVertex, point) > 0 && points.Count < 10000);

            return points;
        }
    }
}

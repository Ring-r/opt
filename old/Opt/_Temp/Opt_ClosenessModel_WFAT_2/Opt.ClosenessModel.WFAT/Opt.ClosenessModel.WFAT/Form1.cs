using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;

namespace Opt.ClosenessModel.WFAT
{
    public partial class Form1 : Form
    {
        private Vertex<Point2d> vertex;
        //private List<Vertex<Point2d>> all_Vs;
        //private List<Vertex<Point2d>> all_VVs;
        private List<Vertex<Point2d>> all_VVVs;

        public Form1()
        {
            InitializeComponent();

            List<Point2d> points = new List<Point2d>();
            points.Add(new Point2d { X = 50, Y = 50 });
            points.Add(new Point2d { X = 250, Y = 50 });
            points.Add(new Point2d { X = 150, Y = 250 });
            vertex = Vertex<Point2d>.CreateClosenessModel(points[1], points[2], points[0]);
            vertex = vertex.Cros;

            //all_Vs = All_Vs(vertex);
            //all_VVs = All_VVs(vertex);
            all_VVVs = All_VVVs(vertex);
        }

        private bool PointInTriple(Point2d point, Vertex<Point2d> vertex)
        {
            bool point_in_triple = true;
            Vertex<Point2d> vertex_temp = vertex;
            for (int i = 0; i < 3; i++)
            {
                Point2d point_temp = vertex_temp.DataInVertex;
                Vector2d vector_temp = vertex_temp.Next.DataInVertex - vertex_temp.DataInVertex;
                Vector2d vector_temp_ = new Vector2d() { X = -vector_temp.Y, Y = vector_temp.X };
                point_in_triple = point_in_triple && (vector_temp_ * (point - point_temp) > 0);

                vertex_temp = vertex_temp.Next;
            }
            return point_in_triple;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Добавить вершину в структуру близости некоторым образом!!?
            Point2d point = new Point2d() { X = e.X, Y = e.Y };
            int k = -1;
            for (int i = 0; i < all_VVVs.Count; i++)
                if (PointInTriple(point, all_VVVs[i]))
                    k = i;
            if (k > -1)
            {
                all_VVVs[k].BreakCrosBy(point);
                all_VVVs[k].Rebuild();

                //all_Vs = All_Vs(vertex);
                //all_VVs = All_VVs(vertex);
                all_VVVs = All_VVVs(vertex);
                Invalidate();
            }
        }

        //private List<Vertex<Point2d>> All_Vs(Vertex<Point2d> vertex)
        //{
        //    // Поиск всех вершин в триангуляции.
        //    DateTime dt = DateTime.Now;
        //    List<Vertex<Point2d>> list = new List<Vertex<Point2d>>();
        //    All_Vs(list, vertex, dt);
        //    return list;
        //}
        //private void All_Vs(List<Vertex<Point2d>> list, Vertex<Point2d> vertex, DateTime dt)
        //{
        //    if (vertex.Somes.LastChecked != dt)
        //    {
        //        // Добавляем вершину.
        //        list.Add(vertex);

        //        Vertex<Point2d> vertex_temp = vertex;

        //        // Отмечем все вершины.
        //        do
        //        {
        //            vertex_temp.Somes.LastChecked = dt;
        //            vertex_temp = vertex_temp.Next.Cros.Next;
        //        } while (vertex_temp != vertex);

        //        // Запускаем для отмеченных.
        //        do
        //        {
        //            All_Vs(list, vertex_temp.Next, dt);
        //            vertex_temp.Somes.LastChecked = dt;
        //            vertex_temp = vertex_temp.Next.Cros.Next;
        //        } while (vertex_temp != vertex);
        //    }
        //}

        //private List<Vertex<Point2d>> All_VVs(Vertex<Point2d> vertex)
        //{
        //    // Поиск всех рёбер в триангуляции.
        //    DateTime dt = DateTime.Now;
        //    List<Vertex<Point2d>> list = new List<Vertex<Point2d>>();
        //    All_VVs(list, vertex, dt);
        //    return list;
        //}
        //private void All_VVs(List<Vertex<Point2d>> list, Vertex<Point2d> vertex, DateTime dt)
        //{
        //    if (vertex.Somes.LastChecked != dt)
        //    {
        //        // Добавляем вершину.
        //        list.Add(vertex);

        //        Vertex<Point2d> vertex_temp = vertex;

        //        // Отмечем все вершины.
        //        do
        //        {
        //            vertex_temp.Somes.LastChecked = dt;
        //            vertex_temp.Prev.Cros.Next.Somes.LastChecked = dt;
        //            All_VVs(list, vertex_temp.Next, dt);
        //            vertex_temp = vertex_temp.Next.Cros.Next;
        //        } while (vertex_temp != vertex);
        //    }
        //}

        private List<Vertex<Point2d>> All_VVVs(Vertex<Point2d> vertex)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<Point2d>> list = new List<Vertex<Point2d>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            All_VVVs(list, vertex.Cros, dt);
            return list;
        }
        private void All_VVVs(List<Vertex<Point2d>> list, Vertex<Point2d> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                list.Add(vertex);

                List<Vertex<Point2d>> list_temp = new List<Vertex<Point2d>>();

                // Отмечем все тройки.
                Vertex<Point2d> vertex_temp = vertex;
                do
                {
                    vertex_temp.Somes.LastChecked = dt;
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);

                // Запускаем для отмеченных.
                do
                {
                    All_VVVs(list, vertex_temp.Cros, dt);
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (vertex != null)
            {
                e.Graphics.Clear(System.Drawing.Color.White);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //for (int i = 0; i < all_Vs.Count; i++)
                //{
                //    e.Graphics.FillEllipse(System.Drawing.Brushes.Black, (float)all_Vs[i].DataInVertex.X - 3, (float)all_Vs[i].DataInVertex.Y - 3, 7, 7);
                //}

                //for (int i = 0; i < all_VVs.Count; i++)
                //{
                //    e.Graphics.DrawLine(System.Drawing.Pens.Silver, (float)all_VVs[i].DataInVertex.X, (float)all_VVs[i].DataInVertex.Y, (float)all_VVs[i].Next.DataInVertex.X, (float)all_VVs[i].Next.DataInVertex.Y);
                //}

                for (int i = 0; i < all_VVVs.Count; i++)
                {
                    Vertex<Point2d> vertex_temp = all_VVVs[i];
                    for (int j = 0; j < 3; j++)
                    {
                        e.Graphics.FillEllipse(System.Drawing.Brushes.Black, (float)vertex_temp.DataInVertex.X - 3, (float)vertex_temp.DataInVertex.Y - 3, 7, 7);
                        e.Graphics.DrawLine(System.Drawing.Pens.Red, (float)vertex_temp.DataInVertex.X, (float)vertex_temp.DataInVertex.Y, (float)vertex_temp.Next.DataInVertex.X, (float)vertex_temp.Next.DataInVertex.Y);
                        vertex_temp = vertex_temp.Next;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Invalidate();
        }
    }
}

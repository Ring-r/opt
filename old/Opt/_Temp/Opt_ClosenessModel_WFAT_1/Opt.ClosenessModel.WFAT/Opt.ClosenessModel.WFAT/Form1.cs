using System.Collections.Generic;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;

namespace Opt.ClosenessModel.WFAT
{
    public partial class Form1 : Form
    {
        private List<Point2d> points;
        private Vertex<Point2d> vertex;

        public Form1()
        {
            InitializeComponent();

            points = new List<Point2d>();
        }

        private bool PointInTriple(Point2d point, Vertex<Point2d> vertex)
        {
            bool point_in_triple = true;
            for (int i = 0; i < 3; i++)
            {
                Vertex<Point2d> vertex_temp = vertex;
                Point2d point_temp = vertex_temp.DataInVertex;
                Vector2d vector_temp = vertex_temp.Next.DataInVertex - vertex_temp.DataInVertex;
                Vector2d vector_temp_ = new Vector2d() { X = -vector_temp.Y, Y = vector_temp.X };
                point_in_triple = point_in_triple && (vector_temp_ * (point - point_temp) > 0);
            }
            return point_in_triple;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            points.Add(new Point2d() { X = e.X, Y = e.Y });
            if (points.Count == 3)
            {
                vertex = Vertex<Point2d>.CreateClosenessModel(points[0], points[1], points[2]);

                //vertex.Prev.Somes = new Somes<Point2d>(vertex.Prev);
                //vertex.Somes = new Somes<Point2d>(vertex.Prev);
                //vertex.Next.Somes = new Somes<Point2d>(vertex.Prev);
            }
            if (points.Count > 3)
            {
                // Добавить вершину в структуру близости некоторым образом!!?
                List<Vertex<Point2d>> all_VVVs = All_VVVs(vertex);
                int k = -1;
                for (int i = 0; i < all_VVVs.Count; i++)
                    if (PointInTriple(points[points.Count - 1], all_VVVs[i]))
                        k = i;
                if (k > -1)
                {
                    vertex.BreakCrosBy(points[points.Count - 1]);
                    vertex.Rebuild();
                }
                else
                    points.RemoveAt(points.Count - 1);
            }
            Invalidate();
        }

        private List<Vertex<Point2d>> All_Vs(Vertex<Point2d> vertex)
        {
            // TODO:
            // Поиск всех вершин в триангуляции.
            return new List<Vertex<Point2d>>();
        }
        private List<Vertex<Point2d>> All_VVs(Vertex<Point2d> vertex)
        {
            // TODO:
            // Поиск всех рёбер в триангуляции.
            return new List<Vertex<Point2d>>();
        }
        private List<Vertex<Point2d>> All_VVVs(Vertex<Point2d> vertex)
        {
            // TODO:
            // Поиск всех троек в триангуляции.
            return new List<Vertex<Point2d>>();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (vertex != null)
            {
                Vertex<Point2d> vertex_temp = vertex;
                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Black, (float)vertex_temp.DataInVertex.X - 3, (float)vertex_temp.DataInVertex.Y - 3, 7, 7);
                    vertex_temp = vertex_temp.Next;
                }

                List<Vertex<Point2d>> all_Vs = All_Vs(vertex);
                for (int i = 0; i < all_Vs.Count; i++)
                {
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Black, (float)all_Vs[i].DataInVertex.X - 3, (float)all_Vs[i].DataInVertex.Y - 3, 7, 7);
                }

                List<Vertex<Point2d>> all_VVs = All_VVs(vertex);
                for (int i = 0; i < all_VVs.Count; i++)
                {
                    e.Graphics.DrawLine(System.Drawing.Pens.Silver, (float)all_Vs[i].DataInVertex.X, (float)all_Vs[i].DataInVertex.Y, (float)all_Vs[i].Next.DataInVertex.X, (float)all_Vs[i].Next.DataInVertex.Y);
                }
            }
        }
    }
}

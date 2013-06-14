using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Opt.Geometrics.Geometrics2d.Temp;
using Opt.VD;

namespace Opt.Box.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VD<Circle, DeloneCircle> vd;
        private Vertex<Circle, DeloneCircle> vertex;

        Random rand;

        public MainWindow()
        {
            InitializeComponent();

            rand = new Random(7);

            vd = new VD<Circle, DeloneCircle>(new Circle() { R = 100, X = 150, Y = 200 }, null, new Circle() { R = 50, X = 300, Y = 300 });

            GeometryGroup gg = gVD as GeometryGroup;
            gg.Children.Add(new EllipseGeometry(new System.Windows.Point(300, 300), 50, 50));
            gg.Children.Add(new EllipseGeometry(new System.Windows.Point(150, 200), 100, 100));

            CreateDeloneCirclesGeometric();

            vertex = vd.NextTriple(vd.NullTriple).Vertex;
        }

        private void rg_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawingVisual dv = new DrawingVisual();
            vb.Visual = dv;
            using (DrawingContext dc = dv.RenderOpen())
            {
                Pen pen = new Pen(Brushes.Black, 0.1);
                for (double x = 0; x < rg.ActualWidth; x += 10)
                    dc.DrawLine(pen, new System.Windows.Point(x, 0), new System.Windows.Point(x, rg.ActualHeight));
                for (double y = 0; y < rg.ActualHeight; y += 10)
                    dc.DrawLine(pen, new System.Windows.Point(0, y), new System.Windows.Point(rg.ActualWidth, y));
                Brush brush = Brushes.Red;
            }
        }

        private void CreateGeometrics()
        {
            GeometryGroup gg;
            gg = gCurrTriple as GeometryGroup;
            gg.Children.Clear();
            for (int i = 0; i < 3; i++)
            {
                if (vertex.Data != null)
                    gg.Children.Add(new EllipseGeometry(new System.Windows.Point(vertex.Data.X, vertex.Data.Y), vertex.Data.R, vertex.Data.R));
                vertex = vertex.Next;
            }
            gg = gCurrVertex as GeometryGroup;
            gg.Children.Clear();
            if (vertex.Data != null)
                gg.Children.Add(new EllipseGeometry(new System.Windows.Point(vertex.Data.X, vertex.Data.Y), vertex.Data.R, vertex.Data.R));
        }

        private void CreateDeloneCirclesGeometric()
        {
            (gDeloneCircles as GeometryGroup).Children.Clear();
            Triple<Circle, DeloneCircle> triple = vd.NextTriple(vd.NullTriple);
            while (triple != vd.NullTriple)
            {
                if (!double.IsInfinity(triple.Delone_Circle.R))
                {
                    EllipseGeometry ellipse = new EllipseGeometry(new System.Windows.Point(triple.Delone_Circle.X, triple.Delone_Circle.Y), triple.Delone_Circle.R, triple.Delone_Circle.R);
                    (gDeloneCircles as GeometryGroup).Children.Add(ellipse);
                }
                else
                {
                    LineGeometry line = new LineGeometry(new System.Windows.Point(triple.Delone_Circle.X - 1000 * triple.Delone_Circle.VX, triple.Delone_Circle.Y - 1000 * triple.Delone_Circle.VY), new System.Windows.Point(triple.Delone_Circle.X + 1000 * triple.Delone_Circle.VX, triple.Delone_Circle.Y + 1000 * triple.Delone_Circle.VY));
                    (gDeloneCircles as GeometryGroup).Children.Add(line);
                }
                triple = vd.NextTriple(triple);
            }
        }

        private void GoToFirstTriple_Click(object sender, RoutedEventArgs e)
        {
            vertex = vd.NextTriple(vd.NullTriple).Vertex;
            CreateGeometrics();
        }

        private void GoToPrevTriple_Click(object sender, RoutedEventArgs e)
        {
            vertex = vd.PrevTriple(vertex.Triple).Vertex;
            if (vertex == null)
                vertex = vd.PrevTriple(vd.NullTriple).Vertex;
            CreateGeometrics();
        }

        private void GoToNCNVertex_Click(object sender, RoutedEventArgs e)
        {
            vertex = vertex.Next.Cros.Next;
            CreateGeometrics();
        }

        private void GoToPrevVertex_Click(object sender, RoutedEventArgs e)
        {
            vertex = vertex.Prev;
            CreateGeometrics();
        }

        private void GoToCrosVertex_Click(object sender, RoutedEventArgs e)
        {
            vertex = vertex.Cros;
            CreateGeometrics();
        }

        private void GoToNextVertex_Click(object sender, RoutedEventArgs e)
        {
            vertex = vertex.Next;
            CreateGeometrics();
        }

        private void GoToPCPVertex_Click(object sender, RoutedEventArgs e)
        {
            vertex = vertex.Prev.Cros.Prev;
            CreateGeometrics();
        }

        private void GoToNextTriple_Click(object sender, RoutedEventArgs e)
        {
            vertex = vd.NextTriple(vertex.Triple).Vertex;
            if (vertex == null)
                vertex = vd.NextTriple(vd.NullTriple).Vertex;
            CreateGeometrics();
        }

        private List<double> data = new List<double>();

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point point = e.GetPosition(canvas);
            data.Add(point.X);
            data.Add(point.Y);
            if (data.Count == 4)
            {
                double r = Math.Sqrt((data[2] - data[0]) * (data[2] - data[0]) + (data[3] - data[1]) * (data[3] - data[1]));
                Circle circle = new Circle() { R = r, X = data[0], Y = data[1] };
                EllipseGeometry ellipse = new EllipseGeometry(new System.Windows.Point(circle.X, circle.Y), circle.R, circle.R);
                vd.Insert(circle);
                (gVD as GeometryGroup).Children.Add(ellipse);
                CreateDeloneCirclesGeometric();
                data.Clear();
            }
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (data.Count == 2)
            {
                temp_ellipse.Visibility = System.Windows.Visibility.Visible;
                System.Windows.Point point = e.GetPosition(canvas);
                double r = Math.Sqrt((data[0] - point.X) * (data[0] - point.X) + (data[1] - point.Y) * (data[1] - point.Y));
                temp_ellipse.Width = 2 * r;
                temp_ellipse.Height = 2 * r;
                temp_ellipse.SetValue(Canvas.LeftProperty, data[0] - r);
                temp_ellipse.SetValue(Canvas.TopProperty, data[1] - r);
            }
            else
                temp_ellipse.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}

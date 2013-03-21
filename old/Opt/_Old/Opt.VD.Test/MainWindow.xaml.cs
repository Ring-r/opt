using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using Opt.VD;
using Opt.GeometricObjects;
using System.Xml;
using System.Runtime.Serialization;

namespace Opt
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Circle> ellipses = new List<Circle>();

        List<GeometricObjects.Vector> moves = new List<GeometricObjects.Vector>();
        double step = 10;

        VD<Circle, DeloneCircle> vd;
        Triple<Circle, DeloneCircle> triple;
        List<Triple<Circle, DeloneCircle>> triples = new List<Triple<Circle, DeloneCircle>>();

        const int count = 30;
        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();

            ApplicationCommands.New.Text = "Новый";
            ApplicationCommands.Open.Text = "Открыть...";
            ApplicationCommands.Save.Text = "Сохранить как...";
            ApplicationCommands.Close.Text = "Выход";
            //ApplicationCommands.Close.InputGestures+= new InputGesture();

            for (int i = 0; i < count; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = ellipse.Height = (100) * rand.NextDouble();
                ellipse.Fill = Brushes.Silver;
                ellipse.Stroke = Brushes.Black;
                ellipse.MouseDown += Ellipse_MouseDown;
                stack_panel.Children.Add(ellipse);
            }

            /*
            // Построение начальнойтриангуляции для полосы.
            double r = 100000;
            ellipses.Add(new Circle(r, 0, 400 + r));
            ellipses.Add(new Circle(r, -r, 0));
            ellipses.Add(new Circle(r, 0, 200 - r));
            triple = new Triple<Circle, DeloneCircle>(ellipses[0], ellipses[1], ellipses[2]);
            triple.CalculateDeloneCircle();
            vd = new VD<Circle, DeloneCircle>(triple, null);
            BuildTriples(vd.triples, visual_triples.Data as GeometryGroup, true);
            */

            //lv.DataContext = moves;
        }

        private void NewCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            ellipses.Clear();
            vd = null;
            triple = null;
            triples.Clear();

            gg_triples.Children.Clear();
            gg_others.Children.Clear();
        }
        private void OpenCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ellipses.Clear();

                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {
                    String[] g = sr.ReadLine().Split(' ');
                    Circle circle = new Circle(double.Parse(g[0]), double.Parse(g[1]), double.Parse(g[2]));
                    ellipses.Add(circle);
                }
                sr.Close();

                vd = new VD<Circle, DeloneCircle>(ellipses[0], ellipses[1], null);
                for (int i = 2; i < ellipses.Count; i++)
                    vd.Insert(ellipses[i]);

                if (vd != null)
                    BuildTriples(vd.NextTriple(vd.NullTriple), gg_triples, true);
                else
                    gg_triples.Children.Clear();
                triple = vd.NextTriple(vd.NullTriple);
            }
        }
        private void SaveCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                for (int i = 0; i < ellipses.Count; i++)
                    sw.WriteLine("{0} {1} {2}", ellipses[i].R, ellipses[i].X, ellipses[i].Y);
                sw.Close();
            }
        }
        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void BuildTriple(Triple<Circle, DeloneCircle> triple, GeometryGroup gg, bool is_clearing)
        {
            if (is_clearing)
                gg.Children.Clear();
            if (triple != null)
            {
                Vertex<Circle, DeloneCircle> temp_vertex = triple.Vertex;
                if (temp_vertex.Prev.Data != null)
                    gg.Children.Add(new EllipseGeometry(new System.Windows.Point(temp_vertex.Prev.Data.X, temp_vertex.Prev.Data.Y), temp_vertex.Prev.Data.R, temp_vertex.Prev.Data.R));
                if (temp_vertex.Data != null)
                    gg.Children.Add(new EllipseGeometry(new System.Windows.Point(temp_vertex.Data.X, temp_vertex.Data.Y), temp_vertex.Data.R, temp_vertex.Data.R));
                if (temp_vertex.Next.Data != null)
                    gg.Children.Add(new EllipseGeometry(new System.Windows.Point(temp_vertex.Next.Data.X, temp_vertex.Next.Data.Y), temp_vertex.Next.Data.R, temp_vertex.Next.Data.R));

                if (temp_vertex.Prev.Data != null && temp_vertex.Data != null && temp_vertex.Next.Data != null)
                {
                    gg.Children.Add(new LineGeometry(new System.Windows.Point(temp_vertex.Prev.Data.X, temp_vertex.Prev.Data.Y), new System.Windows.Point(temp_vertex.Data.X, temp_vertex.Data.Y)));
                    gg.Children.Add(new LineGeometry(new System.Windows.Point(temp_vertex.Data.X, temp_vertex.Data.Y), new System.Windows.Point(temp_vertex.Next.Data.X, temp_vertex.Next.Data.Y)));
                    gg.Children.Add(new LineGeometry(new System.Windows.Point(temp_vertex.Next.Data.X, temp_vertex.Next.Data.Y), new System.Windows.Point(temp_vertex.Prev.Data.X, temp_vertex.Prev.Data.Y)));
                }
            }
        }
        private void BuildTriples(Triple<Circle, DeloneCircle> triple, GeometryGroup gg, bool is_clearing)
        {
            if (is_clearing)
                gg.Children.Clear();
            Triple<Circle, DeloneCircle> temp_triple = triple;
            while (temp_triple != vd.NullTriple)
            {
                BuildTriple(temp_triple, gg, false);
                temp_triple = vd.NextTriple(temp_triple);
            }
        }
        private void BuildDeloneCircle(Triple<Circle, DeloneCircle> triple, GeometryGroup gg, bool is_clearing)
        {
            if (is_clearing)
                gg.Children.Clear();
            if (triple != null && triple.Delone_Circle != null)
                if (!double.IsInfinity(triple.Delone_Circle.R))
                    gg.Children.Add(new EllipseGeometry(new System.Windows.Point(triple.Delone_Circle.X, triple.Delone_Circle.Y), triple.Delone_Circle.R, triple.Delone_Circle.R));
                else
                    gg.Children.Add(new LineGeometry(new System.Windows.Point(triple.Delone_Circle.X + 1000 * triple.Delone_Circle.VX, triple.Delone_Circle.Y + 1000 * triple.Delone_Circle.VY), new System.Windows.Point(triple.Delone_Circle.X - 1000 * triple.Delone_Circle.VX, triple.Delone_Circle.Y - 1000 * triple.Delone_Circle.VY)));
        }
        private void BuildDeloneCircles(List<Triple<Circle, DeloneCircle>> triples, GeometryGroup gg, bool is_clearing)
        {
            if (is_clearing)
                gg.Children.Clear();
            foreach (Triple<Circle, DeloneCircle> triple in triples)
                BuildDeloneCircle(triple, gg, false);
        }

        private Ellipse item = null;
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (item != (Ellipse)sender)
            {
                if (item != null)
                    item.Fill = Brushes.Silver;
                item = (Ellipse)sender;
                item.Fill = Brushes.Black;
            }
            else
            {
                item.Fill = Brushes.Silver;
                item = null;
            }
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (item != null)
            {
                stack_panel.Children.Remove(item);
                System.Windows.Point p = e.GetPosition(canvas);
                Circle t_circle = new Circle(item.Width / 2, p.X, p.Y);
                ellipses.Add(t_circle);
                moves.Add(new GeometricObjects.Vector(100 * rand.NextDouble(), 100 * rand.NextDouble()));
                if (ellipses.Count == 2)
                {
                    vd = new VD<Circle, DeloneCircle>(ellipses[0], ellipses[1], null);
                    triple = vd.NextTriple(vd.NullTriple);
                }
                if (ellipses.Count > 2)
                {
                    Triple<Circle, DeloneCircle> temp_triple = TestAlgorithms.Минимальновозможная_тройка(vd, t_circle);
                    //TestAlgorithms.Placing(temp_triple.Vertex.Next.Data, t_circle, temp_triple.Vertex.Prev.Data);

                    vd.Insert(t_circle);
                }

                if (vd != null)
                    BuildTriples(vd.NextTriple(vd.NullTriple), gg_triples, true);
                else
                    gg_triples.Children.Clear();

                item = null;
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (vd != null)
            {
                Circle t_circle;
                System.Windows.Point p = e.GetPosition(sender as IInputElement);
                if (item == null)
                    t_circle = new Circle(0, p.X, p.Y);
                else
                    t_circle = new Circle(item.Width / 2, p.X, p.Y);
                (visual_item.Data as EllipseGeometry).Center = new System.Windows.Point(t_circle.X, t_circle.Y);
                (visual_item.Data as EllipseGeometry).RadiusX = t_circle.R;
                (visual_item.Data as EllipseGeometry).RadiusY = t_circle.R;

                // Поиск ближашего объекта.
                //Vertex<Circle, DeloneCircle> temp_vertex = TestAlgorithms.FindNearestVertex(vd, t_circle);
                //gg_others.Children.Clear();
                //gg_others.Children.Add(new EllipseGeometry(new Point(temp_vertex.Data.X, temp_vertex.Data.Y), temp_vertex.Data.R, temp_vertex.Data.R));
                //~Поиск ближашего объекта.

                // Поиск пути к ближайшему объекту.
                //List<Triple<Circle, DeloneCircle>> temp_triples = TestAlgorithms.FindParthToNearestTriple(vd, t_circle);
                //BuildDeloneCircles(temp_triples, gg_others, true);
                //~Поиск пути к ближайшему объекту.
                List<Triple<Circle, DeloneCircle>> temp_triples = vd.Близжайшая_тройка(t_circle, true);
                BuildDeloneCircles(temp_triples, gg_others, true);

                // Поиск ближайших троек.
                //List<Vertex<Circle, DeloneCircle>> temp_vertexs = TestAlgorithms.Neighbors(TestAlgorithms.FindNearestVertex(vd, t_circle));
                //List<Triple<Circle, DeloneCircle>> temp_triples = new List<Triple<Circle, DeloneCircle>>();
                //foreach (Vertex<Circle, DeloneCircle> temp_vertex in temp_vertexs)
                //    temp_triples.Add(temp_vertex.Triple);
                //BuildDeloneCircles(temp_triples, gg_others, true);
                //~Поиск ближайших троек.

                // Поиск минимально-возможного круга Делоне.
                //Triple<Circle, DeloneCircle> temp_triple = TestAlgorithms.FindMinimalTriple(vd, t_circle);
                //TestAlgorithms.Placing(temp_triple.Vertex.Next.Data, t_circle, temp_triple.Vertex.Prev.Data);
                //BuildDeloneCircle(temp_triple, gg_others, true);
                //~Поиск минимально-возможного круга Делоне.
            }
        }

        System.Windows.Shapes.Path FrontierPointsPath;
        private void FindFrontierPoints(object sender, RoutedEventArgs e)
        {            
            if (FrontierPointsPath != null)
            {
                canvas.Children.Remove(FrontierPointsPath);
                FrontierPointsPath = null;
            }
            else
                if (item != null)
                {
                    Circle t_circle = new Circle(item.Width / 2);

                    Triple<Circle, DeloneCircle> t_triple = vd.NextTriple(vd.NullTriple);
                    while (!double.IsPositiveInfinity(t_triple.Delone_Circle.R))
                        t_triple = vd.NextTriple(t_triple);
                    Vertex<Circle, DeloneCircle> t_vertex = t_triple.Vertex;
                    while (t_vertex.Data != null)
                        t_vertex = t_vertex.Next;

                    List<Opt.GeometricObjects.Point> points = TestAlgorithms.Точки_плотного_размещения(vd, t_circle);
                    FrontierPointsPath = new System.Windows.Shapes.Path();
                    FrontierPointsPath.Name = "frontiers";
                    FrontierPointsPath.Data = new GeometryGroup();
                    (FrontierPointsPath.Data as GeometryGroup).FillRule = FillRule.Nonzero;
                    for (int i = 0; i < points.Count; i++)
                    {
                        EllipseGeometry t_ellipse = new EllipseGeometry();
                        t_ellipse.Center = new System.Windows.Point(points[i].X, points[i].Y);
                        t_ellipse.RadiusX = t_ellipse.RadiusY = t_circle.R;
                        (FrontierPointsPath.Data as GeometryGroup).Children.Add(t_ellipse);
                    }
                    FrontierPointsPath.Stroke = Brushes.Green;
                    canvas.Children.Add(FrontierPointsPath);
                }
        }

        private void DoStep(object sender, RoutedEventArgs e)
        {

        }
    }
}

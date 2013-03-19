using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Threading;

using Opt.Geometrics;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Extentions;
using Opt.ClosenessModel;

using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms.WFAT
{
    public partial class FormMain : Form
    {
        private Random rand;

        private double height;
        private double length;
        private List<Circle> circles;

        public FormMain()
        {
            InitializeComponent();

            rand = new Random();

            circles = new List<Circle>();

            miClear_Click(null, null);



            thread = null;
            thread_start = new ParameterizedThreadStart(MethodInThread);
        }

        private void miClear_Click(object sender, EventArgs e)
        {
            height = 0;
            length = 0;
            circles.Clear();

            placing = null;

            Invalidate();
        }
        private void miLoad_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("data.txt");
            miClear_Click(null, null);
            height = double.Parse(sr.ReadLine());
            int n = int.Parse(sr.ReadLine());
            for (int i = 0; i < n; i++)
                circles.Add(new Circle { Pole = new Point2d { X = double.NegativeInfinity, Y = 0 }, Value = double.Parse(sr.ReadLine()) });

            Invalidate();
        }
        private void miSave_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("data.txt");
            sw.WriteLine(height);
            sw.WriteLine(circles.Count);
            for (int i = 0; i < circles.Count; i++)
                sw.WriteLine(circles[i].Value);
            sw.Close();
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miStripInfo_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите ширину полосы", Info = height.ToString() };
            ft.ShowDialog(this);
            try
            {
                height = double.Parse(ft.Info);
            }
            catch
            {
            }

            Invalidate();
        }
        private void miCircleInfo_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите радиус круга", Info = (height * rand.NextDouble()).ToString() };
            ft.ShowDialog(this);
            try
            {
                circles.Add(new Circle { Pole = new Point2d { X = double.NegativeInfinity, Y = 0 }, Value = double.Parse(ft.Info) });
            }
            catch
            {
            }
            Invalidate();
        }
        private void miCirclesInfo_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите количество кругов, минимальный и максимальный радиусы кругов", Info = "0 0 0" };
            ft.ShowDialog(this);
            try
            {
                string[] s = ft.Info.Split(' ');
                int n = int.Parse(s[0]);
                double min = double.Parse(s[1]);
                double max = double.Parse(s[2]);
                for (int i = 0; i < n; i++)
                    circles.Add(new Circle { Pole = new Point2d { X = double.NegativeInfinity, Y = 0 }, Value = min + (max - min) * rand.NextDouble() });
            }
            catch
            {
            }
            Invalidate();
        }

        private void FormPlaceCircle_Paint(object sender, PaintEventArgs e)
        {
            int cwidth = (sender as Control).ClientSize.Width;
            int cheight = (sender as Control).ClientSize.Height;
            e.Graphics.ScaleTransform(1, -1);
            e.Graphics.TranslateTransform(0, -cheight);

            e.Graphics.Clear(System.Drawing.Color.White);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            HatchBrush brush_back = new HatchBrush(HatchStyle.Cross, System.Drawing.Color.Silver, System.Drawing.Color.White);
            e.Graphics.DrawRectangle(System.Drawing.Pens.Black, 0, 0, (float)length, (float)height);
            e.Graphics.FillRectangle(brush_back, 0, 0, cwidth, (float)height);

            #region Рисование размещённых кругов.
            System.Drawing.Drawing2D.Matrix matrix;
            for (int i = 0; i < circles.Count; i++)
                if (0 <= circles[i].Pole.X + circles[i].Value && circles[i].Pole.X - circles[i].Value < ClientSize.Width)
                {
                    circles[i].FillAndDraw(e.Graphics, System.Drawing.Color.Silver, System.Drawing.Color.Black);

                    matrix = e.Graphics.Transform;
                    e.Graphics.TranslateTransform(+(float)circles[i].Pole.X, +(float)circles[i].Pole.Y);
                    e.Graphics.ScaleTransform(1, -1);
                    e.Graphics.TranslateTransform(-(float)circles[i].Pole.X, -(float)circles[i].Pole.Y);
                    e.Graphics.DrawString(i.ToString(), Font, System.Drawing.Brushes.Black, (float)circles[i].Pole.X, (float)circles[i].Pole.Y);
                    e.Graphics.Transform = matrix;
                }
            #endregion

            #region Рисование неразмещённых кругов.
            double x = 0;
            double y = height;
            for (int i = 0; i < circles.Count && x < ClientSize.Width; i++)
            {
                if (double.IsNegativeInfinity(circles[i].Pole.X))
                {
                    e.Graphics.DrawEllipse(System.Drawing.Pens.Black, (float)x, (float)y, 2 * (float)circles[i].Value, 2 * (float)circles[i].Value);
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Silver, (float)x, (float)y, 2 * (float)circles[i].Value, 2 * (float)circles[i].Value);
                    x += 2 * circles[i].Value;
                }
            }
            #endregion

            if (thread != null && !thread.IsAlive)
            {
                #region Рисование модели близости.
                Vertex<Geometric2d> vertex = null;
                if (placing is Opt.Algorithms.IWithClosenessModel)
                {
                    vertex = (placing as Opt.Algorithms.IWithClosenessModel).Vertex;
                }
                if (vertex != null)
                {
                    List<Vertex<Geometric2d>> triples = VertexExtention.GetTriples(vertex);

                    for (int i = 0; i < triples.Count; i++)
                    {
                        triples[i].Somes.CircleDelone.FillAndDraw(e.Graphics, System.Drawing.Color.FromArgb(150, 255, 255, 0), System.Drawing.Color.Gray);
                    }

                    for (int i = 0; i < triples.Count; i++)
                    {
                        Vertex<Geometric2d> vertex_temp = triples[i];
                        do
                        {
                            //if (vertex_temp.DataInVertex is Circle)
                            //{
                            //    Circle circle = vertex_temp.DataInVertex as Circle;
                            //    if (vertex_temp.Next.DataInVertex is Circle)
                            //    {
                            //        Circle circle_next = vertex_temp.Next.DataInVertex as Circle;
                            //        //if (CircleExt.Расширенное_расстояние(circle, circle_next) < 1e-3)
                            //        e.Graphics.DrawLine(System.Drawing.Pens.Red, (float)circle.Pole.X, (float)circle.Pole.Y, (float)circle_next.Pole.X, (float)circle_next.Pole.Y);
                            //    }
                            //}

                            if (vertex_temp.Somes.CircleDelone.Value != 0 && vertex_temp.Cros.Somes.CircleDelone.Value != 0)
                            {
                                System.Drawing.PointF[] points = AssistantExt.Отрезок(vertex_temp).ToArray();
                                if (points.Length > 2)
                                    e.Graphics.DrawCurve(System.Drawing.Pens.Red, points);
                            }

                            vertex_temp = vertex_temp.Next;
                        } while (vertex_temp != triples[i]);
                    }
                    //double[] cdr = new double[triples.Count];
                    //for (int i = 0; i < triples.Count; i++)
                    //    cdr[i] = triples[i].Somes.CircleDelone.Value;
                    //Array.Sort(cdr);
                    //for (int i = 0; i < cdr.Length; i++)
                    //    e.Graphics.DrawLine(System.Drawing.Pens.Green, i, 0, i, (float)cdr[i]);
                }
                #endregion
            }

            e.Graphics.DrawLine(System.Drawing.Pens.Green, (float)length, 0, (float)length, (float)height);
        }
        private void FormPlaceCircle_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private Placing placing;
        private Thread thread;
        private ParameterizedThreadStart thread_start;
        private void MethodInThread(object o)
        {
            placing.CalculateStart();

            (o as FormMain).Invalidate();
        }

        private void miStepSearchClassic_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                placing = new Opt.Algorithms.Метод_последовательного_одиночного_размещения.Placing(height, circles.ToArray(), 1e-3);

                thread = new Thread(thread_start);
                thread.Start(this);

                timer.Start();

                Text = "Расчёт запущен...";
            }
            else
            {
                thread.Abort();

                Invalidate();
                Text = "Расчёт остановлен!";
            }
        }
        private void miStepSearchWithCloseModel_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                placing = new Opt.Algorithms.Метод_последовательного_одиночного_размещения.PlacingWithCloseModel(height, circles.ToArray(), 1e-3);

                thread = new Thread(thread_start);
                thread.Start(this);

                timer.Start();

                Text = "Расчёт запущен...";
            }
            else
            {
                thread.Abort();

                Invalidate();
                Text = "Расчёт остановлен!";
            }

        }

        private void miStepLocalSearchClassic_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                placing = new Opt.Algorithms.Метод_барьеров.PlacingOpt(height, length, circles.ToArray(), 100, 0.5, 1e-3);
                (placing as Opt.Algorithms.Метод_барьеров.PlacingOpt).FillPoint();

                thread = new Thread(thread_start);
                thread.Start(this);

                timer.Start();

                Text = "Расчёт запущен...";
            }
            else
            {
                thread.Abort();

                (placing as Opt.Algorithms.Метод_барьеров.PlacingOpt).FillCircles();

                Invalidate();
                Text = "Расчёт остановлен!";
            }

        }
        private void miStepLocalSearchWithCloseModel_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                Vertex<Geometric2d> vertex = null;
                if (placing is Opt.Algorithms.IWithClosenessModel)
                {
                    vertex = (placing as Opt.Algorithms.IWithClosenessModel).Vertex;
                }
                if (vertex != null)
                {
                    placing = new Opt.Algorithms.Метод_барьеров.PlacingOptWithCloseModel(height, length, circles.ToArray(), vertex, 100, 0.5, 1e-3);
                    (placing as Opt.Algorithms.Метод_барьеров.Placing).FillPoint();

                    thread = new Thread(thread_start);
                    thread.Start(this);

                    timer.Start();

                    Text = "Расчёт запущен...";
                }
            }
            else
            {
                thread.Abort();

                (placing as Opt.Algorithms.Метод_барьеров.Placing).FillCircles();

                Invalidate();
                Text = "Расчёт остановлен!";
            }

        }

        private void miStepLocalSearch_Click(object sender, EventArgs e)
        {
            //int n = placed_circles.Count;
            //#region Создание и заполнение вектора размещения P (2*n+1 x 1).
            //Matrix P = new Matrix(2 * n + 1);
            //for (int i = 0; i < placed_circles.Count; i++)
            //{
            //    P[2 * i] = placed_circles[i].X;
            //    P[2 * i + 1] = placed_circles[i].Y;
            //}
            //P[2 * n] = length;
            //#endregion
            //#region Создание вектора градиента G (2*n+1 x 1).
            //Matrix G = new Matrix(2 * n + 1);
            //G[G.RowCount - 1] = 1;
            //#endregion
            //#region Создание и заполнение матрицы активных ограничений a (!2*n+1! x 2*n+1).
            //Matrix A = new Matrix(0, 2 * n + 1);
            //Matrix B = new Matrix(0, 2);
            //#region Активные ограничения по полосе.
            //for (int i = 0; i < placed_circles.Count; i++)
            //{
            //    if (placed_circles[i].Y + placed_circles[i].R == height)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i + 1] = -1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -1;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (placed_circles[i].X - placed_circles[i].R == 0)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i] = 1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -2;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (placed_circles[i].Y - placed_circles[i].R == 0)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i + 1] = 1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -3;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (placed_circles[i].X + placed_circles[i].R == length)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i] = -1;
            //        A[A.RowCount - 1, 2 * n] = 1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -4;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //}
            //#endregion
            //#region Активные ограничения между кругами.
            //for (int i = 0; i < placed_circles.Count - 1; i++)
            //    for (int j = i + 1; j < placed_circles.Count; j++)
            //    {
            //        if (Math.Abs(ExtendedDistance.Calc(placed_circles[i], placed_circles[j])) < 0.0001)
            //        {
            //            A.AddRow();
            //            A[A.RowCount - 1, 2 * i] = 2 * (placed_circles[i].X - placed_circles[j].X);
            //            A[A.RowCount - 1, 2 * i + 1] = 2 * (placed_circles[i].Y - placed_circles[j].Y);
            //            A[A.RowCount - 1, 2 * j] = 2 * (placed_circles[j].X - placed_circles[i].X);
            //            A[A.RowCount - 1, 2 * j + 1] = 2 * (placed_circles[j].Y - placed_circles[i].Y);

            //            B.AddRow();
            //            B[B.RowCount - 1, 0] = i;
            //            B[B.RowCount - 1, 1] = j;
            //        }
            //    }
            //#endregion
            //#endregion


            //bool is_exit = false;
            //do
            //{
            //    Matrix ATr = A.Tr();
            //    Matrix AOb = (A * ATr).Ob();
            //    #region Рассчитать вектор множетелей Лагранжа U.
            //    Matrix U = AOb * A * G;
            //    #endregion
            //    #region Получить вектор направления d (2*n+1 x 1).
            //    Matrix D = ATr * U - G;
            //    #endregion

            //    if (D.IsNull(0.0001))
            //    {
            //        int index = U.RowOfMinElement();
            //        if (U[index] < -1e-8)
            //        {
            //            A.DelRow(index);
            //            B.DelRow(index);
            //        }
            //        else
            //            is_exit = true;
            //    }
            //    else
            //    {
            //        #region Находим длину шага L и соответствующее ему ограничение, которое добавляем в A.
            //        double L = double.PositiveInfinity;

            //        Matrix T;
            //        double td;
            //        double l;
            //        for (int i = 0; i < placed_circles.Count; i++)
            //        {
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i + 1] = -1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] + height - placed_circles[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - placed_circles[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i + 1] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - placed_circles[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i] = -1;
            //            T[0, 2 * n] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - placed_circles[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //        }
            //        #endregion
            //        P += D * L;
            //        #region Проверить активные ограничения и удалить лишние.
            //        #endregion
            //    }
            //} while (!is_exit);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {

                timer.Stop();

                Text = "Расчёт закончен.";
            }

            length = placing.Length;

            if (placing is Opt.Algorithms.Метод_барьеров.Placing)
                (placing as Opt.Algorithms.Метод_барьеров.Placing).FillCircles();

            Invalidate();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
        }
    }

    public static class DrawExtention
    {
        public static void FillAndDraw(this Circle circle, System.Drawing.Graphics graphics, System.Drawing.Color color_brush, System.Drawing.Color color_pen)
        {
            if (circle != null)
            {
                graphics.FillEllipse(new System.Drawing.SolidBrush(color_brush), (float)(circle.Pole.X - circle.Value), (float)(circle.Pole.Y - circle.Value), (float)(2 * circle.Value), (float)(2 * circle.Value));
                graphics.DrawEllipse(new System.Drawing.Pen(color_pen), (float)(circle.Pole.X - circle.Value), (float)(circle.Pole.Y - circle.Value), (float)(2 * circle.Value), (float)(2 * circle.Value));
                graphics.FillEllipse(System.Drawing.Brushes.Black, (float)circle.Pole.X - 1, (float)circle.Pole.Y - 1, 2, 2);
            }
        }
        public static void FillAndDraw(this Plane2d plane, System.Drawing.Graphics graphics, System.Drawing.Color color_brush, System.Drawing.Color color_pen)
        {
            //graphics.FillEllipse(new System.Drawing.SolidBrush(color_brush), (float)(circle.Pole.X - circle.R), (float)(circle.Pole.Y - circle.R), (float)(2 * circle.R), (float)(2 * circle.R));
            Vector2d vector = new Vector2d() { X = -plane.Normal.Y, Y = plane.Normal.X };
            Point2d point_prev = plane.Pole - vector * 1000;
            Point2d point_next = plane.Pole + vector * 1000;
            graphics.DrawLine(new System.Drawing.Pen(color_pen), (float)(point_prev.X), (float)(point_prev.Y), (float)(point_next.X), (float)(point_next.Y));
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
            double k;
            int n = 100;
            Point2d point;
            Circle circle = new Circle { Pole = vertex.Somes.CircleDelone.Pole.Copy, Value = vertex.Somes.CircleDelone.Value + ed };
            Circle circle_cros = new Circle { Pole = vertex.Cros.Somes.CircleDelone.Pole.Copy, Value = vertex.Cros.Somes.CircleDelone.Value + ed };
            point = CircleExt.Точка_пересечения_границ(circle, circle_cros);
            if (point != null && !double.IsNaN(point.X) && !double.IsNaN(point.Y))
                points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));

            double ed_end;
            if (vertex.Prev.DataInVertex is Circle)
                ed_end = GeometricExt.Расширенное_расстояние(vertex.Somes.CircleDelone, (vertex.Prev.DataInVertex as Circle).Pole);
            else
                ed_end = 300; // TODO: Доделать!

            k = (ed_end - ed) / n;
            for (int i = 0; i < n; i++)
            {
                circle.Value += k;
                circle_cros.Value += k;
                point = CircleExt.Точка_пересечения_границ(circle, circle_cros);
                if (point != null && !double.IsNaN(point.X) && !double.IsNaN(point.Y))
                    points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));
            }

            points.Reverse();

            circle.Value = vertex.Somes.CircleDelone.Value + ed;
            circle_cros.Value = vertex.Cros.Somes.CircleDelone.Value + ed;

            if (vertex.Next.DataInVertex is Circle)
                ed_end = GeometricExt.Расширенное_расстояние(vertex.Somes.CircleDelone, (vertex.Next.DataInVertex as Circle).Pole) / 2;
            else
                ed_end = 300;

            k = (ed_end - ed) / n;
            for (int i = 0; i < n; i++)
            {
                circle.Value += k;
                circle_cros.Value += k;
                point = CircleExt.Точка_пересечения_границ(circle_cros, circle);
                if (point != null && !double.IsNaN(point.X) && !double.IsNaN(point.Y))
                    points.Add(new System.Drawing.PointF((float)point.X, (float)point.Y));
            }
            
            return points;
        }
    }
}

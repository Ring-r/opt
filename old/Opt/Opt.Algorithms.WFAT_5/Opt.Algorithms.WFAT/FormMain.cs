using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Opt.ClosenessModel;
using Opt.Geometrics;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms.WFAT
{
    public partial class FormMain : Form
    {
        private Random rand;

        private double height;
        private double length;
        private List<Circle> circles = new List<Circle>();
        private List<Circle> placed_circles = new List<Circle>();
        private int current_index;

        private Vertex<Geometric2d> vertex;
        private List<Vertex<Geometric2d>> triples;

        public FormMain()
        {
            InitializeComponent();

            rand = new Random();

            miClear_Click(this, EventArgs.Empty);


            thread = null;
            thread_start = new ParameterizedThreadStart(MethodInThread);

        }

        private void miClear_Click(object sender, EventArgs e)
        {
            length = 0;
            height = 0;
            circles = new List<Circle>();
            placed_circles = new List<Circle>();
            current_index = 0;

            vertex = null;
            triples = null;

            Invalidate();
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            StreamReader sr=new StreamReader("data.txt");
            miClear_Click(null, null);
            height = double.Parse(sr.ReadLine());
            int n = int.Parse(sr.ReadLine());
            for (int i = 0; i < n; i++)
                circles.Add(new Circle { Value = double.Parse(sr.ReadLine()) });

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоуольника. !!!Потом переделать на полосу!!!
            List<Geometric2d> borders = new List<Geometric2d>();
            borders.Add(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width / 2, Y = height }, Normal = new Vector2d { X = 0, Y = -1 } });
            borders.Add(new Plane2d { Pole = new Point2d { X = 0, Y = height / 2 }, Normal = new Vector2d { X = 1, Y = 0 } });
            borders.Add(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width / 2, Y = 0 }, Normal = new Vector2d { X = 0, Y = 1 } });

            vertex = Vertex<Geometric2d>.CreateClosenessModel(borders[0], borders[1], borders[2]);
            vertex.BreakCrosBy(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width, Y = height / 2 }, Normal = new Vector2d { X = -1, Y = 0 } }); // Добавить четвёртую сторону.
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать!!
            vertex.SetCircleDelone(new Circle { Pole = new Point2d { X = height / 2, Y = height / 2 }, Value = height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Width - height / 2, Y = height / 2 }, Value = height / 2 });

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion

            triples = vertex.GetTriples();
            //vertex = vertex.Cros;

            Invalidate();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("data.txt");
            sw.WriteLine(height);
            sw.WriteLine(circles.Count);
            for (int i = 0; i < circles.Count; i++)
                sw.WriteLine(circles[i].Value);
            sw.Close();

            //foreach (Circle circle in placed_circles)
            //    sw.WriteLine("{0} {1} {2}", 2 * circle.R, circle.X, circle.Y);

            //sw.WriteLine();
            //Opt.VD.DeloneCircle dc = new VD.DeloneCircle();
            //dc.Calculate(placed_circles[2], placed_circles[1], placed_circles[0]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[1], placed_circles[2], placed_circles[3]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[4], placed_circles[3], placed_circles[2]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.WriteLine();
            //dc.Calculate(placed_circles[0], placed_circles[1], new StripLine(0, 0, 0, 1));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[0], placed_circles[2], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[1], placed_circles[3], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[2], placed_circles[4], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(placed_circles[3], placed_circles[4], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.WriteLine();
            //dc.Calculate(new StripLine(0, 0, 0, 1), placed_circles[0], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(new StripLine(0, 0, 0, 1), placed_circles[1], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(new StripLine(0, 100, 1, 0), placed_circles[4], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.Close();
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

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоуольника. !!!Потом переделать на полосу!!!
            List<Geometric2d> borders = new List<Geometric2d>();
            borders.Add(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width / 2, Y = height }, Normal = new Vector2d { X = 0, Y = -1 } });
            borders.Add(new Plane2d { Pole = new Point2d { X = 0, Y = height / 2 }, Normal = new Vector2d { X = 1, Y = 0 } });
            borders.Add(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width / 2, Y = 0 }, Normal = new Vector2d { X = 0, Y = 1 } });

            vertex = Vertex<Geometric2d>.CreateClosenessModel(borders[0], borders[1], borders[2]);
            vertex.BreakCrosBy(new Plane2d { Pole = new Point2d { X = ClientRectangle.Width, Y = height / 2 }, Normal = new Vector2d { X = -1, Y = 0 } }); // Добавить четвёртую сторону.
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать!!
            vertex.SetCircleDelone(new Circle { Pole = new Point2d { X = height / 2, Y = height / 2 }, Value = height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = ClientRectangle.Width - height / 2, Y = height / 2 }, Value = height / 2 });

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion

            triples = vertex.GetTriples();
            //vertex = vertex.Cros;

            Invalidate();
        }
        private void miCircleInfo_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите радиус круга", Info = (height * rand.NextDouble()).ToString() };
            ft.ShowDialog(this);
            try
            {
                circles.Add(new Circle { Value = double.Parse(ft.Info) });
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
                    circles.Add(new Circle { Value = min + (max - min) * rand.NextDouble() });
            }
            catch
            {
            }
            Invalidate();
        }

        private bool Step()
        {
            #region Шаг-1. Проверка размера круга относительно полосы.
            if (2 * circles[current_index].Value > height)
                return false;
            #endregion
            #region Шаг-2. Создание списка точек возможных размещений и добавление двух начальных точек.
            List<Point2d> points = new List<Point2d>();
            points.Add(new Point2d { X = circles[current_index].Value, Y = circles[current_index].Value });
            points.Add(new Point2d { X = circles[current_index].Value, Y = height - circles[current_index].Value });
            #endregion
            #region Шаг-3. Создание и заполнение списка годографов.
            List<Circle> godographs = new List<Circle>(circles.Count);
            for (int i = 0; i < placed_circles.Count; i++)
                godographs.Add(CircleExt.Годограф_функции_плотного_размещения(placed_circles[i], circles[current_index]));
            #endregion
            #region Шаг-4. Поиск точек пересечения круга с полосой.
            for (int i = 0; i < godographs.Count; i++)
            {
                #region Шаг-4.1. Поиск точек пересечения круга с левой границей полосы.
                if (godographs[i].Pole.X - godographs[i].Value < circles[current_index].Value)
                {
                    double x = circles[current_index].Value - godographs[i].Pole.X;
                    double y = Math.Sqrt(godographs[i].Value * godographs[i].Value - x * x);
                    Point2d point;
                    point = new Point2d { X = circles[current_index].Value, Y = godographs[i].Pole.Y - y };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                    point = new Point2d { X = circles[current_index].Value, Y = godographs[i].Pole.Y + y };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                }
                #endregion
                #region Шаг-4.2. Поиск точек пересечения круга с нижней границей полосы.
                if (godographs[i].Pole.Y - godographs[i].Value < circles[current_index].Value)
                {
                    double y = circles[current_index].Value - godographs[i].Pole.Y;
                    double x = Math.Sqrt(godographs[i].Value * godographs[i].Value - y * y);
                    Point2d point;
                    point = new Point2d { X = godographs[i].Pole.X - x, Y = circles[current_index].Value };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                    point = new Point2d { X = godographs[i].Pole.X + x, Y = circles[current_index].Value };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                }
                #endregion
                #region Шаг-4.3. Поиск точек пересечения круга с верхней границей полосы.
                if (godographs[i].Pole.Y + godographs[i].Value > height - circles[current_index].Value)
                {
                    double y = height - circles[current_index].Value - godographs[i].Pole.Y;
                    double x = Math.Sqrt(godographs[i].Value * godographs[i].Value - y * y);
                    Point2d point;
                    point = new Point2d { X = godographs[i].Pole.X - x, Y = height - circles[current_index].Value };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                    point = new Point2d { X = godographs[i].Pole.X + x, Y = height - circles[current_index].Value };
                    if (IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point);
                }
                #endregion
            }
            #endregion
            #region Шаг-5. Поиск точек пересечения годографов.
            for (int i = 0; i < godographs.Count - 1; i++)
                for (int j = i + 1; j < godographs.Count; j++)
                {
                    Point2d point;

                    point = CircleExt.Точка_пересечения_границ(godographs[i], godographs[j]);
                    if (point != null && IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.

                    point = CircleExt.Точка_пересечения_границ(godographs[j], godographs[i]);
                    if (point != null && IsCheckedStrip(point, circles[current_index], height))
                        points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.
                }
            #endregion
            #region Шаг-6. Сортировка набора точек возможного размещения.!!! Данная часть не нужна, если использовать сортировку при вставке точек в набор данных.
            for (int i = 0; i < points.Count - 1; i++)
                for (int j = i + 1; j < points.Count; j++)
                    if (points[i].X > points[j].X || (points[i].X == points[j].X && points[i].Y > points[j].Y))
                    {
                        Point2d temp_point = points[i];
                        points[i] = points[j];
                        points[j] = temp_point;
                    }
            #endregion
            #region Шаг-7. Выбор наилучшей точки размещения, при которой не возникает пересечение кругов и размещение круга.
            int p = -1;
            do
            {
                p++;
                circles[current_index].Pole.Copy = points[p];
            } while (!IsCheckedCircles(circles[current_index], placed_circles, 0.0001));
            #endregion
            #region Шаг-8. Пересчёт ширины занятой части полосы.
            length = Math.Max(length, circles[current_index].Pole.X + circles[current_index].Value);
            #endregion
            return true;
        }
        private void miCreateStep_Click(object sender, EventArgs e)
        {
            if (Step())
            {
                #region Шаг-10. Изменение списка для текущего круга.
                placed_circles.Add(circles[current_index]);
                circles.RemoveAt(current_index);
                Invalidate();
                #endregion
                Text = "Размещено: " + placed_circles.Count.ToString() + " из " + circles.Count.ToString() + ". Длина:" + length.ToString() + ".";
            }
            else
                current_index++;
        }

        private bool Функция_расширенного_расстояния_на_отрезке_монотонна(Vertex<Geometric2d> vertex)
        {
            if (vertex.Next.DataInVertex is Plane2d && vertex.Prev.DataInVertex is Plane2d)
                return true;
            Plane2d plane = GeometricExt.Серединная_полуплоскость(vertex.Next.DataInVertex, vertex.Prev.DataInVertex);
            return PlaneExt.Расширенное_расстояние(plane, vertex.Somes.CircleDelone.Pole) * PlaneExt.Расширенное_расстояние(plane, vertex.Cros.Somes.CircleDelone.Pole) > 0;
        }
        private bool Существует_точка_плотного_размещения_второго_рода(Circle circle, Vertex<Geometric2d> vertex)
        {
            if (circle.Value > vertex.Somes.CircleDelone.Value)
                return false;
            else
                if (circle.Value >= vertex.Cros.Somes.CircleDelone.Value)
                    return true;
                else
                    if (Функция_расширенного_расстояния_на_отрезке_монотонна(vertex)) // Придумать что-то другое?
                        return false;
                    else
                        return 2 * circle.Value >= GeometricExt.Расширенное_расстояние(vertex.Prev.DataInVertex, vertex.Next.DataInVertex);
        }

        private bool StepUpgrade()
        {
            #region Шаг 5.1. Проверяем размер круга относительно полосы.
            if (2 * circles[current_index].Value > height)
                return false;
            #endregion
            #region Шаг 5.2. Устанавливаем начальное значение для точки размещения текущего объекта и связанной с ней вершиной.
            Point2d point_global = new Point2d { X = double.PositiveInfinity };
            Vertex<Geometric2d> vertex_global = null;
            #endregion
            #region Шаг 5.3. Для каждой вершины выполняем следующее...
            for (int i = 0; i < triples.Count; i++)
            {
                Vertex<Geometric2d> vertex = triples[i];
                do
                {
                    #region Шаг 5.3.1. Если выполняются все условия существования точки плотного размещения второго рода, то находим её.
                    if (Существует_точка_плотного_размещения_второго_рода(circles[current_index], vertex))
                    {
                        Point2d point_temp = circles[current_index].Точка_близости_второго_рода(vertex.Next.DataInVertex, vertex.Prev.DataInVertex);

                        #region Шаг 5.3.1.1. Если точка даёт меньшее приращение функции цели, то сохраняем вершину и точку размещения.
                        if (point_temp.X < point_global.X)
                        {
                            point_global = point_temp;
                            vertex_global = vertex;
                        }
                        #endregion
                    }
                    #endregion
                    vertex = vertex.Next;
                } while (vertex != triples[i]);
            }
            #endregion
            #region Шаг 5.4. Устанавливаем объект в найденную точку размещения.
            circles[current_index].Pole.Copy = point_global;
            #endregion
            #region Шаг 5.5. Вставляем объект в ребро напротив найденной вершины.
            vertex_global.BreakCrosBy(circles[current_index]);
            vertex_global = vertex_global.Cros;
            #endregion
            #region Шаг 5.7. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
            Vertex<Geometric2d> vertex_temp = vertex_global;
            do
            {
                while (CircleExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                    vertex_temp.Rebuild();

                vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                vertex_temp = vertex_temp.Next.Cros.Next;
            } while (vertex_temp != vertex_global);
            #endregion

            triples = this.vertex.GetTriples();

            #region Шаг 5.8. Пересчёт ширины занятой части полосы.
            length = Math.Max(length, circles[current_index].Pole.X + circles[current_index].Value);
            #endregion
            return true;
        }

        private void miCreateStepUpgrade_Click(object sender, EventArgs e)
        {
            if (StepUpgrade())
            {
                #region Шаг 5.9. Изменение списка для текущего круга.
                placed_circles.Add(circles[current_index]);
                circles.RemoveAt(current_index);
                Invalidate();
                #endregion
                Text = "Размещено: " + placed_circles.Count.ToString() + " из " + circles.Count.ToString() + ". Длина:" + length.ToString() + ".";
            }
            else
                current_index++;
        }

        private DateTime dt;
        private void miStart_Click(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            timer1.Start();
        }
        private void miStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void miInterval_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите интервал таймера", Info = timer.Interval.ToString() };
            ft.ShowDialog(this);
            try
            {
                timer1.Interval = int.Parse(ft.Info);
            }
            catch
            {
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (current_index < circles.Count)
                //miCreateStep_Click(sender, EventArgs.Empty);
                miCreateStepUpgrade_Click(sender, EventArgs.Empty);
            else
            {
                TimeSpan ts = DateTime.Now - dt;
                Text = ts.Minutes.ToString() + ":" + ts.Seconds.ToString() + ":" + ts.Milliseconds.ToString();
                timer1.Stop();
                //Invalidate();
            }
        }

        /// <summary>
        /// Проверка на попадание круга в полосу.
        /// </summary>
        /// <param name="point">Вектор размещения круга.</param>
        /// <param name="circle">Круг.</param>
        /// <param name="height">Высота полосы.</param>
        /// <returns>Возвращает True, если круг полностью лежит внутри полосы. False - в противном случае.</returns>
        private bool IsCheckedStrip(Point2d point, Circle circle, double height)
        {
            return (point.Y + circle.Value <= height) && (point.X - circle.Value >= 0) && (point.Y - circle.Value >= 0); //!!! Необходимо учитывать погрешность.
        }
        /// <summary>
        /// Проверка на непересечение круга с множеством кругов.
        /// </summary>
        /// <param name="point">Вектор размещения круга.</param>
        /// <param name="circle">Круг.</param>
        /// <param name="circles">Множество кругов.</param>
        /// <param name="height">Значение допустимой погрешности. Положительное число.</param>
        /// <returns>Возвращает True, если круг не пересекается ни с одним кругом заданного множества. False - в противном случае.</returns>
        public static bool IsCheckedCircles(Circle circle, List<Circle> circles, double eps)
        {
            for (int i = 0; i < circles.Count; i++)
                if (CircleExt.Расширенное_расстояние(circle, circles[i]) < -eps) // !!! Необходимо учитывать погрешность?
                    return false;
            return true; ;
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
            e.Graphics.DrawRectangle(System.Drawing.Pens.Black, 0, 0, cwidth, (float)height);
            e.Graphics.FillRectangle(brush_back, 0, 0, cwidth, (float)height);

            #region Рисование модели близости.
            if (vertex != null)
            {
                for (int i = 0; i < triples.Count; i++)
                {
                    triples[i].Somes.CircleDelone.FillAndDraw(e.Graphics, System.Drawing.Color.FromArgb(150, 255, 255, 0), System.Drawing.Color.Gray);

                    Vertex<Geometric2d> vertex_temp = triples[i];
                    do
                    {
                        if (vertex_temp.DataInVertex is Circle)
                        {
                            Circle circle = vertex_temp.DataInVertex as Circle;
                            if (vertex_temp.Next.DataInVertex is Circle)
                            {
                                Circle circle_next = vertex_temp.Next.DataInVertex as Circle;
                                if (CircleExt.Расширенное_расстояние(circle, circle_next) < 1e-3)
                                    e.Graphics.DrawLine(System.Drawing.Pens.Red, (float)circle.Pole.X, (float)circle.Pole.Y, (float)circle_next.Pole.X, (float)circle_next.Pole.Y);
                            }
                            circle.FillAndDraw(e.Graphics, System.Drawing.Color.Silver, System.Drawing.Color.Black);
                        }
                        else
                            (vertex_temp.DataInVertex as Plane2d).FillAndDraw(e.Graphics, System.Drawing.Color.Silver, System.Drawing.Color.Black);

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }
            #endregion

            for (int i = 0; i < placed_circles.Count; i++)
                if (placed_circles[i].Pole.X - placed_circles[i].Value < ClientSize.Width)
                {
                    placed_circles[i].FillAndDraw(e.Graphics, System.Drawing.Color.Silver, System.Drawing.Color.Black);
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Black, (float)placed_circles[i].Pole.X - 1, (float)placed_circles[i].Pole.Y - 1, 2, 2);
                }
            //if (placed_circles.Count > 0)
            //{
            //    int i = placed_circles.Count - 1;
            //    placed_circles[i].FillAndDraw(e.Graphics, System.Drawing.Color.Black, System.Drawing.Color.Black);
            //}

            #region Рисование модели близости.
            if (vertex != null)
            {
                for (int i = 0; i < triples.Count; i++)
                {
                    Vertex<Geometric2d> vertex_temp = triples[i];
                    do
                    {
                        if (vertex_temp.DataInVertex is Circle)
                        {
                            Circle circle = vertex_temp.DataInVertex as Circle;
                            if (vertex_temp.Next.DataInVertex is Circle)
                            {
                                Circle circle_next = vertex_temp.Next.DataInVertex as Circle;
                                //if (CircleExt.Расширенное_расстояние(circle, circle_next) < 1e-3)
                                    e.Graphics.DrawLine(System.Drawing.Pens.Red, (float)circle.Pole.X, (float)circle.Pole.Y, (float)circle_next.Pole.X, (float)circle_next.Pole.Y);
                            }
                        }

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }
            #endregion


            double x = 0;
            double y = height;
            for (int i = 0; i < circles.Count && x < ClientSize.Width; i++)
            {
                e.Graphics.DrawEllipse(System.Drawing.Pens.Black, (float)x, (float)y, 2 * (float)circles[i].Value, 2 * (float)circles[i].Value);
                e.Graphics.FillEllipse(System.Drawing.Brushes.Silver, (float)x, (float)y, 2 * (float)circles[i].Value, 2 * (float)circles[i].Value);
                x += 2 * circles[i].Value;
            }
        }
        private void FormPlaceCircle_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
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


        private Placing placing;

        private Thread thread;
        private ParameterizedThreadStart thread_start;

        private void MethodInThread(object o)
        {
            placing.CalculateStart();

            (o as FormMain).Invalidate();
        }

        private void miStepLocalSearchB_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                placing = new Placing(height, length, placed_circles.ToArray(), 100, 0.5, 1e-3);
                placing.FillPoint();

                thread = new Thread(thread_start);
                thread.Start(this);

                timer.Start();

                Text = "Расчёт запущен...";
            }
            else
            {
                thread.Abort();

                placing.FillCircles();

                Invalidate();
                Text = "Расчёт остановлен!";
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {

                timer.Stop();

                Text = "Расчёт закончен.";

                #region Перестроение триангуляции. Переделать!
                for (int i = 0; i < triples.Count; i++)
                {
                    Vertex<Geometric2d> vertex_temp = triples[i];
                    vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                }

                #region Шаг 5.7. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < triples.Count; i++)
                    {
                        Vertex<Geometric2d> vertex_temp = triples[i];
                        do
                        {
                            if (vertex_temp.DataInVertex is Circle)
                                if (CircleExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                                {
                                    vertex_temp.Rebuild();

                                    vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                                    vertex_temp.Cros.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Cros.Prev.DataInVertex, vertex_temp.Cros.DataInVertex, vertex_temp.Cros.Next.DataInVertex));
                                }

                            vertex_temp = vertex_temp.Next.Cros.Next;
                        } while (vertex_temp != triples[i]);
                    }
                }
                #endregion
                #endregion
            }

            placing.FillCircles();

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
                if (vertex.Somes.CircleDelone.Value != 0)
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
    }
}

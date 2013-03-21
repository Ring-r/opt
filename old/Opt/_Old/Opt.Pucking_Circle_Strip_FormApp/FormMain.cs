using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

using Opt.GeometricObjects;
using Opt.Model;

namespace Opt
{
    namespace Pucking_Circle_Strip_FormApp
    {
        public partial class FormMain : Form
        {
            private Random rand;

            private double height;
            private double length;
            private List<Circle> circles = new List<Circle>();
            private List<Circle> placed_circles = new List<Circle>();
            private int current_index;

            private VertexClass vertex;
            private TripleClass triple;

            public FormMain()
            {
                InitializeComponent();

                rand = new Random();

                miClear_Click(this, EventArgs.Empty);
            }

            private void miClear_Click(object sender, EventArgs e)
            {
                length = 0;
                height = 0;
                circles = new List<Circle>();
                placed_circles = new List<Circle>();
                current_index = 0;

                triple = new TripleClass();

                Invalidate();
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
                VertexClass vertex = VertexClass.CreateModel(new StripLine(0, height, 0, -1), new StripLine(0, 0, 1, 0), new StripLine(0, 0, 0, 1), new StripLine(2 * height, 0, -1, 0));
                #endregion
                #region Шаг 2. Связываем перекрёстные вершины с тройками и добавляем их в список.  !!Необязательно. Убрать класс троек.
                triple.Add(new TripleClass(vertex));
                triple.Add(new TripleClass(vertex.Cros));
                #endregion
                #region Шаг 3. Устанавливаем для полученных троек круги Делоне. !!!Задать правильные круги Делоне!!!
                vertex.Triple.Data = new Circle(height / 2, height / 2, height / 2);
                vertex.Cros.Triple.Data = new Circle(height / 2, height / 2, height / 2);
                #endregion

                Invalidate();
            }
            private void miCircleInfo_Click(object sender, EventArgs e)
            {
                FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите радиус круга", Info = (height * rand.NextDouble()).ToString() };
                ft.ShowDialog(this);
                try
                {
                    circles.Add(new Circle(double.Parse(ft.Info)));
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
                        circles.Add(new Circle(min + (max - min) * rand.NextDouble()));
                }
                catch
                {
                }
                Invalidate();
            }

            private bool Step()
            {
                #region Шаг-1. Проверка размера круга относительно полосы.
                if (2 * circles[current_index].R > height)
                    return false;
                #endregion
                #region Шаг-2. Создание списка точек возможных размещений и добавление двух начальных точек.
                List<Opt.GeometricObjects.Point> points = new List<Opt.GeometricObjects.Point>();
                points.Add(new Opt.GeometricObjects.Point(circles[current_index].R, circles[current_index].R));
                points.Add(new Opt.GeometricObjects.Point(circles[current_index].R, height - circles[current_index].R));
                #endregion
                #region Шаг-3. Создание и заполнение списка годографов.
                List<Circle> godographs = new List<Circle>(circles.Count);
                for (int i = 0; i < placed_circles.Count; i++)
                    godographs.Add(new Circle() { R = placed_circles[i].R + circles[current_index].R, X = placed_circles[i].X, Y = placed_circles[i].Y });
                #endregion
                #region Шаг-4. Поиск точек пересечения круга с полосой.
                for (int i = 0; i < godographs.Count; i++)
                {
                    #region Шаг-4.1. Поиск точек пересечения круга с левой границей полосы.
                    if (godographs[i].X - godographs[i].R < circles[current_index].R)
                    {
                        double x = circles[current_index].R - godographs[i].X;
                        double y = Math.Sqrt(godographs[i].R * godographs[i].R - x * x);
                        Opt.GeometricObjects.Point point;
                        point = new Opt.GeometricObjects.Point(circles[current_index].R, godographs[i].Y - y);
                        if (IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point);
                        point = new Opt.GeometricObjects.Point(circles[current_index].R, godographs[i].Y + y);
                        if (IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point);
                    }
                    #endregion
                    #region Шаг-4.2. Поиск точек пересечения круга с нижней границей полосы.
                    if (godographs[i].Y - godographs[i].R < circles[current_index].R)
                    {
                        double y = circles[current_index].R - godographs[i].Y;
                        double x = Math.Sqrt(godographs[i].R * godographs[i].R - y * y);
                        Opt.GeometricObjects.Point point;
                        point = new Opt.GeometricObjects.Point(godographs[i].X - x, circles[current_index].R);
                        if (IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point);
                        point = new Opt.GeometricObjects.Point(godographs[i].X + x, circles[current_index].R);
                        if (IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point);
                    }
                    #endregion
                    #region Шаг-4.3. Поиск точек пересечения круга с верхней границей полосы.
                    if (godographs[i].Y + godographs[i].R > height - circles[current_index].R)
                    {
                        double y = height - circles[current_index].R - godographs[i].Y;
                        double x = Math.Sqrt(godographs[i].R * godographs[i].R - y * y);
                        Opt.GeometricObjects.Point point;
                        point = new Opt.GeometricObjects.Point(godographs[i].X - x, height - circles[current_index].R);
                        if (IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point);
                        point = new Opt.GeometricObjects.Point(godographs[i].X + x, height - circles[current_index].R);
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
                        Opt.GeometricObjects.Point point;

                        point = PlacingPoint.Calc(godographs[i], godographs[j]);
                        if (point != null && IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.

                        point = PlacingPoint.Calc(godographs[j], godographs[i]);
                        if (point != null && IsCheckedStrip(point, circles[current_index], height))
                            points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.
                    }
                #endregion
                #region Шаг-6. Сортировка набора точек возможного размещения.!!! Данная часть не нужна, если использовать сортировку при вставке точек в набор данных.
                for (int i = 0; i < points.Count - 1; i++)
                    for (int j = i + 1; j < points.Count; j++)
                        if (points[i].X > points[j].X || (points[i].X == points[j].X && points[i].Y > points[j].Y))
                        {
                            Opt.GeometricObjects.Point temp_point = points[i];
                            points[i] = points[j];
                            points[j] = temp_point;
                        }
                #endregion
                #region Шаг-7. Выбор наилучшей точки размещения, при которой не возникает пересечение кругов и размещение круга.
                int p = -1;
                do
                {
                    p++;
                    circles[current_index].Set(points[p].X, points[p].Y);
                } while (!ModelExtending.IsCheckedCircles(circles[current_index], placed_circles, 0.0001));
                #endregion
                #region Шаг-8. Пересчёт ширины занятой части полосы.
                length = Math.Max(length, circles[current_index].X + circles[current_index].R);
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

            private bool ChBF(VertexClass vertex)
            {
                TripleClass triple = vertex.Cros.Triple;
                if (triple == null)
                    return true;
                Circle delone_circle = triple.Data;
                Circle circle = vertex.Data as Circle;
                double dx = circle.X - delone_circle.X;
                double dy = circle.Y - delone_circle.Y;
                return Math.Sqrt(dx * dx + dy * dy) - circle.R - delone_circle.R >= 0; // Находим расширенное расстояние от Circle до Circle.
            }
            private void ChVF(VertexClass vertex)
            {
                vertex.Triple.Update();
            }
            private bool MonotonCheck(VertexClass vertex, Circle circle)
            {
                return true;
            }

            private bool StepUpgrade()
            {
                #region Шаг 5.1. Проверяем размер круга относительно полосы.
                if (2 * circles[current_index].R > height)
                    return false;
                #endregion
                #region Шаг 5.2. Устанавливаем начальное значение для точки размещения текущего объекта и связанной с ней вершиной.
                Opt.GeometricObjects.Point point_global = new Opt.GeometricObjects.Point() { X = double.PositiveInfinity };
                VertexClass vertex_global = null;
                #endregion
                #region Шаг 5.3. Для каждой тройки (кроме NullTriple) выполняем следующее...
                TripleClass.Enumerator en_triples = triple.GetEnumerator();
                en_triples.MoveNext();
                do
                {
                    #region Шаг 5.3.1. Если радиус круга Делоне больше радиуса размещаемого круга, то...
                    if (en_triples.Curren.Data.R >= circles[current_index].R)
                    {
                        #region Шаг 5.3.1.1. Для каждой вершины тройки выполняем следующее...
                        VertexClass.Enumerator en_vertexes = en_triples.Curren.Vertex.GetEnumerator();
                        do
                        {
                            Opt.GeometricObjects.Point point_temp = new Opt.GeometricObjects.Point(double.PositiveInfinity, double.PositiveInfinity);
                            #region Шаг 5.3.1.1.1. Если выполняются все условия существования точки плотного размещения второго рода, то находим её.
                            bool is_exist;
                            if (en_vertexes.Current.Cros.Triple.Data.R <= circles[current_index].R)
                                //is_exist = true;
                                is_exist = (ModelExtending.ED(en_vertexes.Current.Prev.Data, en_vertexes.Current.Next.Data) <= 2 * circles[current_index].R);
                            else
                                if (MonotonCheck(en_vertexes.Current, circles[current_index])) // Проверка монотонности функции. !!!Дописать!!!
                                    is_exist = false;
                                else
                                    is_exist = (ModelExtending.ED(en_vertexes.Current.Prev.Data, en_vertexes.Current.Next.Data) <= 2 * circles[current_index].R);
                            #region Шаг 5.3.1.1.2. Поиск точки плотного размещения для двух объектов.
                            if (is_exist)
                            {
                                point_temp = ModelExtending.PP(en_vertexes.Current.Prev.Data, en_vertexes.Current.Next.Data, circles[current_index]); // !!!Дописать!!!
                                #region Шаг 5.3.1.1.3. Если точка даёт меньшее приращение функции цели, то сохраняем вершину и точку размещения.
                                if (point_temp.X < point_global.X)
                                {
                                    point_global = point_temp;
                                    vertex_global = en_vertexes.Current;
                                }
                                #endregion
                            }
                            #endregion
                            #endregion
                            #region !Шаг 5.3.1.1.4. Если точка не даёт приращение выйти из 5.3. Необязательно!
                            #endregion
                        } while (en_vertexes.MoveNextInTriple());
                        #endregion
                    }
                    #endregion
                } while (en_triples.MoveNext());
                #endregion
                #region Шаг 5.4. Устанавливаем объект в найденную точку размещения.
                circles[current_index].Center = point_global;
                #endregion
                #region Шаг 5.5. Вставляем объект в ребро напротив найденной вершины.
                vertex_global.BreakCrosBy(circles[current_index]);
                #endregion
                #region Шаг 5.6. Создаём для новых троек Triple и добавляем их в список.
                triple.Add(new TripleClass(vertex_global.Cros));
                triple.Add(new TripleClass(vertex_global.Cros.Next.Cros));
                #endregion
                #region Шаг 5.7. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
                VertexClass.Enumerator en_checking_vertexes = vertex_global.Cros.GetEnumerator();
                while (en_checking_vertexes.MoveNextInNode(ChBF, ChVF)) ; // !!!Не работает пока ещё!!!
                #endregion
                #region Шаг 5.8. Пересчёт ширины занятой части полосы.
                length = Math.Max(length, circles[current_index].X + circles[current_index].R);
                #endregion
                return true;
            }
            private void miCreateStepUpgrade(object sender, EventArgs e)
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

            private void miStart_Click(object sender, EventArgs e)
            {
                timer1.Start();
            }
            private void miStop_Click(object sender, EventArgs e)
            {
                timer1.Stop();
            }
            private void miInterval_Click(object sender, EventArgs e)
            {
                FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите интервал таймера", Info = timer1.Interval.ToString() };
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
                    miCreateStep_Click(sender, EventArgs.Empty);
                else
                    timer1.Stop();
            }

            /// <summary>
            /// Проверка на попадание круга в полосу.
            /// </summary>
            /// <param name="point">Вектор размещения круга.</param>
            /// <param name="circle">Круг.</param>
            /// <param name="height">Высота полосы.</param>
            /// <returns>Возвращает True, если круг полностью лежит внутри полосы. False - в противном случае.</returns>
            private bool IsCheckedStrip(Opt.GeometricObjects.Point point, Circle circle, double height)
            {
                return (point.Y + circle.R <= height) && (point.X - circle.R >= 0) && (point.Y - circle.R >= 0); //!!! Необходимо учитывать погрешность.
            }

            private void FormPlaceCircle_Paint(object sender, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                e.Graphics.ScaleTransform(1, -1);
                e.Graphics.TranslateTransform(0, -ClientSize.Height);

                e.Graphics.Clear(Color.White);

                HatchBrush brush_back = new HatchBrush(HatchStyle.Cross, Color.Silver, Color.White);
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, ClientSize.Width, (float)height);
                e.Graphics.FillRectangle(brush_back, 0, 0, ClientSize.Width, (float)height);

                for (int i = 0; i < placed_circles.Count; i++)
                {
                    if (placed_circles[i].X - placed_circles[i].R < ClientSize.Width)
                    {
                        e.Graphics.DrawEllipse(Pens.Black, (float)(placed_circles[i].X - placed_circles[i].R), (float)(placed_circles[i].Y - placed_circles[i].R), 2 * (float)placed_circles[i].R, 2 * (float)placed_circles[i].R);
                        e.Graphics.FillEllipse(Brushes.Silver, (float)(placed_circles[i].X - placed_circles[i].R), (float)(placed_circles[i].Y - placed_circles[i].R), 2 * (float)placed_circles[i].R, 2 * (float)placed_circles[i].R);
                    }
                }
                if (placed_circles.Count > 0)
                {
                    int i = placed_circles.Count - 1;
                    e.Graphics.DrawEllipse(Pens.Black, (float)(placed_circles[i].X - placed_circles[i].R), (float)(placed_circles[i].Y - placed_circles[i].R), 2 * (float)placed_circles[i].R, 2 * (float)placed_circles[i].R);
                    e.Graphics.FillEllipse(Brushes.Black, (float)(placed_circles[i].X - placed_circles[i].R), (float)(placed_circles[i].Y - placed_circles[i].R), 2 * (float)placed_circles[i].R, 2 * (float)placed_circles[i].R);
                }

                double x = 0;
                double y = height;
                for (int i = 0; i < circles.Count && x < ClientSize.Width; i++)
                {
                    e.Graphics.DrawEllipse(Pens.Black, (float)x, (float)y, 2 * (float)circles[i].R, 2 * (float)circles[i].R);
                    e.Graphics.FillEllipse(Brushes.Silver, (float)x, (float)y, 2 * (float)circles[i].R, 2 * (float)circles[i].R);
                    x += 2 * circles[i].R;
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
                int n = placed_circles.Count;
                #region Создание и заполнение вектора размещения P (2*n+1 x 1).
                Matrix P = new Matrix(2 * n + 1);
                for (int i = 0; i < placed_circles.Count; i++)
                {
                    P[2 * i] = placed_circles[i].X;
                    P[2 * i + 1] = placed_circles[i].Y;
                }
                P[2 * n] = length;
                #endregion
                #region Создание вектора градиента G (2*n+1 x 1).
                Matrix G = new Matrix(2 * n + 1);
                G[G.RowCount - 1] = 1;
                #endregion
                #region Создание и заполнение матрицы активных ограничений a (!2*n+1! x 2*n+1).
                Matrix A = new Matrix(0, 2 * n + 1);
                Matrix B = new Matrix(0, 2);
                #region Активные ограничения по полосе.
                for (int i = 0; i < placed_circles.Count; i++)
                {
                    if (placed_circles[i].Y + placed_circles[i].R == height)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, 2 * i + 1] = -1;
                        B.AddRow();
                        B[B.RowCount - 1, 0] = -1;
                        B[B.RowCount - 1, 1] = i;
                    }
                    if (placed_circles[i].X - placed_circles[i].R == 0)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, 2 * i] = 1;
                        B.AddRow();
                        B[B.RowCount - 1, 0] = -2;
                        B[B.RowCount - 1, 1] = i;
                    }
                    if (placed_circles[i].Y - placed_circles[i].R == 0)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, 2 * i + 1] = 1;
                        B.AddRow();
                        B[B.RowCount - 1, 0] = -3;
                        B[B.RowCount - 1, 1] = i;
                    }
                    if (placed_circles[i].X + placed_circles[i].R == length)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, 2 * i] = -1;
                        A[A.RowCount - 1, 2 * n] = 1;
                        B.AddRow();
                        B[B.RowCount - 1, 0] = -4;
                        B[B.RowCount - 1, 1] = i;
                    }
                }
                #endregion
                #region Активные ограничения между кругами.
                for (int i = 0; i < placed_circles.Count - 1; i++)
                    for (int j = i + 1; j < placed_circles.Count; j++)
                    {
                        if (Math.Abs(ExtendedDistance.Calc(placed_circles[i], placed_circles[j])) < 0.0001)
                        {
                            A.AddRow();
                            A[A.RowCount - 1, 2 * i] = 2 * (placed_circles[i].X - placed_circles[j].X);
                            A[A.RowCount - 1, 2 * i + 1] = 2 * (placed_circles[i].Y - placed_circles[j].Y);
                            A[A.RowCount - 1, 2 * j] = 2 * (placed_circles[j].X - placed_circles[i].X);
                            A[A.RowCount - 1, 2 * j + 1] = 2 * (placed_circles[j].Y - placed_circles[i].Y);

                            B.AddRow();
                            B[B.RowCount - 1, 0] = i;
                            B[B.RowCount - 1, 1] = j;
                        }
                    }
                #endregion
                #endregion


                bool is_exit = false;
                do
                {
                    Matrix ATr = A.Tr();
                    Matrix AOb = (A * ATr).Ob();
                    #region Рассчитать вектор множетелей Лагранжа U.
                    Matrix U = AOb * A * G;
                    #endregion
                    #region Получить вектор направления d (2*n+1 x 1).
                    Matrix D = ATr * U - G;
                    #endregion

                    if (D.IsNull(0.0001))
                    {
                        int index = U.RowOfMinElement();
                        if (U[index] < -1e-8)
                        {
                            A.DelRow(index);
                            B.DelRow(index);
                        }
                        else
                            is_exit = true;
                    }
                    else
                    {
                        #region Находим длину шага L и соответствующее ему ограничение, которое добавляем в A.
                        double L = double.PositiveInfinity;

                        Matrix T;
                        double td;
                        double l;
                        for (int i = 0; i < placed_circles.Count; i++)
                        {
                            T = new Matrix(1, 2 * n + 1);
                            T[0, 2 * i + 1] = -1;
                            td = (T * D)[0];
                            if (td < -1e-8)
                            {
                                l = -((T * P)[0] + height - placed_circles[i].R) / td;
                                if (L > l)
                                    L = l;
                            }
                            T = new Matrix(1, 2 * n + 1);
                            T[0, 2 * i] = 1;
                            td = (T * D)[0];
                            if (td < -1e-8)
                            {
                                l = -((T * P)[0] - placed_circles[i].R) / td;
                                if (L > l)
                                    L = l;
                            }
                            T = new Matrix(1, 2 * n + 1);
                            T[0, 2 * i + 1] = 1;
                            td = (T * D)[0];
                            if (td < -1e-8)
                            {
                                l = -((T * P)[0] - placed_circles[i].R) / td;
                                if (L > l)
                                    L = l;
                            }
                            T = new Matrix(1, 2 * n + 1);
                            T[0, 2 * i] = -1;
                            T[0, 2 * n] = 1;
                            td = (T * D)[0];
                            if (td < -1e-8)
                            {
                                l = -((T * P)[0] - placed_circles[i].R) / td;
                                if (L > l)
                                    L = l;
                            }
                        }
                        #endregion
                        P += D * L;
                        #region Проверить активные ограничения и удалить лишние.
                        #endregion
                    }
                } while (!is_exit);
            }

            private void tsmiSave_Click(object sender, EventArgs e)
            {
                StreamWriter sw = new StreamWriter("save.txt");
                foreach (Circle circle in placed_circles)
                    sw.WriteLine("{0} {1} {2}", 2 * circle.R, circle.X, circle.Y);

                sw.WriteLine();
                Opt.VD.DeloneCircle dc = new VD.DeloneCircle();
                dc.Calculate(placed_circles[2], placed_circles[1], placed_circles[0]);
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[1], placed_circles[2], placed_circles[3]);
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[4], placed_circles[3], placed_circles[2]);
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

                sw.WriteLine();
                dc.Calculate(placed_circles[0], placed_circles[1], new StripLine(0, 0, 0, 1));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[0], placed_circles[2], new StripLine(0, 0, -1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[1], placed_circles[3], new StripLine(0, 100, 1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[2], placed_circles[4], new StripLine(0, 0, -1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(placed_circles[3], placed_circles[4], new StripLine(0, 100, 1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

                sw.WriteLine();
                dc.Calculate(new StripLine(0, 0, 0, 1), placed_circles[0], new StripLine(0, 0, -1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(new StripLine(0, 0, 0, 1), placed_circles[1], new StripLine(0, 100, 1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
                dc.Calculate(new StripLine(0, 100, 1, 0), placed_circles[4], new StripLine(0, 0, -1, 0));
                sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

                sw.Close();
            }
        }
    }
}
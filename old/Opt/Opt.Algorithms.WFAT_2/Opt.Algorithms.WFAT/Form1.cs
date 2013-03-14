using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

using System.Xml;
using System.Runtime.Serialization;

using Opt.Geometrics;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Extentions.WFA;
using Opt.ClosenessModel;

namespace Opt.Algorithms.WFAT
{
    public partial class FormMain : Form
    {
        private Random rand;

        #region Переменные и методы , которые должны облегчить работу с сохранением и загрузкой настроек и данных.
        [Serializable]
        private class DataList
        {
            [Serializable]
            private struct Data
            {
                public Vector strip_size;
                public List<Circle> circles;
                public int method_index;
            }

            private string file_name;
            private int index;
            private List<Data> list;

            public DataList(string file_name)
            {
                this.file_name = file_name;
                list = new List<Data>();
            }

            public void Save()
            {
                #region Сериализация списка.
                XmlTextWriter xmlw = new XmlTextWriter(file_name, System.Text.Encoding.UTF8);
                xmlw.Formatting = Formatting.Indented;
                NetDataContractSerializer dcs = new NetDataContractSerializer();
                dcs.WriteObject(xmlw, list);
                xmlw.Close();
                #endregion
            }
            public void Load()
            {
                #region Десериализация списка.
                XmlTextReader xmlr = new XmlTextReader(file_name);
                NetDataContractSerializer dcs = new NetDataContractSerializer();
                list = (List<Data>)dcs.ReadObject(xmlr);
                xmlr.Close();
                #endregion

                index = list.Count - 1;
            }

            public void Get(ref Vector strip_size, ref List<Circle> circles, ref int method_index)
            {
                if (0 <= index && index < list.Count)
                {
                    strip_size = list[index].strip_size.Copy;
                    circles = new List<Circle>(list[index].circles.Count);
                    for (int i = 0; i < list[index].circles.Count - 1; i++)
                        circles.Add(list[index].circles[i].Copy);
                    method_index = list[index].method_index;
                }
            }
            public void Add(Vector strip_size, List<Circle> circles, int method_index)
            {
                Data data = new Data();
                data.strip_size = strip_size.Copy;
                data.circles = new List<Circle>(circles.Count);
                for (int i = 0; i < circles.Count; i++)
                    data.circles.Add(circles[i].Copy);
                data.method_index = method_index;

                list.Add(data);

                Save();
            }
            public void Del()
            {
                if (0 < index && index < list.Count)
                    list.RemoveAt(index);
                if (index > list.Count - 1)
                    index = list.Count - 1;
            }

            public void MovePrev()
            {
                if (list.Count > 0)
                {
                    index--;
                    if (index < 0)
                        index = 0;
                }
            }
            public void MoveNext()
            {
                if (list.Count > 0)
                {
                    index++;
                    if (index > list.Count - 1)
                        index = list.Count - 1;
                }
            }
            public void MoveTo(int index)
            {
                if (0 <= index && index < list.Count)
                    this.index = index;
            }
        }

        private DataList data_list;
        #endregion

        private Vector strip_size;
        private List<Circle> circles;

        private int method_index;
        private int current_index;
        private List<Circle> circles_placed;
        private List<Circle> circles_placed_;
        private TimeSpan time_span;
        private TimeSpan time_span_local;
        private int iteration_count = 0;

        public FormMain()
        {
            InitializeComponent();

            rand = new Random();

            string file_name = "Data.xml";
            data_list = new DataList(file_name);
            if (!File.Exists(file_name))
            {
                strip_size = new Vector();
                circles = new List<Circle>();
                method_index = 0;

                data_list.Add(strip_size, circles, method_index);
                data_list.Save();
            }

            data_list.Load();
            data_list.Get(ref strip_size, ref  circles, ref  method_index);

            current_index = -1;
            circles_placed = new List<Circle>();
            circles_placed_ = new List<Circle>();
            time_span = new TimeSpan();
            time_span_local = new TimeSpan();
            iteration_count = 0;
        }

        #region Меню "Файл".
        private void Очистить_Click(object sender, EventArgs e)
        {
            data_list.MoveTo(0);
            data_list.Get(ref strip_size, ref circles, ref method_index);

            current_index = -1;
            circles_placed = new List<Circle>();
            circles_placed_ = new List<Circle>();
            time_span = new TimeSpan();
            time_span_local = new TimeSpan();
            iteration_count = 0;
            Invalidate();
        }
        private void Очистить_размещение_Click(object sender, EventArgs e)
        {
            current_index = -1;
            circles_placed.Clear();
            circles_placed_.Clear();
            time_span = new TimeSpan();
            time_span_local = new TimeSpan();
            iteration_count = 0;
            Invalidate();
        }
        private void Выход_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Меню "Данные".
        private void Настроить_полосу_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите ширину полосы", Info = strip_size.Y.ToString() };
            ft.ShowDialog(this);
            try
            {
                strip_size.Y = double.Parse(ft.Info);
            }
            catch
            {
            }
            Invalidate();
        }
        private void Создать_круг_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите радиус круга", Info = (strip_size.Y * rand.NextDouble()).ToString() };
            ft.ShowDialog(this);
            try
            {
                circles.Add(new Circle { Radius = double.Parse(ft.Info) });
            }
            catch
            {
            }
            Invalidate();
        }
        private void Создать_несколько_кругов_Click(object sender, EventArgs e)
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
                    circles.Add(new Circle { Radius = min + (max - min) * rand.NextDouble() });
            }
            catch
            {
            }
            Invalidate();
        }

        private void Сохранить_данные_Click(object sender, EventArgs e)
        {
            data_list.Add(strip_size, circles, method_index);
            data_list.Save();
        }
        private void Перейти_на_более_старые_данные_Click(object sender, EventArgs e)
        {
            data_list.MovePrev();
            data_list.Get(ref strip_size, ref  circles, ref  method_index);
            current_index = -1;
            circles_placed = new List<Circle>();
            circles_placed_ = new List<Circle>();
            time_span = new TimeSpan();
            time_span_local = new TimeSpan();
            iteration_count = 0;
            Invalidate();
        }
        private void Перейти_на_более_новые_данные_Click(object sender, EventArgs e)
        {
            data_list.MoveNext();
            data_list.Get(ref strip_size, ref  circles, ref  method_index);
            current_index = -1;
            circles_placed = new List<Circle>();
            circles_placed_ = new List<Circle>();
            time_span = new TimeSpan();
            time_span_local = new TimeSpan();
            iteration_count = 0;
            Invalidate();
        }
        private void Удалить_данные_Click(object sender, EventArgs e)
        {
            #region Удаляем данные, которые считаются текущими.
            data_list.Del();
            data_list.Save();
            if (MessageBox.Show(this, "Перегрузить данные на форме?", "Осторожно! Есть возможность потерять текущие данные!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                data_list.Get(ref strip_size, ref circles, ref method_index);
                current_index = -1;
                circles_placed = new List<Circle>();
                circles_placed_ = new List<Circle>();
                time_span = new TimeSpan();
                time_span_local = new TimeSpan();
                iteration_count = 0;
                Invalidate();
            }
            #endregion
        }
        #endregion

        #region Меню "Задача".
        private void Выбрать_метод_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите номер метода: 1 - метод последовательного одиночного размещения; 2 - метод последовательного одиночного размещения (модифицированный); 3 - метод локального поиска", Info = method_index.ToString() };
            ft.ShowDialog(this);
            try
            {
                method_index = int.Parse(ft.Info);
            }
            catch
            {
            }
        }
        private void Установить_интервал_Click(object sender, EventArgs e)
        {
            FormTemp ft = new FormTemp() { Width = this.Width, Text = "Введите интервал таймера", Info = timer.Interval.ToString() };
            ft.ShowDialog(this);
            try
            {
                timer.Interval = int.Parse(ft.Info);
            }
            catch
            {
            }
        }

        private void Сделать_шаг_Click(object sender, EventArgs e)
        {
            #region Манипуляции с временем.
            DateTime date_time = DateTime.Now;
            #endregion

            if (method_index == 1)
            {
                if (current_index < circles.Count - 1)
                {
                    #region Выполнить шаг метода последовательного одиночного размещения.
                    current_index++;
                    if (Step(strip_size, circles_placed, circles[current_index]))
                        #region Переносим круг в список размещённых.
                        circles_placed.Add(circles[current_index]);
                        #endregion
                    else
                        #region Переносим круг в список неразмещённых.
                        circles_placed_.Add(circles[current_index]);
                        #endregion
                    #endregion
                }
                else
                    timer.Stop();
            }
            else if (method_index == 2)
            {
                if (current_index < circles.Count - 1)
                {
                    if (current_index == -1)
                        #region Выполнить предобработку модифицированного метода последовательного одиночного размещения.
                        Step_(ref triples);
                        #endregion
                    #region Выполнить шаг модифицированного метода последовательного одиночного размещения.
                    current_index++;
                    if (Step(strip_size, ref triples, circles[current_index]))
                        #region Переносим круг в список размещённых.
                        circles_placed.Add(circles[current_index]);
                        #endregion
                    else
                        #region Переносим круг в список неразмещённых.
                        circles_placed_.Add(circles[current_index]);
                        #endregion
                    #endregion
                }
                else
                    timer.Stop();
            }
            else if (method_index == 3)
            {
                #region Выполнить предобработку метода локального поиска.
                if (iteration_count==0)
                {                    
                    Шаг_0();
                }
                #endregion
                #region Выполнить шаг метода локального поиска.
               
                if (!StepLocal())
                {
               //     iteration_count = 0;
                    timer.Stop();
                }
                iteration_count++;
                #endregion
            }
            else if (method_index == 4)
            {
                //#region Выполнить предобработку метода локального поиска.
                //if (iteration_count == 0)
                //{
                //    Шаг_0();
                //}
                //#endregion
                #region Выполнить шаг метода локального поиска.
             
                if (!StepLocal_barier())
                {
                   // iteration_count = 0;
                    timer.Stop();
                }
                iteration_count++;
                #endregion
            }

            #region Манипуляции с временем.
            time_span += DateTime.Now - date_time;
            if (method_index > 2)
                time_span_local += DateTime.Now - date_time;
            #endregion

            #region Обновить визуальную и текстовую информацию о состоянии задачи размещения.
            Invalidate();
            #endregion
        }

        private void Запустить_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
        private void Остановить_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Сделать_шаг_Click(null, null);
        }
        #endregion


        #region Метод последовательного одиночного размещения.
        #region Метод последовательного одиночного размещения (классический).
        #region Вспомогательные методы.
        /// <summary>
        /// Проверка на попадание круга в полосу.
        /// </summary>
        /// <param name="point">Вектор размещения круга.</param>
        /// <param name="circle">Круг.</param>
        /// <param name="height">Высота полосы.</param>
        /// <returns>Возвращает True, если круг полностью лежит внутри полосы. False - в противном случае.</returns>
        private bool IsCheckedStrip(Point point, Circle circle, double height)
        {
            return (point.Y + circle.Radius <= height) && (point.X - circle.Radius >= 0) && (point.Y - circle.Radius >= 0); //!!! Необходимо учитывать погрешность.
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
        #endregion

        private bool Step(Vector strip_size, List<Circle> circles, Circle circle)
        {
            #region Шаг-1. Проверка размера круга относительно полосы.
            if (2 * circle.Radius > strip_size.Y)
                return false;
            #endregion
            #region Шаг-2. Создание списка точек возможных размещений и добавление двух начальных точек.
            List<Point> points = new List<Point>();
            points.Add(new Point { X = circle.Radius, Y = circle.Radius });
            points.Add(new Point { X = circle.Radius, Y = strip_size.Y - circle.Radius });
            #endregion
            #region Шаг-3. Создание и заполнение списка годографов.
            List<Circle> godographs = new List<Circle>(circles.Count);
            for (int i = 0; i < circles.Count; i++)
                godographs.Add(CircleExt.Годограф_функции_плотного_размещения(circles[i], circle));
            #endregion
            #region Шаг-4. Поиск точек пересечения круга с полосой.
            for (int i = 0; i < godographs.Count; i++)
            {
                #region Шаг-4.1. Поиск точек пересечения круга с левой границей полосы.
                if (godographs[i].Pole.X - godographs[i].Radius < circle.Radius)
                {
                    double x = circle.Radius - godographs[i].Pole.X;
                    double y = Math.Sqrt(godographs[i].Radius * godographs[i].Radius - x * x);
                    Point point;
                    point = new Point { X = circle.Radius, Y = godographs[i].Pole.Y - y };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                    point = new Point { X = circle.Radius, Y = godographs[i].Pole.Y + y };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                }
                #endregion
                #region Шаг-4.2. Поиск точек пересечения круга с нижней границей полосы.
                if (godographs[i].Pole.Y - godographs[i].Radius < circle.Radius)
                {
                    double y = circle.Radius - godographs[i].Pole.Y;
                    double x = Math.Sqrt(godographs[i].Radius * godographs[i].Radius - y * y);
                    Point point;
                    point = new Point { X = godographs[i].Pole.X - x, Y = circle.Radius };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                    point = new Point { X = godographs[i].Pole.X + x, Y = circle.Radius };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                }
                #endregion
                #region Шаг-4.3. Поиск точек пересечения круга с верхней границей полосы.
                if (godographs[i].Pole.Y + godographs[i].Radius > strip_size.Y - circle.Radius)
                {
                    double y = strip_size.Y - circle.Radius - godographs[i].Pole.Y;
                    double x = Math.Sqrt(godographs[i].Radius * godographs[i].Radius - y * y);
                    Point point;
                    point = new Point { X = godographs[i].Pole.X - x, Y = strip_size.Y - circle.Radius };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                    point = new Point { X = godographs[i].Pole.X + x, Y = strip_size.Y - circle.Radius };
                    if (IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point);
                }
                #endregion
            }
            #endregion
            #region Шаг-5. Поиск точек пересечения годографов.
            for (int i = 0; i < godographs.Count - 1; i++)
                for (int j = i + 1; j < godographs.Count; j++)
                {
                    Point point;

                    point = CircleExt.Точка_пересечения_границ(godographs[i], godographs[j]);
                    if (point != null && IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.

                    point = CircleExt.Точка_пересечения_границ(godographs[j], godographs[i]);
                    if (point != null && IsCheckedStrip(point, circle, strip_size.Y))
                        points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.
                }
            #endregion
            #region Шаг-6. Сортировка набора точек возможного размещения.!!! Данная часть не нужна, если использовать сортировку при вставке точек в набор данных.
            for (int i = 0; i < points.Count - 1; i++)
                for (int j = i + 1; j < points.Count; j++)
                    if (points[i].X > points[j].X || (points[i].X == points[j].X && points[i].Y > points[j].Y))
                    {
                        Point temp_point = points[i];
                        points[i] = points[j];
                        points[j] = temp_point;
                    }
            #endregion
            #region Шаг-7. Выбор наилучшей точки размещения, при которой не возникает пересечение кругов и размещение круга.
            int p = -1;
            do
            {
                p++;
                circle.Pole.Copy = points[p];
            } while (!IsCheckedCircles(circle, circles, 0.0001));
            #endregion
            #region Шаг-8. Пересчёт ширины занятой части полосы.
            strip_size.X = Math.Max(strip_size.X, circle.Pole.X + circle.Radius);
            #endregion
            return true;
        }
        #endregion

        #region Метод последовательного одиночного размещения (модифицированный).
        #region Вспомогательные данные.
        private List<Vertex<Geometric>> triples;
        private double max_x = 1e5;
        #endregion

        #region Вспомогательные методы.
        private bool Функция_расширенного_расстояния_на_отрезке_монотонна(Vertex<Geometric> vertex)
        {
            if (vertex.Next.DataInVertex is Plane && vertex.Prev.DataInVertex is Plane)
                return true;
            Plane plane = GeometricExt.Серединная_полуплоскость(vertex.Next.DataInVertex, vertex.Prev.DataInVertex);
            return PlaneExt.Расширенное_расстояние(plane, vertex.Somes.CircleDelone.Pole) * PlaneExt.Расширенное_расстояние(plane, vertex.Cros.Somes.CircleDelone.Pole) > 0;
        }
        private bool Существует_точка_плотного_размещения_второго_рода(Circle circle, Vertex<Geometric> vertex)
        {
            if (circle.Radius > vertex.Somes.CircleDelone.Radius)
                return false;
            else
                if (circle.Radius >= vertex.Cros.Somes.CircleDelone.Radius)
                    return true;
                else
                    if (Функция_расширенного_расстояния_на_отрезке_монотонна(vertex)) // Придумать что-то другое?
                        return false;
                    else
                        return 2 * circle.Radius >= GeometricExt.Расширенное_расстояние(vertex.Prev.DataInVertex, vertex.Next.DataInVertex);
        }
        #endregion

        /// <summary>
        /// Предварительный шаг.
        /// </summary>
        /// <param name="triples">Набор троек.</param>
        private void Step_(ref List<Vertex<Geometric>> triples)
        {
            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоугольника. !!!Потом переделать на полосу!!!
            List<Geometric> borders = new List<Geometric>();
            borders.Add(new Plane { Pole = new Point { X = max_x / 2, Y = strip_size.Y }, Normal = new Vector { X = 0, Y = -1 } });
            borders.Add(new Plane { Pole = new Point { X = 0, Y = strip_size.Y / 2 }, Normal = new Vector { X = 1, Y = 0 } });
            borders.Add(new Plane { Pole = new Point { X = max_x / 2, Y = 0 }, Normal = new Vector { X = 0, Y = 1 } });

            Vertex<Geometric> vertex = Vertex<Geometric>.CreateClosenessModel(borders[0], borders[1], borders[2]);
            vertex.BreakCrosBy(new Plane { Pole = new Point { X = max_x, Y = strip_size.Y / 2 }, Normal = new Vector { X = -1, Y = 0 } }); // Добавить четвёртую сторону.
            #endregion
            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !!Нужно ли автоматизировать!!
            vertex.SetCircleDelone(new Circle { Pole = new Point { X = strip_size.Y / 2, Y = strip_size.Y / 2 }, Radius = strip_size.Y / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point { X = max_x - strip_size.Y / 2, Y = strip_size.Y / 2 }, Radius = strip_size.Y / 2 });

            vertex.Prev.Cros.SetCircleDelone(new Circle());
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle());
            #endregion
            triples = vertex.GetTriples();
        }

        private bool Step(Vector strip_size, ref List<Vertex<Geometric>> triples, Circle circle)
        {
            #region Шаг 5.1. Проверяем размер круга относительно полосы.
            if (2 * circle.Radius > strip_size.Y)
                return false;
            #endregion
            #region Шаг 5.2. Устанавливаем начальное значение для точки размещения текущего объекта и связанной с ней вершиной.
            Point point_global = new Point { X = double.PositiveInfinity };
            Vertex<Geometric> vertex_global = null;
            #endregion
            #region Шаг 5.3. Для каждой вершины выполняем следующее...
            for (int i = 0; i < triples.Count; i++)
            {
                Vertex<Geometric> vertex = triples[i];
                do
                {
                    #region Шаг 5.3.1. Если выполняются все условия существования точки плотного размещения второго рода, то находим её.
                    if (Существует_точка_плотного_размещения_второго_рода(circle, vertex))
                    {
                        Point point_temp = circle.Точка_близости_второго_рода(vertex.Next.DataInVertex, vertex.Prev.DataInVertex);
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
            circle.Pole.Copy = point_global;
            #endregion
            #region Шаг 5.5. Вставляем объект в ребро напротив найденной вершины.
            vertex_global.BreakCrosBy(circle);
            vertex_global = vertex_global.Cros;
            #endregion
            #region Шаг 5.7. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
            Vertex<Geometric> vertex_temp = vertex_global;
            do
            {
                while (CircleExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                    vertex_temp.Rebuild();

                vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                vertex_temp = vertex_temp.Next.Cros.Next;
            } while (vertex_temp != vertex_global);
            #endregion

            triples = triples[0].GetTriples();

            #region Шаг 5.8. Пересчёт ширины занятой части полосы.
            strip_size.X = Math.Max(strip_size.X, circle.Pole.X + circle.Radius);
            #endregion
            return true;
        }
        #endregion
        #endregion

        #region Метод локального поиска.
        #region Глобальные описания для метода локального поиска
        double eps = 1e-8; //1e-6
        double eps1 = 1e-4;
        //StreamWriter sw = new StreamWriter("Out.txt", false);
        Matrix P; // текущее значение переменных
        Matrix G; // вектор градиента 
        Matrix A; // матрица активных ограничений
        Matrix B = new Matrix(0, 2); // номера объектов в матрице А
        bool[] wl; // рабочий список: номера ограничений, входящих в А
        int NumStep = 0; // номер шага
        bool is_exit = false;
        //для метода барьеров
        double eps2 = 1e-2;
        double eps3 = 0;//1e-6;
        double eps4 = 2;
        double h = 1; // длина шага в барьерном методе и его начальное значение
        double c1 = 1.5; // коеффициент увеличения длины шага в барьерном методе по отношению к предыд шагу
        double ff = 0.8;
        Matrix d_l; // вектор направления движения на предыдущем шаге 
        Matrix g_l; // вектор градиента на предыдущем шаге
        double b = 0;

        #endregion

        private double FB(Matrix X)
        {
            int n = circles_placed.Count;
            double f = X[2 * n]; // длина полосы
            // барьерная функция по полосе
            for (int i = 0; i < circles_placed.Count; i++)
            {
                f += ff / (- X[2 * i + 1] - circles_placed[i].Radius + strip_size.Y + eps2);
                f += ff / (X[2 * i] - circles_placed[i].Radius + eps2);
                f += ff / (X[2 * i + 1] - circles_placed[i].Radius + eps2);
                f += ff / ( - X[2 * i] - circles_placed[i].Radius + X[2 * n] + eps2);
            }
            // барьерная функция по непересечению
            for (int i = 0; i < circles_placed.Count - 1; i++)
                for (int j = i + 1; j < circles_placed.Count; j++)
                {
                    f += ff / (Math.Pow(X[2 * i] - X[2 * j], 2) + 
                              Math.Pow(X[2 * i + 1] - X[2 * j + 1], 2) -
                              Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2) + eps2);
                }
            return f;
        }

        private Matrix g_FB(Matrix X) 
        {
            int n = circles_placed.Count;
            Matrix g = new Matrix(2 * n + 1); //
            g[2 * n] = 1;
            // барьерная функция по полосе
            for (int i = 0; i < circles_placed.Count; i++)
            {
                g[2 * i] = - ff / Math.Pow(X[2 * i] - circles_placed[i].Radius + eps2, 2);
                g[2 * i] += ff / Math.Pow(- X[2 * i] - circles_placed[i].Radius + X[2 * n] + eps2, 2);
                g[2 * i + 1] = ff / Math.Pow(- X[2 * i + 1] - circles_placed[i].Radius + strip_size.Y + eps2, 2);
                g[2 * i + 1] += - 1 / Math.Pow(X[2 * i + 1] - circles_placed[i].Radius + eps2, 2);
                g[2 * n] += - ff / Math.Pow(- X[2 * i] - circles_placed[i].Radius + X[2 * n] + eps2, 2);
            }
            // барьерная функция по непересечению
            double d;
            for (int i = 0; i < circles_placed.Count - 1; i++)
                for (int j = i + 1; j < circles_placed.Count; j++)
                {
                    d =       (Math.Pow(X[2 * i] - X[2 * j], 2) +
                              Math.Pow(X[2 * i + 1] - X[2 * j + 1], 2) -
                              Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2) + eps2);
                    d *= d;
                    g[2 * i] += -2 * (X[2 * i] - X[2 * j]) * ff / d;
                    g[2 * j] += 2 * (X[2 * i] - X[2 * j]) * ff / d;
                    g[2 * i + 1] += -2 * (X[2 * i + 1] - X[2 * j + 1]) * ff / d; ;
                    g[2 * j + 1] += 2 * (X[2 * i + 1] - X[2 * j + 1]) * ff / d; ;

                }
            return g;
        }

        private bool Is_feasible(Matrix X)
        {
            int n = circles_placed.Count;
            bool feasi = true;
            double f = X[2 * n]; // длина полосы
            // барьерная функция по полосе
            for (int i = 0; i < circles_placed.Count && feasi; i++)
            {
                feasi &= (-X[2 * i + 1] - circles_placed[i].Radius + strip_size.Y + eps2) >= eps3;
                feasi &= (X[2 * i] - circles_placed[i].Radius + eps2) >= eps3;
                feasi &= (X[2 * i + 1] - circles_placed[i].Radius + eps2) >= eps3;
                feasi &= (-X[2 * i] - circles_placed[i].Radius + X[2 * n] + eps2) >= eps3;
            }
            // барьерная функция по непересечению
            for (int i = 0; i < circles_placed.Count - 1 && feasi; i++)
                for (int j = i + 1; j < circles_placed.Count && feasi; j++)
                {
                    feasi &= //CircleExt.Расширенное_расстояние(circles_placed[i], circles_placed[j]) > eps3;

                             (Math.Pow(X[2 * i] - X[2 * j], 2) +
                              Math.Pow(X[2 * i + 1] - X[2 * j + 1], 2) -
                              Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2) + eps2) >= eps3;
                }
            return feasi;
        }

        private bool StepLocal_barier()
        {
            int n = circles.Count;
            if (iteration_count == 0)
            {

                circles_placed = circles;

                //double x_t = 0;
                //for (int i = 0; i < circles_placed.Count; ++i)
                //{
                //    circles_placed[i].Pole.Y = circles_placed[i].Radius + eps4;
                //    x_t += circles_placed[i].Radius + eps4;
                //    circles_placed[i].Pole.X = x_t;
                //    x_t += circles_placed[i].Radius + eps4;

                //}
                strip_size.X = circles_placed[n - 1].Pole.X + circles_placed[n - 1].Radius + eps4;
                Invalidate();

                d_l = new Matrix(2 * n + 1); // вектор 
                g_l = new Matrix(2 * n + 1); // вектор
                #region Создание и заполнение вектора размещения P (2*n+1 x 1).
                P = new Matrix(2 * n + 1); // текущее значение переменных
                for (int i = 0; i < circles_placed.Count; i++)
                {
                    P[2 * i] = circles_placed[i].Pole.X;
                    P[2 * i + 1] = circles_placed[i].Pole.Y;
                }
                P[2 * n] = strip_size.X;
                #endregion
             }


            Matrix d = new Matrix(2 * n + 1); // вектор направления движения
            d = -g_FB(P);


            if (iteration_count % (2 * n) != 0)
            {
                b = (g_FB(P) * g_FB(P).Tr())[0] / (g_l * g_l.Tr())[0];
                d += b * d_l;
            }
            
            g_l = -g_FB(P);

            Matrix M1 = d * d;
            double norm = (d * d.Tr())[0];
            norm = 1 / Math.Sqrt(norm);
            d = norm * d;
            d_l = d;
            Matrix P_new = new Matrix(2 * n + 1);
            h = c1 * h;
            P_new = P + h * d;
            
           // if (h < eps2) h = 1;
            bool b1 = FB(P_new) < FB(P) + eps;
            bool b2 = Is_feasible(P_new);
            bool b3 = h >= eps;
            while ( !( b1 && b2 ) )//|| b3)
            {
                h /= 2;
                P_new = P + h * d;
                b1 = FB(P_new) < FB(P) + eps;
                b2 = Is_feasible(P_new);
                b3 = h >= eps;
            }
            P = P_new;
            for (int i = 0; i < circles_placed.Count; i++)
            {
                circles_placed[i].Pole.X = P[2 * i];
                circles_placed[i].Pole.Y = P[2 * i + 1];
            }
             strip_size.X = P[2 * n];

             Invalidate();
             is_exit = h < eps;
             return !is_exit;
        }

        private void Шаг_0()
        {
            int n = circles_placed.Count;
            #region Создание и заполнение вектора размещения P (2*n+1 x 1).
            P = new Matrix(2 * n + 1); // текущее значение переменных
            for (int i = 0; i < circles_placed.Count; i++)
            {
                P[2 * i] = circles_placed[i].Pole.X;
                P[2 * i + 1] = circles_placed[i].Pole.Y;
            }
            P[2 * n] = strip_size.X;
            #endregion
            #region Создание вектора градиента G (2*n+1 x 1).
            G = new Matrix(2 * n + 1);
            G[G.RowCount - 1] = 1;
            #endregion
            #region Создание и заполнение матрицы активных ограничений a (!2*n+1! x 2*n+1).
            A = new Matrix(0, 2 * n + 1);
            wl = new bool[4 * n + n * (n - 1) / 2];
            #region Активные ограничения по полосе.
            int i_a = 0;
            for (int i = 0; i < circles_placed.Count; i++)
            {
                if (circles_placed[i].Pole.Y + circles_placed[i].Radius == strip_size.Y)
                {
                    A.AddRow();
                    A[A.RowCount - 1, 2 * i + 1] = -1;
                    B.AddRow();
                    B[B.RowCount - 1, 0] = -1;
                    B[B.RowCount - 1, 1] = i;
                    wl[4 * i] = true;

                }
                if (circles_placed[i].Pole.X - circles_placed[i].Radius == 0)
                {
                    A.AddRow();
                    A[A.RowCount - 1, 2 * i] = 1;
                    B.AddRow();
                    B[B.RowCount - 1, 0] = -2;
                    B[B.RowCount - 1, 1] = i;
                    wl[4 * i + 1] = true;
                }
                if (circles_placed[i].Pole.Y - circles_placed[i].Radius == 0)
                {
                    A.AddRow();
                    A[A.RowCount - 1, 2 * i + 1] = 1;
                    B.AddRow();
                    B[B.RowCount - 1, 0] = -3;
                    B[B.RowCount - 1, 1] = i;
                    wl[4 * i + 2] = true;
                }
                if (circles_placed[i].Pole.X + circles_placed[i].Radius == strip_size.X)
                {
                    A.AddRow();
                    A[A.RowCount - 1, 2 * i] = -1;
                    A[A.RowCount - 1, 2 * n] = 1;
                    B.AddRow();
                    B[B.RowCount - 1, 0] = -4;
                    B[B.RowCount - 1, 1] = i;
                    wl[4 * i + 3] = true;
                }
            }
            i_a = 4 * n - 1;
            #endregion
            #region Активные ограничения между кругами.
            for (int i = 0; i < circles_placed.Count - 1; i++)
                for (int j = i + 1; j < circles_placed.Count; j++)
                {
                    ++i_a;
                    if (Math.Abs(CircleExt.Расширенное_расстояние(circles_placed[i], circles_placed[j])) < eps)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, 2 * i] = 2 * (circles_placed[i].Pole.X - circles_placed[j].Pole.X);
                        A[A.RowCount - 1, 2 * i + 1] = 2 * (circles_placed[i].Pole.Y - circles_placed[j].Pole.Y);
                        A[A.RowCount - 1, 2 * j] = 2 * (circles_placed[j].Pole.X - circles_placed[i].Pole.X);
                        A[A.RowCount - 1, 2 * j + 1] = 2 * (circles_placed[j].Pole.Y - circles_placed[i].Pole.Y);

                        B.AddRow();
                        B[B.RowCount - 1, 0] = i;
                        B[B.RowCount - 1, 1] = j;
                        wl[i_a] = true;
                    }
                }
            #endregion
            #endregion
        }

        private bool StepLocal()
        {
            int n = circles_placed.Count;


            int i_a = 0;
            Matrix ATr = A.Tr();
            Matrix AOb = (A * ATr).Ob();
            #region Рассчитать вектор множетелей Лагранжа U.
            Matrix U = AOb * A * G;
            #endregion
            #region Получить вектор направления d (2*n+1 x 1).
            Matrix D = ATr * U - G;

            #endregion

            if (D.IsNull(eps))
            {
                int index = U.RowOfMinElement();
                if (U[index] < -eps1)
                {
                    int i = (int)B[index, 0];
                    int j = (int)B[index, 1];
                    if (i < 0)
                    {
                        wl[4 * j + Math.Abs(i) - 1] = false;
                    }
                    else
                    {
                        wl[4 * n - 1 + i * n - i * (i + 1) / 2 + j - i] = false;
                    }
                    A.DelRow(index);
                    B.DelRow(index);

                }
                else
                {
                    #region Проверить активные ограничения и удалить лишние.
                    double d_max = eps;
                    int k_max = -1;
                    int i_max = -1;
                    int j_max = -1;
                    for (int k = 0; k < A.RowCount - 1; k++)
                    {
                        if (B[k, 0] >= 0)
                        {
                            int i = (int)B[k, 0];
                            int j = (int)B[k, 1];
                            double dist = Math.Pow(circles_placed[i].Pole.X - circles_placed[j].Pole.X, 2) +
                                Math.Pow(circles_placed[i].Pole.Y - circles_placed[j].Pole.Y, 2) -
                                Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2);
                            //if (ExtendedDistance.Calc(circles_placed[i], circles_placed[j]) > eps)
                            if (dist > d_max)
                            {
                                d_max = dist; k_max = k; i_max = i; j_max = j;
                            }

                        }
                    }
                    if (k_max >= 0)
                    {
                        A.DelRow(k_max);
                        B.DelRow(k_max);
                        wl[4 * n - 1 + i_max * n - i_max * (i_max + 1) / 2 + j_max - i_max] = false;
                    }
                    else
                    {
                        Text += "минимум найден";
                        is_exit = true;
                    }
                    #endregion
                }
            }
            else
            {
                #region Находим длину шага L и соответствующее ему ограничение, которое добавляем в A.
                double L = double.PositiveInfinity;

                Matrix T;
                double td;
                double l;
                Matrix TNew = new Matrix(1, 2 * n + 1); // добавляемое ограничение
                A.AddRow();
                B.AddRow();
                for (int i = 0; i < circles_placed.Count; i++)
                {
                    if (!wl[4 * i])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i + 1] = -1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] + strip_size.Y - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -1;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 1])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -2;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 2])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i + 1] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -3;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 3])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i] = -1;
                        T[0, 2 * n] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -4;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                }
                i_a = 4 * n - 1;
                for (int i = 0; i < circles_placed.Count - 1; i++)
                    for (int j = i + 1; j < circles_placed.Count; j++)
                    {
                        ++i_a;
                        if (!wl[i_a])
                        {
                            double a = Math.Pow(D[2 * i] - D[2 * j], 2) + Math.Pow(D[2 * i + 1] - D[2 * j + 1], 2);
                            double b = 2 * (D[2 * i] - D[2 * j]) * (P[2 * i] - P[2 * j]) + 2 * (D[2 * i + 1] - D[2 * j + 1]) * (P[2 * i + 1] - P[2 * j + 1]);
                            double c = Math.Pow(P[2 * i] - P[2 * j], 2) + Math.Pow(P[2 * i + 1] - P[2 * j + 1], 2)
                                                - Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2);
                            double d = b * b - 4 * a * c;
                            if (d > eps)
                            {
                                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                                double x = Math.Min(x1, x2);
                                if (x > -eps && x < L)
                                {
                                    L = x;
                                    B[B.RowCount - 1, 0] = i;
                                    B[B.RowCount - 1, 1] = j;
                                }

                            }
                        }
                    }
                #endregion
                //sw.WriteLine("Длина шага {0}", L);
                P += D * L;
                for (int i = 0; i < circles_placed.Count; i++)
                {
                    circles_placed[i].Pole.X = P[2 * i];
                    circles_placed[i].Pole.Y = P[2 * i + 1];
                }
                strip_size.X = P[2 * n];

                #region Добавление в матрицу нового ограничения
                {
                    int i = (int)B[B.RowCount - 1, 0];
                    int j = (int)B[B.RowCount - 1, 1];
                    if (i >= 0)
                    {
                        A[A.RowCount - 1, 2 * i] = 2 * (circles_placed[i].Pole.X - circles_placed[j].Pole.X);
                        A[A.RowCount - 1, 2 * i + 1] = 2 * (circles_placed[i].Pole.Y - circles_placed[j].Pole.Y);
                        A[A.RowCount - 1, 2 * j] = 2 * (circles_placed[j].Pole.X - circles_placed[i].Pole.X);
                        A[A.RowCount - 1, 2 * j + 1] = 2 * (circles_placed[j].Pole.Y - circles_placed[i].Pole.Y);
                        wl[4 * n - 1 + i * n - i * (i + 1) / 2 + j - i] = true;
                    }
                    else
                    {
                        switch (i)
                        {
                            case -1: A[A.RowCount - 1, 2 * j + 1] = -1; break;
                            case -2: A[A.RowCount - 1, 2 * j] = 1; break;
                            case -3: A[A.RowCount - 1, 2 * j + 1] = 1; break;
                            case -4: A[A.RowCount - 1, 2 * j] = -1; A[A.RowCount - 1, 2 * n] = 1; ; break;
                        }
                        wl[4 * j + Math.Abs(i) - 1] = true;

                    }
                }
                #endregion
                ++NumStep;
                Text = "Шаг " + NumStep + " f= " + strip_size.X;
            }

            Invalidate();
            return !is_exit;
        }

        #region Пока ещё не разобрался, что из этого нужно.
        private void miStepLocalSearch_Click(object sender, EventArgs e)
        {
            //int n = circles_placed.Count;
            //#region Создание и заполнение вектора размещения P (2*n+1 x 1).
            //Matrix P = new Matrix(2 * n + 1);
            //for (int i = 0; i < circles_placed.Count; i++)
            //{
            //    P[2 * i] = circles_placed[i].X;
            //    P[2 * i + 1] = circles_placed[i].Y;
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
            //for (int i = 0; i < circles_placed.Count; i++)
            //{
            //    if (circles_placed[i].Y + circles_placed[i].R == height)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i + 1] = -1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -1;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (circles_placed[i].X - circles_placed[i].R == 0)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i] = 1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -2;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (circles_placed[i].Y - circles_placed[i].R == 0)
            //    {
            //        A.AddRow();
            //        A[A.RowCount - 1, 2 * i + 1] = 1;
            //        B.AddRow();
            //        B[B.RowCount - 1, 0] = -3;
            //        B[B.RowCount - 1, 1] = i;
            //    }
            //    if (circles_placed[i].X + circles_placed[i].R == length)
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
            //for (int i = 0; i < circles_placed.Count - 1; i++)
            //    for (int j = i + 1; j < circles_placed.Count; j++)
            //    {
            //        if (Math.Abs(ExtendedDistance.Calc(circles_placed[i], circles_placed[j])) < 0.0001)
            //        {
            //            A.AddRow();
            //            A[A.RowCount - 1, 2 * i] = 2 * (circles_placed[i].X - circles_placed[j].X);
            //            A[A.RowCount - 1, 2 * i + 1] = 2 * (circles_placed[i].Y - circles_placed[j].Y);
            //            A[A.RowCount - 1, 2 * j] = 2 * (circles_placed[j].X - circles_placed[i].X);
            //            A[A.RowCount - 1, 2 * j + 1] = 2 * (circles_placed[j].Y - circles_placed[i].Y);

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
            //        for (int i = 0; i < circles_placed.Count; i++)
            //        {
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i + 1] = -1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] + height - circles_placed[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - circles_placed[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i + 1] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - circles_placed[i].R) / td;
            //                if (L > l)
            //                    L = l;
            //            }
            //            T = new Matrix(1, 2 * n + 1);
            //            T[0, 2 * i] = -1;
            //            T[0, 2 * n] = 1;
            //            td = (T * D)[0];
            //            if (td < -1e-8)
            //            {
            //                l = -((T * P)[0] - circles_placed[i].R) / td;
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

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("save.txt");
            //foreach (Circle circle in circles_placed)
            //    sw.WriteLine("{0} {1} {2}", 2 * circle.R, circle.X, circle.Y);

            //sw.WriteLine();
            //Opt.VD.DeloneCircle dc = new VD.DeloneCircle();
            //dc.Calculate(circles_placed[2], circles_placed[1], circles_placed[0]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[1], circles_placed[2], circles_placed[3]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[4], circles_placed[3], circles_placed[2]);
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.WriteLine();
            //dc.Calculate(circles_placed[0], circles_placed[1], new StripLine(0, 0, 0, 1));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[0], circles_placed[2], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[1], circles_placed[3], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[2], circles_placed[4], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(circles_placed[3], circles_placed[4], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.WriteLine();
            //dc.Calculate(new StripLine(0, 0, 0, 1), circles_placed[0], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(new StripLine(0, 0, 0, 1), circles_placed[1], new StripLine(0, 100, 1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);
            //dc.Calculate(new StripLine(0, 100, 1, 0), circles_placed[4], new StripLine(0, 0, -1, 0));
            //sw.WriteLine("{0} {1} {2}", dc.R, dc.X, dc.Y);

            //sw.Close();
        }

        private void сделатьШагToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int n = circles_placed.Count;


            int i_a = 0;
            Matrix ATr = A.Tr();
            Matrix AOb = (A * ATr).Ob();
            #region Рассчитать вектор множетелей Лагранжа U.
            Matrix U = AOb * A * G;
            #endregion
            #region Получить вектор направления d (2*n+1 x 1).
            Matrix D = ATr * U - G;
            #endregion

            if (D.IsNull(eps))
            {
                int index = U.RowOfMinElement();
                if (U[index] < -eps1)
                {
                    int i = (int)B[index, 0];
                    int j = (int)B[index, 1];
                    if (i < 0)
                    {
                        wl[4 * j + Math.Abs(i) - 1] = false;
                    }
                    else
                    {
                        wl[4 * n - 1 + i * n - i * (i + 1) / 2 + j - i] = false;
                    }
                    A.DelRow(index);
                    B.DelRow(index);

                }
                else
                {
                    #region Проверить активные ограничения и удалить лишние.
                    double d_max = eps;
                    int k_max = -1;
                    int i_max = -1;
                    int j_max = -1;
                    for (int k = 0; k < A.RowCount - 1; k++)
                    {
                        if (B[k, 0] >= 0)
                        {
                            int i = (int)B[k, 0];
                            int j = (int)B[k, 1];
                            double dist = Math.Pow(circles_placed[i].Pole.X - circles_placed[j].Pole.X, 2) +
                                Math.Pow(circles_placed[i].Pole.Y - circles_placed[j].Pole.Y, 2) -
                                Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2);
                            //if (ExtendedDistance.Calc(circles_placed[i], circles_placed[j]) > eps)
                            if (dist > d_max)
                            {
                                d_max = dist; k_max = k; i_max = i; j_max = j;
                            }

                        }
                    }
                    if (k_max >= 0)
                    {
                        A.DelRow(k_max);
                        B.DelRow(k_max);
                        wl[4 * n - 1 + i_max * n - i_max * (i_max + 1) / 2 + j_max - i_max] = false;
                    }
                    else
                    {
                        Text += "минимум найден";
                        is_exit = true;
                    }
                    #endregion
                }
            }
            else
            {
                #region Находим длину шага L и соответствующее ему ограничение, которое добавляем в A.
                double L = double.PositiveInfinity;

                Matrix T;
                double td;
                double l;
                Matrix TNew = new Matrix(1, 2 * n + 1); // добавляемое ограничение
                A.AddRow();
                B.AddRow();
                for (int i = 0; i < circles_placed.Count; i++)
                {
                    if (!wl[4 * i])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i + 1] = -1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] + strip_size.Y - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -1;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 1])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -2;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 2])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i + 1] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -3;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                    if (!wl[4 * i + 3])
                    {
                        T = new Matrix(1, 2 * n + 1);
                        T[0, 2 * i] = -1;
                        T[0, 2 * n] = 1;
                        td = (T * D)[0];
                        if (td < -eps)
                        {
                            l = -((T * P)[0] - circles_placed[i].Radius) / td;
                            if (L > l)
                            {
                                L = l;
                                TNew = T;
                                B[B.RowCount - 1, 0] = -4;
                                B[B.RowCount - 1, 1] = i;
                            }

                        }
                    }
                }
                i_a = 4 * n - 1;
                for (int i = 0; i < circles_placed.Count - 1; i++)
                    for (int j = i + 1; j < circles_placed.Count; j++)
                    {
                        ++i_a;
                        if (!wl[i_a])
                        {
                            double a = Math.Pow(D[2 * i] - D[2 * j], 2) + Math.Pow(D[2 * i + 1] - D[2 * j + 1], 2);
                            double b = 2 * (D[2 * i] - D[2 * j]) * (P[2 * i] - P[2 * j]) + 2 * (D[2 * i + 1] - D[2 * j + 1]) * (P[2 * i + 1] - P[2 * j + 1]);
                            double c = Math.Pow(P[2 * i] - P[2 * j], 2) + Math.Pow(P[2 * i + 1] - P[2 * j + 1], 2)
                                                - Math.Pow(circles_placed[i].Radius + circles_placed[j].Radius, 2);
                            double d = b * b - 4 * a * c;
                            if (d > eps)
                            {
                                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                                double x = Math.Min(x1, x2);
                                if (x > -eps && x < L)
                                {
                                    L = x;
                                    B[B.RowCount - 1, 0] = i;
                                    B[B.RowCount - 1, 1] = j;
                                }

                            }
                        }
                    }
                #endregion
                //sw.WriteLine("Длина шага {0}", L);
                P += D * L;
                for (int i = 0; i < circles_placed.Count; i++)
                {
                    circles_placed[i].Pole.X = P[2 * i];
                    circles_placed[i].Pole.Y = P[2 * i + 1];
                }
                strip_size.X = P[2 * n];

                #region Добавление в матрицу нового ограничения
                {
                    int i = (int)B[B.RowCount - 1, 0];
                    int j = (int)B[B.RowCount - 1, 1];
                    if (i >= 0)
                    {
                        A[A.RowCount - 1, 2 * i] = 2 * (circles_placed[i].Pole.X - circles_placed[j].Pole.X);
                        A[A.RowCount - 1, 2 * i + 1] = 2 * (circles_placed[i].Pole.Y - circles_placed[j].Pole.Y);
                        A[A.RowCount - 1, 2 * j] = 2 * (circles_placed[j].Pole.X - circles_placed[i].Pole.X);
                        A[A.RowCount - 1, 2 * j + 1] = 2 * (circles_placed[j].Pole.Y - circles_placed[i].Pole.Y);
                        wl[4 * n - 1 + i * n - i * (i + 1) / 2 + j - i] = true;
                    }
                    else
                    {
                        switch (i)
                        {
                            case -1: A[A.RowCount - 1, 2 * j + 1] = -1; break;
                            case -2: A[A.RowCount - 1, 2 * j] = 1; break;
                            case -3: A[A.RowCount - 1, 2 * j + 1] = 1; break;
                            case -4: A[A.RowCount - 1, 2 * j] = -1; A[A.RowCount - 1, 2 * n] = 1; ; break;
                        }
                        wl[4 * j + Math.Abs(i) - 1] = true;

                    }
                }
                #endregion
                ++NumStep;
                Text = "Шаг " + NumStep + " f= " + strip_size.X;
            }

            Invalidate();
        }
        #endregion
        #endregion


        private void FormPlaceCircle_Paint(object sender, PaintEventArgs e)
        {
            // Сглаживание.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Переворот оси Y.
            e.Graphics.ScaleTransform(1, -1);
            e.Graphics.TranslateTransform(0, -ClientSize.Height);

            // Очистка.
            e.Graphics.Clear(System.Drawing.Color.White);

            #region Рисование полубесконечной полосы.
            HatchBrush brush_back = new HatchBrush(HatchStyle.Cross, System.Drawing.Color.Silver, System.Drawing.Color.White);
            e.Graphics.DrawRectangle(System.Drawing.Pens.Black, 0, 0, ClientSize.Width, (float)strip_size.Y);
            e.Graphics.FillRectangle(brush_back, 0, 0, ClientSize.Width, (float)strip_size.Y);
            #endregion

            #region Рисование результата метода последовательного одиночного размещения.
            if (method_index == 1)
            {
                for (int i = 0; i < circles_placed.Count; i++)
                    if (circles_placed[i].Pole.X - circles_placed[i].Radius < ClientSize.Width)
                        e.Graphics.FillAndDraw(System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, circles_placed[i]);
            }
            #endregion

            #region Рисование результата модифицированного метода последовательного одиночного размещения.
            if (method_index == 2 && current_index!=-1 || method_index == 3)
            {
                for (int i = 0; i < triples.Count; i++)
                {
                    e.Graphics.FillAndDraw(System.Drawing.Brushes.Yellow, System.Drawing.Pens.Gray, triples[i].Somes.CircleDelone);

                    Vertex<Geometric> vertex_temp = triples[i];
                    do
                    {
                        if (vertex_temp.DataInVertex is Circle)
                        {
                            Circle circle = vertex_temp.DataInVertex as Circle;
                            if (vertex_temp.Next.DataInVertex is Circle)
                            {
                                Circle circle_next = vertex_temp.Next.DataInVertex as Circle;
                                e.Graphics.DrawLine(System.Drawing.Pens.Red, (float)circle.Pole.X, (float)circle.Pole.Y, (float)circle_next.Pole.X, (float)circle_next.Pole.Y);
                            }
                            e.Graphics.FillAndDraw(System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, circle);
                        }

                        vertex_temp = vertex_temp.Next;
                    } while (vertex_temp != triples[i]);
                }
            }
            #endregion

            #region Рисование результата метода локального поиска.
            if (method_index > 2)
            {
                for (int i = 0; i < circles_placed.Count; i++)
                    if (circles_placed[i].Pole.X - circles_placed[i].Radius < ClientSize.Width)
                        e.Graphics.FillAndDraw(System.Drawing.Brushes.Silver, System.Drawing.Pens.Black, circles_placed[i]);
            }
            #endregion

            #region Рисование последнего размещённого круга.
            if (circles_placed.Count > 0)
                e.Graphics.FillAndDraw(System.Drawing.Brushes.Black, System.Drawing.Pens.Black, circles_placed[circles_placed.Count - 1]);
            #endregion

            #region Рисование неразмещённых кругов.
            double x = 0;
            double y = strip_size.Y;
            for (int i = current_index + 1; i < circles.Count && x < ClientSize.Width; i++)
            {
                e.Graphics.DrawEllipse(System.Drawing.Pens.Black, (float)x, (float)y, 2 * (float)circles[i].Radius, 2 * (float)circles[i].Radius);
                e.Graphics.FillEllipse(System.Drawing.Brushes.Silver, (float)x, (float)y, 2 * (float)circles[i].Radius, 2 * (float)circles[i].Radius);
                x += 2 * circles[i].Radius;
            }
            #endregion

            #region Рисование неразмещаемых кругов.
            // Пока не требовалось.
            #endregion

            #region Вывод текстовой информации.
            Text = string.Format("Размещено: {0} из {1}. Длина: {2}. Метод: {3}. Время работы: {4} m, {5} s, {6} ms.", circles_placed.Count, circles.Count, strip_size.X, method_index, (int)time_span.TotalMinutes, time_span.Seconds, time_span.Milliseconds);
            if (iteration_count > 0)
            {
                Text += string.Format(" Количество итераций: {0}. Время работы: {1} m, {2} s, {3} ms.", iteration_count, (int)time_span_local.TotalMinutes, time_span_local.Seconds, time_span_local.Milliseconds);
                if (is_exit) Text += "оптимум найден";
            }
            #endregion
        }
        private void FormPlaceCircle_Resize(object sender, EventArgs e)
        {
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
                if (vertex.Somes.CircleDelone.Radius != 0)
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
    }
}

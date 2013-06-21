using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.SpecialGeometrics;

namespace Opt.Geometrics.WPFT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Класс содержащий набор многоугольников и рисующих их.
        /// </summary>
        private Polygons polygons;
        /// <summary>
        /// Класс для отображения вспомогательной горизонтальной и вертикальной линий.
        /// </summary>
        private RectangleLines rectangle_lines;

        /// <summary>
        /// Конструктор. Содержит начальные настройки.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            RectangleGrid rg = new RectangleGrid();
            grid.Children.Add(rg);
            rg.SetValue(Grid.ZIndexProperty, -1);

            rectangle_lines = new RectangleLines();
            grid.Children.Add(rectangle_lines);
            rectangle_lines.SetValue(Grid.ZIndexProperty, 100);
            rectangle_lines.InvalidateVisual();

            polygons = new Polygons(new ObservableCollection<PolygonShell>());
            grid.Children.Add(polygons);
            polygons.SetValue(Grid.ZIndexProperty, 0);

            #region Тестовые многоугольники. !!Потом удалить.
            PolygonShell polygon = new PolygonShell();
            polygon.Pole.Copy = new Point2d { X = 150, Y = 50 };
            //polygon.Add(new Point { X = 0, Y = 0 });
            //polygon.Add(new Point { X = 100, Y = 0 });
            //polygon.Add(new Point { X = 100, Y = 100 });
            //polygons.List.Add(polygon);
            //polygon = new PolygonShell();
            //polygon.Pole.Copy = new Point { X = 400, Y = 30 };
            polygon.Add(new Point2d { X = 0, Y = 0 });
            polygon.Add(new Point2d { X = 100, Y = 100 });
            polygon.Add(new Point2d { X = 0, Y = 100 });
            polygons.List.Add(polygon);

            polygon = new PolygonShell();
            polygon.Pole.Copy = new Point2d { X = 400, Y = 80 };
            polygon.Add(new Point2d { X = 0, Y = 0 });
            polygon.Add(new Point2d { X = 100, Y = 0 });
            polygon.Add(new Point2d { X = 100, Y = 100 });
            polygons.List.Add(polygon);
            #endregion

            list_view.ItemsSource = polygons.List;
        }

        /// <summary>
        /// Запускается при созданиии многоугольника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPolygon_Click(object sender, RoutedEventArgs e)
        {
            PolygonShell polygon = new PolygonShell();
            polygons.List.Add(polygon);
        }

        /// <summary>
        /// Запускаеться при добавлении точки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPoint_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            // В свойстве Tag записана информациия о многоугольнике, в который добавляеться точка.
            PolygonShell polygon = (PolygonShell)button.Tag;
            polygon.Add(new Point2d());

            double length = 0;
            for (int i = 0; i < polygons.List.Count; i++)
            {
                double max_x = double.NegativeInfinity;
                for (int j = 0; j < polygons.List[i].Count; j++)
                    if (max_x < polygons.List[i][j].X)
                        max_x = polygons.List[i][j].X;
                if (length < polygons.List[i].Pole.X + max_x)
                    length = polygons.List[i].Pole.X + max_x;
            }
            strip_length.Text = length.ToString();
        }

        /// <summary>
        /// Удаляються все многоугольники из класса Polygons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            polygons.List.Clear();

            strip_length.Text = "0";
        }

        /// <summary>
        /// Загрузка данных из файла "data.xml" (десериализация).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
                        System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlTextReader xmlr = new XmlTextReader(openFileDialog1.FileName);
                NetDataContractSerializer dcs = new NetDataContractSerializer();
                polygons.List = (ObservableCollection<PolygonShell>)dcs.ReadObject(xmlr);
                xmlr.Close();

                list_view.ItemsSource = null;
                list_view.ItemsSource = polygons.List;

                double length = 0;
                for (int i = 0; i < polygons.List.Count; i++)
                {
                    double max_x = double.NegativeInfinity;
                    for (int j = 0; j < polygons.List[i].Count; j++)
                        if (max_x < polygons.List[i][j].X)
                            max_x = polygons.List[i][j].X;
                    if (length < polygons.List[i].Pole.X + max_x)
                        length = polygons.List[i].Pole.X + max_x;
                }
                strip_length.Text = length.ToString();
            }
        }

        /// <summary>
        /// Сохранение данных в файл "data.xml" (сериализация).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlTextWriter xmlw = new XmlTextWriter(saveFileDialog1.FileName, System.Text.Encoding.UTF8);
                xmlw.Formatting = Formatting.Indented;
                NetDataContractSerializer dcs = new NetDataContractSerializer();
                dcs.WriteObject(xmlw, polygons.List);
                xmlw.Close();
            }
        }

        /// <summary>
        /// Закрытие программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Кнопка запускающая перерисовку всех многоугольников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            polygons.InvalidateVisual();
        }


        #region Операции с векторами.
        private double[] Copy(double[] vector_this)
        {
            double[] res = new double[vector_this.Length];
            for (int i = 0; i < vector_this.Length; i++)
                res[i] = vector_this[i];
            return res;
        }
        private double[] Summation(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] += vector[i];
            return vector_this;
        }
        private double[] Multiplication(double[] vector_this, double value)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] *= value;
            return vector_this;
        }
        private double[] Multiplication(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] *= vector[i];
            return vector_this;
        }
        private double MultiplicationValue(double[] vector_this, double[] vector)
        {
            double res = 0;
            for (int i = 0; i < vector_this.Length; i++)
                res += vector_this[i] * vector[i];
            return res;
        }
        #endregion

        /// <summary>
        /// Расчёт значения линейной функции F в точке X.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Значение линейной функции F в точке X.</returns>
        private double Value(double[] F, double[] X)
        {
            double res = F[F.Length - 1];
            for (int i = 0; i < X.Length; i++)
                res += F[i] * X[i];
            return res;
        }
        /// <summary>
        /// Расчёт значения дополнительной части функции, используемой в методе барьеров.
        /// </summary>
        /// <param name="G">Набор линейных ограничений, заданных массивами коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Значение дополнительной части функции, используемой в методе барьеров.</returns>
        private double Value(List<double[]> G, double[] X, double mu)
        {
            double res = 0;
            for (int i = 0; i < G.Count; i++)
                res += 1 / Value(G[i], X);
            return -mu * res;
        }
        /// <summary>
        /// Вычисление значение функции, используемой в методе барьеров.
        /// </summary>
        /// <param name="F">Основная функция.</param>
        /// <param name="G">Список ограничений.</param>
        /// <param name="X">Точка, в которой необходимо определить значение функции.</param>
        /// <param name="mu">Дополнительный параметр, используемый при создании барьерной функции.</param>
        /// <returns></returns>
        private double Value(double[] F, List<double[]> G, double[] X, double mu)
        {
            double res = Value(F, X) + Value(G, X, mu);
            return res;
        }
        /// <summary>
        /// Градиент линейной функции.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <returns>Градиент линейной функции, заданный массивом данных.</returns>
        private double[] Grad(double[] F)
        {
            double[] res = new double[F.Length - 1];
            for (int i = 0; i < res.Length; i++)
                res[i] = F[i];
            return res;
        }
        /// <summary>
        /// Расчёт вектора градиента для полной барьерной функции F с ограничениями G в точке X.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="G">Набор линейных ограничений, заданных массивами коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Вектора градиента для полной барьерной функции F с ограничениями G в точке X.</returns>
        private double[] Grad(double[] F, List<double[]> G, double[] X, double mu)
        {
            double[] res = Grad(F);
            for (int i = 0; i < G.Count; i++)
                Summation(res, Multiplication(Grad(G[i]), mu / Math.Pow(Value(G[i], X), 2)));
            return res;
        }

        /// <summary>
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="G">Набор линейных ограничений, заданных массивами коэффициентов (в виде G(x)<=0)</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <param name="mu"></param>
        /// <param name="beta"></param>
        /// <param name="eps">Погрешность.</param>
        public void Calculate(double[] F, List<double[]> G, ref double[] X, double mu, double beta, double eps)
        {
            #region Шаг 1. Пока значение дополнительной части функции больше погрешности...
            while (Value(G, X, mu) > eps)
            {
                #region Шаг 1.1. Пока происходит движение в сторону антиградиента...
                bool is_exit;
                do
                {
                    #region Шаг 1.1.1. Находим вектор направления - многомерный безусловный поиск (метод найскорейшего спуска, градиентный метод).
                    #region Шаг 1.1.1.1. Находим вектор антиградиента.
                    double[] d = Multiplication(Grad(F, G, X, mu), -1);
                    #endregion
                    #region Шаг 1.1.1.2. Нормируем вектор направления.
                    Multiplication(d, 1 / Math.Sqrt(MultiplicationValue(d, d)));
                    #endregion
                    #region Шаг 1.1.1.3. Находим новую точку решения задачи (находим длину шага - одномерный поиск). !!Проверить!!
                    double a1 = X[X.Length - 1];
                    is_exit = true;
                    do
                    {                        
                        double length = a1 / 2;
                        double[] XX = Summation(Copy(X), Multiplication(Copy(d), length));
                        bool is_opt = true;
                        for (int i = 0; i < G.Count && is_opt; i++)
                            is_opt = Value(G[i], XX) < 0;
                        if (is_opt && Value(F, G, XX, mu) < Value(F, G, X, mu))
                        {
                            X = XX;                            
                            a1 = a1 - length;
                            is_exit = false;


                            //for (int i = 0; i < polygons.list.Count; i++)
                            //{
                            //    polygons.list[i].Pole.X = X[2 * i];
                            //    polygons.list[i].Pole.Y = X[2 * i + 1];
                            //}
                            //this.polygons.InvalidateVisual();
                            //MessageBox.Show("Продолжить?");
                        }
                        else
                            a1 = length;
                    } while (a1 > eps);
                    #endregion
                    #endregion
                } while (!is_exit);
                #endregion

                #region Шаг 1.2. Изменяем коэффициент mu для поиска более точного решения.
                mu *= beta;
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения многоугольников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Barriers_Click(object sender, RoutedEventArgs e)
        {
            List<Polygon2d> polygons = new List<Polygon2d>(this.polygons.List);

            #region Шаг 1. Создаём начальную точку.
            #region Шаг 1.1. Заполняем координаты полюсов.
            double[] X = new double[2 * polygons.Count + 1];
            for (int i = 0; i < polygons.Count; i++)
            {
                X[2 * i] = polygons[i].Pole.X;
                X[2 * i + 1] = polygons[i].Pole.Y;
            }
            #endregion
            #region Шаг 1.2. Заполняем длину занятой части полосы.
            X[2 * polygons.Count] = 0;
            for (int i = 0; i < polygons.Count; i++)
            {
                double max_x = double.NegativeInfinity;
                for (int j = 0; j < polygons[i].Count; j++)
                    if (max_x < polygons[i][j].X)
                        max_x = polygons[i][j].X;
                if (X[2 * polygons.Count] < polygons[i].Pole.X + max_x)
                    X[2 * polygons.Count] = polygons[i].Pole.X + max_x;
            }
            X[2 * polygons.Count] *= 2;
            #endregion
            #endregion

            #region Шаг 2. Создаём функцию цели.
            double[] F = new double[2 * polygons.Count + 2];
            F[2 * polygons.Count] = 1;
            #endregion

            #region Шаг 3. Создаём набор ограничений.
            List<double[]> G = new List<double[]>();
            #region Шаг 3.1. Для каждого многоугольника...
            for (int i = 0; i < polygons.Count; i++)
            {
                #region Шаг 3.1.1. Находим минимальное и максимальное значение прямоугольной оболочки для многоугольника.
                Point min = new Point { X = double.PositiveInfinity, Y = double.PositiveInfinity };
                Point max = new Point { X = double.NegativeInfinity, Y = double.NegativeInfinity };
                for (int j = 0; j < polygons[i].Count; j++)
                {
                    if (min.X > polygons[i][j].X)
                        min.X = polygons[i][j].X;
                    if (max.X < polygons[i][j].X)
                        max.X = polygons[i][j].X;
                    if (min.Y > polygons[i][j].Y)
                        min.Y = polygons[i][j].Y;
                    if (max.Y < polygons[i][j].Y)
                        max.Y = polygons[i][j].Y;
                }
                #endregion
                #region Шаг 3.1.2. Создаём набор ограничений по полосе. Проверить!!
                double[] g;
                #region Шаг 3.1.2.1. Ограничение по нижней границе. // Y-min.Y>=0  -->  -Y+min.Y<=0
                g = new double[2 * polygons.Count + 2];
                g[2 * i + 1] = -1;
                g[2 * polygons.Count + 1] = min.Y;
                G.Add(g);
                #endregion
                #region Шаг 3.1.2.2. Ограничение по левой границе. // X-min.X>=0  -->  -X+min.X<=0
                g = new double[2 * polygons.Count + 2];
                g[2 * i] = -1;
                g[2 * polygons.Count + 1] = min.X;
                G.Add(g);
                #endregion
                #region Шаг 3.1.2.3. Ограничение по верхней границе. // Y+max.Y<=H  -->  Y+max.Y-H<=0
                g = new double[2 * polygons.Count + 2];
                g[2 * i + 1] = 1;
                g[2 * polygons.Count + 1] = max.Y - strip.Height;
                G.Add(g);
                #endregion
                #region Шаг 3.1.2.4. Ограничение по правой границе. // X+max.X<=Z  -->  X-Z+max.X<=0
                g = new double[2 * polygons.Count + 2];
                g[2 * i] = 1;
                g[2 * polygons.Count] = -1;
                g[2 * polygons.Count + 1] = max.X;
                G.Add(g);
                #endregion
                #endregion
            }
            #endregion
            #region Шаг 3.2. Для каждой пары многоугольников...
            for (int i = 0; i < polygons.Count - 1; i++)
                for (int j = i + 1; j < polygons.Count; j++)
                {
                    #region Шаг 3.2.1. Создаём и находим разделяющую.
                    PlaneDividing pd = new PlaneDividing(new Polygon2d.Iterator(i, polygons[i], 0), new Polygon2d.Iterator(j, polygons[j], 0));
                    pd.Find();
                    #endregion
                    #region Шаг 3.2.2. Создаём ограничение. !!Проверить!!
                    Vector2d vector = pd.IteratorPlane.Point(1) - pd.IteratorPlane.Point(0);
                    Vector2d normal = new Vector2d { X = -vector.Y, Y = vector.X };
                    double length = Math.Sqrt(normal * normal);
                    normal.X /= length; normal.Y /= length;
                    double[] g = new double[2 * polygons.Count + 2];
                    g[2 * pd.IteratorPoint.IndexPolygon] = normal.X;
                    g[2 * pd.IteratorPoint.IndexPolygon + 1] = normal.Y;
                    g[2 * pd.IteratorPlane.IndexPolygon] = -normal.X;
                    g[2 * pd.IteratorPlane.IndexPolygon + 1] = -normal.Y;
                    g[2 * polygons.Count + 1] = ((pd.IteratorPoint.Polygon[pd.IteratorPoint.Index] - pd.IteratorPlane.Polygon[pd.IteratorPlane.Index]) * normal);
                    G.Add(g);
                    #endregion
                }
            #endregion
            #endregion

            #region Шаг 6. Выполняем поиск локального минимума с заданными ограничениями.
            double mu = 1000; // Должно вводиться с формы!
            double beta = 0.5; // Должно вводиться с формы!
            double eps = 1e-3; // Должно вводиться с формы!
            Calculate(F, G, ref X, mu, beta, eps);
            #endregion

            #region Шаг 7. Преобразуем результат метода барьеров в результат задачи размещения и возвращаем длину занятой части полосы.
            for (int i = 0; i < polygons.Count; i++)
            {
                polygons[i].Pole.X = X[2 * i];
                polygons[i].Pole.Y = X[2 * i + 1];
            }
            strip_length.Text = X[X.Length - 1].ToString();
            #endregion

            this.polygons.InvalidateVisual();
        }
    }

    /// <summary>
    /// Класс, содержащий расширенную информацию о многоугольнике.
    /// </summary>
    [Serializable, DataContract]
    public class PolygonShell : Polygon2d, IEnumerable, INotifyCollectionChanged
    {
        /// <summary>
        /// Переопределение метода добавления точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        public new void Add(Point2d point)
        {
            base.Add(point);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, point));
        }

        #region Реализация IEnumerator.
        /// <summary>
        /// Метод, который превращает многоугольник в коллекцию точек.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return base.list_elements.GetEnumerator();
        }
        #endregion

        #region Реализация INotifyCollectionChanged.
        /// <summary>
        /// Событие возникающее при изменении коллекции.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        /// <summary>
        /// Методвызываемый приизменении коллекции.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        #endregion
    }

    public class PolygonShellUI : UIElement
    {
        public Pen pen { get; set; }
        public Brush brush { get; set; }
        public PolygonShell PolygonShell { get; set; }

        public PolygonShellUI()
        {
            pen = new Pen(Brushes.White, 0.1);
            pen.Freeze();
            brush = Brushes.Pink;
            brush.Freeze();
        }

        /// <summary>
        /// Метод перерисовки многоугольника.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (PolygonShell.Count > 1)
            {
                System.Windows.Point start = new System.Windows.Point(PolygonShell[0].X + PolygonShell.Pole.X, PolygonShell[0].Y + PolygonShell.Pole.Y);
                LineSegment[] segments = new LineSegment[PolygonShell.Count - 1];
                for (int j = 1; j < PolygonShell.Count; j++)
                    segments[j - 1] = new LineSegment(new System.Windows.Point(PolygonShell[j].X + PolygonShell.Pole.X, PolygonShell[j].Y + PolygonShell.Pole.Y), true);

                PathFigure figure = new PathFigure(start, segments, true);
                PathGeometry geo = new PathGeometry(new PathFigure[] { figure });
                drawingContext.DrawGeometry(brush, pen, geo);
            }
        }
    }

    /// <summary>
    /// Класс, который отображает координатную сетку.
    /// </summary>
    public class RectangleGrid : UIElement
    {
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public double Size { get; set; }

        public RectangleGrid()
        {
            Pen = new Pen(Brushes.White, 0.1);
            Pen.Freeze();
            Brush = Brushes.Black;
            Brush.Freeze();

            Size = 10;
        }        

        /// <summary>
        /// Метод перерисовки координатной сетки.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect rect = new Rect(RenderSize);

            drawingContext.DrawRectangle(Brush, Pen, rect);
            for (double x = 0; x < rect.Width; x += Size)
                drawingContext.DrawLine(Pen, new System.Windows.Point(x, 0), new System.Windows.Point(x, rect.Height));
            for (double y = 0; y < rect.Height; y += Size)
                drawingContext.DrawLine(Pen, new System.Windows.Point(0, y), new System.Windows.Point(rect.Width, y));
        }
    }

    /// <summary>
    /// Класс, который отображает вспомоательные горизонтальную и вертикальную линии координат.
    /// </summary>
    public class RectangleLines : UIElement
    {
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public Brush Foregrond { get; set; }

        public System.Windows.Point Point { get; set; }

        public RectangleLines()
        {
            Pen = new Pen(Brushes.White, 2);
            Pen.Freeze();
            Brush = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
            Brush.Freeze();
            Foregrond = Brushes.White;
            Foregrond.Freeze();
            MouseMove += new MouseEventHandler(RectangleLines_MouseMove);

            Point = new System.Windows.Point();
        }

        public void RectangleLines_MouseMove(object sender, MouseEventArgs e)
        {
            Point = e.GetPosition(this);
            this.InvalidateVisual();
        }

        /// <summary>
        /// Метод перерисовки вспомогательных линий координат.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect rect = new Rect(RenderSize);

            drawingContext.DrawRectangle(Brush, Pen, rect);
            drawingContext.DrawLine(Pen, new System.Windows.Point(Point.X, 0), new System.Windows.Point(Point.X, rect.Height));
            drawingContext.DrawLine(Pen, new System.Windows.Point(0, Point.Y), new System.Windows.Point(rect.Width, Point.Y));
            drawingContext.DrawText(new FormattedText(Point.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Colibri"), 12, Foregrond), Point);
        }
    }

    /// <summary>
    /// Класс, который отображает (рисует) все многоугольники на экране.
    /// </summary>
    [Serializable]
    public class Polygons : UIElement
    {
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public ObservableCollection<PolygonShell> list;
        public ObservableCollection<PolygonShell> List 
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        public Polygons(ObservableCollection<PolygonShell> _polygon_list)
        {
            Pen = new Pen(Brushes.White, 0.1);
            Pen.Freeze();
            Brush = Brushes.Pink;
            Brush.Freeze();

            List = _polygon_list;
        }

        /// <summary>
        /// Метод перерисовки всех многоугольников.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Count > 1)
                {
                    System.Windows.Point start = new System.Windows.Point(List[i][0].X + List[i].Pole.X, List[i][0].Y + List[i].Pole.Y);
                    LineSegment[] segments = new LineSegment[List[i].Count - 1];
                    for (int j = 1; j < List[i].Count; j++)
                        segments[j - 1] = new LineSegment(new System.Windows.Point(List[i][j].X + List[i].Pole.X, List[i][j].Y + List[i].Pole.Y), true);

                    PathFigure figure = new PathFigure(start, segments, true);
                    PathGeometry geo = new PathGeometry(new PathFigure[] { figure });
                    drawingContext.DrawGeometry(Brush, Pen, geo);
                }
            }
        }
    }
}

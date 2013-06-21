using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;
using Rectangle = Opt.Geometrics.Geometrics2d.Geometric2dWithPointVector;

namespace PlacingRectangle
{
    public class Placement
    {
        private Task task;

        private Vector2d region_size;
        public Vector2d RegionSize
        {
            get
            {
                return region_size;
            }
        }

        private List<Vector2d> objects_sizes;
        private List<int> sort;
        private List<int> objects_busy_numbers;
        private List<int> objects_free_numbers;
        private List<Point2d> objects_busy_points;
        public BindingSource Objects_BindingSource()
        {
            BindingSource bs = new BindingSource();
            for (int i = 0; i < sort.Count; i++)
                bs.Add(objects_sizes[sort[i]]);
            return bs;
        }
        public BindingSource ObjectsBusy_BindingSource()
        {
            BindingSource bs = new BindingSource();
            for (int i = 0; i < objects_busy_numbers.Count; i++)
                bs.Add(new Rectangle { Pole = objects_busy_points[i].Copy, Vector = objects_sizes[objects_busy_numbers[i]].Copy });
            return bs;
        }
        public BindingSource ObjectsFree_BindingSource()
        {
            BindingSource bs = new BindingSource();
            for (int i = 0; i < objects_free_numbers.Count; i++)
                bs.Add(objects_sizes[objects_free_numbers[i]]);
            return bs;
        }
        private double objects_busy_square;

        private double object_function;
        public double ObjectFunction
        {
            get
            {
                return object_function;
            }
        }
        private void ObjectFunctionCalculate()
        {
            switch (task.TaskIndex)
            {
                case Task.TaskEnum.RectangleHall:
                    object_function = region_size.X * region_size.Y;
                    break;
                case Task.TaskEnum.Strip:
                    object_function = region_size.X;
                    break;
                case Task.TaskEnum.RectangleRegion:
                    object_function = region_size.X * region_size.Y - objects_busy_square;
                    break;
            }
        }

        protected Placement()
        {
            region_size = new Vector2d();
        }
        public static Placement Create(Task task, List<Vector2d> objects_sizes)
        {
            if (task != null)
            {
                Placement placement = new Placement();
                placement.task = task;
                if (double.IsPositiveInfinity(task.RegionWidth))
                    placement.region_size.X = 0;
                else
                    placement.region_size.X = task.RegionWidth;
                if (double.IsPositiveInfinity(task.RegionHeight))
                    placement.region_size.Y = 0;
                else
                    placement.region_size.Y = task.RegionHeight;

                placement.objects_sizes = objects_sizes;
                placement.sort = new List<int>();
                placement.objects_busy_numbers = new List<int>();
                placement.objects_free_numbers = new List<int>();
                placement.objects_busy_points = new List<Point2d>();
                placement.objects_busy_square = 0;

                placement.object_function = double.NaN;

                return placement;
            }
            else
                return null;
        }

        public void SortCreateNext()
        {
        }
        public void SortCreateRandom()
        {
            Random rand = new Random();
            for (int i = 0; i < task.ObjectsCount; i++)
                sort.Add(i);
            int n = rand.Next(task.ObjectsCount);
            for (int i = 0; i < n; i++)
            {
                int a = rand.Next(task.ObjectsCount - 1);
                int b = rand.Next(task.ObjectsCount - 1);
                int c = sort[a];
                sort[a] = sort[b];
                sort[b] = c;
            }
        }

        private bool CheckIntersectWithRegion(Point2d point, Vector2d size)
        {
            double eps = 1e-4f;
            return point.X + size.X <= task.RegionWidth + eps && point.Y + size.Y <= task.RegionHeight + eps;
        }
        private bool CheckIntersectWithRectangles(Point2d point, Vector2d size)
        {
            bool without_intersect = true;
            for (int i = 0; i < objects_busy_numbers.Count && without_intersect; i++)
                without_intersect = without_intersect && CheckIntersectBetweenRectangles(point, size, objects_busy_points[i], objects_sizes[objects_busy_numbers[i]]);
            return without_intersect;
        }
        private bool CheckIntersectBetweenRectangles(Point2d point_i, Vector2d size_i, Point2d point_j, Vector2d size_j)
        {
            double eps = 1e-4f;
            return
                point_i.X + size_i.X <= point_j.X + eps ||
                point_i.Y + size_i.Y <= point_j.Y + eps ||
                point_j.X + size_j.X <= point_i.X + eps ||
                point_j.Y + size_j.Y <= point_i.Y + eps;
        }
        public void Calculate()
        {
            #region Создание списка точек размещения и добавление первой точки.
            List<Point2d> point_of_placement = new List<Point2d>();
            point_of_placement.Add(new Point2d());
            #endregion

            objects_busy_square = 0;

            #region Для каждого объекта размещения...
            for (int i = 0; i < sort.Count; i++)
            {
                Vector2d size = objects_sizes[sort[i]];
                #region Определение начального значения точки размещения текущего объекта размещения и соответствующих ей занятой части области размещения и её площади (используется для определения лучшей точки размещения).
                Point2d point = new Point2d { X = double.PositiveInfinity, Y = double.PositiveInfinity };
                double width = Math.Max(region_size.X, point.X + size.X);
                double height = Math.Max(region_size.Y, point.Y + size.Y);
                double object_function = width * height;
                #endregion

                #region Для каждой точки размещения...
                for (int j = 0; j < point_of_placement.Count; j++)
                {
                    Point2d point_temp = point_of_placement[j];
                    #region Проверка попадания текущего объекта размещения в текущей точке размещения в область размещения.
                    if (CheckIntersectWithRegion(point_temp, size))
                        #region Проверка непересечения текущего объекта размещения в текущей точке размещения со всеми размещёнными объектами.
                        if (CheckIntersectWithRectangles(point_temp, size))
                        {
                            #region Определение занятой части области размещения и её площади при размещении текущего объекта размещения в текущей точке размещения.
                            double width_temp = Math.Max(region_size.X, point_temp.X + size.X);
                            double height_temp = Math.Max(region_size.Y, point_temp.Y + size.Y);
                            double object_function_temp = width_temp * height_temp;
                            #endregion
                            #region Определение того, является ли текущая точка размещения лучше сохранённой (оптимальной).
                            bool is_change =
                                object_function > object_function_temp ||
                                object_function == object_function_temp && point.X > point_temp.X ||
                                object_function == object_function_temp && point.X == point_temp.X && point.Y >= point_temp.Y;
                            #endregion
                            #region Сохранение лучшей точки размещения и значения функции цели.
                            if (is_change)
                            {
                                point = point_temp;
                                object_function = object_function_temp;
                            }
                            #endregion
                        }
                        #endregion
                    #endregion
                }
                #endregion

                #region Проверка существования точки размещения для текущего объекта размещения.
                if (!double.IsPositiveInfinity(point.X) && !double.IsPositiveInfinity(point.Y))
                {
                    #region Сохранение точки размещения текущего объетка.
                    objects_busy_points.Add(point);
                    #endregion
                    #region Добавление номера объекта в список размещённых объектов.
                    objects_busy_numbers.Add(sort[i]);
                    #endregion
                    #region Определение площади размещённых объектов (используется для расчёта функции цели при размещении в прямоугольнике).
                    objects_busy_square += size.X * size.Y;
                    #endregion

                    #region Сохранение предыдущего значения функции цели.
                    double object_function_old = region_size.X * region_size.Y;
                    #endregion

                    #region Определение размеров новой занятой части области размещения.
                    region_size.X = Math.Max(region_size.X, point.X + size.X);
                    region_size.Y = Math.Max(region_size.Y, point.Y + size.Y);
                    #endregion

                    #region Оперделение и добавление новых точек размещения.
                    point_of_placement.Add(new Point2d { X = 0, Y = point.Y + size.Y });
                    point_of_placement.Add(new Point2d { X = point.X + size.X, Y = 0 });
                    for (int j = 0; j < objects_busy_numbers.Count - 1; j++)
                    {
                        point_of_placement.Add(new Point2d { X = point.X + size.X, Y = objects_busy_points[j].Y + objects_sizes[sort[j]].Y });
                        point_of_placement.Add(new Point2d { X = objects_busy_points[j].X + objects_sizes[sort[j]].X, Y = point.Y + size.Y });
                    }
                    #endregion
                }
                else
                    #region Добавление номера объекта в список неразмещённых объектов.
                    objects_free_numbers.Add(sort[i]);
                    #endregion
                #endregion
            }
            #endregion

            ObjectFunctionCalculate();
        }

        public void Draw(System.Drawing.Graphics graphics, int Width, int Height, System.Drawing.Pen pen_region, System.Drawing.Brush brush_region, System.Drawing.Pen pen_objects, System.Drawing.Brush brush_objects, System.Drawing.Brush brush_text)
        {
            System.Drawing.Font my_font = new System.Drawing.Font("Arial", 6);
            graphics.FillRectangle(brush_region, 0, 0, (float)Math.Min(Width, region_size.X), (float)Math.Min(Height, region_size.Y));
            graphics.DrawRectangle(pen_region, 0, 0, (float)Math.Min(Width, task.RegionWidth), (float)Math.Min(Height, task.RegionHeight));
            for (int i = 0; i < objects_busy_numbers.Count; i++)
            {
                Rectangle rect = new Rectangle { Pole = objects_busy_points[i].Copy, Vector = objects_sizes[objects_busy_numbers[i]].Copy };
                graphics.FillRectangle(brush_objects, (float)rect.Pole.X, (float)rect.Pole.Y, (float)(rect.Pole.X + rect.Vector.X), (float)(rect.Pole.X + rect.Vector.Y));
                graphics.DrawRectangle(pen_objects, (float)rect.Pole.X, (float)rect.Pole.Y, (float)(rect.Pole.X + rect.Vector.X), (float)(rect.Pole.X + rect.Vector.Y));
                graphics.DrawString(i.ToString(), my_font, brush_text, new System.Drawing.RectangleF((float)rect.Pole.X, (float)rect.Pole.Y, (float)(rect.Vector.X), (float)(rect.Vector.Y)));
            }
        }
        public void ReadLine(StreamReader sr)
        {
            string[] s;

            sr.ReadLine(); // Занятая часть области.
            region_size.X = double.Parse(sr.ReadLine());
            region_size.Y = double.Parse(sr.ReadLine());

            sr.ReadLine(); // Сортировка.
            s = sr.ReadLine().Split(' ');
            sort = new List<int>();
            for (int i = 0; i < s.Length; i++)
                sort.Add(int.Parse(s[i]));

            sr.ReadLine(); // Номера размещённых объектов.
            s = sr.ReadLine().Split(' ');
            objects_busy_numbers = new List<int>();
            for (int i = 0; i < s.Length; i++)
                objects_busy_numbers.Add(int.Parse(s[i]));

            sr.ReadLine(); // Номера неразмещённых объектов.
            s = sr.ReadLine().Split(' ');
            objects_free_numbers = new List<int>();
            if (!(s.Length == 1 && s[0] == ""))
                for (int i = 0; i < s.Length; i++)
                    objects_free_numbers.Add(int.Parse(s[i]));

            sr.ReadLine(); // Точки размещения размещённых объектов.
            s = sr.ReadLine().Split(' ');
            objects_busy_points = new List<Point2d>();
            for (int i = 0; i < s.Length; i += 2)
                objects_busy_points.Add(new Point2d { X = double.Parse(s[i]), Y = double.Parse(s[i + 1]) });

            objects_busy_square = 0;
            for (int i = 0; i < objects_busy_numbers.Count; i++)
                objects_busy_square += objects_sizes[objects_busy_numbers[i]].X * objects_sizes[objects_busy_numbers[i]].Y;
            ObjectFunctionCalculate();
        }
        public void WriteLine(StreamWriter sw)
        {
            sw.WriteLine("Занятая часть области:");
            sw.WriteLine(region_size.X);
            sw.WriteLine(region_size.Y);

            sw.WriteLine("Сортировка:");
            for (int i = 0; i < sort.Count; i++)
                if (i == sort.Count - 1)
                    sw.Write("{0}", sort[i]);
                else
                    sw.Write("{0} ", sort[i]);
            sw.WriteLine();

            sw.WriteLine("Номера размещённых объектов:");
            for (int i = 0; i < objects_busy_numbers.Count; i++)
                if (i == sort.Count - 1)
                    sw.Write("{0}", objects_busy_numbers[i]);
                else
                    sw.Write("{0} ", objects_busy_numbers[i]);
            sw.WriteLine();

            sw.WriteLine("Номера неразмещённых объектов:");
            for (int i = 0; i < objects_free_numbers.Count; i++)
                if (i == sort.Count - 1)
                    sw.Write("{0}", objects_free_numbers[i]);
                else
                    sw.Write("{0} ", objects_free_numbers[i]);
            sw.WriteLine();

            sw.WriteLine("Точки размещения размещённых объектов:");
            for (int i = 0; i < objects_busy_points.Count; i++)
                if (i == objects_sizes.Count - 1)
                    sw.Write("{0} {1}", objects_busy_points[i].X, objects_busy_points[i].Y);
                else
                    sw.Write("{0} {1} ", objects_busy_points[i].X, objects_busy_points[i].Y);
            sw.WriteLine();

            sw.WriteLine("Значение функции цели:");
            sw.WriteLine(ObjectFunction);
        }
    }
}

using System;
using System.Collections.Generic;
using Opt.ClosenessModel;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms.Метод_последовательного_одиночного_размещения
{
    public class Placing : Opt.Algorithms.Placing
    {
        public Placing(double height, Circle[] circles, double eps)
            : base(height, 0, circles, eps)
        {
        }

        /// <summary>
        /// Проверка на попадание круга в полосу.
        /// </summary>
        /// <param name="point">Вектор размещения круга.</param>
        /// <param name="circle">Круг.</param>
        /// <param name="height">Высота полосы.</param>
        /// <returns>Возвращает True, если круг полностью лежит внутри полосы. False - в противном случае.</returns>
        protected bool IsCheckedStrip(Point2d point, Circle circle, double height)
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
        protected static bool IsCheckedCircles(Circle circle, Circle[] circles, int length, double eps)
        {
            for (int i = 0; i < length; i++)
                if (CircleExt.Расширенное_расстояние(circle, circles[i]) < -eps) // !!! Необходимо учитывать погрешность?
                    return false;
            return true; ;
        }

        /// <summary>
        /// Поиск локального минимума с использованием метода последовательного одиночного размещения.
        /// </summary>
        protected override void Calculate()
        {
            #region Шаг 1. Метод последовательного одиночного размещения. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                #region Шаг 1.1. Создание списка точек возможных размещений и добавление двух начальных точек.
                List<Point2d> points = new List<Point2d>();
                points.Add(new Point2d { X = circles[i].Value, Y = circles[i].Value });
                points.Add(new Point2d { X = circles[i].Value, Y = height - circles[i].Value });
                #endregion
                #region Шаг 1.2. Создание и заполнение списка годографов.
                Circle[] godographs = new Circle[i];
                for (int j = 0; j < i; j++)
                    godographs[j] = CircleExt.Годограф_функции_плотного_размещения(circles[j], circles[i]);
                #endregion
                #region Шаг 1.3. Поиск точек пересечения круга с полосой.
                for (int j = 0; j < godographs.Length; j++)
                {
                    #region Шаг 1.3.1. Поиск точек пересечения круга с левой границей полосы.
                    if (godographs[j].Pole.X - godographs[j].Value < circles[i].Value)
                    {
                        double x = circles[i].Value - godographs[j].Pole.X;
                        double y = Math.Sqrt(godographs[j].Value * godographs[j].Value - x * x);
                        Point2d point;

                        point = new Point2d { X = circles[i].Value, Y = godographs[j].Pole.Y - y };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);

                        point = new Point2d { X = circles[i].Value, Y = godographs[j].Pole.Y + y };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);
                    }
                    #endregion
                    #region Шаг 1.3.2. Поиск точек пересечения круга с нижней границей полосы.
                    if (godographs[j].Pole.Y - godographs[j].Value < circles[i].Value)
                    {
                        double y = circles[i].Value - godographs[j].Pole.Y;
                        double x = Math.Sqrt(godographs[j].Value * godographs[j].Value - y * y);
                        Point2d point;

                        point = new Point2d { X = godographs[j].Pole.X - x, Y = circles[i].Value };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);

                        point = new Point2d { X = godographs[j].Pole.X + x, Y = circles[i].Value };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);
                    }
                    #endregion
                    #region Шаг 1.3.3. Поиск точек пересечения круга с верхней границей полосы.
                    if (godographs[j].Pole.Y + godographs[j].Value > height - circles[i].Value)
                    {
                        double y = height - circles[i].Value - godographs[j].Pole.Y;
                        double x = Math.Sqrt(godographs[j].Value * godographs[j].Value - y * y);
                        Point2d point;

                        point = new Point2d { X = godographs[j].Pole.X - x, Y = height - circles[i].Value };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);
                        point = new Point2d { X = godographs[j].Pole.X + x, Y = height - circles[i].Value };
                        if (IsCheckedStrip(point, circles[i], height))
                            points.Add(point);
                    }
                    #endregion
                }
                #endregion
                #region Шаг 1.4. Поиск точек пересечения годографов.
                for (int j = 0; j < godographs.Length - 1; j++)
                    for (int k = j + 1; k < godographs.Length; k++)
                    {
                        Point2d point;

                        point = CircleExt.Точка_пересечения_границ(godographs[j], godographs[k]);
                        if (point != null && IsCheckedStrip(point, circles[i], height))
                            points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.

                        point = CircleExt.Точка_пересечения_границ(godographs[k], godographs[j]);
                        if (point != null && IsCheckedStrip(point, circles[i], height))
                            points.Add(point); // Заменить на "Добавить в отсортированный набор данных". Лучше всего использовать бинарное взвешенное дерево.
                    }
                #endregion
                #region Шаг 1.5. Сортировка набора точек возможного размещения.!!! Данная часть не нужна, если использовать сортировку при вставке точек в набор данных.
                for (int j = 0; j < points.Count - 1; j++)
                    for (int k = j + 1; k < points.Count; k++)
                        if (points[j].X > points[k].X || (points[j].X == points[k].X && points[j].Y > points[k].Y))
                        {
                            Point2d temp_point = points[j];
                            points[j] = points[k];
                            points[k] = temp_point;
                        }
                #endregion
                #region Шаг 1.6. Выбор наилучшей точки размещения, при которой не возникает пересечение кругов и размещение круга.
                int p = -1;
                do
                {
                    p++;
                    circles[i].Pole.Copy = points[p];
                } while (!IsCheckedCircles(circles[i], circles, i, eps));
                #endregion
                #region Шаг 1.7. Пересчёт ширины занятой части полосы.
                length = Math.Max(length, circles[i].Pole.X + circles[i].Value);
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения кругов (метод последовательного одиночного размещения).
        /// </summary>
        public override void CalculateStart()
        {
            #region Шаг 1. Проверка размеров размещаемых кругов относительно высоты полосы.
            for (int i = 0; i < circles.Length; i++)
            {
                if (2 * circles[i].Value > height)
                    throw new Exception("Диаметр " + i.ToString() + "-го круга больше высоты полосы!");
            }
            #endregion

            #region Шаг 2. Запуск метода последовательного одиночного размещения.
            Calculate();
            #endregion
        }
    }

    public class PlacingWithCloseModel : Opt.Algorithms.Placing, Opt.Algorithms.IWithClosenessModel
    {
        protected Vertex<Geometric2d> vertex;
        public Vertex<Geometric2d> Vertex
        {
            get
            {
                return vertex;
            }
        }
        protected List<Vertex<Geometric2d>> triples;

        public PlacingWithCloseModel(double height, Circle[] circles, double eps)
            : base(height, 0, circles, eps)
        {
            length = 2 * height;

            #region Шаг 1. Создаём начальную модель, состоящую из сторон прямоугольника. !!!Потом переделать на полосу!!!
            Geometric2d border_1 = new Plane2d { Id = -1, Pole = new Point2d { X = 0, Y = 2 * height / 2 }, Normal = new Vector2d { X = 0, Y = -1 } };
            Geometric2d border_2 = new Plane2d { Id = -2, Pole = new Point2d { X = 0, Y = 1 * height / 2 }, Normal = new Vector2d { X = 1, Y = 0 } };
            Geometric2d border_3 = new Plane2d { Id = -3, Pole = new Point2d { X = 0, Y = 0 * height / 2 }, Normal = new Vector2d { X = 0, Y = +1 } };

            Geometric2d border_4 = new Plane2d { Id = -4, Pole = new Point2d { X = length, Y = height / 2 }, Normal = new Vector2d { X = -1, Y = 0 } };

            vertex = Vertex<Geometric2d>.CreateClosenessModel(border_1, border_2, border_3);
            vertex.BreakCrosBy(border_4);
            #endregion

            #region Шаг 2. Устанавливаем для полученных троек круги Делоне. !Для полосы можно не автоматизировать. Для многоугольника необходимо придумать автоматизацию.
            vertex.SetCircleDelone(new Circle { Pole = new Point2d { X = height / 2, Y = height / 2 }, Value = height / 2 });
            vertex.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = length - height / 2, Y = height / 2 }, Value = height / 2 });

            vertex.Prev.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = -height / 2, Y = height / 2 }, Value = 0 });
            vertex.Cros.Prev.Cros.SetCircleDelone(new Circle { Pole = new Point2d { X = double.PositiveInfinity /*length + height / 2*/, Y = height / 2 }, Value = 0 });
            #endregion

            length = 0;

            vertex = vertex.Cros.Next.Cros.Next;

            triples = vertex.GetTriples();
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


        /// <summary>
        /// Поиск локального минимума с использованием метода последовательного одиночного размещения.
        /// </summary>
        protected override void Calculate()
        {
            #region Шаг 1. Метод последовательного одиночного размещения. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                System.Threading.Thread.Sleep(15);
                #region Шаг 1.1. Устанавливаем начальное значение для точки размещения текущего объекта и связанной с ней вершиной.
                Point2d point_global = new Point2d { X = double.PositiveInfinity };
                Vertex<Geometric2d> vertex_global = null;
                #endregion
                #region Шаг 1.2. Для каждой тройки выполняем следующее...
                for (int j = 0; j < triples.Count; j++)
                {
                    #region Шаг 1.2.1. Для каждой вершины выполняем следующее...
                    Vertex<Geometric2d> vertex_local = triples[j];
                    do
                    {
                        #region Шаг 1.2.1.1. Если выполняются все условия существования точки плотного размещения второго рода, то находим её.
                        if (Существует_точка_плотного_размещения_второго_рода(circles[i], vertex_local))
                        {
                            #region Шаг 1.2.1.1.1. Поиск точки близости второго рода.
                            Point2d point_temp = circles[i].Точка_близости_второго_рода(vertex_local.Next.DataInVertex, vertex_local.Prev.DataInVertex);
                            #endregion

                            #region Шаг 1.2.1.1.2. Если точка даёт меньшее приращение функции цели, то сохраняем вершину и точку размещения.
                            if (point_temp.X < point_global.X)
                            {
                                point_global = point_temp;
                                vertex_global = vertex_local;
                            }
                            #endregion
                        }
                        #endregion
                        vertex_local = vertex_local.Next;
                    } while (vertex_local != triples[j]);
                    #endregion
                }
                #endregion
                #region Шаг 1.3. Устанавливаем объект в найденную точку размещения.
                circles[i].Pole.Copy = point_global;
                #endregion
                #region Шаг 1.4. Вставляем объект в ребро напротив найденной вершины.
                vertex_global.BreakCrosBy(circles[i]);
                vertex_global = vertex_global.Cros;
                #endregion
                #region Шаг 1.5. Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
                Vertex<Geometric2d> vertex_temp = vertex_global;
                do
                {
                    while (CircleExt.Расширенное_расстояние(vertex_temp.DataInVertex as Circle, vertex_temp.Cros.Somes.CircleDelone) < 0)
                        vertex_temp.Rebuild();

                    vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));

                    vertex_temp = vertex_temp.Next.Cros.Next;
                } while (vertex_temp != vertex_global);
                #endregion

                #region Шаг 1.6. Находим список всех троек. !Можно уменьшить сложность данного пункта, если использовать только новые полученные тройки!
                triples = this.vertex.GetTriples();
                #endregion

                #region Шаг 1.7. Пересчёт ширины занятой части полосы.
                length = Math.Max(length, circles[i].Pole.X + circles[i].Value);
                if (((Plane2d)(this.vertex.DataInVertex)).Pole.X < length + 2 * height)
                {
                    (this.vertex.DataInVertex as Plane2d).Pole.X = length + 2 * height;
                    this.vertex.Next.Cros.Next.Somes.CircleDelone.Pole.X = length + 2 * height - height / 2;
                }
                #endregion
            }
            #endregion

            #region Шаг 2. Изменение расположения полуплоскости, которая определяет правую границу полосы.
            (this.vertex.DataInVertex as Plane2d).Pole.X = length;
            this.vertex.Next.Cros.Next.Somes.CircleDelone.Pole.X = length - height / 2;

            Vertex<Geometric2d> vertex_t = this.vertex;

            do
            {
                while (GeometricExt.Расширенное_расстояние(vertex_t.DataInVertex, vertex_t.Cros.Somes.CircleDelone) < 0)
                {
                    vertex_t.Rebuild();

                    vertex_t.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_t.Prev.DataInVertex, vertex_t.DataInVertex, vertex_t.Next.DataInVertex));
                    vertex_t.Next.Cros.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_t.Next.Cros.Prev.DataInVertex, vertex_t.Next.Cros.DataInVertex, vertex_t.Next.Cros.Next.DataInVertex));
                }

                vertex_t = vertex_t.Next.Cros.Next;
            } while (vertex_t != this.vertex);
            #endregion
        }

        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения кругов (метод последовательного одиночного размещения).
        /// </summary>
        public override void CalculateStart()
        {
            #region Шаг 1. Проверка размеров размещаемых кругов относительно высоты полосы.
            for (int i = 0; i < circles.Length; i++)
            {
                if (2 * circles[i].Value > height)
                    throw new Exception("Диаметр " + i.ToString() + "-го круга больше высоты полосы!");
            }
            #endregion

            #region Шаг 2. Запуск метода последовательного одиночного размещения.
            Calculate();
            #endregion
        }
    }
}

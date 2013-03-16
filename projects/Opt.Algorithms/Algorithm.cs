using System;
using System.Collections.Generic;

using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Extentions;

using Rectangle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleVector;

namespace Opt.Algorithms
{
    public class StripRegion : Rectangle
    {
        public int Count { get; set; }

        public bool IsCanContain(Rectangle rectangle)
        {
            bool is_checked = true;
            for (int j = 1; j <= this.vector.Dim && is_checked; j++)
                is_checked = rectangle.Vector[j] <= this.vector[j];
            return is_checked;
        }
        public bool IsContain(Rectangle rectangle, Point2d pole)
        {
            Point2d pole_temp = rectangle.Pole;
            rectangle.Pole = pole;
            bool is_checked = true;
            for (int j = 1; j <= this.vector.Dim && is_checked; j++)
            {
                double coor = rectangle.Pole[j] + rectangle.Vector[j];
                is_checked = 0 <= coor && coor <= this.vector[j];
            }
            rectangle.Pole = pole_temp;

            return is_checked;
        }

        public Point2d OptPole(Point2d opt_pole, Point2d pole)
        {
            if (pole.X < opt_pole.X)
                return pole;
            if (pole.X > opt_pole.X)
                return opt_pole;
            if (pole.X == opt_pole.X)
            {
                if (pole.Y < opt_pole.Y)
                    return pole;
                if (pole.Y > opt_pole.Y)
                    return opt_pole;
            }
            throw new NotImplementedException();
        }
    }

    public class Algorithm
    {
        private List<Point2d> poles;

        private void Steps(List<Polygon2d> polygon_list, StripRegion region)
        {
            List<Polygon2d> polygon_placed_list = new List<Polygon2d>();

            #region Шаг-1. Для каждого размещаемого Polygon...
            for (int i = 0; i < polygon_list.Count; i++)
                if (Step(polygon_list[i], region, polygon_placed_list))
                    polygon_placed_list.Add(polygon_list[i]);
            #endregion
        }

        private Polygon2d Годораф_функции_плотного_размещения(Polygon2d polygon, StripRegion region)
        {
            throw new NotImplementedException();
        } // Пролесковский.
        private Polygon2d Годораф_функции_плотного_размещения(Polygon2d polygon_i, Polygon2d polygon_j)
        {
            throw new NotImplementedException();
        }  // Пролесковский.
        private List<Point2d> Точки_пересечения_многоугольников(Polygon2d polygon_i, Polygon2d polygon_j)
        {
            throw new NotImplementedException();
        } // Кузовлев.
        private bool Точка_принадлежит_многоугольнику(Point2d point, Polygon2d polygon)
        {
            throw new NotImplementedException();
        } // Харин, Пудло

        private bool Step(Polygon2d polygon, StripRegion strip_region, List<Polygon2d> polygon_placed_list)
        {
            #region Шаг-1. Установка начального значения (бесконечность) точки размещения текущего Polygon.
            polygon.Pole = new Point2d();
            for (int j = 1; j <= polygon.Pole.Dim; j++)
                polygon.Pole[j] = double.PositiveInfinity;
            #endregion

            #region Шаг-2. Построение всех годофов функции плотного размещения.
            List<Polygon2d> polygon_godograph_list = new List<Polygon2d>(); // Установить Capacity.
            #region Шаг-2.1. Годораф функции плотного размещения по полосе.
            polygon_godograph_list.Add(Годораф_функции_плотного_размещения(polygon, strip_region));
            #endregion
            #region Шаг-2.2. Годорафы функции плотного размещения по многоугольникам.
            for (int i = 0; i < polygon_placed_list.Count; i++)
                polygon_godograph_list.Add(Годораф_функции_плотного_размещения(polygon, polygon_placed_list[i]));
            #endregion
            #endregion

            #region Шаг-3. Создание списка возможных точек размещения.
            poles = new List<Point2d>();
            #endregion

            #region Шаг-4. Добавляем точки пересечения сторон полосы.
            // Вершины polygon_godograph_list[0];
            #endregion

            #region Шаг-5. Для каждой пары годорафов функции плотного размещения находим их точки пересечения.
            for (int i = 0; i < polygon_godograph_list.Count - 1; i++)
                for (int j = i + 1; j < polygon_godograph_list.Count; j++)
                    poles.AddRange(Точки_пересечения_многоугольников(polygon_godograph_list[i], polygon_godograph_list[j]));
            #endregion

            #region Шаг-6. Для каждой возможной точки размещения...
            bool is_placed = false;
            for (int i = 0; i < poles.Count; i++)
            {
                #region Шаг-6.1. Проверяем условие принадлежности области размещения.
                bool is_point_right = Точка_принадлежит_многоугольнику(poles[i], polygon_godograph_list[0]);
                #endregion
                #region Шаг-6.2. Проверяем условие непересечения с размещёнными объектами.
                for (int j = 1; j < polygon_godograph_list.Count && is_point_right; j++)
                    is_point_right = !Точка_принадлежит_многоугольнику(poles[i], polygon_godograph_list[j]);
                #endregion

                #region Шаг-6.3. Если выполняются все условия размещения, то...
                if (is_point_right)
                {
                    #region Устанавливаем объект в точку размещения (из двух точек размещения выбираеться более оптимальная). // Подойдёт ли такой вариант?
                    polygon.Pole.Copy = strip_region.OptPole(polygon.Pole, poles[i]);
                    #endregion

                    is_placed = true;
                }
                #endregion
            }
            #endregion

            return is_placed;
        }
    }
}
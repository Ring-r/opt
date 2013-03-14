using System;
using System.Collections.Generic;

using Opt.Geometrics;
using Opt.Geometrics.Extentions;

namespace Opt.Algorithms
{
    public class StripRegion : Rectangle
    {
        public int Count { get; set; }

        public bool IsCanContain(Rectangle rectangle)
        {
            bool is_checked = true;
            for (int j = 1; j <= Size.Dim && is_checked; j++)
                is_checked = rectangle.Size[j] <= Size[j];
            return is_checked;
        }
        public bool IsContain(Rectangle rectangle, Point pole)
        {
            Point pole_temp = rectangle.Pole;
            rectangle.Pole = pole;
            bool is_checked = true;
            for (int j = 1; j <= Size.Dim && is_checked; j++)
            {
                double coor = rectangle.Pole[j] + rectangle.Size[j];
                is_checked = 0 <= coor && coor <= Size[j];
            }
            rectangle.Pole = pole_temp;

            return is_checked;
        }

        public Point OptPole(Point opt_pole, Point pole)
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
        private List<Point> poles;

        private void Steps(List<Polygon> polygon_list, StripRegion region)
        {
            List<Polygon> polygon_placed_list = new List<Polygon>();

            #region Шаг-1. Для каждого размещаемого Polygon...
            for (int i = 0; i < polygon_list.Count; i++)
                if (Step(polygon_list[i], region, polygon_placed_list))
                    polygon_placed_list.Add(polygon_list[i]);
            #endregion
        }

        private Polygon Годораф_функции_плотного_размещения(Polygon polygon, StripRegion region)
        {
            throw new NotImplementedException();
        } // Пролесковский.
        private Polygon Годораф_функции_плотного_размещения(Polygon polygon_i, Polygon polygon_j)
        {
            throw new NotImplementedException();
        }  // Пролесковский.
        private List<Point> Точки_пересечения_многоугольников(Polygon polygon_i, Polygon polygon_j)
        {
            throw new NotImplementedException();
        } // Кузовлев.
        private bool Точка_принадлежит_многоугольнику(Point point, Polygon polygon)
        {
            throw new NotImplementedException();
        } // Харин, Пудло

        private bool Step(Polygon polygon, StripRegion strip_region, List<Polygon> polygon_placed_list)
        {
            #region Шаг-1. Установка начального значения (бесконечность) точки размещения текущего Polygon.
            polygon.Pole = new Point();
            for (int j = 1; j <= polygon.Pole.Dim; j++)
                polygon.Pole[j] = double.PositiveInfinity;
            #endregion

            #region Шаг-2. Построение всех годофов функции плотного размещения.
            List<Polygon> polygon_godograph_list = new List<Polygon>(); // Установить Capacity.
            #region Шаг-2.1. Годораф функции плотного размещения по полосе.
            polygon_godograph_list.Add(Годораф_функции_плотного_размещения(polygon, strip_region));
            #endregion
            #region Шаг-2.2. Годорафы функции плотного размещения по многоугольникам.
            for (int i = 0; i < polygon_placed_list.Count; i++)
                polygon_godograph_list.Add(Годораф_функции_плотного_размещения(polygon, polygon_placed_list[i]));
            #endregion
            #endregion

            #region Шаг-3. Создание списка возможных точек размещения.
            poles = new List<Point>();
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
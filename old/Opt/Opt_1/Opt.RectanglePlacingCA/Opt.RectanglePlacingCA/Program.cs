using System;
using System.Collections.Generic;

using Opt.Geometrics;
using Opt.Geometrics.Generics;

namespace Opt.RectanglePlacingCA
{
    class Program
    {
        static bool NextPerm(int[] mm, int[] mt)
        {
            int k = mm.Length - 1;

            while (true)
            {

                if (mm[k] < mt.Length - 1)
                {
                    mt[mm[k]]--;
                    mm[k]++;
                    mt[mm[k]]++;

                    if (mt[mm[k]] == 1)
                        return true;
                }
                else
                {
                    if (k > 0)
                    {
                        mt[mm[k]]--;
                        mm[k] = 0;
                        mt[0]++;
                        k--;
                    }
                    else
                        return false;
                }
            }
        }

        private bool CheckIntersectWithRegion(Point<Vector3d> point, Vector3d size)
        {
            throw new NotImplementedException();
            //double eps = 1e-4f;
            //return point.Vector.X + size.X <= task.RegionWidth + eps && point.Vector.Y + size.Y <= task.RegionHeight + eps;
        }
        private bool CheckIntersectWithRectangles(Point<Vector3d> point, Vector3d size)
        {
            throw new NotImplementedException();
            //bool without_intersect = true;
            //for (int i = 0; i < objects_busy_numbers.Count && without_intersect; i++)
            //    without_intersect = without_intersect && CheckIntersectBetweenRectangles(point, size, objects_busy_points[i], objects_sizes[objects_busy_numbers[i]]);
            //return without_intersect;
        }
        private bool CheckIntersectBetweenRectangles(Point<Vector3d> point_i, Vector3d size_i, Point<Vector3d> point_j, Vector3d size_j)
        {
            throw new NotImplementedException();
            //double eps = 1e-4f;
            //return
            //    point_i.Vector.X + size_i.X <= point_j.Vector.X + eps ||
            //    point_i.Vector.Y + size_i.Y <= point_j.Vector.Y + eps ||
            //    point_j.Vector.X + size_j.X <= point_i.Vector.X + eps ||
            //    point_j.Vector.Y + size_j.Y <= point_i.Vector.Y + eps;
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            List<Rectangle<Vector3d>> rectangle_list = new List<Rectangle<Vector3d>>();
            int count = rand.Next(5, 10);
            for (int i = 0; i < count; i++)
                rectangle_list.Add(new Rectangle<Vector3d> { Size = new Vector3d { X = rand.Next(20, 100), Y = rand.Next(20, 100) } });

            List<Rectangle<Vector3d>> rectangle_placed_list = new List<Rectangle<Vector3d>>();
            rectangle_placed_list.Add(new Rectangle<Vector3d>());

            for (int i = 0; i < rectangle_list.Count; i++)
            {
                Rectangle<Vector3d> rect = rectangle_list[i];

                int m = 3; // Размерность пространства.
                int[] mm = new int[m]; // Массив, который хранит выборку.
                int[] mt = new int[rectangle_placed_list.Count]; // Массив, необходимый для быстрой проверки одинаковых элементов.
                mt[0] = m;
                do
                {
                    Point<Vector3d> point_temp = new Point<Vector3d> { Vector = new Vector3d { X = rectangle_placed_list[mm[0]].Pole.Vector.X + rectangle_placed_list[mm[0]].Size.X, Y = rectangle_placed_list[mm[1]].Pole.Vector.Y + rectangle_placed_list[mm[1]].Size.Y, Z = rectangle_placed_list[mm[2]].Pole.Vector.Z + rectangle_placed_list[mm[2]].Size.Z } };                    
                    #region Проверка попадания текущего объекта размещения в текущей точке размещения в область размещения.
                    if (CheckIntersectWithRegion(point_temp, size))
                        #region Проверка непересечения текущего объекта размещения в текущей точке размещения со всеми размещёнными объектами.
                        if (CheckIntersectWithRectangles(point_temp, size))
                        {
                            #region Определение занятой части области размещения и её площади при размещении текущего объекта размещения в текущей точке размещения.
                            //double width_temp = Math.Max(region_size.X, point_temp.X + size.X);
                            //double height_temp = Math.Max(region_size.Y, point_temp.Y + size.Y);
                            //double object_function_temp = width_temp * height_temp;
                            #endregion
                            //#region Определение того, является ли текущая точка размещения лучше сохранённой (оптимальной).
                            //bool is_change =
                            //    object_function > object_function_temp ||
                            //    object_function == object_function_temp && point.X > point_temp.X ||
                            //    object_function == object_function_temp && point.X == point_temp.X && point.Y >= point_temp.Y;
                            //#endregion
                            //#region Сохранение лучшей точки размещения и значения функции цели.
                            //if (is_change)
                            //{
                            //    point = point_temp;
                            //    object_function = object_function_temp;
                            //}
                            //#endregion
                        }
                        #endregion
                    #endregion
                    // 1. Поиск точки.
                    // 2. Проверка точки.
                    // 3. Выбор лучшей точки.
                } while (NextPerm(mm, mt));
            }
        }
    }
}

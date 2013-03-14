using System;
using System.Collections.Generic;
using Opt.GeometricObjects;
using Opt.Model;

namespace Opt
{
    namespace Pucking_Circle_Strip_FormApp
    {
        public class ModelExtending
        {
            public static double ED(Object object_i, Object object_j)
            {
                if (object_i is Circle)
                    if (object_j is Circle)
                        return ExtendedDistance.Calc(object_i as Circle, object_j as Circle);
                    else
                        return ExtendedDistance.Calc(object_i as Circle, object_j as StripLine);
                else
                    if (object_j is Circle)
                        return ExtendedDistance.Calc(object_i as StripLine, object_j as Circle);
                    else
                        return ExtendedDistance.Calc(object_i as StripLine, object_j as StripLine);
            }
            public static Point PP(Object object_i, Object object_j, Circle circle)
            {
                if (object_i is Circle)
                {
                    Circle circle_i = object_i as Circle;
                    if (object_j is Circle)
                    {
                        Circle circle_j = object_j as Circle;
                        return PlacingPoint.Calc(new Circle() { X = circle_i.X, Y = circle_i.Y, R = circle_i.R + circle.R }, new Circle() { X = circle_j.X, Y = circle_j.Y, R = circle_j.R + circle.R });
                    }
                    else
                    {
                        StripLine strip_line_j = object_j as StripLine;
                        return PlacingPoint.Calc(new Circle() { X = circle_i.X, Y = circle_i.Y, R = circle_i.R + circle.R }, new StripLine() { PX = strip_line_j.PX + strip_line_j.VY * circle.R, PY = strip_line_j.PY - strip_line_j.VX * circle.R, VX = strip_line_j.VX, VY = strip_line_j.VY });
                    }
                }
                else
                {
                    StripLine strip_line_i = object_i as StripLine;
                    if (object_j is Circle)
                    {
                        Circle circle_j = object_j as Circle;
                        return PlacingPoint.Calc(new StripLine() { PX = strip_line_i.PX + strip_line_i.VY * circle.R, PY = strip_line_i.PY - strip_line_i.VX * circle.R, VX = strip_line_i.VX, VY = strip_line_i.VY }, new Circle() { X = circle_j.X, Y = circle_j.Y, R = circle_j.R + circle.R });
                    }
                    else
                    {
                        StripLine strip_line_j = object_j as StripLine;
                        return PlacingPoint.Calc(new StripLine() { PX = strip_line_i.PX + strip_line_i.VY * circle.R, PY = strip_line_i.PY - strip_line_i.VX * circle.R, VX = strip_line_i.VX, VY = strip_line_i.VY }, new StripLine() { PX = strip_line_j.PX + strip_line_j.VY * circle.R, PY = strip_line_j.PY - strip_line_j.VX * circle.R, VX = strip_line_j.VX, VY = strip_line_j.VY });
                    }
                }
            }
            public static Circle DC(Object object_i, Object object_j, Object object_k)
            {
                if (object_i is Circle)
                    if (object_j is Circle)
                        if (object_k is Circle)
                            return DeloneCircle.Calc(object_i as Circle, object_j as Circle, object_k as Circle);
                        else
                            return DeloneCircle.Calc(object_k as StripLine, object_i as Circle, object_j as Circle);
                    else
                        if (object_k is Circle)
                            return DeloneCircle.Calc(object_j as StripLine, object_k as Circle, object_i as Circle);
                        else
                            return DeloneCircle.Calc(object_j as StripLine, object_k as StripLine, object_i as Circle);
                else
                    if (object_j is Circle)
                        if (object_k is Circle)
                            return DeloneCircle.Calc(object_i as StripLine, object_j as Circle, object_k as Circle);
                        else
                            return DeloneCircle.Calc(object_k as StripLine, object_i as StripLine, object_j as Circle);
                    else
                        if (object_k is Circle)
                            return DeloneCircle.Calc(object_i as StripLine, object_j as StripLine, object_k as Circle);
                        else
                            return DeloneCircle.Calc(object_i as StripLine, object_j as StripLine, object_k as StripLine);
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
                    if (ExtendedDistance.Calc(circle, circles[i]) < -eps) // !!! Необходимо учитывать погрешность?
                        return false;
                return true; ;
            }
        }
    }
}
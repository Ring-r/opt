using System;
using Opt.Geometrics;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Временный класс всех расширений геометрических объектов.
    /// </summary>
    public static class Assistant
    {
        #region Круг Делоне.
        /// <summary>
        /// Получить круг Делоне.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="circle_curr">Круг.</param>
        /// <param name="circle_next">Круг.</param>
        /// <returns>Круг Делоне.</returns>
        public static Circle Круг_Делоне(Circle circle_prev, Circle circle_curr, Circle circle_next)
        {
            double xpc = circle_prev.Pole.X - circle_curr.Pole.X; // x21
            double ypc = circle_prev.Pole.Y - circle_curr.Pole.Y; // y21
            double rpc = circle_prev.Radius - circle_curr.Radius; // r21
            double zpc = (xpc * xpc + ypc * ypc - rpc * rpc) / 2; // z21

            double xnc = circle_next.Pole.X - circle_curr.Pole.X; // x31
            double ync = circle_next.Pole.Y - circle_curr.Pole.Y; // y31
            double rnc = circle_next.Radius - circle_curr.Radius; // r31
            double znc = (xnc * xnc + ync * ync - rnc * rnc) / 2; // z31

            double XY = xnc * ypc - xpc * ync;
            if (XY == 0)
                return null; // Исключительная ситуация!!! Убрать!!!

            double Ax = (ync * rpc - ypc * rnc) / XY;
            double Ay = -(xnc * rpc - xpc * rnc) / XY;
            double Bx = -(ync * zpc - ypc * znc) / XY;
            double By = (xnc * zpc - xpc * znc) / XY;
            double A = Ax * Ax + Ay * Ay - 1;
            double B = Ax * Bx + Ay * By;
            double C = Bx * Bx + By * By;
            double D = B * B - A * C;
            if (D < 0)
                return null; // Исключительная ситуация!!! Убрать!!!

            double R1 = (-B - Math.Sqrt(D)) / A;
            double R2 = (-B + Math.Sqrt(D)) / A;

            double x = Ax * R1 + Bx + circle_curr.Pole.X;
            double y = Ay * R1 + By + circle_curr.Pole.Y;
            double r = R1 - circle_curr.Radius;


            // Проверка на правильный обход круга.
            double R = r;
            //double R;
            //if (r > 0)
            //    R = r;
            //else
            //    R = -r;
            double xp = (circle_prev.Pole.X - x) / (circle_prev.Radius + R);
            double yp = (circle_prev.Pole.Y - y) / (circle_prev.Radius + R);
            double xc = (circle_curr.Pole.X - x) / (circle_curr.Radius + R);
            double yc = (circle_curr.Pole.Y - y) / (circle_curr.Radius + R);
            double xn = (circle_next.Pole.X - x) / (circle_next.Radius + R);
            double yn = (circle_next.Pole.Y - y) / (circle_next.Radius + R);
            double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
            //if ((r > 0 && s < 0) || (r < 0 && s > 0))
            if (r > 0 && s < 0)
            {
                x = Ax * R2 + Bx + circle_curr.Pole.X;
                y = Ay * R2 + By + circle_curr.Pole.Y;
                r = R2 - circle_curr.Radius;
            }

            return new Circle { Pole = new Point { X = x, Y = y }, Radius = r };



            #region Подготовка данных.
            //double[,] matr_a = new double[2, 3]
            //{
            //    {circle_next.Pole.X - circle_curr.Pole.X, circle_next.Pole.Y - circle_curr.Pole.Y, circle_next.R - circle_curr.R},
            //    {circle_next.Pole.X - circle_curr.Pole.X, circle_next.Pole.Y - circle_curr.Pole.Y, circle_next.R - circle_curr.R}
            //};

            //double[,] matr_b = new double[2, 1]
            //{
            //    {matr_a[0, 2] * matr_a[0, 2] - (matr_a[0, 1] * matr_a[0, 1] + matr_a[0, 0] * matr_a[0, 0]) / 2},
            //    {matr_a[1, 2] * matr_a[1, 2] - (matr_a[1, 1] * matr_a[1, 1] + matr_a[1, 0] * matr_a[1, 0]) / 2}
            //};
            #endregion

            #region Решение системы линейных уравнений.
            //double XY = 0;// matr_a[0, 0] * matr_a[1, 1] - matr_a[1, 0] * matr_a[0, 1];
            ////if (XY == 0)
            ////    return null; // Исключительная ситуация!!! Убрать!!!

            //Vector A = new Vector();// { X = +matr_a[0, 1] * matr_a[1, 2] - matr_a[1, 1] * matr_a[0, 2], Y = -matr_a[0, 0] * matr_a[1, 2] + matr_a[1, 0] * matr_a[0, 2] };
            //Vector B = new Vector();// { X = -matr_a[0, 1] * matr_b[1, 0] + matr_a[1, 1] * matr_b[0, 0], Y = +matr_a[0, 0] * matr_b[1, 0] - matr_a[1, 0] * matr_b[0, 0] };
            #endregion

            #region Решение квадратичного уравнения.
            //double a = XY * XY - A * A;
            //double b = A * B;
            //double c = B * B;
            //double d = b * b - a * c;
            //if (d < 0)
            //    return null; // !!!Круг Делоне с комплексными радиусами.!!!

            //double R_prev = (b - Math.Sqrt(d)) / a;
            //double R_next = (b + Math.Sqrt(d)) / a;
            #endregion

            #region Расчёт круга Делоне.
            //Circle circle_delone_prev = new Circle { Pole = circle_curr.Pole + A * R_prev + B, Radius = R_prev - circle_curr.Radius };
            //Circle circle_delone_next = new Circle { Pole = circle_curr.Pole + A * R_next + B, Radius = R_next - circle_curr.Radius };
            #endregion

            #region Проверка на правильный обход круга Делоне.
            //if (circle_delone_prev.Radius >= 0 && circle_delone_next.Radius < 0)
            //    return circle_delone_prev;
            //if (circle_delone_prev.Radius < 0 && circle_delone_next.Radius >= 0)
            //    return circle_delone_next;
            //if (circle_delone_prev.Radius >= 0 && circle_delone_next.Radius >= 0)
            //    if (true) //Правильный обход круга Делоне.
            //        return circle_delone_prev;
            //    else
            //        return circle_delone_next;
            #endregion

            #region Пожарное возвращение результата.
            //return null;
            #endregion
        }
        /// <summary>
        /// Получить круг Делоне.
        /// </summary>
        /// <param name="circle_prev">Круг.</param>
        /// <param name="circle_curr">Круг.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Круг Делоне.</returns>
        public static Circle Круг_Делоне(Circle circle_prev, Circle circle_curr, Plane plane_next)
        {
            // Исправить!!!
            double xpc = circle_prev.Pole.X - circle_curr.Pole.X; // x21
            double ypc = circle_prev.Pole.Y - circle_curr.Pole.Y; // y21
            double rpc = circle_prev.Radius - circle_curr.Radius; // r21
            double zpc = (xpc * xpc + ypc * ypc - rpc * rpc) / 2; // z21

            double xnc = plane_next.Normal.X; // x31*
            double ync = plane_next.Normal.Y; // y31*
            double rnc = -1; // r31*
            double xpnc = plane_next.Pole.X - circle_curr.Pole.X; // xp31*
            double ypnc = plane_next.Pole.Y - circle_curr.Pole.Y; // yp31
            double znc = xnc * xpnc + ync * ypnc - circle_curr.Radius; // z31*

            double XY = xnc * ypc - xpc * ync;
            if (XY == 0)
                return null; // Исключительная ситуация!!! Убрать!!!

            double Ax = (ync * rpc - ypc * rnc);
            double Ay = -(xnc * rpc - xpc * rnc);
            double Bx = -(ync * zpc - ypc * znc);
            double By = (xnc * zpc - xpc * znc);
            double A = Ax * Ax + Ay * Ay - XY * XY;
            double B = Ax * Bx + Ay * By;
            double C = Bx * Bx + By * By;
            double D = B * B - A * C;
            if (D < 0)
                return null; // Исключительная ситуация!!! Убрать!!!

            double R1 = (-B - Math.Sqrt(D)) / A;
            double R2 = (-B + Math.Sqrt(D)) / A;

            double x = (Ax * R1 + Bx) / XY + circle_curr.Pole.X;
            double y = (Ay * R1 + By) / XY + circle_curr.Pole.Y;
            double r = R1 - circle_curr.Radius;


            // Проверка на правильный обход круга. //!!!Изменить!!!
            double R = r;
            //double R;
            //if (r > 0)
            //    R = r;
            //else
            //    R = -r;
            double xp = (circle_prev.Pole.X - x) / (circle_prev.Radius + R);
            double yp = (circle_prev.Pole.Y - y) / (circle_prev.Radius + R);
            double xc = (circle_curr.Pole.X - x) / (circle_curr.Radius + R);
            double yc = (circle_curr.Pole.Y - y) / (circle_curr.Radius + R);
            double xn = -plane_next.Normal.X;
            double yn = -plane_next.Normal.Y;
            double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
            // if ((r > 0 && s < 0) || (r < 0 && s > 0))
            if (r > 0 && s < 0)
            {
                x = (Ax * R2 + Bx) / XY + circle_curr.Pole.X;
                y = (Ay * R2 + By) / XY + circle_curr.Pole.Y;
                r = R2 - circle_curr.Radius;
            }

            return new Circle { Pole = new Point { X = x, Y = y }, Radius = r };
        }
        /// <summary>
        /// Получить круг Делоне.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="circle_curr">Круг.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Круг Делоне.</returns>
        public static Circle Круг_Делоне(Plane plane_prev, Circle circle_curr, Plane plane_next)
        {
            double xpc = plane_prev.Normal.X; // x21*
            double ypc = plane_prev.Normal.Y; // y21*
            double rpc = -1; // r21*
            double xppc = plane_prev.Pole.X - circle_curr.Pole.X; // xp21*
            double yppc = plane_prev.Pole.Y - circle_curr.Pole.Y; // yp21*
            double zpc = xpc * xppc + ypc * yppc - circle_curr.Radius; // z21*

            double xnc = plane_next.Normal.X; // x31*
            double ync = plane_next.Normal.Y; // y31*
            double rnc = -1; // r31*
            double xpnc = plane_next.Pole.X - circle_curr.Pole.X; // xp31*
            double ypnc = plane_next.Pole.Y - circle_curr.Pole.Y; // yp31*
            double znc = xnc * xpnc + ync * ypnc - circle_curr.Radius; // z21*

            double XY = xnc * ypc - xpc * ync;
            double x;
            double y;
            double r;
            if (XY != 0)
            {
                double Ax = (ync * rpc - ypc * rnc);
                double Ay = -(xnc * rpc - xpc * rnc);
                double Bx = -(ync * zpc - ypc * znc);
                double By = (xnc * zpc - xpc * znc);
                double A = Ax * Ax + Ay * Ay - XY * XY;
                double B = Ax * Bx + Ay * By;
                double C = Bx * Bx + By * By;
                double D = B * B - A * C;
                if (D < 0)
                    return null; // Исключительная ситуация!!! Убрать!!!

                double R1 = (-B - Math.Sqrt(D)) / A;
                double R2 = (-B + Math.Sqrt(D)) / A;

                x = (Ax * R1 + Bx) / XY + circle_curr.Pole.X;
                y = (Ay * R1 + By) / XY + circle_curr.Pole.Y;
                r = R1 - circle_curr.Radius;


                // Проверка на правильный обход круга. //!!!Изменить!!!
                double R = r;
                //double R;
                //if (r > 0)
                //    R = r;
                //else
                //    R = -r;
                double xp = -plane_prev.Normal.X;
                double yp = -plane_prev.Normal.Y;
                double xc = (circle_curr.Pole.X - x) / (circle_curr.Radius + R);
                double yc = (circle_curr.Pole.Y - y) / (circle_curr.Radius + R);
                double xn = -plane_next.Normal.X;
                double yn = -plane_next.Normal.Y;
                double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
                //if ((r > 0 && s < 0) || (r < 0 && s > 0))
                if (r > 0 && s < 0)
                {
                    x = (Ax * R2 + Bx) / XY + circle_curr.Pole.X;
                    y = (Ay * R2 + By) / XY + circle_curr.Pole.Y;
                    r = R2 - circle_curr.Radius;
                }
            }
            else
            {
                double h = Math.Abs((plane_prev.Pole - plane_next.Pole) * plane_next.Normal);
                r = h / 2;
                y = h / 2;
                x = -Math.Sqrt((circle_curr.Radius + circle_curr.Pole.Y) * (h + circle_curr.Radius - circle_curr.Pole.Y)) + circle_curr.Pole.X;

                // Проверка на правильный обход круга. //!!!Изменить!!!
                double R = r;
                //double R;
                //if (r > 0)
                //    R = r;
                //else
                //    R = -r;
                double xp = -plane_prev.Normal.X;
                double yp = -plane_prev.Normal.Y;
                double xc = (circle_curr.Pole.X - x) / (circle_curr.Radius + R);
                double yc = (circle_curr.Pole.Y - y) / (circle_curr.Radius + R);
                double xn = -plane_next.Normal.X;
                double yn = -plane_next.Normal.Y;
                double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
                //if ((r > 0 && s < 0) || (r < 0 && s > 0))
                if (r > 0 && s < 0)
                    x = Math.Sqrt((circle_curr.Radius + circle_curr.Pole.Y) * (h + circle_curr.Radius - circle_curr.Pole.Y)) + circle_curr.Pole.X;
            }

            return new Circle { Pole = new Point { X = x, Y = y }, Radius = r };
        }
        /// <summary>
        /// Получить круг Делоне.
        /// </summary>
        /// <param name="plane_prev">Полуплоскость.</param>
        /// <param name="plane_curr">Полуплоскость.</param>
        /// <param name="plane_next">Полуплоскость.</param>
        /// <returns>Круг Делоне.</returns>
        public static Circle Круг_Делоне(Plane plane_prev, Plane plane_curr, Plane plane_next)
        {
            double xpc = plane_prev.Normal.X; // x21
            double ypc = plane_prev.Normal.Y; // y21
            double rpc = -1; // r21
            double xppc = plane_prev.Pole.X - plane_curr.Pole.X; // xp21*
            double yppc = plane_prev.Pole.Y - plane_curr.Pole.Y; // yp21*
            double zpc = xpc * xppc + ypc * yppc; // z21*

            double xnc = plane_next.Normal.X; // x31*
            double ync = plane_next.Normal.Y; // y31*
            double rnc = -1; // r31*
            double xpnc = plane_next.Pole.X - plane_curr.Pole.X; // xp31*
            double ypnc = plane_next.Pole.Y - plane_curr.Pole.Y; // yp31*
            double znc = xnc * xpnc + ync * ypnc; // z21*

            double XY = xnc * ypc - xpc * ync;
            if (XY == 0)
                return null; // Исключительная ситуация!!! Убрать!!!

            double Ax = (ync * rpc - ypc * rnc);
            double Ay = -(xnc * rpc - xpc * rnc);
            double Bx = -(ync * zpc - ypc * znc);
            double By = (xnc * zpc - xpc * znc);

            double A = Ax * plane_curr.Normal.X + Ay * plane_curr.Normal.Y - XY;
            double B = Bx * plane_curr.Normal.X + By * plane_curr.Normal.Y;

            double r = -B / A;
            double x = (Ax * r + Bx) / XY + plane_curr.Pole.X;
            double y = (Ay * r + By) / XY + plane_curr.Pole.Y;
            // Проверка на правильный обход круга не нужна.

            return new Circle { Pole = new Point { X = x, Y = y }, Radius = r };
        }
        #endregion
    }
}
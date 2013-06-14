using System;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Temp;

namespace Opt
{
    namespace VD
    {
        public interface IObject<Object>
        {
            double ExtendedDistance(Object curr_object);
        }

        public interface IDeloneCircle<Object> : IObject<Object>
        {
            void Calculate(Object prev_object, Object next_object);
            void Calculate(Object prev_object, Object curr_object, Object next_object);
        }

        public class DeloneCircle : Circle, IDeloneCircle<Circle>
        {
            protected double vx;
            protected double vy;

            public double VX
            {
                get
                {
                    return vx;
                }
                set
                {
                    vx = value;
                }
            }
            public double VY
            {
                get
                {
                    return vy;
                }
                set
                {
                    vy = value;
                }
            }
            public new Vector2d CenterVector
            {
                get
                {
                    return new Vector2d() { X = vx, Y = vy };
                }
                set
                {
                    vx = value.X;
                    vy = value.Y;
                }
            }

            public void Calculate(Circle prev_circle, Circle next_circle)
            {
                this.R = double.PositiveInfinity;

                double xnp = next_circle.X - prev_circle.X;
                double ynp = next_circle.Y - prev_circle.Y;
                double length = Math.Sqrt(xnp * xnp + ynp * ynp);
                xnp /= length;
                ynp /= length;

                this.X = prev_circle.X - prev_circle.R * ynp;
                this.Y = prev_circle.Y + prev_circle.R * xnp;

                vx = next_circle.X - next_circle.R * ynp - this.X;
                vy = next_circle.Y + next_circle.R * xnp - this.Y;

                length = Math.Sqrt(vx * vx + vy * vy);

                vx /= length;
                vy /= length;
            }

            public void Calculate(Circle prev_circle, Circle curr_circle, Circle next_circle)
            {
                double xpc = prev_circle.X - curr_circle.X; // x21
                double ypc = prev_circle.Y - curr_circle.Y; // y21
                double rpc = prev_circle.R - curr_circle.R; // r21
                double zpc = (xpc * xpc + ypc * ypc - rpc * rpc) / 2; // z21

                double xnc = next_circle.X - curr_circle.X; // x31
                double ync = next_circle.Y - curr_circle.Y; // y31
                double rnc = next_circle.R - curr_circle.R; // r31
                double znc = (xnc * xnc + ync * ync - rnc * rnc) / 2; // z31

                double XY = xnc * ypc - xpc * ync;
                if (XY == 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double Ax = (ync * rpc - ypc * rnc) / XY;
                double Ay = -(xnc * rpc - xpc * rnc) / XY;
                double Bx = -(ync * zpc - ypc * znc) / XY;
                double By = (xnc * zpc - xpc * znc) / XY;
                double A = Ax * Ax + Ay * Ay - 1;
                double B = Ax * Bx + Ay * By;
                double C = Bx * Bx + By * By;
                double D = B * B - A * C;
                if (D < 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double R1 = (-B - Math.Sqrt(D)) / A;
                double R2 = (-B + Math.Sqrt(D)) / A;

                this.X = Ax * R1 + Bx + curr_circle.X;
                this.Y = Ay * R1 + By + curr_circle.Y;
                this.R = R1 - curr_circle.R;


                // Проверка на правильный обход круга.
                double R;
                if (this.R > 0)
                    R = this.R;
                else
                    R = -this.R;
                double xp = (prev_circle.X - this.X) / (prev_circle.R + R);
                double yp = (prev_circle.Y - this.Y) / (prev_circle.R + R);
                double xc = (curr_circle.X - this.X) / (curr_circle.R + R);
                double yc = (curr_circle.Y - this.Y) / (curr_circle.R + R);
                double xn = (next_circle.X - this.X) / (next_circle.R + R);
                double yn = (next_circle.Y - this.Y) / (next_circle.R + R);
                double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
                if ((this.R > 0 && s < 0) || (this.R < 0 && s > 0))
                {
                    this.X = Ax * R2 + Bx + curr_circle.X;
                    this.Y = Ay * R2 + By + curr_circle.Y;
                    this.R = R2 - curr_circle.R;
                }
            }
            public void Calculate(Circle prev_circle, Circle curr_circle, StripLine next_strip_line)
            {
                double xpc = prev_circle.X - curr_circle.X; // x21
                double ypc = prev_circle.Y - curr_circle.Y; // y21
                double rpc = prev_circle.R - curr_circle.R; // r21
                double zpc = (xpc * xpc + ypc * ypc - rpc * rpc) / 2; // z21

                double xnc = next_strip_line.VX; // x31*
                double ync = next_strip_line.VX; // y31*
                double rnc = -1; // r31*
                double znc = -(next_strip_line.VX * (next_strip_line.PX - curr_circle.X) + next_strip_line.VY * (next_strip_line.PY - curr_circle.X)); // z31*

                double XY = xnc * ypc - xpc * ync;
                if (XY == 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double Ax = (ync * rpc - ypc * rnc) / XY;
                double Ay = -(xnc * rpc - xpc * rnc) / XY;
                double Bx = -(ync * zpc - ypc * znc) / XY;
                double By = (xnc * zpc - xpc * znc) / XY;
                double A = Ax * Ax + Ay * Ay - 1;
                double B = Ax * Bx + Ay * By;
                double C = Bx * Bx + By * By;
                double D = B * B - A * C;
                if (D < 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double R1 = (-B - Math.Sqrt(D)) / A;
                double R2 = (-B + Math.Sqrt(D)) / A;

                this.X = Ax * R1 + Bx + curr_circle.X;
                this.Y = Ay * R1 + By + curr_circle.Y;
                this.R = R1 - curr_circle.R;


                // Проверка на правильный обход круга. //!!!Изменить!!!
                double R;
                if (this.R > 0)
                    R = this.R;
                else
                    R = -this.R;
                double xp = (prev_circle.X - this.X) / (prev_circle.R + R);
                double yp = (prev_circle.Y - this.Y) / (prev_circle.R + R);
                double xc = (curr_circle.X - this.X) / (curr_circle.R + R);
                double yc = (curr_circle.Y - this.Y) / (curr_circle.R + R);
                double xn = next_strip_line.VX;
                double yn = next_strip_line.VY;
                double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
                if ((this.R > 0 && s < 0) || (this.R < 0 && s > 0))
                {
                    this.X = Ax * R2 + Bx + curr_circle.X;
                    this.Y = Ay * R2 + By + curr_circle.Y;
                    this.R = R2 - curr_circle.R;
                }
            }
            public void Calculate(StripLine prev_strip_line, Circle curr_circle, StripLine next_strip_line)
            {
                double xpc = prev_strip_line.VX; // x21
                double ypc = prev_strip_line.VX; // y21
                double rpc = -1; // r21
                double zpc = -(xpc * (prev_strip_line.VX - curr_circle.X) + ypc * (prev_strip_line.VY - curr_circle.Y)); // z21

                double xnc = next_strip_line.VX; // x31
                double ync = next_strip_line.VX; // y31
                double rnc = -1; // r31
                double znc = -(xnc * (next_strip_line.VX - curr_circle.X) + ync * (next_strip_line.VY - curr_circle.Y)); // z31

                double XY = xnc * ypc - xpc * ync;
                if (XY == 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double Ax = (ync * rpc - ypc * rnc) / XY;
                double Ay = -(xnc * rpc - xpc * rnc) / XY;
                double Bx = -(ync * zpc - ypc * znc) / XY;
                double By = (xnc * zpc - xpc * znc) / XY;
                double A = Ax * Ax + Ay * Ay - 1;
                double B = Ax * Bx + Ay * By;
                double C = Bx * Bx + By * By;
                double D = B * B - A * C;
                if (D < 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double R1 = (-B - Math.Sqrt(D)) / A;
                double R2 = (-B + Math.Sqrt(D)) / A;

                this.X = Ax * R1 + Bx + curr_circle.X;
                this.Y = Ay * R1 + By + curr_circle.Y;
                this.R = R1 - curr_circle.R;


                // Проверка на правильный обход круга. //!!!Изменить!!!
                double R;
                if (this.R > 0)
                    R = this.R;
                else
                    R = -this.R;
                double xp = prev_strip_line.VX;
                double yp = prev_strip_line.VY;
                double xc = (curr_circle.X - this.X) / (curr_circle.R + R);
                double yc = (curr_circle.Y - this.Y) / (curr_circle.R + R);
                double xn = next_strip_line.VX;
                double yn = next_strip_line.VX;
                double s = xp * (yc - yn) + xc * (yn - yp) + xn * (yp - yc);
                if ((this.R > 0 && s < 0) || (this.R < 0 && s > 0))
                {
                    this.X = Ax * R2 + Bx + curr_circle.X;
                    this.Y = Ay * R2 + By + curr_circle.Y;
                    this.R = R2 - curr_circle.R;
                }
            }
            public void Calculate(StripLine prev_strip_line, StripLine curr_strip_line, StripLine next_strip_line)
            {
                double xpc = prev_strip_line.VX; // x21
                double ypc = prev_strip_line.VX; // y21
                double rpc = -1; // r21
                double zpc = -(xpc * (prev_strip_line.VX - curr_strip_line.PX) + ypc * (prev_strip_line.VY - curr_strip_line.PY)); // z21

                double xnc = next_strip_line.VX; // x31
                double ync = next_strip_line.VX; // y31
                double rnc = -1; // r31
                double znc = -(xnc * (next_strip_line.VX - curr_strip_line.PX) + ync * (next_strip_line.VY - curr_strip_line.PY)); // z31

                double XY = xnc * ypc - xpc * ync;
                if (XY == 0)
                    return; // Исключительная ситуация!!! Убрать!!!

                double Ax = (ync * rpc - ypc * rnc) / XY;
                double Ay = -(xnc * rpc - xpc * rnc) / XY;
                double Bx = -(ync * zpc - ypc * znc) / XY;
                double By = (xnc * zpc - xpc * znc) / XY;

                double A = 1 - (Ax * curr_strip_line.VX + Ay * curr_strip_line.VY);
                double B = Bx * curr_strip_line.VX + By * curr_strip_line.VX;

                double r = B / A;
                this.X = Ax * r + Bx + curr_strip_line.PX;
                this.Y = Ay * r + By + curr_strip_line.PY;
                // Проверка на правильный обход круга не нужна.
            }

            public new double ExtendedDistance(Circle circle)
            {
                if (double.IsInfinity(this.R))
                {
                    if (circle == null)
                        return 0;
                    return (circle.X - this.X) * vy - (circle.Y - this.Y) * vx - circle.R;
                }
                else
                    if (this.R > 0)
                    {
                        if (circle == null)
                            return double.PositiveInfinity;
                        return Math.Sqrt(Math.Pow(circle.X - this.X, 2.0f) + Math.Pow(circle.Y - this.Y, 2.0f)) - (circle.R + this.R);
                    }
                    else
                    {
                        if (circle == null)
                            return double.NegativeInfinity;
                        return circle.R - this.R - Math.Sqrt(Math.Pow(circle.X - this.X, 2.0f) + Math.Pow(circle.Y - this.Y, 2.0f));
                    }
            }
        }

        internal class DeloneCircleCalculator
        {
            public static DeloneCircle Calculate(Object prev, Object curr, Object next)
            {
                if (prev is Circle)
                    return Calculate_1((Circle)prev, curr, next);
                else
                    if (curr is Circle)
                        return Calculate_1((Circle)curr, next, prev);
                    else
                        if (next is Circle)
                            return Calculate_1((Circle)next, prev, curr);

                DeloneCircle delone_circle = new DeloneCircle();
                //Ошибка, если рассматриваются другие геометрические объекты кроме кругов и прямых линий.
                delone_circle.Calculate((StripLine)prev, (StripLine)curr, (StripLine)next);
                return delone_circle;
            }

            private static DeloneCircle Calculate_1(Circle prev, Object curr, Object next)
            {
                if (curr is Circle)
                    return Calculate_11(prev, (Circle)curr, next);
                else
                    if (next is Circle)
                        return Calculate_11((Circle)next, prev, curr);

                DeloneCircle delone_circle = new DeloneCircle();
                //Ошибка, если рассматриваются другие геометрические объекты кроме кругов и прямых линий.
                delone_circle.Calculate((StripLine)next, prev, (StripLine)curr);
                return delone_circle;
            }

            private static DeloneCircle Calculate_11(Circle prev, Circle curr, Object next)
            {
                DeloneCircle delone_circle = new DeloneCircle();

                //Ошибка, если рассматриваются другие геометрические объекты кроме кругов и прямых линий.
                if (next is Circle)
                    delone_circle.Calculate(prev, curr, (Circle)next);
                else
                    delone_circle.Calculate(prev, curr, (StripLine)next);

                return delone_circle;
            }
        }
    }
}

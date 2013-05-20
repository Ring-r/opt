using System;

namespace Opt
{
    namespace GeometricObjects
    {
        /// <summary>
        /// Круг в двухмерном пространстве.
        /// </summary>
        [Serializable]
        public class Circle : IEquatable<Circle>
        {
            /// <summary>
            /// Радиус.
            /// </summary>
            protected double r;
            /// <summary>
            /// Координата X центра.
            /// </summary>
            protected double x;
            /// <summary>
            /// Координата Y центра.
            /// </summary>
            protected double y;

            #region Circle(...)
            public Circle()
            {
            }
            public Circle(Circle circle)
            {
                this.r = circle.r;
                this.x = circle.x;
                this.y = circle.y;
            }
            #endregion

            /// <summary>
            /// Получает или задаёт радиус.
            /// </summary>
            public double R
            {
                get
                {
                    return r;
                }
                set
                {
                    r = value;
                }
            }
            /// <summary>
            /// Получает или задаёт координату X центра.
            /// </summary>
            public double X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }
            /// <summary>
            /// Получает или задаёт координату Y центра.
            /// </summary>
            public double Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }
            /// <summary>
            /// Получает или задаёт центр как точку.
            /// </summary>
            public Point Center
            {
                get
                {
                    return new Point(x, y);
                }
                set
                {
                    x = value.X;
                    y = value.Y;
                }
            }

            #region Set(...)
            public void Set()
            {
                this.r = 0;
                this.x = 0;
                this.y = 0;
            }
            public void Set(Circle circle)
            {
                this.r = circle.r;
                this.x = circle.x;
                this.y = circle.y;
            }
            public void Set(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public void Set(double r, double x, double y)
            {
                this.r = r;
                this.x = x;
                this.y = y;
            }
            #endregion

            #region Get(...)
            #endregion

            public bool Equals(Circle other)
            {
                return x == other.x && y == other.y;
            }

            #region Дополнительные функции.
            public double ExtendedDistance(Point point)
            {
                return Math.Sqrt((point.X - X) * (point.X - X) + (point.Y - Y) * (point.Y - Y)) - R;
            }
            public double ExtendedDistance(Circle circle)
            {
                if (circle == null)
                    return double.PositiveInfinity;
                return Math.Sqrt((circle.X - X) * (circle.X - X) + (circle.Y - Y) * (circle.Y - Y)) - circle.R - R;
            }
            #endregion

            public override string ToString()
            {
                return "(" + r.ToString() + ";" + x.ToString() + ";" + y.ToString() + ")";
            } // !!Временно!!
        }
    }
}
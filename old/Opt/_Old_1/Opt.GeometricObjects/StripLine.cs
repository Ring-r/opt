using System;

namespace Opt.GeometricObjects
{
    /// <summary>
    /// Прямая линия в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class StripLine : IEquatable<StripLine>
    {
        protected double px;
        protected double py;
        protected double vx;
        protected double vy;

        #region StripLine(...)
        public StripLine()
        {
            this.px = 0;
            this.py = 0;
            this.vx = 0;
            this.vy = 0;
        }
        public StripLine(StripLine strip_line)
        {
            this.px = strip_line.px;
            this.py = strip_line.py;
            this.vx = strip_line.vx;
            this.vy = strip_line.vy;
        }
        public StripLine(Point point_start, Point point_end)
        {
            this.px = point_start.X;
            this.py = point_start.Y;
            this.vx = point_end.X - point_start.X;
            this.vy = point_end.Y - point_start.Y;
        }
        public StripLine(Point point, Vector vector)
        {
            this.px = point.X;
            this.py = point.Y;
            this.vx = vector.X;
            this.vy = vector.Y;
        }
        public StripLine(double point_x, double point_y, double vector_x, double vector_y)
        {
            this.px = point_x;
            this.py = point_y;
            this.vx = vector_x;
            this.vy = vector_y;
        }
        #endregion

        public double PX
        {
            get
            {
                return px;
            }
            set
            {
                px = value;
            }
        }
        public double PY
        {
            get
            {
                return py;
            }
            set
            {
                py = value;
            }
        }
        public Point Point
        {
            get
            {
                return new Point(px, py);
            }
            set
            {
                px = value.X;
                py = value.Y;
            }
        }
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
        public Vector Vector
        {
            get
            {
                return new Vector(vx, vy);
            }
            set
            {
                vx = value.X;
                vy = value.Y;
            }
        }

        #region Set(...)
        public void Set()
        {
            this.px = 0;
            this.py = 0;
            this.vx = 0;
            this.vy = 0;
        }
        public void Set(StripLine strip_line)
        {
            this.px = strip_line.px;
            this.py = strip_line.py;
            this.vx = strip_line.vx;
            this.vy = strip_line.vy;
        }
        public void Set(Point point_start, Point point_end)
        {
            this.px = point_start.X;
            this.py = point_start.Y;
            this.vx = point_end.X - point_start.X;
            this.vy = point_end.Y - point_start.Y;
        }
        public void Set(Point point, Vector vector)
        {
            this.px = point.X;
            this.py = point.Y;
            this.vx = vector.X;
            this.vy = vector.Y;
        }
        public void Set(double point_x, double point_y, double vector_x, double vector_y)
        {
            this.px = point_x;
            this.py = point_y;
            this.vx = vector_x;
            this.vy = vector_y;
        }
        #endregion

        #region Get(...)
        #endregion

        public bool Equals(StripLine other)
        {
            return px == other.px && py == other.py && vx == other.vx && vy == other.vy;
        }

        #region Дополнительные функции.
        public double ExtendedDistance(Point point)
        {
            return (point.X - px) * vy - (point.Y - py) * vx;
        }
        #endregion
    }
}

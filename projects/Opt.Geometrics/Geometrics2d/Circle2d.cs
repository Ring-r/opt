namespace Opt.Geometrics.Geometrics2d
{
    public class Circle2d : Geometric2dWithPointScalar
    {
        public Point2d Center { get { return this.point; } set { this.point = value; } }
        public double X { get { return this.point.X; } set { this.point.X = value; } }
        public double Y { get { return this.point.Y; } set { this.point.Y = value; } }
        public double R { get { return this.scalar; } set { this.scalar = value; } }
    }
}

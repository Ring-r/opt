namespace Opt.Geometrics.Geometrics2d
{
    public class StripLine2d : Plane2d
    {
        public double PX { get { return this.point.X; } set { this.point.X = value; } }
        public double PY { get { return this.point.Y; } set { this.point.Y = value; } }
        public double VX { get { return this.vector.X; } set { this.vector.X = value; } }
        public double VY { get { return this.vector.Y; } set { this.vector.Y = value; } }
    }
}

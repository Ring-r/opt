using Opt.Geometrics.Geometrics2d;

namespace Opt.GeometricObjects.Extending
{
    public class StripLine : Plane2d
    {
        public Point2d Point { get { return this.pole; } set { this.pole = value; } }
        public double PX { get { return this.pole.X; } set { this.pole.X = value; } }
        public double PY { get { return this.pole.Y; } set { this.pole.Y = value; } }
        public double VX { get { return this.vector.X; } set { this.vector.X = value; } }
        public double VY { get { return this.vector.Y; } set { this.vector.Y = value; } }
    }
}

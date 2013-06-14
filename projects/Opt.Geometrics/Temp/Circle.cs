namespace Opt.Geometrics.Geometrics2d.Temp
{
    public class Circle : Geometric2dWithPoleValue
    {
        public Point2d Center { get { return this.pole; } set { this.pole = value; } }
        public double X { get { return this.pole.X; } set { this.pole.X = value; } }
        public double Y { get { return this.pole.Y; } set { this.pole.Y = value; } }
        public double R { get { return this.value; } set { this.value = value; } }
    }
}

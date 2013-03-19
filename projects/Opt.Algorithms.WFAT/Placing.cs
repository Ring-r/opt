using System;

using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms
{
    public abstract class Placing
    {
        protected double height;
        public double Height
        {
            get
            {
                return height;
            }
        }
        protected double length;
        public double Length
        {
            get
            {
                return length;
            }
        }
        protected Circle[] circles;

        protected double eps;

        public Placing(double height, double length, Circle[] circles, double eps)
        {
            this.height = height;
            this.length = length;
            this.circles = circles;

            this.eps = eps;
        }

        protected abstract void Calculate();

        public abstract void CalculateStart();
    }
}

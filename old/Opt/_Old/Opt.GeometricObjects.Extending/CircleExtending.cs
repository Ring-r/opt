using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opt
{
    namespace GeometricObjects
    {
        namespace Extending
        {
            public class CircleExtending
            {
                public static Point PointOfIntersection(Circle circle_i, Circle circle_j, int rotation)
                {
                    Vector vector = new Vector(circle_j.X - circle_i.X, circle_j.Y - circle_i.Y);

                    double d = Math.Sqrt(vector * vector);

                    if (d < Math.Abs(circle_j.R - circle_i.R) || circle_j.R + circle_i.R < d)
                        return null;

                    vector.Divide(d);

                    Vector vector_ = new Vector(-vector.Y, vector.X);

                    double a = (d - (circle_j.R * circle_j.R - circle_i.R * circle_i.R) / d) / 2;
                    double h = Math.Sqrt(circle_i.R * circle_i.R - a * a);

                    vector.Multiply(a);
                    vector_.Multiply(h * rotation);

                    Point point = circle_i.Center;

                    point.Add(vector);
                    point.Add(vector_);

                    return point;
                }
            }
        }
    }
}
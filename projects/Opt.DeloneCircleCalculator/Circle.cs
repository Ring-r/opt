using System;

namespace DeloneCircleCalculator
{
    public class Circle
    {
        /// <summary>
        /// Радиус гиперсферы (radius).
        /// </summary>
        public Double R { get; set; }
        /// <summary>
        /// Центр гиперсферы (position).
        /// </summary>
        public Double[] P { get; set; }

        public Circle(int dimension=2)
        {
            P = new Double[dimension];
        }
    }
}

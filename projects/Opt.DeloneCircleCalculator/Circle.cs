using System;

namespace DeloneCircleCalculator
{
    public class Circle
    {
        /// <summary>
        /// Центр гиперсферы.
        /// </summary>
        public Double[] Point { get; set; }
        /// <summary>
        /// Радиус гиперсферы.
        /// </summary>
        public Double Value { get; set; }

        public Circle(int dimension = 2)
        {
            this.Point = new Double[dimension];
        }
    }
}

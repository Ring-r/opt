using System;

namespace DeloneCircleCalculator
{
    public class Polyplane
    {
        /// <summary>
        /// Точка, задающая полупространство.
        /// </summary>
        public Double[] Point { get; private set; }
        /// <summary>
        /// Вектор нормали, направленный от полупространства.
        /// </summary>
        public Double[] Vector { get; private set; }

        public Polyplane(int dimension)
        {
            this.Point = new Double[dimension];
            this.Vector = new Double[dimension];
        }
    }
}

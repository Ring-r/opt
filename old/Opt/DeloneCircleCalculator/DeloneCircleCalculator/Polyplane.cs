using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloneCircleCalculator
{
    public class Polyplane
    {
        /// <summary>
        /// Точка, задающая полупространство (point, position).
        /// </summary>
        public Double[] P { get; set; }
        /// <summary>
        /// Вектор нормали, направленный от полупространства (normal).
        /// </summary>
        public Double[] N { get; set; }

        public Polyplane(int dimension)
        {
            P = new Double[dimension];
            N = new Double[dimension];
        }
    }
}

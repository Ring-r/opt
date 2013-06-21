using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class SemiinfiniteStrip
    {
        public double b;//ширина полосы
        public double z;//длина полосы
        public int n;//количество многоугольников

        public SemiinfiniteStrip(double b, double z, int n)
        {
            this.b = b;
            this.z = z;
            this.n = n;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class InitialEstimate
    {
        Polygon[] polygons;
        double height;
        double width;

        public InitialEstimate(Polygon[] p, double h, double w)
        {
            this.polygons = p;
            this.height = h;
            this.width = w;
            ChangePolusAndVertices(ref polygons);
        }

        //первый шаг - получение начального приближения
        public void FirstStep()
        {
            double[] X = new double[polygons.Count() * 2 + 1];
            ChangePolusAndVertices(ref polygons);
            for (int i = 0; i < polygons.Count(); i++)
            {
                X[i] = polygons[i].polus[0];
                X[i + 1] = polygons[i].polus[1];
                i++;
            }
            X[polygons.Count() * 2] = polygons[polygons.Count() - 1].polus[0] + polygons[polygons.Count() - 1].MinAndMax[2];
        }

        //выстраиваем многоугольники в полосу
        public void ChangePolusAndVertices(ref Polygon[] polygons)
        {
            polygons[0].polus[0] = -polygons[0].MinAndMax[0];
            polygons[0].polus[1] = -polygons[0].MinAndMax[1];
            for (int i = 1; i < polygons.Count(); i++)
            {
                polygons[i].polus[0] = polygons[i - 1].polus[0] + polygons[i - 1].MinAndMax[2] - polygons[i].MinAndMax[0] + 5;
                polygons[i].polus[1] = -polygons[i].MinAndMax[1];

            }
        }
    }
}

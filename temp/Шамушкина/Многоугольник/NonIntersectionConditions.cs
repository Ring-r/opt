using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class NonIntersectionConditions
    {
        public List<List<int[]>> IntersectionConditions; //хранит разделяющие линии для пары выпуклых многоугольников

        public NonIntersectionConditions(Polygon P1, Polygon P2)
        {
            IntersectionConditions = new List<List<int[]>>();
            FillMatrix(P1, P2);
        }

        //заполняем матрицу ограничений
        private void FillMatrix(Polygon P1, Polygon P2)
        {
            int[] A = new int[2];
            for (int t = 0; t < P1.GetCountOfConvexPoligons(); t++)
            {
                for (int j = 0; j < P2.GetCountOfConvexPoligons(); j++)
                {
                    A = DividingLine(P1.ListOfConvexPoligons[t], P2.ListOfConvexPoligons[j], P1.polus, P1, P2);
                    if (A[0] != -1)
                    {

                        IntersectionConditions.Add(new List<int[]>());

                        IntersectionConditions[IntersectionConditions.Count - 1].Add(new int[3]);
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][0] = P1.number;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][1] = t;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][2] = A[0];

                        IntersectionConditions[IntersectionConditions.Count - 1].Add(new int[3]);
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][0] = P2.number;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][1] = j;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][2] = A[1];
                    }
                    else
                    {
                        A = DividingLine(P2.ListOfConvexPoligons[j], P1.ListOfConvexPoligons[t], P2.polus, P2, P1);
                        IntersectionConditions.Add(new List<int[]>());

                        IntersectionConditions[IntersectionConditions.Count - 1].Add(new int[3]);
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][0] = P2.number;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][1] = j;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][2] = A[0];

                        IntersectionConditions[IntersectionConditions.Count - 1].Add(new int[3]);
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][0] = P1.number;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][1] = t;
                        IntersectionConditions[IntersectionConditions.Count - 1][IntersectionConditions[IntersectionConditions.Count - 1].Count - 1][2] = A[1];
                    }
                }
            }
        }

        //находим разделяющую прямую
        public int[] DividingLine(List<double[]> pol1, List<double[]> pol2, double[] polus1, Polygon P1, Polygon P2)
        {
            double a, b, c;
            int[] A = new int[2];
            A[0] = -1;

            for (int i = 0; i < pol1.Count; i++)
            {
                double[] currentpoint = new double[2];

                double[] x1 = pol1[i];
                double[] x2 = P1.NextElement(pol1[i], P1.ListOfConvexPoligons.IndexOf(pol1));

                bool check = true;
                a = x2[1] - x1[1];
                b = -x2[0] + x1[0];
                c = -(P1.polus[0] + x1[0]) * (x2[1] - x1[1]) + (x2[0] - x1[0]) * (P1.polus[1] + x1[1]);
                //c = -x1[0] * x2[1] + x2[0] * x1[1];
                for (int j = 0; j < pol2.Count; j++)
                {
                    currentpoint[0] = P2.polus[0] + pol2[j][0];
                    currentpoint[1] = P2.polus[1] + pol2[j][1];
                    if (Function(a, b, c, currentpoint) > 0)
                    {
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    A[0]=i;
                    currentpoint[0] = P2.polus[0] + pol2[0][0];
                    currentpoint[1] = P2.polus[1] + pol2[0][1];
                    double min = Math.Abs(Function(a, b, c, currentpoint));
                    int m=0;
                    for (int j = 1; j < pol2.Count; j++)
                    {
                        currentpoint[0] = P2.polus[0] + pol2[j][0];
                        currentpoint[1] = P2.polus[1] + pol2[j][1];
                        if (min > Math.Abs(Function(a, b, c, currentpoint)))
                        {
                            min = Math.Abs(Function(a, b, c, currentpoint));
                            m = j;
                        }

                    }
                    A[1] = m;
                    return A;
                }
            }
            return A;
        }

        //находим функцию по двум точкам
        public double Function(double a, double b, double c, double[] x)
        {
            double result = a * x[0] + b * x[1] + c;
            return result;
        }

        //длина перпендикуляра, опущенного из точки к прямой
        public double PerpendLength(double a, double b, double c, double[] point)
        {
            double d = Math.Abs(a * point[0] + b * point[1] + c) / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            return d;
        }
    }
}

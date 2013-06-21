using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class DoubleChangeCondition
    {
         public int[] NewCond; //хранит разделяющую линию для пары выпуклых многоугольников

        public DoubleChangeCondition(int[] cond, Polygon[] polygons)
        {
            NewCond = new int[2];
            int[] A = DividingLine(cond, polygons);
            if (A[0] != -1)
            {
                NewCond = A;
            }
            else
            {

                int ll = cond[0];
                cond[0] = cond[3];
                cond[3] = ll;

                ll = cond[1];
                cond[1] = cond[4];
                cond[4] = ll;

                A = DividingLine1(cond, polygons);

                NewCond = A;
            }
        }
        public int[] DividingLine(int[] cond, Polygon[] polygons)
        {
            double a, b, c;
            int[] A = new int[2];
            A[0] = -1;
            List<double[]> pol1 = polygons[cond[0]].ListOfConvexPoligons[cond[1]];
            List<double[]> pol2 = polygons[cond[3]].ListOfConvexPoligons[cond[4]];

            for (int i = 0; i < pol1.Count; i++)
            {
                if (i != cond[2])
                {
                    double[] currentpoint = new double[2];

                    double[] x1 = pol1[i];
                    double[] x2 = polygons[cond[0]].NextElement(pol1[i], cond[1]);

                    bool check = true;
                    a = x1[1] - x2[1];
                    b = -x1[0] + x2[0];
                    c = -(polygons[cond[0]].polus[0] + x2[0]) * (x1[1] - x2[1]) + (x1[0] - x2[0]) * (polygons[cond[0]].polus[1] + x2[1]);
                    //c = -x1[0] * x2[1] + x2[0] * x1[1];
                    for (int j = 0; j < pol2.Count; j++)
                    {
                        currentpoint[0] = polygons[cond[3]].polus[0] + pol2[j][0];
                        currentpoint[1] = polygons[cond[3]].polus[1] + pol2[j][1];
                        if (Function(a, b, c, currentpoint) > 0)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        A[0] = i;
                        currentpoint[0] = polygons[cond[3]].polus[0] + pol2[0][0];
                        currentpoint[1] = polygons[cond[3]].polus[1] + pol2[0][1];
                        double min = Math.Abs(Function(a, b, c, currentpoint));
                        int m = 0;
                        for (int j = 1; j < pol2.Count; j++)
                        {
                            currentpoint[0] = polygons[cond[3]].polus[0] + pol2[j][0];
                            currentpoint[1] = polygons[cond[3]].polus[1] + pol2[j][1];
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
            }

            return A;
        }
        public int[] DividingLine1(int[] cond, Polygon[] polygons)
        {
            double a, b, c;
            int[] A = new int[2];
            A[0] = -1;
            List<double[]> pol1 = polygons[cond[0]].ListOfConvexPoligons[cond[1]];
            List<double[]> pol2 = polygons[cond[3]].ListOfConvexPoligons[cond[4]];

            for (int i = 0; i < pol1.Count; i++)
            {
                double[] currentpoint = new double[2];

                double[] x1 = pol1[i];
                double[] x2 = polygons[cond[0]].NextElement(pol1[i], cond[1]);

                bool check = true;
                a = x1[1] - x2[1];
                b = -x1[0] + x2[0];
                c = -(polygons[cond[0]].polus[0] + x2[0]) * (x1[1] - x2[1]) + (x1[0] - x2[0]) * (polygons[cond[0]].polus[1] + x2[1]);
                //c = -x1[0] * x2[1] + x2[0] * x1[1];
                for (int j = 0; j < pol2.Count; j++)
                {
                    currentpoint[0] = polygons[cond[3]].polus[0] + pol2[j][0];
                    currentpoint[1] = polygons[cond[3]].polus[1] + pol2[j][1];
                    if (Function(a, b, c, currentpoint) > 0)
                    {
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    A[0] = i;
                    currentpoint[0] = polygons[cond[3]].polus[0] + pol2[0][0];
                    currentpoint[1] = polygons[cond[3]].polus[1] + pol2[0][1];
                    double min = Math.Abs(Function(a, b, c, currentpoint));
                    int m = 0;
                    for (int j = 1; j < pol2.Count; j++)
                    {
                        currentpoint[0] = polygons[cond[3]].polus[0] + pol2[j][0];
                        currentpoint[1] = polygons[cond[3]].polus[1] + pol2[j][1];
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Многоугольник
{
    class Triangulation
    {
        List<List<double[]>> Triangles = new List<List<double[]>>();//список будет содержать полученные треугольники
        List<int> A;//список смежности, сoдержащий последовательность вершин
     
        //List<double[]> ListPoints;//список точек
        List<double[]> CopyListPoints;//список точек

        public Triangulation() { ;}

        public Triangulation(List<double []>lp1)
        {
            //this.ListPoints = lp;
            this.CopyListPoints = lp1;
            A = new List<int>();
           
            for (int i = 0; i < lp1.Count-1; i++)
            {
                A.Add(i + 1);
            }
            A.Add(0);
           
            
        }

        //скалярное произведение
        private double ScalarProduct(double[] a, double[] b, double[] c)
        {
            return ((c[0] - b[0]) * (a[0] - b[0]) + (c[1] - b[1]) * (a[1] - b[1]));
        }

        //векторное произведение
        protected double VectorProduct(double[] a, double[] b, double[] c)
        {
            return (a[0] * b[1] - a[0] * c[1] - b[0] * a[1] + b[0] * c[1] + c[0] * a[1] - c[0] * b[1]);
        }

        //функция определяет принадлежность точки треугольнику
        private bool IsPointBelongsToTriangle(double[] p, double[] p1, double[] p2, double[] p3)
        {
            double a = VectorProduct(p, p1, p2);
            double b = VectorProduct(p, p2, p3);
            double c = VectorProduct(p, p3, p1);
            //проверяем на однонаправленность вектора
            return (a > 0 && b > 0 && c > 0 || a < 0 && b < 0 && c < 0 || a == 0 && b == 0 && c == 0);
        }

        //функция изменения списка смежности
        private void ChangeList(double[] p1, double[] p3)
        {
            if ((CopyListPoints.IndexOf(p3) != 0) || (A[CopyListPoints.IndexOf(p3)] != 1))
                for (int i = CopyListPoints.IndexOf(p3); (i < CopyListPoints.Count - 1); i++)
                {
                    A[i] = A[i] - 1;
                }
            A[CopyListPoints.Count - 1] = 0;
        }

        //триангуляция
        public List<List<double[]>> StartTriangulation()
        {
            int t = 0;

            //Direction();

            double[] p1, p2, p3;
            p1 = CopyListPoints[t];
            p2 = CopyListPoints[t + 1];
            p3 = CopyListPoints[t + 2];
            while (A.Count != 3)
            {
                if (VectorProduct(p1, p2, p3) <= 0)//проверяем, образуют ли вектора правую тройку
                {
                    bool check = true;
                    //поверяем, не попала ли вершина в треугольник
                    for (int i = 0; i < CopyListPoints.IndexOf(p1); i++)
                    {
                        if (IsPointBelongsToTriangle(CopyListPoints[i], p1, p2, p3))
                        {
                            check = false;
                            p1 = CopyListPoints[A[CopyListPoints.IndexOf(p1)]];
                            p2 = CopyListPoints[A[CopyListPoints.IndexOf(p2)]];
                            p3 = CopyListPoints[A[CopyListPoints.IndexOf(p3)]];
                            break;
                        }

                    }
                    //продолжение предыдущей проверки
                    for (int i = CopyListPoints.IndexOf(p3) + 1; i < CopyListPoints.Count; i++)
                    {
                        if (IsPointBelongsToTriangle(CopyListPoints[i], p1, p2, p3))
                        {
                            check = false;
                            p1 = CopyListPoints[A[CopyListPoints.IndexOf(p1)]];
                            p2 = CopyListPoints[A[CopyListPoints.IndexOf(p2)]];
                            p3 = CopyListPoints[A[CopyListPoints.IndexOf(p3)]];
                            break;
                        }
                    }
                    if (check == true)
                    {
                        //добавляем новый треугольник
                        Triangles.Add(new List<double[]>());
                        Triangles[Triangles.Count - 1].Add(new double[2]);
                        Triangles[Triangles.Count - 1][0] = p1;
                        Triangles[Triangles.Count - 1].Add(new double[2]);
                        Triangles[Triangles.Count - 1][1] = p2;
                        Triangles[Triangles.Count - 1].Add(new double[2]);
                        Triangles[Triangles.Count - 1][2] = p3;
                        A.RemoveAt(CopyListPoints.IndexOf(p2));

                        CopyListPoints.Remove(p2);

                        ChangeList(p1, p3);

                        p2 = p3;
                        p3 = CopyListPoints[A[CopyListPoints.IndexOf(p2)]];
                    }
                }
                else // вектора не образуют правую тройку, делаем сдвиг
                {
                    p1 = CopyListPoints[A[CopyListPoints.IndexOf(p1)]];
                    p2 = CopyListPoints[A[CopyListPoints.IndexOf(p2)]];
                    p3 = CopyListPoints[A[CopyListPoints.IndexOf(p3)]];
                }
            }
            //добавляем оставшиеся три вершины
            Triangles.Add(new List<double[]>());
            Triangles[Triangles.Count - 1].Add(new double[2]);
            Triangles[Triangles.Count - 1][0] = CopyListPoints[0];
            Triangles[Triangles.Count - 1].Add(new double[2]);
            Triangles[Triangles.Count - 1][1] = CopyListPoints[1];
            Triangles[Triangles.Count - 1].Add(new double[2]);
            Triangles[Triangles.Count - 1][2] = CopyListPoints[2];

            return Triangles;
        }
    }
}

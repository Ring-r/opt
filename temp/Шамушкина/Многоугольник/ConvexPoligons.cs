using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class ConvexPoligons:Triangulation
    {
        List<List<double[]>> Triangles;
        List<double[]> ListPoints;

        public ConvexPoligons() { ;}

        public ConvexPoligons(List<List<double[]>> tr, List<double[]> lp)
        {
            this.Triangles = tr;
            this.ListPoints = lp;
        }

        //находим треугольники с общей стороной
        public List<double[]> CommomEdge(List<double[]> p, List<double[]> Triangle)
        {
            List<double[]> p1 = new List<double[]>();
            for (int j = 0; j < 3; j++)
            {
                bool check = false;
                for (int l = 0; l < p.Count; l++)
                {
                    if (Triangle[j] == p[l])
                    {
                        check = true;
                    }
                }
                if (check == false)
                {

                    p1.Add(new double[2]);
                    p1[p1.Count - 1] = Triangle[j];
                }
            }
            return p1;
        }

        //объеденяем треугольники
        private int UnionOfTriangles(int i, List<double[]> Triangle, ref List<List<double[]>> Poligon)
        {
            int k = -1;
            for (int j = 0; j < Triangles.Count; j++)
            {
               
                List<double[]> p1 = CommomEdge(Triangle, Triangles[j]);
                if (p1.Count == 1)
                {
                    Poligon[Poligon.Count - 1].Add(p1[0]);
                    k = j;

                    break;
                }
            }

            return k;
        }

        //сортируем вершины полученного многоугольника многоугольника
        private void SortVertices(List<double[]> Poligon)
        {
            double[] c = new double[2];
            double[] m = new double[2];

            for (int i = 0; i < Poligon.Count; i++)
            {
                for (int j = 0; j < Poligon.Count-1; j++)
                {
                    c = Poligon[j];
                    int k = ListPoints.IndexOf(c);
                    if ((ListPoints.IndexOf(Poligon[j])) > (ListPoints.IndexOf(Poligon[j + 1])))
                    {
                        c = Poligon[j];
                        Poligon[j] = Poligon[j + 1];
                        Poligon[j + 1] = c;
                    }
                }
            }
        }

        //проверяем на выпуклость полученный многоугольник
        public bool CheckOnConvex(List<List<double[]>> Poligons)
        {
            bool check = true;
            for (int j = 0; j < Poligons[Poligons.Count - 1].Count - 2; j++)
            {
                if (VectorProduct(Poligons[Poligons.Count - 1][j], Poligons[Poligons.Count - 1][j + 1], Poligons[Poligons.Count - 1][j + 2]) > 0)
                {
                    return check = false;
                }
            }
            if (VectorProduct(Poligons[Poligons.Count - 1][Poligons[Poligons.Count - 1].Count - 2], Poligons[Poligons.Count - 1][Poligons[Poligons.Count - 1].Count - 1], Poligons[Poligons.Count - 1][0]) > 0)
                return check = false;

            if (VectorProduct(Poligons[Poligons.Count - 1][Poligons[Poligons.Count - 1].Count - 1], Poligons[Poligons.Count - 1][0], Poligons[Poligons.Count - 1][1]) > 0)
                return check = false;

            return check;
        }

        //находим выпуклые многоугольники
        public List<List<double[]>> TrianglesConnection()
        {
            List<List<double[]>> Poligons = new List<List<double[]>>();

            while (Triangles.Count >0)
            {
                int k = 0;
                bool check=true;

                Poligons.Add(new List<double[]>());
                Poligons[Poligons.Count - 1].Add(Triangles[0][0]);
                Poligons[Poligons.Count - 1].Add(Triangles[0][1]);
                Poligons[Poligons.Count - 1].Add(Triangles[0][2]);
                int stop = 4;
                int stop1 = Poligons[Poligons.Count - 1].Count;

                while ((check != false) && (k != -1))
                {
                    //если многоугольник выпуклый, то продолжаем добавление
                    Triangles.RemoveAt(k);
                    k = UnionOfTriangles(0, Poligons[Poligons.Count - 1], ref Poligons);
                    //SortVertices(Poligons[Poligons.Count - 1]);
                    stop = Poligons[Poligons.Count - 1].Count;
                    check = CheckOnConvex(Poligons);
                }
                if (check ==false)
                {
                    Poligons[Poligons.Count - 1].RemoveAt(Poligons[Poligons.Count - 1].Count - 1);
                }
               
            }
          
            return Poligons;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class Polygon: ConvexPoligons
    {
        public List<double[]> Vertices; //вершины многоугольника
        public List<double[]> CopyVertices; //вершины многоугольника
        public List<List<double[]>> ListOfConvexPoligons;//список выпуклых многоугольников
        public double[] polus; // полюс многоугольника
        public double[] prevPolus;
        public int number;
        public double[] MinAndMax;


        public Polygon() { ;}

        public Polygon(List<double[]> Vertices, List<double[]> CopyVertices, int number, double[] p)
        {
            this.Vertices = Vertices; //вершины многоугольника
            this.CopyVertices = CopyVertices;
            this.polus = p;
            FindListOfConvexPoligons();
            this.number = number;
            MinAndMax = FindMinAndMax();
        }

        //находим максимальные и минимальные координаты
        public double[] FindMinAndMax()
        {
            double[] a = new double[4];
            a[0] = FindMinX();
            a[1] = FindMinY();
            a[2] = FindMaxX();
            a[3] = FindMaxY();
            return a;
        }

        public double FindMinX()
        {
            double min = Vertices[0][0];
            for (int i = 1; i < Vertices.Count; i++)
            {
                if (min > Vertices[i][0])
                {
                    min = Vertices[i][0];
                }
            }
            return min;
        }

        public double FindMaxX()
        {
            double max = Vertices[0][0];
            for (int i = 1; i < Vertices.Count; i++)
            {
                if (max < Vertices[i][0])
                {
                    max = Vertices[i][0];
                }
            }
            return max;
        }

        public double FindMinY()
        {

            double min = Vertices[0][1];
            for (int i = 1; i < Vertices.Count; i++)
            {
                if (min > Vertices[i][1])
                {
                    min = Vertices[i][1];
                }
            }
            return min;
        }

        public double FindMaxY()
        {
            double max = Vertices[0][1];
            for (int i = 1; i < Vertices.Count; i++)
            {
                if (max < Vertices[i][1])
                {
                    max = Vertices[i][1];
                }
            }
            return max;
        }
        //находим полюс многоугольника
        public void FindPole(ref double[] p)
        {
            double sum1 = 0, sum2 = 0;
            for (int i = 0; i < Vertices.Count; i++)
            {
                sum1 += Vertices[i][0];
                sum2 += Vertices[i][1];
            }
            p[0] = (int) sum1 / Vertices.Count; //координата x полюса
            p[1] = (int) sum2 / Vertices.Count; //координата у полюса
        }

        //находим вершину по индексу
        public double[] FindVertexByIndex(int i)
        {
            return Vertices.ElementAt(i);
        }

        //находим индекс по заданной вершине
        public int FindIndexOfElement(double[] p)
        {
            return Vertices.IndexOf(p);
        }

        //находим индекс по заданной одной координате вершины
        public int FindIndexOfElement(double p, int k)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i][k] == p)
                    return i;
            }
            return -1;
        }

        //трансляция на вектор
        public void TranslateToVector(double[] p)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i][0] = Vertices[i][0] + p[0];
                Vertices[i][1] = Vertices[i][1] + p[1];
            }
        }

        //следующий элемент
        public double[] NextElement(double [] p)
        {
            double[] p1;
            if (Vertices.IndexOf(p) != Vertices.Count)
                p1 = Vertices[Vertices.IndexOf(p) + 1];
            else p1 = Vertices[0];
            return p1;
        }

        //следующий элемент в выпуклом многоугольнике
        public double[] NextElement(double[] p, int i)
        {
            double[] p1;
            int k = ListOfConvexPoligons[i].IndexOf(p);
            if (k != ListOfConvexPoligons[i].Count - 1)
                p1 = ListOfConvexPoligons[i][k + 1];
            else p1 = ListOfConvexPoligons[i][0];
            return p1;
        }

        //находим элемент в выпуклом многоугольнике
        public double[] FindElementInConvexPolygon(int indexOfElement, int indexOfPolygon)
        {
            double[] p1 = ListOfConvexPoligons[indexOfPolygon].ElementAt(indexOfElement);
            return p1;
        }

        //находим список выпуклых многоугольников
        public void FindListOfConvexPoligons()
        {
            Triangulation tr = new Triangulation(CopyVertices); //объявляем переменную класса Triangulation
            List<List<double[]>> Triangles = tr.StartTriangulation(); //начинаем триангуляцию
            ConvexPoligons cp = new ConvexPoligons(Triangles, Vertices); //объявляем переменную класса ConvexPoligons
            ListOfConvexPoligons = cp.TrianglesConnection();//создаём набор невыпуклых многоугольников
        }

        //получить выпуклый многоугольник по номеру
        public List<double[]> GetConvexpoligon(int i)
        {
            return ListOfConvexPoligons.ElementAt(i);
        }

        //получить колличество выпуклых многоугольников
        public int GetCountOfConvexPoligons()
        {
            return ListOfConvexPoligons.Count;
        }
    }
}

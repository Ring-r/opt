using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Многоугольник
{
    class GradientProjectionMethod
    {
        // Метод локального поиска.
        // Глобальные описания для метода локального поиска
        double eps = 1e-8;
        double eps1 = 1e-4;
        double eps2 = 1e-4;
        //StreamWriter sw = new StreamWriter("Out.txt", false);
        Matrix P; // текущее значение переменных
        Matrix G; // вектор градиента 
        Matrix A; // матрица активных ограничений
        Matrix B = new Matrix(0, 1); // номера объектов в матрице А
        bool[] wl; // рабочий список: номера ограничений, входящих в А
        //int NumStep = 0; // номер шага
        //bool is_exit = false;
        double height;
        double width;

        Polygon[] polygons;
        NonIntersectionConditions cond;
        SemiinfiniteStrip st;
        List<int[]> IntersCond = new List<int[]>();

        public GradientProjectionMethod(Polygon[] pol, SemiinfiniteStrip s, double h, double w)
        {
            this.polygons = pol;
            this.st = s;
            this.height = h;
            this.width = w;
        }

        public void Step_0()
        {
            for (int i = 0; i < polygons.Count(); i++)
            {
                for (int t = 0; t < polygons.Count(); t++)
                {
                    if ((t != i) && (i < t))
                    {
                        cond = new NonIntersectionConditions(polygons[i], polygons[t]);
                        for (int j = 0; j < cond.IntersectionConditions.Count(); j++)
                        {
                            IntersCond.Add(new int[6]);
                            for (int k = 0; k < 3; k++)
                            {
                                IntersCond[IntersCond.Count - 1][k] = cond.IntersectionConditions[j][0][k];
                                IntersCond[IntersCond.Count - 1][k + 3] = cond.IntersectionConditions[j][1][k];
                            }
                        }
                    }
                }

            }
            int[] indexes = { 0, 1, 0, 1 };
            for (int i = 0; i < polygons.Count(); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    IntersCond.Add(new int[6]);
                    IntersCond[IntersCond.Count - 1][0] = -1;
                    IntersCond[IntersCond.Count - 1][1] = -1;
                    IntersCond[IntersCond.Count - 1][2] = j + 1;
                    IntersCond[IntersCond.Count - 1][3] = i;
                    IntersCond[IntersCond.Count - 1][4] = -1;
                    IntersCond[IntersCond.Count - 1][5] = j;
                }
            }
            // InitialEstimate ie = new InitialEstimate(polygons, height, width);
            int n = polygons.Count();//количество многоугольников
            P = new Matrix(2 * n + 1);//текущее значение переменных
            double max = polygons[0].MinAndMax[2] + polygons[0].polus[0];
            for (int i = 1; i < polygons.Count(); i++)
            {
                if (max < polygons[i].MinAndMax[2] + polygons[i].polus[0])
                    max = polygons[i].MinAndMax[2] + polygons[i].polus[0];
            }
            for (int i = 0; i < polygons.Count(); i++)
            {
                P[2 * i] = polygons[i].polus[0];
                P[2 * i + 1] = polygons[i].polus[1];
            }

            st.z = max;
            P[2 * n] = st.z;
            
            //создание вектора градиента
            G = new Matrix(2 * n + 1);
            G[G.RowCount - 1] = 1;
            //создание и заполнение матрицы активных ограничений
            A = new Matrix(0, 2 * n + 1);
            wl = new bool[IntersCond.Count];

            double[] currentpoint = new double[2];
            ////активные ограничения по полосе и между многоугольниками
            for (int i = 0; i < IntersCond.Count; i++)
            {
                if (IntersCond[i][0] != -1)
                {
                    double[] polus2 = polygons[IntersCond[i][3]].polus;
                    double[] polus1 = polygons[IntersCond[i][0]].polus;

                    currentpoint[0] = polus2[0] - polus1[0];
                    currentpoint[1] = polus2[1] - polus1[1];

                    double[] d = polygons[IntersCond[i][3]].FindElementInConvexPolygon(IntersCond[i][5], IntersCond[i][4]);
                    
                    double[] koef = Koefficients(IntersCond[i], d);

                    double a = koef[0];
                    double b = koef[1];
                    double c = koef[2];


                    if (Function(a,b,c, currentpoint) == 0)
                    {
                        A.AddRow();
                        A[A.RowCount - 1, IntersCond[i][0] * 2] = a;
                        A[A.RowCount - 1, IntersCond[i][0] * 2 + 1] = b;

                        A[A.RowCount - 1, IntersCond[i][3] * 2] = -a;
                        A[A.RowCount - 1, IntersCond[i][3] * 2 + 1] = -b;

                        B.AddRow();
                        B[B.RowCount - 1, 0] = i;
                        wl[i] = true;
                    }
                }
                else
                {
                    switch (IntersCond[i][2])
                    {
                        case 1:
                            if (LeftBorder(polygons[IntersCond[i][3]].polus, polygons[IntersCond[i][3]].MinAndMax[0]) == 0)
                            {
                                A.AddRow();
                                A[A.RowCount - 1, IntersCond[i][3] * 2] = -1;
                                B.AddRow();
                                B[B.RowCount - 1, 0] = i;
                                wl[i] = true;
                            }
                            break;
                        case 2:
                            if (DownBorder(polygons[IntersCond[i][3]].polus, polygons[IntersCond[i][3]].MinAndMax[1]) == 0)
                            {
                                A.AddRow();
                                A[A.RowCount - 1, IntersCond[i][3] * 2 + 1] = -1;
                                B.AddRow();
                                B[B.RowCount - 1, 0] = i;
                                wl[i] = true;
                            }
                            break;
                        case 3:
                            if (RightBorder(polygons[IntersCond[i][3]].polus, polygons[IntersCond[i][3]].MinAndMax[2]) == 0)
                            {
                                A.AddRow();
                                A[A.RowCount - 1, IntersCond[i][3] * 2] = 1;
                                A[A.RowCount - 1, 2 * n] = -1;
                                B.AddRow();
                                B[B.RowCount - 1, 0] = i;
                                wl[i] = true;
                            }
                            break;
                        case 4:
                            if (UpperBorder(polygons[IntersCond[i][3]].polus, polygons[IntersCond[i][3]].MinAndMax[3]) == 0)
                            {
                                A.AddRow();
                                A[A.RowCount - 1, IntersCond[i][3] * 2 + 1] = 1;
                                B.AddRow();
                                B[B.RowCount - 1, 0] = i;
                                wl[i] = true;
                            }
                            break;
                    }
                }
            }

        }

        public bool StepLocal()
        {
            int n = polygons.Count();
            Matrix ATr = A.Tr();
            Matrix AOb = (A * ATr).Ob();
            Matrix U = AOb * A * G;//Рассчитать вектор множетелей Лагранжа U
            //Получить вектор направления d (2*n+1 x 1).
            Matrix D = ATr * U - G;
            if (D.IsNull(eps))
            {

                bool check1 = false;
                double d_max = eps;
                for (int i = 0; i < A.RowCount; i++)
                {
                    if (IntersCond[(int)B[i]][0] != -1)
                    {
                        bool check = CheckCond(IntersCond[(int)B[i]]);
                        if (check == false)
                        {
                            ChangeIntersectionCondition cc = new ChangeIntersectionCondition(IntersCond[(int)B[i]], polygons);

                            if (cc.NewCond[0] != -1)
                            {
                                IntersCond[(int)B[i]][2] = cc.NewCond[0];
                                IntersCond[(int)B[i]][5] = cc.NewCond[1];

                                wl[(int)B[i]] = false;
                                A.DelRow(i);
                                B.DelRow(i);
                                i--;
                                check1 = true;
                            }
                            else
                            {
                                DoubleChangeCondition dc = new DoubleChangeCondition(IntersCond[(int)B[i]], polygons);
                                IntersCond[(int)B[i]][2] = dc.NewCond[0];
                                IntersCond[(int)B[i]][5] = dc.NewCond[1];

                                wl[(int)B[i]] = false;
                                A.DelRow(i);
                                B.DelRow(i);
                                i--;
                                check1 = true;
                            }
                        }
                    }
                }
                if (!check1)
                {
                    //int index = U.RowOfMinElement();
                    //if (U[index] < -eps1)
                    int index = U.RowOfMaxElement();
                    if (U[index] > eps1)
                    {

                        wl[(int)B[index, 0]] = false;
                        A.DelRow(index);
                        B.DelRow(index);
                    }
                    else
                    {
                        //MessageBox.Show("Минимум найден");
                        return false;
                    }
                }
            }
            else
            {
                // Находим длину шага L и соответствующее ему ограничение, которое добавляем в A.
                double L = double.PositiveInfinity;

                Matrix T;
                double td;
                double l;
                Matrix TNew = new Matrix(1, 2 * n + 1); // добавляемое ограничение

                //добавляем ограничение

                Matrix LamdaMax = new Matrix(0, 1);
                Matrix CopyB = new Matrix(0, 1);
                Matrix CopyA = new Matrix(0, 2 * polygons.Count() + 1);
               
                Matrix B1 = new Matrix(1);
                Matrix A1Tr;
                Matrix compare;
                Matrix num;
                Matrix den;

                for (int i = 0; i < wl.Count(); i++)
                {
                    if (!wl[i])
                    {


                        if (IntersCond[i][0] != -1)
                        {


                            double[] polus2 = polygons[IntersCond[i][3]].polus;


                            double[] d = polygons[IntersCond[i][3]].FindElementInConvexPolygon(IntersCond[i][5], IntersCond[i][4]);
                           
                            double[] koef = Koefficients(IntersCond[i], d);

                            double a = koef[0];
                            double b = koef[1];
                            double c = koef[2];

                            Matrix A1 = new Matrix(2 * polygons.Count() + 1);

                            A1[IntersCond[i][0] * 2] = a;
                            A1[IntersCond[i][0] * 2 + 1] = b;

                            A1[IntersCond[i][3] * 2] = -a;
                            A1[IntersCond[i][3] * 2 + 1] = -b;

                            B1[0] = c;

                            A1Tr = A1.Tr();

                            compare = A1Tr * D;

                            if ((compare[0] > 0)&&(compare[0]>eps2))
                            {
                                LamdaMax.AddRow();
                                num = -A1Tr * P + B1;
                                if (num[0] < eps2)
                                    num[0] = 0;
                                den = compare;
                                Matrix lamda = new Matrix(1);
                                lamda[0] = num[0] / den[0];
                                LamdaMax[LamdaMax.RowCount - 1] = lamda[0];

                                CopyB.AddRow();
                                CopyB[CopyB.RowCount - 1] = i;
                                CopyA.AddRow();
                                CopyA[CopyA.RowCount - 1, IntersCond[i][0] * 2] = a;
                                CopyA[CopyA.RowCount - 1, IntersCond[i][0] * 2 + 1] = b;
                                CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2] = -a;
                                CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2 + 1] = -b;

                            }


                        }
                        else
                        {
                            Matrix A1 = new Matrix(2 * polygons.Count() + 1);
                            switch (IntersCond[i][2])
                            {
                                case 1:


                                    A1[IntersCond[i][3] * 2] = -1;
                                    B1[0] = polygons[IntersCond[i][3]].MinAndMax[IntersCond[i][5]];
                                    A1Tr = A1.Tr();

                                    compare = A1Tr * D;

                                    if ((compare[0] > 0) && (compare[0] > eps2))
                                    {
                                        LamdaMax.AddRow();
                                        num = -A1Tr * P + B1;
                                        if (num[0] < eps2)
                                            num[0] = 0;
                                
                                        den = compare;
                                        Matrix lamda = new Matrix(1);
                                        lamda[0] = num[0] / den[0];
                                        LamdaMax[LamdaMax.RowCount - 1] = lamda[0];

                                        CopyB.AddRow();
                                        CopyB[CopyB.RowCount - 1] = i;
                                        CopyA.AddRow();
                                        CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2] = -1;
                                    }

                                    break;

                                case 2:

                                    A1[IntersCond[i][3] * 2 + 1] = -1;
                                    B1[0] = polygons[IntersCond[i][3]].MinAndMax[IntersCond[i][5]];
                                    A1Tr = A1.Tr();

                                    compare = A1Tr * D;

                                    if ((compare[0] > 0) && (compare[0] > eps2))
                                    {
                                        LamdaMax.AddRow();
                                        num = -A1Tr * P + B1;

                                        if (num[0] < eps2)
                                            num[0] = 0;
                                        
                                        den = compare;
                                        Matrix lamda = new Matrix(1);
                                        lamda[0] = num[0] / den[0];
                                        LamdaMax[LamdaMax.RowCount - 1] = lamda[0];

                                        CopyB.AddRow();
                                        CopyB[CopyB.RowCount - 1] = i;

                                        CopyA.AddRow();
                                        CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2 + 1] = -1;
                                    }

                                    break;

                                case 3:

                                    A1[IntersCond[i][3] * 2] = 1;
                                    A1[2 * n] = -1;
                                    B1[0] = -polygons[IntersCond[i][3]].MinAndMax[IntersCond[i][5]];
                                    A1Tr = A1.Tr();

                                    compare = A1Tr * D;

                                    if ((compare[0] > 0) && (compare[0] > eps2))
                                    {
                                        LamdaMax.AddRow();
                                        num = -A1Tr * P + B1;
                                        if (num[0] < eps2)
                                            num[0] = 0;
                                
                                        den = compare;
                                        Matrix lamda = new Matrix(1);
                                        lamda[0] = num[0] / den[0];
                                        LamdaMax[LamdaMax.RowCount - 1] = lamda[0];

                                        CopyB.AddRow();
                                        CopyB[CopyB.RowCount - 1] = i;

                                        CopyA.AddRow();
                                        CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2] = 1;
                                        CopyA[CopyA.RowCount - 1, 2 * n] = -1;
                                    }

                                    break;

                                case 4:

                                    A1[IntersCond[i][3] * 2 + 1] = 1;
                                    B1[0] = -polygons[IntersCond[i][3]].MinAndMax[IntersCond[i][5]] + st.b;
                                    A1Tr = A1.Tr();

                                    compare = A1Tr * D;

                                    if ((compare[0] > 0) && (compare[0] > eps2))
                                    {
                                        LamdaMax.AddRow();
                                        num = -A1Tr * P + B1;
                                        if (num[0] < eps2)
                                            num[0] = 0;
                                
                                        den = compare;
                                        Matrix lamda = new Matrix(1);
                                        lamda[0] = num[0] / den[0];
                                        LamdaMax[LamdaMax.RowCount - 1] = lamda[0];

                                        CopyB.AddRow();
                                        CopyB[CopyB.RowCount - 1] = i;

                                        CopyA.AddRow();
                                        CopyA[CopyA.RowCount - 1, IntersCond[i][3] * 2 + 1] = 1;
                                    }
                                    break;

                            }
                        }
                    }
                }

                if (LamdaMax.RowCount != 0)
                {
                    A.AddRow();
                    B.AddRow();
                    L = LamdaMax[LamdaMax.RowOfMinElement()];
                    P += D * L;
                    st.z = P[2 * n];
                    for (int i = 0; i < polygons.Count(); i++)
                    {
                        polygons[i].polus[0] = P[i * 2];
                        polygons[i].polus[1] = P[i * 2 + 1];
                    }
                    //Добавление в матрицу нового ограничения
                    B[B.RowCount - 1] = CopyB[LamdaMax.RowOfMinElement()];
                    for (int i = 0; i < CopyA.ColCount; i++)
                    {
                        A[A.RowCount - 1, i] = CopyA[LamdaMax.RowOfMinElement(), i];
                    }

                }
            }

            return true;

        }

      
        //проверяем ограничения непересечения на возможность их перерасчёта
        public bool CheckCond(int[] cond)
        {
            bool check = true;

            double[] polus1 = polygons[cond[0]].polus;
            double[] polus2 = polygons[cond[3]].polus;

            List<double[]> P1 = polygons[cond[0]].ListOfConvexPoligons[cond[1]];
            List<double[]> P2 = polygons[cond[3]].ListOfConvexPoligons[cond[4]];

            double[] x = polygons[cond[0]].FindElementInConvexPolygon(cond[2], cond[1]);
            double[] x2 = new double[2];
            x2[0] = polus1[0] + polygons[cond[0]].NextElement(x, cond[1])[0];
            x2[1] = polus1[1] + polygons[cond[0]].NextElement(x, cond[1])[1];

            double[] x1 = new double[2];
            x1[0] = polus1[0] + x[0];
            x1[1] = polus1[1] + x[1];

            double[] d = new double[2];
            d[0] = polus2[0] + polygons[cond[3]].FindElementInConvexPolygon(cond[5], cond[4])[0];
            d[1] = polus2[1] + polygons[cond[3]].FindElementInConvexPolygon(cond[5], cond[4])[1];

            double[] koef = new double[3];
            koef[0] = x1[1] - x2[1];
            koef[1] = -x1[0] + x2[0];
            koef[2] = -x2[0] * x1[1] + x2[1] * x1[0];

            if (!Belongs(x2, x1, d))
                check = false;
            if ((x2[0] == d[0]) && (x2[1] == d[1]) || ((x1[0] == d[0])  && (x1[1] == d[1])))
                check = false;

            return check;
        }

        //принадлежность точки отрезку
        public bool Belongs(double[] x1, double[] x2, double[] x)
        {
            bool answer;
            if ((Math.Sqrt(Math.Pow(x[0] - x1[0], 2) + Math.Pow(x[1] - x1[1], 2)) + Math.Sqrt(Math.Pow(x2[0] - x[0], 2) + Math.Pow(x2[1] - x[1], 2)) - Math.Sqrt(Math.Pow(x2[0] - x1[0], 2) + Math.Pow(x2[1] - x1[1], 2))) <= eps)
                answer = true;
            //if (((x[0] - x1[0]) * (x2[1] - x1[1]) - (x[1] - x1[1]) * (x2[0] - x1[0]) == 0) && (((x1[0] < x[0]) && (x[0] < x2[0])) || ((x2[0] < x[0]) && (x[0] < x1[0]))))
            //{
            //    answer = true;
            //}
            else answer = false;
            return answer;
        }
        //находим коэффициенты нашей функции
        public double[] Koefficients(int[] cond, double[] d)
        {
            double[] koef = new double[3];

            double[] polus1 = polygons[cond[0]].polus;

            List<double[]> P1 = polygons[cond[0]].ListOfConvexPoligons[cond[1]];
            List<double[]> P2 = polygons[cond[3]].ListOfConvexPoligons[cond[4]];

            double[] x = polygons[cond[0]].FindElementInConvexPolygon(cond[2], cond[1]);
            double[] x2 = new double[2];
            x2[0] = polygons[cond[0]].NextElement(x, cond[1])[0];
            x2[1] = polygons[cond[0]].NextElement(x, cond[1])[1];

            double[] x1 = new double[2];
            x1[0] = x[0];
            x1[1] = x[1];

            koef[0] = x1[1] - x2[1];
            koef[1] = -x1[0] + x2[0];
            koef[2] = -x2[0] * x1[1] + x2[1] * x1[0] + Function(koef[0], koef[1], d);

            return koef;
        }
        //находим функцию по двум точкам
        public double Function(double a, double b, double c, double[] x)
        {
            double result = a * x[0] + b * x[1] + c;
            return result;
        }
        public double Function(double a, double b, double[] x)
        {
            double result = a * x[0] + b * x[1];
            return result;
        }
        //длина перпендикуляра, опущенного из точки к прямой
        public double PerpendLength(double a, double b, double c, double[] point)
        {
            double d = Math.Abs(a * point[0] + b * point[1] + c) / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            return d;
        }

        //левое ограничение
        private double LeftBorder(double[] polus, double xl)
        {
            return polus[0] + xl;
        }

        //нижнее ограничение
        private double DownBorder(double[] polus, double xd)
        {
            return polus[1] + xd;
        }

        //правое ограничение
        private double UpperBorder(double[] polus, double xr)
        {
            return polus[1] + xr - st.b;
        }

        //верхнее ограничение
        private double RightBorder(double[] polus, double xu)
        {
            return polus[0] + xu - st.z;
        }

    }
}
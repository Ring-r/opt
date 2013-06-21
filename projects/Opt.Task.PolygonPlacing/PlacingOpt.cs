using System;
using Opt;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Extentions;
using Opt.Geometrics.SpecialGeometrics;

namespace PolygonPlacingTest
{
    public class PlacingOpt
    {
        private PolygonWithMinMax[] polygons;
        private Vector2d strip_vector;

        /// <summary>
        /// Точка, заданная массивом коэффициентов.
        /// </summary>
        private double[] X;
        /// <summary>
        /// Набор линейных ограничений, заданных массивами коэффициентов (в виде Gs(x)>=0).
        /// </summary>
        private double[][] Gs;

        ///<summary>
        /// Индекс элемента вектора, который отвечает за длину занятой полосы.
        /// </summary>
        private int index_strip;

        private DateTime time_of_start;
        private DateTime time_of_finish;
        public TimeSpan Time
        {
            get
            {
                if (time_of_start != time_of_finish)
                    return time_of_finish - time_of_start;
                else
                    return DateTime.Now - time_of_start;
            }
        }

        public PlacingOpt(PolygonWithMinMax[] polygons, Vector2d strip_vector)
        {
            this.polygons = polygons;
            this.strip_vector = strip_vector;

            int n = polygons.Length;
            X = new double[2 * n + 1];
            Gs = new double[4 * n + n * (n - 1) / 2][];
            for (int i = 0; i < Gs.Length; i++)
                Gs[i] = new double[10];

            index_strip = 2 * n;
        }

        #region Операции с векторами.
        //private double[] Clear(double[] vector_this)
        //{
        //    for (int i = 0; i < vector_this.Length; i++)
        //        vector_this[i] = 0;
        //    return vector_this;
        //}
        //private double[][] Clear(double[][] matrix_this)
        //{
        //    for (int i = 0; i < matrix_this.Length; i++)
        //        Clear(matrix_this[i]);
        //    return matrix_this;
        //}
        //private double[] Copy(double[] vector_this, double[] vector)
        //{
        //    for (int i = 0; i < vector_this.Length; i++)
        //        vector[i] = vector_this[i];
        //    return vector;
        //}
        //private double[] Summation(double[] vector_this, double[] vector)
        //{
        //    for (int i = 0; i < vector_this.Length; i++)
        //        vector_this[i] += vector[i];
        //    return vector_this;
        //}
        //private double[] Multiplication(double[] vector_this, double value)
        //{
        //    for (int i = 0; i < vector_this.Length; i++)
        //        vector_this[i] *= value;
        //    return vector_this;
        //}
        //private double MultiplicationValue(double[] vector_this, double[] vector)
        //{
        //    double res = 0;
        //    for (int i = 0; i < vector_this.Length; i++)
        //        res += vector_this[i] * vector[i];
        //    return res;
        //}
        #endregion

        #region Операции с функциями.
        private double ValueG(double[] G, double[] X)
        {
            double res = 0;

            int i = (int)G[0];
            int j = (int)G[1];
            res += G[4] * X[2 * i];
            res += G[5] * X[2 * i + 1];
            res += G[6] * X[2 * j];
            res += G[7] * X[2 * j + 1];
            res += G[8] * X[index_strip];
            res += G[9];

            return res;
        }
        private double ValueGsWithCheck(double[] X)
        {
            double res = 0;
            foreach (double[] G in Gs)
            {
                double value = ValueG(G, X);
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
            }

            return res;
        }

        private double[] GradGs(double[][] Gs, double[] X, double[] res)
        {
            Matrix.Clear(res);

            foreach (double[] G in Gs)
            {
                double value = ValueG(G, X);
                value = 1 / (value * value);

                int i = (int)G[0];
                int j = (int)G[1];
                res[2 * i] += value * G[4];
                res[2 * i + 1] += value * G[5];
                res[2 * j] += value * G[6];
                res[2 * j + 1] += value * G[7];
                res[index_strip] += value * G[8];
            }

            return res;
        }
        #endregion

        /// <summary>
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        /// <param name="mu"></param>
        /// <param name="beta"></param>
        /// <param name="eps">Погрешность.</param>
        private void Calculate(double mu, double beta, double eps)
        {
            #region Шаг 1. Метод барьеров. Пока значение дополнительной части функции больше погрешности...
            while (mu * ValueGsWithCheck(X) > eps)
            {
                #region Шаг 1.1. Метод антиградиента (метод найскорейшего спуска). Пока происходит движение в сторону антиградиента...
                double value;

                double[] d = new double[X.Length];

                double length;

                do
                {
                    #region Шаг 1.1.1. Находим вектор антиградиента в текущей точке.
                    #region d <-- grad(F) - mu * Sum(grad(G) / G^2(X));
                    GradGs(Gs, X, d);
                    Matrix.Multiply(d, -mu);
                    d[index_strip] += 1;
                    #endregion
                    Matrix.Multiply(d, -1);
                    #endregion

                    #region Шаг 1.1.2. Нормируем вектор антиградиента в текущей точке.
                    Matrix.Multiply(d, 1 / Math.Sqrt(Matrix.MultiplyValue(d, d)));
                    #endregion

                    #region Шаг 1.1.3. Находим длину шага (одномерный поиск - метод половинного деления).

                    double length_a = 0;
                    double value_a = X[index_strip] + mu * ValueGsWithCheck(X);

                    // TODO: Подобрать начальную длину шага. Сложность метода половинного деления равна log(length_b/eps,2). Чем больше длина шага, тем быстрее работает метод найскорейшего спуска. Чем меньше длина шага тем быстрее работает метод половинного деления.
                    double length_b = 5;// X[index_strip]; // Длина максимально возможного шага.
                    double value_b = double.PositiveInfinity;

                    double[] XX = new double[X.Length];
                    double[] XX_eps = new double[X.Length];

                    while (Math.Abs(length_b - length_a) > eps)
                    {
                        length = (length_a + length_b) / 2;

                        #region XX <-- X + d * length;
                        Matrix.Copy(XX, d);
                        Matrix.Multiply(XX, length);
                        Matrix.Summate(XX, X);
                        #endregion

                        value = XX[index_strip] + mu * ValueGsWithCheck(XX);
                        if (value >= value_a)
                        {
                            length_b = length;
                            value_b = value;
                        }
                        else
                        {
                            if (value > value_b)
                            {
                                length_a = length;
                                value_a = value;
                            }
                            else
                            {
                                #region XX_eps <-- X + d * (length - eps / 2);
                                Matrix.Copy(XX_eps, d);
                                Matrix.Multiply(XX_eps, length - eps / 2);
                                Matrix.Summate(XX_eps, X);
                                #endregion

                                double value_eps = XX_eps[index_strip] + mu * ValueGsWithCheck(XX_eps);
                                //if (value < value_eps)
                                {
                                    length_a = length;
                                    value_a = value;
                                }
                                //else
                                //{
                                //    length_b = length;
                                //    value_b = value;
                                //}
                                // TODO: Хм! Определить правильный алгоритм метода половинного деления для данной задачи!
                            }
                        }
                    }

                    length = length_a;
                    #endregion

                    #region Шаг 1.1.4. Вычисляем новую точку решения задачи.
                    if (length > eps)
                        Matrix.Summate(X, Matrix.Multiply(d, length));
                    #endregion
                } while (length > eps);
                #endregion

                #region Шаг 1.2. Изменяем коэффициент mu для поиска более точного решения.
                mu *= beta;
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// Преобразование координат полюсов многоугольников и длины полосы в многомерную точку размещения.
        /// </summary>
        public void FillPoint()
        {
            #region Шаг 1.1. Заполняем координаты полюсов.
            for (int i = 0; i < polygons.Length; i++)
            {
                X[2 * i] = polygons[i].Pole.X;
                X[2 * i + 1] = polygons[i].Pole.Y;
            }
            #endregion
            #region Шаг 1.2. Заполняем длину занятой части полосы.
            X[index_strip] = strip_vector.X;
            #endregion
        }

        /// <summary>
        /// Преобразование многомерной точки размещения в координаты полюсов и длину полосы.
        /// </summary>
        public void FillPolygons()
        {
            for (int i = 0; i < polygons.Length; i++)
            {
                polygons[i].Pole.X = X[2 * i];
                polygons[i].Pole.Y = X[2 * i + 1];
            }

            strip_vector.X = X[index_strip];
        }

        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения многоугольников (метод барьерных функций).
        /// </summary>
        /// <param name="mu"></param>
        /// <param name="beta"></param>
        /// <param name="eps"></param>
        public void CalculateStart(double mu, double beta, double eps)
        {
            time_of_start = time_of_finish = DateTime.Now;            

            double width_old;
            do
            {
                width_old = strip_vector.X;

                #region Шаг 1. Создаём начальную точку.
                FillPoint();
                #endregion

                #region Шаг 2. Создаём функцию цели.
                // Функция цели явно не храниться.
                #endregion

                #region Шаг 3. Создаём набор ограничений.
                Matrix.Clear(Gs);
                int k = 0;
                #region Шаг 3.1. Для каждого многоугольника...
                for (int i = 0; i < polygons.Length; i++)
                {
                    #region Шаг 3.1.1. Создаём набор ограничений (G >= 0) по полосе.
                    #region Шаг 3.1.1.1. Ограничение по нижней границе. //  Y - min.Y >= 0
                    Gs[k][0] = i;
                    Gs[k][5] = 1;
                    Gs[k][9] = -polygons[i].MinY;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.2. Ограничение по левой границе. //   X - min.X >= 0
                    Gs[k][0] = i;
                    Gs[k][4] = 1;
                    Gs[k][9] = -polygons[i].MinX;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.3. Ограничение по верхней границе. // Y + max.Y <= H  --> -Y - max.Y + H >= 0
                    Gs[k][0] = i;
                    Gs[k][5] = -1;
                    Gs[k][9] = -polygons[i].MaxY + strip_vector.Y;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.4. Ограничение по правой границе. //  X + max.X <= Z  --> -X + Z - max.X >= 0
                    Gs[k][0] = i;
                    Gs[k][4] = -1;
                    Gs[k][8] = 1;
                    Gs[k][9] = -polygons[i].MaxX;
                    k++;
                    #endregion
                    #endregion
                }
                #endregion
                #region Шаг 3.2. Для каждой пары многоугольников...
                for (int i = 0; i < polygons.Length - 1; i++)
                    for (int j = i + 1; j < polygons.Length; j++)
                    {
                        #region Шаг 3.2.1. Создаём и находим разделяющую.
                        //PlaneDividing pd = new PlaneDividing(new Polygon2d.Iterator(i, polygons[i], 0), new Polygon2d.Iterator(j, polygons[j], 0));
                        //pd.Find();

                        #region Временный код.
                        PlaneDividing pd;
                        Polygon2d.Iterator it1i = new Polygon2d.Iterator(i, polygons[i], 0);
                        Polygon2d.Iterator it1j = new Polygon2d.Iterator(j, polygons[j], 0);
                        double ed1 = Find(it1i, it1j);

                        Polygon2d.Iterator it2i = new Polygon2d.Iterator(i, polygons[i], 0);
                        Polygon2d.Iterator it2j = new Polygon2d.Iterator(j, polygons[j], 0);
                        double ed2 = Find(it2j, it2i);

                        if (ed1 > ed2)
                            pd = new PlaneDividing(it1i, it1j);
                        else
                            pd = new PlaneDividing(it2j, it2i);
                        #endregion
                        #endregion

                        #region Шаг 3.2.2. Создаём ограничение.
                        Vector2d vector = pd.IteratorPlane.Point(1) - pd.IteratorPlane.Point(0);
                        Vector2d normal = new Vector2d { X = -vector.Y, Y = vector.X };
                        //normal.Normalize(); // Нужна ли нормализация вектора?

                        Gs[k][0] = pd.IteratorPlane.IndexPolygon;
                        Gs[k][1] = pd.IteratorPoint.IndexPolygon;
                        Gs[k][2] = pd.IteratorPlane.Index;
                        Gs[k][3] = pd.IteratorPoint.Index;
                        Gs[k][4] = normal.X;
                        Gs[k][5] = normal.Y;
                        Gs[k][6] = -normal.X;
                        Gs[k][7] = -normal.Y;
                        Gs[k][9] = -((pd.IteratorPoint.Polygon[pd.IteratorPoint.Index] - pd.IteratorPlane.Polygon[pd.IteratorPlane.Index]) * normal);
                        k++;
                        #endregion
                    }
                #endregion
                #endregion

                #region Шаг 4. Выполняем поиск локального минимума с заданными ограничениями.
                Calculate(mu, beta, eps);
                #endregion

                #region Шаг 5. Преобразуем результат метода барьеров в результат задачи размещения.
                FillPolygons();
                #endregion
            } while (Math.Abs(strip_vector.X - width_old) > eps);
            
            time_of_finish = DateTime.Now;
        }

        // Метод поиска разделяющих. // TODO: Переделать.
        private double Find(Polygon2d.Iterator iti, Polygon2d.Iterator itj)
        {
            int gi = -1;
            int gj = -1;
            double ged = double.NegativeInfinity;

            for (int i = 0; i < iti.Count; i++)
            {
                int lj = -1;
                double led = double.PositiveInfinity;
                for (int j = 0; j < itj.Count; j++)
                {
                    double ed = Plane2dExt.Расширенное_расстояние(iti.Plane(i), itj.Point(j));
                    if (led > ed)
                    {
                        led = ed;
                        lj = j;
                    }
                    if (ed < 0)
                    {
                        led = double.PositiveInfinity;
                        break;
                    }
                }

                if (!double.IsPositiveInfinity(led))
                {
                    if (ged < led)
                    {
                        ged = led;
                        gi = i;
                        gj = lj;
                    }
                }
            }

            if (!double.IsNegativeInfinity(ged))
            {
                iti.Move(gi);
                itj.Move(gj);
            }

            return ged;
        }

        // Получить разделяющие для объекта, заданного номером. // TODO: Переделать.
        public System.Collections.Generic.List<Plane2d> GetPlanes(int index)
        {
            System.Collections.Generic.List<Plane2d> res = new System.Collections.Generic.List<Plane2d>();
            int k=4*polygons.Length;
            for(int i=0; i<polygons.Length-1;i++)
                for (int j = i + 1; j < polygons.Length; j++)
                {
                    if (index == i || index == j)
                    {
                        Polygon2d.Iterator iti = new Polygon2d.Iterator((int)Gs[k][0], polygons[(int)Gs[k][0]], (int)Gs[k][2]);
                        res.Add(iti.Plane(0));
                    }
                    k++;
                }
            return res;
        }
    }
}
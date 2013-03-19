using System;
using System.Collections.Generic;
using Opt.ClosenessModel;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms.Метод_барьеров
{
    public abstract class Placing : Opt.Algorithms.Placing
    {
        protected double e = 1; // Насколько можно нарушить ограничения.

        /// <summary>
        /// Точка, заданная массивом коэффициентов.
        /// </summary>
        protected double[] X;

        ///<summary>
        /// Индекс элемента вектора, который отвечает за длину занятой полосы.
        /// </summary>
        protected int index_strip;

        protected double mu;
        protected double beta;

        public Placing(double height, double length, Circle[] circles, double mu, double beta, double eps)
            : base(height, length, circles, eps)
        {
            index_strip = 2 * circles.Length;

            X = new double[index_strip + 1];

            this.mu = mu;
            this.beta = beta;
        }

        /// <summary>
        /// Преобразование координат полюсов кругов и длины полосы в многомерную точку размещения.
        /// </summary>
        public void FillPoint()
        {
            #region Шаг 1.1. Заполняем координаты полюсов.
            for (int i = 0; i < circles.Length; i++)
            {
                X[2 * i] = circles[i].Pole.X;
                X[2 * i + 1] = circles[i].Pole.Y;
            }
            #endregion
            #region Шаг 1.2. Заполняем длину занятой части полосы.
            X[index_strip] = length;
            #endregion
        }

        /// <summary>
        /// Преобразование многомерной точки размещения в координаты полюсов и длину полосы.
        /// </summary>
        public void FillCircles()
        {
            for (int i = 0; i < circles.Length; i++)
            {
                circles[i].Pole.X = X[2 * i];
                circles[i].Pole.Y = X[2 * i + 1];
            }

            length = X[index_strip];
        }


        /// <summary>
        /// Определение дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <returns>Дополнительная часть функции цели в заданной точке.</returns>
        protected abstract double ValueGsWithCheck(double[] X);

        /// <summary>
        /// Определение градиента дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <param name="res">Градиент дополнительной части функции цели в заданной точке.</param>
        /// <returns>Градиент дополнительной части функции цели в заданной точке.</returns>
        protected abstract double[] GradGs(double[] X, double[] res);

        /// <summary>
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        protected override void Calculate()
        {
            #region Шаг 1. Метод барьеров. Пока значение дополнительной части функции больше погрешности...
            do
            {
                #region Шаг 1.1. Метод антиградиента (метод найскорейшего спуска). Пока происходит движение в сторону антиградиента...

                // Значение дополнительной части функции цели.
                double value;
                // Вектор движения.
                double[] d = new double[X.Length];
                // Длина вектора движения.
                double length;

                do
                {
                    #region Шаг 1.1.1. Находим вектор антиградиента в текущей точке.
                    #region d <-- grad(F) - 2 * mu * Sum(grad(G) / G^2(X));
                    GradGs(X, d);
                    Matrix.Multiply(d, -2 * mu);
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
            } while (mu * ValueGsWithCheck(X) > eps);
            #endregion
        }
    }

    public class PlacingAll : Placing
    {
        /// <summary>
        /// Набор линейных ограничений, заданных массивами коэффициентов (в виде Gs(x)>=0).
        /// </summary>
        protected double[][] Gs;

        public PlacingAll(double height, double length, Circle[] circles, double mu, double beta, double eps)
            :base(height, length, circles, mu, beta, eps)
        {
            #region Временный код.
            //for (int i = 0; i < circles.Length; i++)
            //    if (circles[i].Value > 1)
            //        circles[i].Value -= 1;
            #endregion
        }

        protected double ValueG(double[] G, double[] X)
        {
            int i = (int)G[0];
            int j = (int)G[1];
            if (j >= 0)
            {
                double x = X[2 * j] - X[2 * i];
                double y = X[2 * j + 1] - X[2 * i + 1];
                double r = circles[i].Value + circles[j].Value - 2 * e;
                //return x * x + y * y - r * r;
                return Math.Sqrt(x * x + y * y) - r;
            }
            else
            {
                double res = 0;

                res += G[2] * X[2 * i];
                res += G[3] * X[2 * i + 1];
                res += G[4] * X[index_strip];
                res += G[5] + e;

                return res;
            }
        }
        
        /// <summary>
        /// Определение дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <returns>Дополнительная часть функции цели в заданной точке.</returns>
        protected override double ValueGsWithCheck(double[] X)
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
        
        /// <summary>
        /// Определение градиента дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <param name="res">Градиент дополнительной части функции цели в заданной точке.</param>
        /// <returns>Градиент дополнительной части функции цели в заданной точке.</returns>
        protected override double[] GradGs(double[] X, double[] res)
        {
            Matrix.Clear(res);
            double value;

            foreach (double[] G in Gs)
            {
                value = ValueG(G, X);
                value = 1 / (value * value);

                int i = (int)G[0];
                int j = (int)G[1];
                if (j >= 0)
                {
                    double x = 2 * (X[2 * j] - X[2 * i]) * value;
                    double y = 2 * (X[2 * j + 1] - X[2 * i + 1]) * value;

                    res[2 * j] += x;
                    res[2 * i] += -x;
                    res[2 * j + 1] += y;
                    res[2 * i + 1] += -y;
                }
                else
                {
                    res[2 * i] += value * G[2];
                    res[2 * i + 1] += value * G[3];
                    res[index_strip] += value * G[4];
                }
            }

            return res;
        }


        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения кругов (метод барьерных функций).
        /// </summary>
        public override void CalculateStart()
        {
            #region Шаг 1. Создаём начальную точку.
            FillPoint();
            #endregion

            #region Шаг 2. Создаём функцию цели.
            // Функция цели явно не храниться.
            #endregion

            #region Шаг 3. Создаём набор ограничений.
            Matrix.Clear(Gs);
            int k = 0;
            #region Шаг 3.1. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                #region Шаг 3.1.1. Создаём набор ограничений (G >= 0) по полосе.
                #region Шаг 3.1.1.1. Ограничение по левой границе. //   X - R >= 0
                Gs[k][0] = i;
                Gs[k][1] = -1;
                Gs[k][2] = 1;
                Gs[k][5] = -circles[i].Value;
                k++;
                #endregion
                #region Шаг 3.1.1.2. Ограничение по нижней границе. //  Y - R >= 0
                Gs[k][0] = i;
                Gs[k][1] = -2;
                Gs[k][3] = 1;
                Gs[k][5] = -circles[i].Value;
                k++;
                #endregion
                #region Шаг 3.1.1.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                Gs[k][0] = i;
                Gs[k][1] = -3;
                Gs[k][3] = -1;
                Gs[k][5] = -circles[i].Value + height;
                k++;
                #endregion
                #region Шаг 3.1.1.4. Ограничение по правой границе. //  X + R <= Z  --> -X + Z - R >= 0
                Gs[k][0] = i;
                Gs[k][1] = -4;
                Gs[k][2] = -1;
                Gs[k][4] = 1;
                Gs[k][5] = -circles[i].Value;
                k++;
                #endregion
                #endregion
            }
            #endregion
            #region Шаг 3.2. Для каждой пары кругов...
            for (int i = 0; i < circles.Length - 1; i++)
                for (int j = i + 1; j < circles.Length; j++)
                {
                    #region Шаг 3.2.1. Создаём ограничение.
                    Gs[k][0] = i;
                    Gs[k][1] = j;
                    k++;
                    #endregion
                }
            #endregion
            #endregion

            #region Шаг 4. Если точка является допустимой, то...
            if (!double.IsPositiveInfinity(ValueGsWithCheck(X)))
            {
                #region Шаг 4.1. Выполняем поиск локального минимума методом барьеров.
                Calculate();
                #endregion
            }
            #endregion

            #region Шаг 5. Преобразуем результат метода барьеров в результат задачи размещения.
            FillCircles();
            #endregion
        }
    }

    public class PlacingOpt : Placing
    {
        public PlacingOpt(double height, double length, Circle[] circles, double mu, double beta, double eps)
            : base(height, length, circles, mu, beta, eps)
        {
            #region Временный код.
            //for (int i = 0; i < circles.Length; i++)
            //    if (circles[i].Value > 1)
            //        circles[i].Value -= 1;
            #endregion
        }


        /// <summary>
        /// Определение дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <returns>Дополнительная часть функции цели в заданной точке.</returns>
        protected override double ValueGsWithCheck(double[] X)
        {
            double res = 0;
            double value = 0;
            #region Шаг 1. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                #region Шаг 1.1. Проверяем и суммируем ограничения (G >= 0) по полосе.
                #region Шаг 1.1.1. Ограничение по левой границе. //   X - R >= 0
                value = X[2 * i] - circles[i].Value + e;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.2. Ограничение по нижней границе. //  Y - R >= 0
                value = X[2 * i + 1] - circles[i].Value + e;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                value = -X[2 * i + 1] - circles[i].Value + height + e;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                value = -X[2 * i] - circles[i].Value + X[index_strip] + e;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #endregion
            }
            #endregion
            #region Шаг 2. Для каждой пары кругов...
            for (int i = 0; i < circles.Length - 1; i++)
                for (int j = i + 1; j < circles.Length; j++)
                {
                    #region Шаг 2.1. Проверяем и суммируем ограничение между кругами.
                    double x = X[2 * j] - X[2 * i];
                    double y = X[2 * j + 1] - X[2 * i + 1];
                    double r = circles[i].Value + circles[j].Value - 2 * e;
                    //value = x * x + y * y - r * r;
                    value = Math.Sqrt(x * x + y * y) - r;
                    if (value > 0)
                        res += 1 / value;
                    else
                        return double.PositiveInfinity;
                    #endregion
                }
            #endregion

            return res;
        }

        /// <summary>
        /// Определение градиента дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <param name="res">Градиент дополнительной части функции цели в заданной точке.</param>
        /// <returns>Градиент дополнительной части функции цели в заданной точке.</returns>
        protected override double[] GradGs(double[] X, double[] res)
        {
            Matrix.Clear(res);
            double value;
            #region Шаг 1.1.1.1. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                #region Шаг 1.1.1.2. Проверяем и суммируем ограничения (G >= 0) по полосе.
                #region Шаг 1.1.1.2.1. Ограничение по левой границе. //   X - R >= 0
                value = X[2 * i] - circles[i].Value + e;
                value = 1 / (value * value);
                res[2 * i] += value;
                #endregion
                #region Шаг 1.1.1.2.2. Ограничение по нижней границе. //  Y - R >= 0
                value = X[2 * i + 1] - circles[i].Value + e;
                value = 1 / (value * value);
                res[2 * i + 1] += value;
                #endregion
                #region Шаг 1.1.1.2.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                value = -X[2 * i + 1] - circles[i].Value + height + e;
                value = 1 / (value * value);
                res[2 * i + 1] += -value;
                #endregion
                #region Шаг 1.1.1.2.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                value = -X[2 * i] - circles[i].Value + X[index_strip] + e;
                value = 1 / (value * value);
                res[2 * i] += -value;
                res[index_strip] += value;
                #endregion
                #endregion
            }
            #endregion
            #region Шаг 1.1.1.2. Для каждой пары кругов...
            for (int i = 0; i < circles.Length - 1; i++)
                for (int j = i + 1; j < circles.Length; j++)
                {
                    #region Шаг 1.1.1.2.1. Проверяем и суммируем ограничение между кругами.
                    double x = X[2 * j] - X[2 * i];
                    double y = X[2 * j + 1] - X[2 * i + 1];
                    double r = circles[i].Value + circles[j].Value - 2 * e;
                    //value = x * x + y * y - r * r;
                    value = Math.Sqrt(x * x + y * y) - r;
                    value = 1 / (value * value);

                    x = 2 * (X[2 * j] - X[2 * i]) * value;
                    res[2 * i] += -x;
                    res[2 * j] += x;

                    y = 2 * (X[2 * j + 1] - X[2 * i + 1]) * value;
                    res[2 * i + 1] += -y;
                    res[2 * j + 1] += y;
                    #endregion
                }
            #endregion

            return res;
        }


        /// <summary>
        /// Метод подготовки данных и запуска алгоритма размещения кругов (метод барьерных функций).
        /// </summary>
        public override void CalculateStart()
        {
            #region Шаг 1. Создаём начальную точку.
            FillPoint();
            #endregion

            #region Шаг 2. Создаём функцию цели.
            // Функция цели явно не храниться.
            #endregion

            #region Шаг 3. Создаём набор ограничений.
            // Ограничения явно не хранятся.
            #endregion

            #region Шаг 4. Если точка является допустимой, то...
            if (!double.IsPositiveInfinity(ValueGsWithCheck(X)))
            {
                #region Шаг 4.1. Выполняем поиск локального минимума методом барьеров.
                Calculate();
                #endregion
            }
            #endregion

            #region Шаг 5. Преобразуем результат метода барьеров в результат задачи размещения.
            FillCircles();
            #endregion
        }
    }

    public class PlacingOptWithCloseModel : PlacingOpt, Opt.Algorithms.IWithClosenessModel
    {        
        protected Vertex<Geometric2d> vertex;
        public Vertex<Geometric2d> Vertex
        {
            get
            {
                return vertex;
            }
        }
        protected List<Vertex<Geometric2d>> triples;

        public PlacingOptWithCloseModel(double height, double length, Circle[] circles, Vertex<Geometric2d> vertex, double mu, double beta, double eps)
            : base(height, length, circles, mu, beta, eps)
        {
            this.vertex = vertex;
            this.triples = VertexExtention.GetTriples(vertex);

            for (int i = 0; i < circles.Length; i++)
                circles[i].Id = i;
        }

        /// <summary>
        /// Определение дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <returns>Дополнительная часть функции цели в заданной точке.</returns>
        protected override double ValueGsWithCheck(double[] X)
        {
            double res = 0;
            double value = 0;
            #region Шаг 1. Для каждой пары объектов...
            DateTime data = DateTime.Now;
            for (int t = 0; t < triples.Count; t++)
            {
                Vertex<Geometric2d> vertex = triples[t];
                for (int k = 0; k < 3; k++)
                {
                    if (vertex.Cros.Somes.LastChecked != data)
                    {
                        bool is_right = false;
                        int i = 0;
                        int j = 0;
                        if ((vertex.Next.DataInVertex is Circle) && (vertex.Prev.DataInVertex is Circle))
                        {
                            i = vertex.Next.DataInVertex.Id;
                            j = vertex.Prev.DataInVertex.Id;
                            #region Ограничения для пары кругов.
                            double x = X[2 * i] - X[2 * j];
                            double y = X[2 * i + 1] - X[2 * j + 1];
                            double r = circles[j].Value + circles[i].Value - 2 * e;
                            value = Math.Sqrt(x * x + y * y) - r;
                            if (value > 0)
                                res += 1 / value;
                            else
                                return double.PositiveInfinity;
                            #endregion
                        }
                        else
                        {
                            if ((vertex.Next.DataInVertex is Circle) && (vertex.Prev.DataInVertex is Plane2d))
                            {
                                i = vertex.Next.DataInVertex.Id;
                                j = vertex.Prev.DataInVertex.Id;
                                is_right = true;
                            }
                            if ((vertex.Next.DataInVertex is Plane) && (vertex.Prev.DataInVertex is Circle))
                            {
                                i = vertex.Prev.DataInVertex.Id;
                                j = vertex.Next.DataInVertex.Id;
                                is_right = true;
                            }
                            if (is_right)
                            {
                                #region Ограничения по полосе.
                                switch (j)
                                {
                                    case -1:
                                        #region Шаг 1.1.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                                        value = -X[2 * i + 1] - circles[i].Value + height + e;
                                        if (value > 0)
                                            res += 1 / value;
                                        else
                                            return double.PositiveInfinity;
                                        #endregion
                                        break;
                                    case -2:
                                        #region Шаг 1.1.1. Ограничение по левой границе. //   X - R >= 0
                                        value = X[2 * i] - circles[i].Value + e;
                                        if (value > 0)
                                            res += 1 / value;
                                        else
                                            return double.PositiveInfinity;
                                        #endregion
                                        break;
                                    case -3:
                                        #region Шаг 1.1.2. Ограничение по нижней границе. //  Y - R >= 0
                                        value = X[2 * i + 1] - circles[i].Value + e;
                                        if (value > 0)
                                            res += 1 / value;
                                        else
                                            return double.PositiveInfinity;
                                        #endregion
                                        break;
                                    case -4:
                                        #region Шаг 1.1.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                                        value = -X[2 * i] - circles[i].Value + X[index_strip] + e;
                                        if (value > 0)
                                            res += 1 / value;
                                        else
                                            return double.PositiveInfinity;
                                        #endregion
                                        break;
                                }
                                #endregion
                            }
                        }
                    }
                    vertex = vertex.Next;
                }
                vertex.Somes.LastChecked = data;
            }
            #endregion

            return res;

            #region Вариант.
            //double res = 0;
            //double value = 0;

            //#region Шаг 1. Для каждого круга...
            //for (int i = 0; i < circles.Length; i++)
            //{
            //    #region Шаг 1.1. Проверяем и суммируем ограничения (G >= 0) по полосе.
            //    #region Шаг 1.1.1. Ограничение по левой границе. //   X - R >= 0
            //    value = X[2 * i] - circles[i].Value + eps;
            //    if (value > 0)
            //        res += 1 / value;
            //    else
            //        return double.PositiveInfinity;
            //    #endregion
            //    #region Шаг 1.1.2. Ограничение по нижней границе. //  Y - R >= 0
            //    value = X[2 * i + 1] - circles[i].Value + eps;
            //    if (value > 0)
            //        res += 1 / value;
            //    else
            //        return double.PositiveInfinity;
            //    #endregion
            //    #region Шаг 1.1.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
            //    value = -X[2 * i + 1] - circles[i].Value + height + eps;
            //    if (value > 0)
            //        res += 1 / value;
            //    else
            //        return double.PositiveInfinity;
            //    #endregion
            //    #region Шаг 1.1.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
            //    value = -X[2 * i] - circles[i].Value + X[index_strip] + eps;
            //    if (value > 0)
            //        res += 1 / value;
            //    else
            //        return double.PositiveInfinity;
            //    #endregion
            //    #endregion
            //}
            //#endregion


            //#region Шаг 1. Для каждой пары соседних объектов...
            //DateTime date = DateTime.Now; //задаем точку отчета времени для проверки прошли мы вершину или нет

            //for (int i = 0; i < triples.Count; i++) // проверяем и суммируем ограничения между кругами
            //{
            //    Vertex<Geometric2d> vertex_temp = triples[i];
            //    do
            //    {
            //        if (vertex_temp.Cros.Somes.LastChecked != date)
            //        {                        
            //            if ((vertex_temp.Next.DataInVertex is Circle) && (vertex_temp.Prev.DataInVertex is Circle))
            //            {
            //                int ID_Next = vertex_temp.Next.DataInVertex.ID; // ID для определения кругов в векторе X
            //                int ID_Prev = vertex_temp.Prev.DataInVertex.ID;

            //                double x = X[2 * ID_Next] - X[2 * ID_Prev];
            //                double y = X[2 * ID_Next + 1] - X[2 * ID_Prev + 1];
            //                double r = circles[ID_Prev].Value + circles[ID_Next].Value;
            //                value = Math.Sqrt(x * x + y * y) - r;
            //                if (value > 0)
            //                    res += 1 / value;
            //                else
            //                    return double.PositiveInfinity;
            //            }
            //            if ((vertex_temp.Next.DataInVertex is Circle) && (vertex_temp.Prev.DataInVertex is Plane))
            //            {
            //                // TODO: Расчитать ограничение для круга и полуплоскости.
            //            }
            //            if ((vertex_temp.Next.DataInVertex is Plane) && (vertex_temp.Prev.DataInVertex is Circle))
            //            {
            //                // TODO: Расчитать ограничение для круга и полуплоскости.
            //            }
            //        }

            //        vertex_temp.Somes.LastChecked = date;

            //        vertex_temp = vertex_temp.Next;
            //    } while (vertex_temp != triples[i]);
            //}

            //#endregion

            //return res;
            #endregion
        }

        /// <summary>
        /// Определение градиента дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <param name="res">Градиент дополнительной части функции цели в заданной точке.</param>
        /// <returns>Градиент дополнительной части функции цели в заданной точке.</returns>
        protected override double[] GradGs(double[] X, double[] res)
        {
            Matrix.Clear(res);
            double value;

            #region Шаг 1. Для каждой пары объектов...
            DateTime data = DateTime.Now;
            for (int t = 0; t < triples.Count; t++)
            {
                Vertex<Geometric2d> vertex = triples[t];
                for (int k = 0; k < 3; k++)
                {
                    if (vertex.Cros.Somes.LastChecked != data)
                    {
                        bool is_right = false;
                        int i = 0;
                        int j = 0;
                        if ((vertex.Next.DataInVertex is Circle) && (vertex.Prev.DataInVertex is Circle))
                        {
                            i = vertex.Next.DataInVertex.Id;
                            j = vertex.Prev.DataInVertex.Id;
                            #region Ограничения для пары кругов.
                            double x = X[2 * i] - X[2 * j];
                            double y = X[2 * i + 1] - X[2 * j + 1];
                            double r = circles[j].Value + circles[i].Value - 2 * e;
                            value = Math.Sqrt(x * x + y * y) - r;
                            value = 1 / (value * value);

                            x = 2 * (X[2 * j] - X[2 * i]) * value;
                            res[2 * i] += -x;
                            res[2 * j] += x;

                            y = 2 * (X[2 * j + 1] - X[2 * i + 1]) * value;
                            res[2 * i + 1] += -y;
                            res[2 * j + 1] += y;
                            #endregion
                        }
                        else
                        {
                            if ((vertex.Next.DataInVertex is Circle) && (vertex.Prev.DataInVertex is Plane2d))
                            {
                                i = vertex.Next.DataInVertex.Id;
                                j = vertex.Prev.DataInVertex.Id;
                                is_right = true;
                            }
                            if ((vertex.Next.DataInVertex is Plane2d) && (vertex.Prev.DataInVertex is Circle))
                            {
                                i = vertex.Prev.DataInVertex.Id;
                                j = vertex.Next.DataInVertex.Id;
                                is_right = true;
                            }
                            if (is_right)
                            {
                                #region Ограничения по полосе.
                                switch (j)
                                {
                                    case -1:
                                        #region Шаг 1.1.1.2.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                                        value = -X[2 * i + 1] - circles[i].Value + height + e;
                                        value = 1 / (value * value);
                                        res[2 * i + 1] += -value;
                                        #endregion
                                        break;
                                    case -2:
                                        #region Шаг 1.1.1.2.1. Ограничение по левой границе. //   X - R >= 0
                                        value = X[2 * i] - circles[i].Value + e;
                                        value = 1 / (value * value);
                                        res[2 * i] += value;
                                        #endregion
                                        break;
                                    case -3:
                                        #region Шаг 1.1.1.2.2. Ограничение по нижней границе. //  Y - R >= 0
                                        value = X[2 * i + 1] - circles[i].Value + e;
                                        value = 1 / (value * value);
                                        res[2 * i + 1] += value;
                                        #endregion
                                        break;
                                    case -4:
                                        #region Шаг 1.1.1.2.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                                        value = -X[2 * i] - circles[i].Value + X[index_strip] + e;
                                        value = 1 / (value * value);
                                        res[2 * i] += -value;
                                        res[index_strip] += value;
                                        #endregion
                                        break;
                                }
                                #endregion
                            }
                        }
                    }
                    vertex = vertex.Next;
                }
                vertex.Somes.LastChecked = data;
            }
            #endregion

            return res;



            #region Вариант.
            //Matrix.Clear(res);
            //double value;
            //#region Шаг 1.1.1.1. Для каждого круга...
            //for (int i = 0; i < circles.Length; i++)
            //{
            //    #region Шаг 1.1.1.2. Проверяем и суммируем ограничения (G >= 0) по полосе.
            //    #region Шаг 1.1.1.2.1. Ограничение по левой границе. //   X - R >= 0
            //    value = X[2 * i] - circles[i].Value + eps;
            //    value = 1 / (value * value);
            //    res[2 * i] += value;
            //    #endregion
            //    #region Шаг 1.1.1.2.2. Ограничение по нижней границе. //  Y - R >= 0
            //    value = X[2 * i + 1] - circles[i].Value + eps;
            //    value = 1 / (value * value);
            //    res[2 * i + 1] += value;
            //    #endregion
            //    #region Шаг 1.1.1.2.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
            //    value = -X[2 * i + 1] - circles[i].Value + height + eps;
            //    value = 1 / (value * value);
            //    res[2 * i + 1] += -value;
            //    #endregion
            //    #region Шаг 1.1.1.2.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
            //    value = -X[2 * i] - circles[i].Value + X[index_strip] + eps;
            //    value = 1 / (value * value);
            //    res[2 * i] += -value;
            //    res[index_strip] += value;
            //    #endregion
            //    #endregion
            //}
            //#endregion


            //#region Шаг 1. Для каждой пары соседних объектов...
            //DateTime date = DateTime.Now; //задаем точку отчета времени для проверки прошли мы вершину или нет

            //for (int i = 0; i < triples.Count; i++) // проверяем и суммируем ограничения между кругами
            //{
            //    Vertex<Geometric2d> vertex_temp = triples[i];
            //    do
            //    {
            //        if (vertex_temp.Cros.Somes.LastChecked != date)
            //        {
            //            if ((vertex_temp.Next.DataInVertex is Circle) && (vertex_temp.Prev.DataInVertex is Circle))
            //            {
            //                int ID_Next = vertex_temp.Next.DataInVertex.ID;
            //                int ID_Prev = vertex_temp.Prev.DataInVertex.ID;

            //                double x = X[2 * ID_Next] - X[2 * ID_Prev];
            //                double y = X[2 * ID_Next + 1] - X[2 * ID_Prev + 1];
            //                double r = circles[ID_Prev].Value + circles[ID_Next].Value;
            //                value = Math.Sqrt(x * x + y * y) - r;
            //                value = 1 / (value * value);

            //                x = 2 * (X[2 * ID_Next] - X[2 * ID_Prev]) * value;
            //                res[2 * ID_Next] += x;
            //                res[2 * ID_Prev] += -x;
                            
            //                y = 2 * (X[2 * ID_Next + 1] - X[2 * ID_Prev + 1]) * value;
            //                res[2 * ID_Next + 1] += y;
            //                res[2 * ID_Prev + 1] += -y;
            //            }
            //            if ((vertex_temp.Next.DataInVertex is Circle) && (vertex_temp.Prev.DataInVertex is Plane))
            //            {
            //                // TODO: Расчитать ограничение для круга и полуплоскости.
            //            }
            //            if ((vertex_temp.Next.DataInVertex is Plane) && (vertex_temp.Prev.DataInVertex is Circle))
            //            {
            //                // TODO: Расчитать ограничение для круга и полуплоскости.
            //            }
            //        }

            //        vertex_temp.Somes.LastChecked = date;

            //        vertex_temp = vertex_temp.Next;
            //    } while (vertex_temp != triples[i]);
            //}

            //#endregion

            //return res;
            #endregion
        }

        /// <summary>
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        protected override void Calculate()
        {
            #region Шаг 1. Метод барьеров. Пока значение дополнительной части функции больше погрешности...
            do
            {
                #region Шаг 1.1. Метод антиградиента (метод найскорейшего спуска). Пока происходит движение в сторону антиградиента...

                // Значение дополнительной части функции цели.
                double value;
                // Вектор движения.
                double[] d = new double[X.Length];
                // Длина вектора движения.
                double length;

                do
                {
                    #region Шаг 1.1.1. Находим вектор антиградиента в текущей точке.
                    #region d <-- grad(F) - mu * Sum(grad(G) / G^2(X));
                    GradGs(X, d);
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
                    {
                        Matrix.Summate(X, Matrix.Multiply(d, length));

                        FillCircles();
                        #region Перестроение триангуляции. Переделать!
                        for (int i = 0; i < triples.Count; i++)
                        {
                            Vertex<Geometric2d> vertex_temp = triples[i];
                            vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                        }

                        #region Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
                        {
                            for (int i = 0; i < triples.Count; i++)
                            {
                                Vertex<Geometric2d> vertex_temp = triples[i];
                                do
                                {
                                    while (GeometricExt.Расширенное_расстояние(vertex_temp.DataInVertex, vertex_temp.Cros.Somes.CircleDelone) < 0)
                                    {
                                        vertex_temp.Rebuild();

                                        vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                                        vertex_temp.Next.Cros.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Next.Cros.Prev.DataInVertex, vertex_temp.Next.Cros.DataInVertex, vertex_temp.Next.Cros.Next.DataInVertex));
                                    }

                                    vertex_temp = vertex_temp.Next.Cros.Next;
                                } while (vertex_temp != triples[i]);
                            }

                            for (int i = 0; i < triples.Count; i++)
                            {
                                Vertex<Geometric2d> vertex_temp = triples[i];
                                vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                            }
                        }

                        #endregion
                        #endregion


                    }
                    #endregion
                } while (length > eps);
                #endregion

                #region Шаг 1.2. Изменяем коэффициент mu для поиска более точного решения.
                mu *= beta;
                #endregion
            } while (mu * ValueGsWithCheck(X) > eps);
            #endregion
        }


        public override void CalculateStart()
        {
            base.CalculateStart();

            (this.vertex.DataInVertex as Plane2d).Pole.X = length;
            this.vertex.Somes.CircleDelone.Pole.X = length - height / 2;

            #region Перестроение триангуляции. Переделать!
            for (int i = 0; i < triples.Count; i++)
            {
                Vertex<Geometric2d> vertex_temp = triples[i];
                vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
            }

            #region Проверяем и переразбиваем модель вокруг вершины, связанной со вставленным объектом.
            //for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < triples.Count; i++)
                {
                    Vertex<Geometric2d> vertex_temp = triples[i];
                    do
                    {
                        //if (vertex_temp.DataInVertex is Circle)
                            while (GeometricExt.Расширенное_расстояние(vertex_temp.DataInVertex, vertex_temp.Cros.Somes.CircleDelone) < 0)
                            {
                                vertex_temp.Rebuild();

                                vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                                vertex_temp.Next.Cros.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Next.Cros.Prev.DataInVertex, vertex_temp.Next.Cros.DataInVertex, vertex_temp.Next.Cros.Next.DataInVertex));
                            }

                        vertex_temp = vertex_temp.Next.Cros.Next;
                    } while (vertex_temp != triples[i]);
                }

                //for (int i = 0; i < triples.Count; i++)
                //{
                //    Vertex<Geometric2d> vertex_temp = triples[i];
                //    vertex_temp.SetCircleDelone(GeometricExt.Круг_Делоне(vertex_temp.Prev.DataInVertex, vertex_temp.DataInVertex, vertex_temp.Next.DataInVertex));
                //}
            }

            #endregion
            #endregion

        }
    }
}
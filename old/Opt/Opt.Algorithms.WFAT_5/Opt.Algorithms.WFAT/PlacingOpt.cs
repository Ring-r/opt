using System;

using Opt.Geometrics;
using Opt.Geometrics.SpecialGeometrics;

namespace Opt.Algorithms.WFAT
{
    public class PlacingOpt
    {
        private double height;
        public double Height
        {
            get
            {
                return height;
            }
        }
        private double length;
        public double Length
        {
            get
            {
                return length;
            }
        }
        private Circle[] circles;

        /// <summary>
        /// Точка, заданная массивом коэффициентов.
        /// </summary>
        private double[] X;

        ///<summary>
        /// Индекс элемента вектора, который отвечает за длину занятой полосы.
        /// </summary>
        private int index_strip;

        private double mu;
        private double beta;
        private double eps;

        public PlacingOpt(double height, double length, Circle[] circles, double mu, double beta, double eps)
        {
            this.height = height;
            this.length = length;
            this.circles = circles;

            #region Временный код.
            for (int i = 0; i < circles.Length; i++)
                circles[i].Radius -= 1;
            #endregion

            int n = circles.Length;
            X = new double[2 * n + 1];

            index_strip = 2 * n;

            this.mu = mu;
            this.beta = beta;
            this.eps = eps;
        }

        #region Операции с векторами.
        private double[] Clear(double[] vector_this)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] = 0;
            return vector_this;
        }
        private double[] Copy(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector[i] = vector_this[i];
            return vector;
        }
        private double[] Summation(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] += vector[i];
            return vector_this;
        }
        private double[] Multiplication(double[] vector_this, double value)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] *= value;
            return vector_this;
        }
        private double MultiplicationValue(double[] vector_this, double[] vector)
        {
            double res = 0;
            for (int i = 0; i < vector_this.Length; i++)
                res += vector_this[i] * vector[i];
            return res;
        }
        #endregion

        /// <summary>
        /// Определение дополнительной части функции цели в заданной точке.
        /// </summary>
        /// <param name="X">Заданная точка.</param>
        /// <returns>Дополнительная часть функции цели в заданной точке.</returns>
        private double ValueGsWithCheck(double[] X)
        {
            double res = 0;
            double value = 0;
            #region Шаг 1. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                #region Шаг 1.1. Проверяем и суммируем ограничения (G >= 0) по полосе.
                #region Шаг 1.1.1. Ограничение по левой границе. //   X - R >= 0
                value = X[2 * i] - circles[i].Radius;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.2. Ограничение по нижней границе. //  Y - R >= 0
                value = X[2 * i + 1] - circles[i].Radius;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                value = -X[2 * i + 1] - circles[i].Radius + height;
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
                #endregion
                #region Шаг 1.1.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                value = -X[2 * i] - circles[i].Radius + X[index_strip];
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
                    double r = circles[i].Radius + circles[j].Radius;
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
        private double[] GradGs(double[] X, double[] res)
        {
            Clear(res);
            double value;
            #region Шаг 1.1.1.1. Для каждого круга...
            for (int i = 0; i < circles.Length; i++)
            {
                // TODO: Здесь ошибка! Найти и исправить!
                #region Шаг 1.1.1.2. Проверяем и суммируем ограничения (G >= 0) по полосе.
                #region Шаг 1.1.1.2.1. Ограничение по левой границе. //   X - R >= 0
                value = X[2 * i] - circles[i].Radius;
                value = 1 / (value * value);
                res[2 * i] += value;
                #endregion
                #region Шаг 1.1.1.2.2. Ограничение по нижней границе. //  Y - R >= 0
                value = X[2 * i + 1] - circles[i].Radius;
                value = 1 / (value * value);
                res[2 * i + 1] += value;
                #endregion
                #region Шаг 1.1.1.2.3. Ограничение по верхней границе. // Y + R <= H  --> -Y - R + H >= 0
                value = -X[2 * i + 1] - circles[i].Radius + height;
                value = 1 / (value * value);
                res[2 * i + 1] += -value;
                #endregion
                #region Шаг 1.1.1.2.4. Ограничение по правой границе. //  X + R <= Z  --> -X - R + Z >= 0
                value = -X[2 * i] - circles[i].Radius + X[index_strip];
                value = 1 / (value * value);
                res[2 * i + 1] += -value;
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
                    double r = circles[i].Radius + circles[j].Radius;
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
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        private void Calculate()
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
                    Multiplication(d, -mu);
                    d[index_strip] += 1;
                    #endregion
                    Multiplication(d, -1);
                    #endregion

                    #region Шаг 1.1.2. Нормируем вектор антиградиента в текущей точке.
                    Multiplication(d, 1 / Math.Sqrt(MultiplicationValue(d, d)));
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
                        Copy(d, XX);
                        Multiplication(XX, length);
                        Summation(XX, X);
                        #endregion

                        value = XX[index_strip] + mu * ValueGsWithCheck(XX);
                        if (value < value_a)
                        {
                            if (value > value_b)
                            {
                                value_a = value;
                                length_a = length;
                            }
                            else
                            {
                                #region XX_eps <-- X + d * (length - eps / 2);
                                Copy(d, XX_eps);
                                Multiplication(XX_eps, length - eps / 2);
                                Summation(XX_eps, X);
                                #endregion

                                double value_eps = XX_eps[index_strip] + mu * ValueGsWithCheck(XX_eps);
                                if (value < value_eps)
                                {
                                    value_a = value;
                                    length_a = length;
                                }
                                else
                                {
                                    value_b = value;
                                    length_b = length;
                                }
                            }
                        }
                        else
                        {
                            length_b = length;
                            value_b = value;
                        }
                    }

                    length = length_a;
                    #endregion

                    #region Шаг 1.1.4. Вычисляем новую точку решения задачи.
                    if (length > eps)
                        Summation(X, Multiplication(d, length));
                    #endregion
                } while (length > eps);
                #endregion

                #region Шаг 1.2. Изменяем коэффициент mu для поиска более точного решения.
                mu *= beta;
                #endregion
            } while (mu * ValueGsWithCheck(X) > eps);
            #endregion
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
        /// Метод подготовки данных и запуска алгоритма размещения круг (метод барьерных функций).
        /// </summary>
        public void CalculateStart()
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
}
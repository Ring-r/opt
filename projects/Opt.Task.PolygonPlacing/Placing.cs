using System;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Extentions;
using Opt.Geometrics.SpecialGeometrics;

namespace PolygonPlacingTest
{
    public class Placing
    {
        /// <summary>
        /// Объекты размещения. Массив многоугольников.
        /// </summary>
        private PolygonWithMinMax[] polygons;
        /// <summary>
        /// Область размещения. Размеры полосы.
        /// </summary>
        private Vector2d strip_vector;

        /// <summary>
        /// Функция цели, заданная массивом коеффициентов.
        /// </summary>
        private double[] F;
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

        public Placing(PolygonWithMinMax[] polygons, Vector2d strip_vector)
        {
            this.polygons = polygons;
            this.strip_vector = strip_vector;

            int n = polygons.Length;
            X = new double[2 * n + 1];
            F = new double[2 * n + 2];
            Gs = new double[4 * n + n * (n - 1) / 2][];
            for (int i = 0; i < Gs.Length; i++)
                Gs[i] = new double[2 * n + 2];

            index_strip = 2 * n;
        }

        #region Операции с векторами.
        private double[] Clear(double[] vector_this)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] = 0;
            return vector_this;
        }
        private double[][] Clear(double[][] matrix_this)
        {
            for (int i = 0; i < matrix_this.Length; i++)
                Clear(matrix_this[i]);
            return matrix_this;
        }
        private double[] Copy(double[] vector_this)
        {
            double[] res = new double[vector_this.Length];
            for (int i = 0; i < vector_this.Length; i++)
                res[i] = vector_this[i];
            return res;
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
        private double[] Multiplication(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] *= vector[i];
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

        #region Операции с функциями.
        /// <summary>
        /// Расчёт значения линейной функции F в точке X.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Значение линейной функции F в точке X.</returns>
        private double Value(double[] F, double[] X)
        {
            double res = F[F.Length - 1];
            for (int i = 0; i < X.Length; i++)
                res += F[i] * X[i];
            return res;
        }
        /// <summary>
        /// Расчёт значения дополнительной части функции, используемой в методе барьеров.
        /// </summary>
        /// <param name="Gs">Набор линейных ограничений, заданных массивами коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Значение дополнительной части функции, используемой в методе барьеров.</returns>
        private double Value(double[][] Gs, double[] X)
        {
            double res = 0;
            foreach (double[] G in Gs)
                res += 1 / Value(G, X);
            return res;
        }
        /// <summary>
        /// Вычисление значение функции, используемой в методе барьеров.
        /// </summary>
        /// <param name="F">Основная функция.</param>
        /// <param name="Gs">Список ограничений.</param>
        /// <param name="X">Точка, в которой необходимо определить значение функции.</param>
        /// <param name="mu">Дополнительный параметр, используемый при создании барьерной функции.</param>
        /// <returns></returns>
        private double Value(double[] F, double[][] Gs, double[] X, double mu)
        {
            return Value(F, X) + mu * Value(Gs, X);
        }
        /// <summary>
        /// Вычисление значение функции, используемой в методе барьеров, с проверкой выхода из области допустимых значений.
        /// </summary>
        /// <param name="F">Основная функция.</param>
        /// <param name="Gs">Список ограничений.</param>
        /// <param name="X">Точка, в которой необходимо определить значение функции.</param>
        /// <param name="mu">Дополнительный параметр, используемый при создании барьерной функции.</param>
        /// <returns></returns>
        private double ValueWithCheck(double[] F, double[][] Gs, double[] X, double mu)
        {
            double res = 0;

            foreach (double[] G in Gs)
            {
                double value = Value(G, X);
                if (value > 0)
                    res += 1 / value;
                else
                    return double.PositiveInfinity;
            }

            #region return F(X) + mu * Sum(G(X));
            return Value(F, X) + mu * res;
            #endregion
        }
        /// <summary>
        /// Градиент линейной функции.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <returns>Градиент линейной функции, заданный массивом данных.</returns>
        private double[] Grad(double[] F)
        {
            double[] res = new double[F.Length - 1];
            for (int i = 0; i < res.Length; i++)
                res[i] = F[i];
            return res;
        }
        /// <summary>
        /// Расчёт вектора градиента для полной барьерной функции F с ограничениями Gs в точке X.
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="Gs">Набор линейных ограничений, заданных массивами коэффициентов.</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <returns>Вектора градиента для полной барьерной функции F с ограничениями Gs в точке X.</returns>
        private double[] Grad(double[] F, double[][] Gs, double[] X, double mu)
        {
            double[] res = Grad(F);
            foreach (double[] G in Gs)
            {
                double value = Value(G, X);
                Summation(res, Multiplication(Grad(G), -mu / (value * value)));
            }
            return res;
        }
        #endregion

        /// <summary>
        /// Поиск локального минимума с использованием метода барьеров (многомерный условный поиск).
        /// </summary>
        /// <param name="F">Линейная функция, заданная массивом коэффициентов.</param>
        /// <param name="Gs">Набор линейных ограничений, заданных массивами коэффициентов (в виде Gs(x)<=0)</param>
        /// <param name="X">Точка, заданная массивом коэффициентов.</param>
        /// <param name="mu"></param>
        /// <param name="beta"></param>
        /// <param name="eps">Погрешность.</param>
        private void Calculate(double mu, double beta, double eps)
        {
            #region Шаг 1. Метод барьеров. Пока значение дополнительной части функции больше погрешности...
            while (mu * Value(Gs, X) > eps)
            {
                #region Шаг 1.1. Метод антиградиента (метод найскорейшего спуска). Пока происходит движение в сторону антиградиента...
                double value;
                double length;

                do
                {
                    #region Шаг 1.1.1. Находим вектор вектор антиградиента в текущей точке.
                    double[] d = Multiplication(Grad(F, Gs, X, mu), -1);
                    #endregion

                    #region Шаг 1.1.2. Нормируем вектор антиградиента в текущей точке.
                    Multiplication(d, 1 / Math.Sqrt(MultiplicationValue(d, d)));
                    #endregion

                    #region Шаг 1.1.3. Находим длину шага (одномерный поиск - метод половинного деления).

                    double length_a = 0;
                    double value_a = Value(F, Gs, X, mu);

                    // TODO: Подобрать начальную длину шага. Сложность метода половинного деления равна log(length_b/eps,2). Чем больше длина шага, тем быстрее работает метод найскорейшего спуска. Чем меньше длина шага тем быстрее работает методполовинного деления.
                    double length_b = X[index_strip]; // Длина максимально возможного шага.
                    double value_b = double.PositiveInfinity;

                    double[] XX = new double[X.Length];

                    while (Math.Abs(length_b - length_a) > eps)
                    {
                        length = (length_a + length_b) / 2;

                        #region XX <-- X + d * length;
                        Copy(d, XX);
                        Multiplication(XX, length);
                        Summation(XX, X);
                        #endregion

                        value = ValueWithCheck(F, Gs, XX, mu);
                        if (value < value_a)
                        {
                            if (value_b > value_a)
                            {
                                length_b = length_a;
                                value_b = value_a;
                            }

                            length_a = length;
                            value_a = value;
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
            double width_old;
            do
            {
                width_old = strip_vector.X;

                #region Шаг 1. Создаём начальную точку.
                FillPoint();
                #endregion

                #region Шаг 2. Создаём функцию цели.
                Clear(F);
                F[index_strip] = 1;
                #endregion

                #region Шаг 3. Создаём набор ограничений.
                Clear(Gs);
                int k = 0;
                #region Шаг 3.1. Для каждого многоугольника...
                for (int i = 0; i < polygons.Length; i++)
                {
                    #region Шаг 3.1.1. Создаём набор ограничений (G >= 0) по полосе.
                    #region Шаг 3.1.1.1. Ограничение по нижней границе. //  Y - min.Y >= 0
                    Gs[k][2 * i + 1] = 1;
                    Gs[k][index_strip + 1] = -polygons[i].MinY;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.2. Ограничение по левой границе. //   X - min.X >= 0
                    Gs[k][2 * i] = 1;
                    Gs[k][index_strip + 1] = -polygons[i].MinX;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.3. Ограничение по верхней границе. // Y + max.Y <= H  --> -Y - max.Y + H >= 0
                    Gs[k][2 * i + 1] = -1;
                    Gs[k][index_strip + 1] = -polygons[i].MaxY + strip_vector.Y;
                    k++;
                    #endregion
                    #region Шаг 3.1.1.4. Ограничение по правой границе. //  X + max.X <= Z  --> -X + Z - max.X >= 0
                    Gs[k][2 * i] = -1;
                    Gs[k][index_strip] = 1;
                    Gs[k][index_strip + 1] = -polygons[i].MaxX;
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

                        Gs[k][2 * pd.IteratorPoint.IndexPolygon] = -normal.X;
                        Gs[k][2 * pd.IteratorPoint.IndexPolygon + 1] = -normal.Y;
                        Gs[k][2 * pd.IteratorPlane.IndexPolygon] = normal.X;
                        Gs[k][2 * pd.IteratorPlane.IndexPolygon + 1] = normal.Y;
                        Gs[k][index_strip + 1] = -((pd.IteratorPoint.Polygon[pd.IteratorPoint.Index] - pd.IteratorPlane.Polygon[pd.IteratorPlane.Index]) * normal);
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
    }
}
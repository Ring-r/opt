using System;

namespace Opt.Geometrics.Geometrics2d.Extentions
{
    /// <summary>
    /// Расширения для геометрического объекта.
    /// </summary>
    public static class Geometric2dExt
    {
        #region Расширенное расстояние.
        /// <summary>
        /// Получить расширенное расстояние от геометрического объекта до геометрического объекта.
        /// </summary>
        /// <param name="geometric_this">Геометрический объект.</param>
        /// <param name="geometric">Геометрический объект.</param>
        /// <returns>Расширенное расстояние.</returns>
        public static double Расширенное_расстояние(Geometric2d geometric_this, Geometric2d geometric)
        {
            if (geometric_this is Point2d)
            {
                if (geometric is Point2d)
                    return Point2dExt.Расширенное_расстояние(geometric_this as Point2d, geometric as Point2d);
                if (geometric is Geometric2dWithPointScalar)
                    return Circle2dExt.Расширенное_расстояние(geometric as Geometric2dWithPointScalar, geometric_this as Point2d);
                if (geometric is Plane2d)
                    return Plane2dExt.Расширенное_расстояние(geometric as Plane2d, geometric_this as Point2d);
            }
            if (geometric_this is Geometric2dWithPointScalar)
            {
                if (geometric is Point2d)
                    return Circle2dExt.Расширенное_расстояние(geometric_this as Geometric2dWithPointScalar, geometric as Point2d);
                if (geometric is Geometric2dWithPointScalar)
                    return Circle2dExt.Расширенное_расстояние(geometric_this as Geometric2dWithPointScalar, geometric as Geometric2dWithPointScalar);
                if (geometric is Plane2d)
                    return Plane2dExt.Расширенное_расстояние(geometric as Plane2d, geometric_this as Geometric2dWithPointScalar);
            }
            if (geometric_this is Plane2d)
            {
                if (geometric is Point2d)
                    return Plane2dExt.Расширенное_расстояние(geometric_this as Plane2d, geometric as Point2d);
                if (geometric is Geometric2dWithPointScalar)
                    return Plane2dExt.Расширенное_расстояние(geometric_this as Plane2d, geometric as Geometric2dWithPointScalar);
                if (geometric is Plane2d)
                    return Plane2dExt.Расширенное_расстояние(geometric_this as Plane2d, geometric as Plane2d);
            }
            return double.NaN;
        }
        #endregion

        #region Годограф функции плотного размещения.
        /// <summary>
        /// Получить годограф функции плотного размещения круга и геометрического объекта.
        /// </summary>
        /// <param name="geometric_this">Геометрический объект.</param>
        /// <param name="circle">Круг.</param>
        /// <returns>Годограф функции плотного размещения.</returns>
        public static Geometric2d Годограф_функции_плотного_размещения(Geometric2d geometric_this, Geometric2dWithPointScalar circle)
        {
            if (geometric_this is Geometric2dWithPointScalar)
                return Circle2dExt.Годограф_функции_плотного_размещения(geometric_this as Geometric2dWithPointScalar, circle);
            if (geometric_this is Plane2d)
                return Plane2dExt.Годограф_функции_плотного_размещения(geometric_this as Plane2d, circle);
            return null;
        }
        #endregion

        #region Точка пересечения границ.
        /// <summary>
        /// Получить точку пересечения геометрического объекта и геометрического объекта.
        /// </summary>
        /// <param name="geometric_prev">Геометрический объект.</param>
        /// <param name="geometric_next">Геометрический объект.</param>
        /// <returns>Точка пересечения.</returns>
        public static Point2d Точка_пересечения_границ(Geometric2d geometric_prev, Geometric2d geometric_next)
        {
            if (geometric_prev is Geometric2dWithPointScalar)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return Circle2dExt.Точка_пересечения_границ(geometric_prev as Geometric2dWithPointScalar, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return Circle2dExt.Точка_пересечения_границ(geometric_prev as Geometric2dWithPointScalar, geometric_next as Plane2d);
            }
            if (geometric_prev is Plane2d)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return Plane2dExt.Точка_пересечения_границ(geometric_prev as Plane2d, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return Plane2dExt.Точка_пересечения_границ(geometric_prev as Plane2d, geometric_next as Plane2d);
            }
            return null;
        }
        #endregion

        #region Точка близости второго рода.
        /// <summary>
        /// Получить точку близости второго рода для круга и множества (геометрический объект, геометрический объект).
        /// </summary>
        /// <param name="circle_this">Круг.</param>
        /// <param name="geometric_prev">Геометрический объект.</param>
        /// <param name="geometric_next">Геометрический объект.</param>
        /// <returns>Точка близости.</returns>
        public static Point2d Точка_близости_второго_рода(this Geometric2dWithPointScalar circle_this, Geometric2d geometric_prev, Geometric2d geometric_next)
        {
            if (geometric_prev is Geometric2dWithPointScalar)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return circle_this.Точка_близости_второго_рода(geometric_prev as Geometric2dWithPointScalar, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return circle_this.Точка_близости_второго_рода(geometric_prev as Geometric2dWithPointScalar, geometric_next as Plane2d);
            }
            if (geometric_prev is Plane2d)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return circle_this.Точка_близости_второго_рода(geometric_prev as Plane2d, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return circle_this.Точка_близости_второго_рода(geometric_prev as Plane2d, geometric_next as Plane2d);
            }
            return null;
        }
        #endregion

        #region Серединная полуплоскость.
        /// <summary>
        /// Получить серединную полуплоскость геометрического объекта и геометрического объекта.
        /// </summary>
        /// <param name="geometric_prev">Геометрический объект.</param>
        /// <param name="geometric_next">Геометрический объект.</param>
        /// <returns>Серединная полуплоскость.</returns>
        public static Plane2d Серединная_полуплоскость(Geometric2d geometric_prev, Geometric2d geometric_next)
        {
            if (geometric_prev is Geometric2dWithPointScalar)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return Circle2dExt.Серединная_полуплоскость(geometric_prev as Geometric2dWithPointScalar, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return Circle2dExt.Серединная_полуплоскость(geometric_prev as Geometric2dWithPointScalar, geometric_next as Plane2d);
            }
            if (geometric_prev is Plane2d)
            {
                if (geometric_next is Geometric2dWithPointScalar)
                    return Plane2dExt.Серединная_полуплоскость(geometric_prev as Plane2d, geometric_next as Geometric2dWithPointScalar);
                if (geometric_next is Plane2d)
                    return Plane2dExt.Серединная_полуплоскость(geometric_prev as Plane2d, geometric_next as Plane2d);
            }
            return null;
        }
        #endregion

        #region Круг Делоне.
        public static Geometric2dWithPointScalar Круг_Делоне(params Geometric2d[] geometrics)
        {
            int dim = geometrics.Length - 1;

            Geometric2dWithPointScalar circle_base = null;
            System.Collections.Generic.List<double[]> matr = new System.Collections.Generic.List<double[]>(dim + 1);

            #region Создание системы линейных уравнений.
            for (int i = 0; i < dim + 1; i++)
            {
                if (geometrics[i] is Geometric2dWithPointScalar)
                {
                    if (circle_base == null)
                        circle_base = geometrics[i] as Geometric2dWithPointScalar;
                    else
                    {
                        Geometric2dWithPointScalar circle = geometrics[i] as Geometric2dWithPointScalar;
                        double[] vect = new double[dim + 2];
                        for (int j = 0; j < dim; j++)
                        {
                            vect[j] = circle.Pole[j + 1] - circle_base.Pole[j + 1];
                            vect[dim + 1] -= vect[j] * (circle.Pole[j + 1] + circle_base.Pole[j + 1]);
                        }
                        vect[dim] = circle.Scalar - circle_base.Scalar;
                        vect[dim + 1] += vect[dim] * (circle.Scalar + circle_base.Scalar);
                        vect[dim + 1] /= 2;
                        matr.Add(vect);
                    }
                }
                else if (geometrics[i] is Plane2d)
                {
                    Plane2d plane = geometrics[i] as Plane2d;
                    double[] vect = new double[dim + 2];
                    for (int j = 0; j < dim; j++)
                    {
                        vect[j] = plane.Normal[j + 1];
                        vect[dim + 1] -= vect[j] * plane.Pole[j + 1];
                    }
                    vect[dim] = -1;
                    matr.Add(vect);
                }
                else
                    return null;
            }
            #endregion

            if (matr.Count < dim)
                return null; // Исключительная ситуация! Недостаточно информации!!

            #region Решение системы линейных уравнений.
            int m = matr.Count;
            int index = dim;
            for (int i = 0; i < m; i++)
            {
                #region Получаем диагональный (базисный) элемент.
                double a;
                if (i < index) // Тут ошибка.
                    a = matr[i][i];
                else
                    a = matr[i][i + 1];
                #endregion
                if (a != 0)
                {
                    #region Превращаем диагональный элемент в 1.
                    for (int j = i; j < dim + 2; j++)
                        matr[i][j] /= a;
                    #endregion

                    #region Для каждой строки...
                    for (int ii = 0; ii < m; ii++)
                    {
                        #region Получаем поддиагональный (подбазисный) элемент.
                        if (i < index)
                            a = matr[ii][i];
                        else
                            a = matr[ii][i + 1];
                        #endregion

                        #region Превращаем поддиагональный (подбазисный) элемент в 0. Пропускаем строку с диагональным (базисным) элементом и строки, которые не надо изменять.
                        if (ii != i && a != 0)
                            for (int j = 0; j < dim + 2; j++)
                                matr[ii][j] -= matr[i][j] * a;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Поиск строки с базисным элементом не 0.
                    int ii = i + 1;
                    while (ii < m && matr[ii][i] == 0)
                        ii++;
                    #endregion

                    #region Если такой элемент найден, то меняем местами строки. Иначе меняем значение index.
                    if (ii < m)
                    {
                        double[] t = matr[i];
                        matr[i] = matr[ii];
                        matr[ii] = t;
                    }
                    else if (index == dim)
                        index = i;
                    else
                        return null;
                    i--;
                    #endregion
                }
            }
            #endregion

            if (circle_base != null)
            {
                #region Получение результата.
                double[,] res = new double[2, dim + 1];
                for (int i = 0; i < index; i++)
                {
                    res[0, i] = -matr[i][index];
                    res[1, i] = -matr[i][dim + 1];
                }

                res[0, index] = 1;
                res[1, index] = 0;

                for (int i = index; i < m; i++)
                {
                    res[0, i + 1] = -matr[i][index];
                    res[1, i + 1] = -matr[i][dim + 1];
                }
                #endregion

                #region Построение квадратичного уравнения.
                for (int i = 0; i < dim; i++)
                    res[1, i] -= circle_base.Pole[i + 1];
                res[1, dim] += circle_base.Scalar;

                double A = -res[0, dim] * res[0, dim];
                double B = -res[0, dim] * res[1, dim];
                double C = -res[1, dim] * res[1, dim];
                for (int i = 0; i < dim; i++)
                {
                    A += res[0, i] * res[0, i];
                    B += res[0, i] * res[1, i];
                    C += res[1, i] * res[1, i];
                }

                for (int i = 0; i < dim; i++)
                    res[1, i] += circle_base.Pole[i + 1];
                res[1, dim] -= circle_base.Scalar;
                #endregion

                #region Решение квадратичного уравнения.
                double D = B * B - A * C;
                if (D < 0)
                    return null;

                D = Math.Sqrt(D);
                double r_prev = (-B - D) / A;
                double r_next = (-B + D) / A;

                if (A == 0) // Откуда? Практика показала, что такая ситуация возможна. Может ошибка в другом?
                {
                    r_prev = -C / B / 2;
                    r_next = -C / B / 2;
                }
                #endregion

                #region Построение кругов Делоне.
                Geometric2dWithPointScalar circle_delone_prev = new Geometric2dWithPointScalar { Scalar = res[0, dim] * r_prev + res[1, dim] };
                Geometric2dWithPointScalar circle_delone_next = new Geometric2dWithPointScalar { Scalar = res[0, dim] * r_next + res[1, dim] };
                for (int i = 0; i < dim; i++)
                {
                    circle_delone_prev.Pole[i + 1] = res[0, i] * r_prev + res[1, i];
                    circle_delone_next.Pole[i + 1] = res[0, i] * r_next + res[1, i];
                }
                #endregion

                #region Выбор правильного круга Делоне.
                Vector2d[] vectors = new Vector2d[dim + 1];
                for (int i = 0; i < dim + 1; i++)
                {
                    if (geometrics[i] is Geometric2dWithPointScalar)
                        vectors[i] = ((geometrics[i] as Geometric2dWithPointScalar).Pole - circle_delone_prev.Pole) / ((geometrics[i] as Geometric2dWithPointScalar).Scalar + circle_delone_prev.Scalar);
                    else
                        vectors[i] = (geometrics[i] as Plane2d).Normal * (-1);
                }
                // Векторное произведение? Определитель матрицы, состоящей из векторов.
                double s = vectors[0].X * (vectors[1].Y - vectors[2].Y) + vectors[1].X * (vectors[2].Y - vectors[0].Y) + vectors[2].X * (vectors[0].Y - vectors[1].Y);

                if (s * circle_delone_prev.Scalar > 0)
                    return circle_delone_prev;
                else
                    return circle_delone_next;
                #endregion
            }
            else
            {
                #region Возврат решения линейного уравнения.
                if (index == dim && matr[dim][dim] != 0)
                {
                    Geometric2dWithPointScalar circle_delone = new Geometric2dWithPointScalar { Scalar = -matr[dim][dim + 1] };
                    for (int i = 0; i < dim; i++)
                        circle_delone.Pole[i + 1] = -matr[i][dim + 1];
                    return circle_delone;
                }
                else
                    return null;
                #endregion
            }
        }
        #endregion
    }
}
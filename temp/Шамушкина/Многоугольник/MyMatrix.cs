using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class MyMatrix
    {
        //public double[][] data;
        public MyMatrix() { ;}

        public double[][] MatrixCreate(int rows, int cols)
        {
            // Создаем матрицу, полностью инициализированную
            // значениями 0.0. Проверка входных параметров опущена.
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols]; // автоинициализация в 0.0
            return result;
        }

        //перемножение матриц
        public double[][] MatrixProduct(double[][] A, double[][] B)
        {
            int aRows = A.Length;
            int aCols = A[0].Length;

            int bRows = B.Length;
            int bCols = B[0].Length;

            if (aCols != bRows)
                throw new Exception("Матрицы не согласованы");
            double[][] result = MatrixCreate(aRows, bCols);
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i][j] += A[i][k] * B[k][j];
                    }
                }
            }
            return result;
        }

        //создание единичной матрицы
        public double[][] MatrixIdentity(int n)
        {
            double[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; i++)
            {
                result[i][i] = 1;
            }
            return result;
        }

        //находим транспонированную матрицу
        public double[][] MatrixTransposed(double[][] A)
        {
            int aRows = A.Length;
            int aCols = A[0].Length;

            double[][] result = MatrixCreate(aCols, aRows);

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[j][i] = A[i][j];
                }
            }
            return result;
        }

        //сравнение матрицы на равенство (тождественность)
        public bool MatrixAreEqual(double[][] A, double[][] B, double eps)
        {
            int aRows = A.Length;
            int bCols = B[0].Length;
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    if (Math.Abs(A[i][j] - B[i][j]) > eps)
                        return false;
                }
            }
            return true;
        }

        //
        public double[][] MatrixDuplicate(double[][] matrix)
        {
            // Предполагается, что матрица не нулевая 
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // Копирование значений
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
        }

        //метод разложения матрицы
        public double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {
            // Разложение LUP Дулитла. Предполагается,
            // что матрица квадратная.
            int n = matrix.Length; // для удобства
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i) { perm[i] = i; }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) // каждый столбец
            {
                double colMax = Math.Abs(result[j][j]); // Наибольшее значение в столбце j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // перестановка строк
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; // Меняем информацию о перестановке
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // переключатель перестановки строк
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                    return null;
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                        result[i][k] -= result[i][j] * result[j][k];
                }
            } // основной цикл по столбцу j
            return result;
        }

        //решаем систему уравнений
        public double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            // Решаем luMatrix * x = b
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum;
            }
            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum / luMatrix[i][i];
            }
            return x;
        }
        
        //находим обратную матрицу
        public double[][] MatrixInverse(double[][] matrix)
        {
            int n = matrix.Length;
            double[][] result = MatrixDuplicate(matrix);
            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute inverse");
            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < n; ++j)
                    result[j][i] = x[j];
            }
            return result;
        }

        //находим определитель матрицы
        public double MatrixDeterminant(double[][] matrix)
        {
            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant");
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
                result *= lum[i][i];
            return result;
        }

        //решение систем линейных уравнений
        public double[] SystemSolve(double[][] A, double[] b)
        {
            // Решаем Ax = b
            int n = A.Length;
            int[] perm;
            int toggle;
            double[][] luMatrix = MatrixDecompose(
              A, out perm, out toggle);
            if (luMatrix == null)
                return null; // или исключение
            double[] bp = new double[b.Length];
            for (int i = 0; i < n; ++i)
                bp[i] = b[perm[i]];
            double[] x = HelperSolve(luMatrix, bp);
            return x;
        }
    }
}

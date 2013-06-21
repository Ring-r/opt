using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Многоугольник
{
    class Matrix
    {
        private int col_count;
        private List<double[]> data;

        public int RowCount
        {
            get
            {
                return data.Count;
            }
        }
        public int ColCount
        {
            get
            {
                return col_count;
            }
        }

        public Matrix(int row_count)
        {
            this.col_count = 1;
            data = new List<double[]>(row_count);
            for (int i = 0; i < row_count; i++)
                data.Add(new double[col_count]);
        }
        public Matrix(int row_count, int col_count)
        {
            this.col_count = col_count;
            data = new List<double[]>(row_count);
            for (int i = 0; i < row_count; i++)
                data.Add(new double[col_count]);
        }

        
        //Доступ к елементам вектора столбца.
        public double this[int row]
        {
            get
            {
                return data[row][0];
            }
            set
            {
                data[row][0] = value;
            }
        }
        /// <summary>
        /// Доступ к елементам матрицы.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <param name="col">Номер столбца.</param>
        /// <returns>Елемент с заданным номером строки и столбца.</returns>
        public double this[int row, int col]
        {
            get
            {
                return data[row][col];
            }
            set
            {
                data[row][col] = value;
            }
        }

        /// <summary>
        /// Добавить строку в конец матрицы.
        /// </summary>
        public void AddRow()
        {
            data.Add(new double[col_count]);
        }
        /// <summary>
        /// Удалить строку из матрицы.
        /// </summary>
        /// <param name="index">Номер удаляемой строки.</param>
        public void DelRow(int index)
        {
            data.RemoveAt(index);
        }

        /// <summary>
        /// Получить единичную квадратную матрицу.
        /// </summary>
        /// <param name="count">Количество строк или столбцов.</param>
        /// <returns>Квадратная матрица с единицами на диагонали.</returns>
        public static Matrix I(int count)
        {
            Matrix res = new Matrix(count, count);
            for (int i = 0; i < count; i++)
                res[i, i] = 1;
            return res;
        }

        /// <summary>
        /// вычисление транспонированой матрицы.
        /// </summary>
        /// <returns>Транспонированная матрица.</returns>
        public Matrix Tr()
        {
            Matrix res = new Matrix(ColCount, RowCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    res[j, i] = this[i, j];
            return res;
        }
        /// <summary>
        /// Вычисление обратной матрицы.
        /// </summary>
        /// <returns>Обратная матрица.</returns>
        public Matrix Ob()
        {
            double[,] temp_data = Convert(this);
            int info;
            alglib.matinvreport rep;
            alglib.rmatrixinverse(ref temp_data, out info, out rep);
            return Matrix.Convert(temp_data);
        }
        /// <summary>
        /// Сравнение с 0-матрицей.
        /// </summary>
        /// <returns></returns>
        public bool IsNull(double eps)
        {
            bool is_null = true;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    is_null = is_null && Math.Abs(this[i, j]) < eps;
            return is_null;
        }
        /// <summary>
        /// Номер строки с минимальным елементом.
        /// </summary>
        /// <returns></returns>
        public int RowOfMinElement()
        {
            int row_min = 0;
            for (int i = 0; i < RowCount; i++)
                if (this[row_min] > this[i])
                    row_min = i;
            return row_min;
        }

        /// <summary>
        /// Номер строки с максимальным елементом.
        /// </summary>
        /// <returns></returns>
        public int RowOfMaxElement()
        {
            int row_max = 0;
            for (int i = 0; i < RowCount; i++)
                if (this[row_max] < this[i])
                    row_max = i;
            return row_max;
        }

        public static Matrix operator -(Matrix data)
        {
            Matrix res = new Matrix(data.RowCount, data.ColCount);
            for (int i = 0; i < data.RowCount; i++)
                for (int j = 0; j < data.ColCount; j++)
                    res[i, j] -= data[i, j];
            return res;
        }
        /// <summary>
        /// Суммирование матриц.
        /// </summary>
        /// <param name="left">Первое слагаемое.</param>
        /// <param name="right">Второе слагаемое.</param>
        /// <returns>Сумма матриц.</returns>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            Matrix res = new Matrix(left.RowCount, left.ColCount);
            for (int i = 0; i < left.RowCount; i++)
                for (int j = 0; j < left.ColCount; j++)
                    res[i, j] = left[i, j] + right[i, j];
            return res;
        }
        /// <summary>
        /// Вычитание матриц.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right">Вычитаемое.</param>
        /// <returns>Разность.</returns>
        public static Matrix operator -(Matrix left, Matrix right)
        {
            Matrix res = new Matrix(left.RowCount, left.ColCount);
            for (int i = 0; i < left.RowCount; i++)
                for (int j = 0; j < left.ColCount; j++)
                    res[i, j] = left[i, j] - right[i, j];
            return res;
        }
        /// <summary>
        /// Произведение матриц.
        /// </summary>
        /// <param name="left">Первый множитель.</param>
        /// <param name="right">Второй множитель.</param>
        /// <returns>Произведение.</returns>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            Matrix res = new Matrix(left.RowCount, right.ColCount);
            for (int i = 0; i < left.RowCount; i++)
                for (int j = 0; j < left.ColCount; j++)
                    for (int k = 0; k < right.ColCount; k++)
                        res[i, k] += left[i, j] * right[j, k];
            return res;
        }
        /// <summary>
        /// Умножение на число.
        /// </summary>
        /// <param name="left">Матрица.</param>
        /// <param name="right">Чичло.</param>
        /// <returns>Матрица.</returns>
        public static Matrix operator *(Matrix left, double right)
        {
            Matrix res = new Matrix(left.RowCount, left.ColCount);
            for (int i = 0; i < left.RowCount; i++)
                for (int j = 0; j < left.ColCount; j++)
                    res[i, j] = left[i, j] * right;
            return res;
        }
        /// <summary>
        /// Умножение на число.
        /// </summary>
        /// <param name="left">Матрица.</param>
        /// <param name="right">Чичло.</param>
        /// <returns>Матрица.</returns>
        public static Matrix operator *(double left, Matrix right)
        {
            Matrix res = new Matrix(right.RowCount, right.ColCount);
            for (int i = 0; i < right.RowCount; i++)
                for (int j = 0; j < right.ColCount; j++)
                    res[i, j] = left * right[i, j];
            return res;
        }

        //преобразование из класса матриц в двумерный массив
        public static double[,] Convert(Matrix data)
        {
            double[,] res = new double[data.RowCount, data.ColCount];
            for (int i = 0; i < data.RowCount; i++)
                for (int j = 0; j < data.ColCount; j++)
                    res[i, j] = data[i, j];
            return res;
        }

        //преоюразование двумерный массив в класс матриц
        public static Matrix Convert(double[,] data)
        {
            Matrix res = new Matrix(data.GetLength(0), data.GetLength(1));
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    res[i, j] = data[i, j];
            return res;
        }
    }
}

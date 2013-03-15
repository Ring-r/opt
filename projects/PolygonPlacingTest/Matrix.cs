using System;

namespace Opt
{
    public static class Matrix
    {
        public static double[] Clear(double[] vector_this)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] = 0;
            return vector_this;
        }
        public static double[][] Clear(double[][] matrix_this)
        {
            for (int i = 0; i < matrix_this.Length; i++)
                Clear(matrix_this[i]);
            return matrix_this;
        }
        public static double[] Copy(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] = vector[i];
            return vector_this;
        }
        public static double[] Summate(double[] vector_this, double[] vector)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] += vector[i];
            return vector_this;
        }
        public static double[] Multiply(double[] vector_this, double value)
        {
            for (int i = 0; i < vector_this.Length; i++)
                vector_this[i] *= value;
            return vector_this;
        }
        public static double MultiplyValue(double[] vector_this, double[] vector)
        {
            double res = 0;
            for (int i = 0; i < vector_this.Length; i++)
                res += vector_this[i] * vector[i];
            return res;
        }
    }
}
using System;

namespace Rectangle3DPlacing
{
    /// <summary>
    /// Абстрактный класс геометрического объекта.
    /// </summary>
    public abstract class R
    {
        /// <summary>
        /// Размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public static int Dim = 3;

        /// <summary>
        /// Класс, хранящий координаты точки или вектора в многомерном пространстве.
        /// </summary>
        public class Coor
        {
            /// <summary>
            /// Значения координаты в многомерном пространстве.
            /// </summary>
            private double[] coor;
            /// <summary>
            /// Возвращает значение координаты, заданной индексом.
            /// </summary>
            /// <param name="index">Индекс.</param>
            /// <returns></returns>
            public double this[int index]
            {
                get
                {
                    return coor[index];
                }
                set
                {
                    coor[index] = value;
                }
            }

            /// <summary>
            /// Конструктор по-умолчанию.
            /// </summary>
            public Coor()
            {
                coor = new double[R.Dim];
            }
        }

        /// <summary>
        /// Размер геометрического объекта, заданный многомерным вектором.
        /// </summary>
        protected Coor size;

        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        public R()
        {
            size = new Coor();
        }

        /// <summary>
        /// Получить координату размера по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата размера.</returns>
        public virtual double Size(int index)
        {
            return size[index];
        }
        /// <summary>
        /// Установить координату размера по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Новое значение координаты размера.</param>
        /// <returns>Координата размера.</returns>
        public virtual double Size(int index, double value)
        {
            size[index] = value;
            return size[index];
        }

        /// <summary>
        /// Скопировать размер во внешнюю переменную.
        /// </summary>
        /// <param name="size">Внешняя переменная.</param>
        public virtual void CopySizeTo(Coor size)
        {
            for (int i = 0; i < Dim; i++)
                size[i] = this.size[i];
        }
        /// <summary>
        /// Скопировать размер из внешней переменной.
        /// </summary>
        /// <param name="size">Внешняя переменная.</param>
        public virtual void CopySizeFrom(Coor size)
        {
            for (int i = 0; i < Dim; i++)
                this.size[i] = size[i];
        }


        /// <summary>
        /// Получить значение минимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата минимума.</returns>
        public virtual double Min(int index)
        {
            return 0;
        }
        /// <summary>
        /// Установить значение минимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Координата минимума.</returns>
        public virtual double Min(int index, double value)
        {
            return 0;
        }
        /// <summary>
        /// Получить значение максимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата максимума.</returns>
        public virtual double Max(int index)
        {
            return size[index];
        }
    }
}

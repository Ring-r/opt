using System;
using System.Text;

namespace Rectangle3DPlacing
{
    /// <summary>
    /// Класс области размещения.
    /// </summary>
    public class Region : R
    {
        /// <summary>
        /// Фиксация сторон области размещения.
        /// </summary>
        protected bool[] freez;

        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        public Region()
        {
            freez = new bool[Dim];
        }

        /// <summary>
        /// Получить информацию о том, зафиксирована ли сторона по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Результат фиксации.</returns>
        public bool Freez(int index)
        {
            return freez[index];
        }
        /// <summary>
        /// Установить фиксацию стороны по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Значение фиксации.</param>
        /// <returns>Результат фиксации.</returns>
        public bool Freez(int index, bool value)
        {
            freez[index] = value;
            return freez[index];
        }


        /// <summary>
        /// Сбросить все незафиксированные размеры области размещения.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Dim; i++)
                if (!freez[i])
                    size[i] = 0;
        }


        /// <summary>
        /// Получить координату размера по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата размера.</returns>
        public override double Size(int index, double value)
        {
            if (!freez[index])
                size[index] = value;
            return size[index];
        }

        /// <summary>
        /// Скопировать размер из внешней переменной.
        /// </summary>
        /// <param name="size">Внешняя переменная.</param>
        public override void CopySizeFrom(Coor size)
        {
            for (int i = 0; i < Dim; i++)
                if (!freez[i])
                    this.size[i] = size[i];
        }

        /// <summary>
        /// Превращает объект в строку.
        /// </summary>
        /// <returns>Строка.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Dim; i++)
                sb.AppendFormat("{0} {1} ", size[i], freez[i]);
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }


        /// <summary>
        /// Возвращает значение функции цели для данной области размещения.
        /// </summary>
        /// <returns>Значение функции цели.</returns>
        public double ObjFunc()
        {
            double res = 1;
            for (int i = 0; i < Dim; i++)
                if (!freez[i])
                    res *= size[i];
            return res;
        }

        /// <summary>
        /// Проверка того, содержит ли данная область размещения параллелепипед.
        /// </summary>
        /// <param name="rect">Параллелепипед.</param>
        /// <param name="eps">Погрешность.</param>
        /// <returns>Возвращает true, если область размещения полностью содержит параллелепипед.</returns>
        public bool IsContaine(Rect rect, double eps = 0)
        {
            bool is_containe = true;
            for (int i = 0; i < Dim && is_containe; i++)
            {
                is_containe = (Min(i) <= rect.Min(i) + eps);
                if (freez[i])
                    is_containe = is_containe && (rect.Max(i) <= Max(i) + eps);
            }
            return is_containe;
        }


        /// <summary>
        /// Получение объекта со строки.
        /// </summary>
        /// <param name="s">Строка.</param>
        /// <returns>Область размещения.</returns>
        public static Region Parse(string s)
        {
            Region res = new Region();
            string[] ss = s.Split(' ');
            for (int i = 0; i < Dim; i++)
            {
                res.size[i] = int.Parse(ss[2 * i]);
                res.freez[i] = bool.Parse(ss[2 * i + 1]);
            }
            return res;
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;

namespace Rectangle3DPlacing
{
    /// <summary>
    /// Класс параллелепипеда.
    /// </summary>
    public class Rect : R
    {
        /// <summary>
        /// Коррдинаты размещения параллелепипеда.
        /// </summary>
        protected Coor coor;

        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        public Rect()
        {
            coor = new Coor();
        }


        /// <summary>
        /// Получить координату размещения по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата размещения.</returns>
        public void CopyCoorTo(Coor coor)
        {
            for (int i = 0; i < Dim; i++)
                coor[i] = this.coor[i];
        }
        /// <summary>
        /// Установить координату размещения по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Новое значение координаты размещения.</param>
        /// <returns>Координата размещения.</returns>
        public void CopyCoorFrom(Coor coor)
        {
            for (int i = 0; i < Dim; i++)
                this.coor[i] = coor[i];
        }


        /// <summary>
        /// Получить значение минимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата минимума.</returns>
        public override double Min(int index)
        {
            return coor[index];
        }
        /// <summary>
        /// Установить значение минимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Координата минимума.</returns>
        public override double Min(int index, double value)
        {
            coor[index] = value;
            return coor[index];
        }
        /// <summary>
        /// Получить значение максимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Координата максимума.</returns>
        public override double Max(int index)
        {
            return coor[index] + size[index];
        }
        /// <summary>
        /// Установить значение максимума по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Координата максимума.</returns>
        public double Max(int index, double value)
        {
            size[index] = value - coor[index];
            return size[index];
        }

        /// <summary>
        /// Превращает объект в строку.
        /// </summary>
        /// <returns>Строка.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Dim; i++)
                sb.AppendFormat("{0} ", coor[i]);
            for (int i = 0; i < Dim; i++)
                sb.AppendFormat("{0} ", size[i]);
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }


        /// <summary>
        /// Проверка на пересечение с параллелепипедом.
        /// </summary>
        /// <param name="rect">Параллелепипед.</param>
        /// <param name="eps">Погрешность.</param>
        /// <returns>Возвращает true, если произошло пересечение с параллелепипедом.</returns>
        public bool IsCollideWith(Rect rect, double eps = 0)
        {
            bool is_collide = true;
            for (int i = 0; i < Dim && is_collide; i++)
                is_collide = (Min(i) < rect.Max(i) - eps) && (rect.Min(i) < Max(i) - eps);
            return is_collide;
        }
        /// <summary>
        /// Проверка на пересечение со списком параллелепипедов.
        /// </summary>
        /// <param name="rects">Список паралллепипедов.</param>
        /// <param name="eps">Погрешность.</param>
        /// <returns>Возвращает true, если произошло пересечение хотя бы с одним параллелепипедом из списка.</returns>
        public bool IsCollideWith(IEnumerable<Rect> rects, double eps = 0)
        {
            bool is_collide = false;
            IEnumerator<Rect> enumerator = rects.GetEnumerator();
            while (enumerator.MoveNext() && !is_collide)
                is_collide = IsCollideWith(enumerator.Current, eps);
            return is_collide;
        }
        /// <summary>
        /// Проверка на пересечение со списком параллелепипедов.
        /// </summary>
        /// <param name="rects">Список паралллепипедов.</param>
        /// <param name="rects_number">Количество параллелепипедов для проверки.</param>
        /// <param name="eps">Погрешность.</param>
        /// <returns>Возвращает true, если произошло пересечение хотя бы с одним параллелепипедом из списка.</returns>
        public bool IsCollideWith(IEnumerable<Rect> rects, int rects_number, double eps = 0)
        {
            bool is_collide = false;
            int i = 0;
            IEnumerator<Rect> enumerator = rects.GetEnumerator();
            while (enumerator.MoveNext() && i < rects_number && !is_collide)
            {
                is_collide = IsCollideWith(enumerator.Current, eps);
                i++;
            }
            return is_collide;
        }


        /// <summary>
        /// Получение объекта со строки.
        /// </summary>
        /// <param name="s">Строка.</param>
        /// <returns>Параллелепипед.</returns>
        public static Rect Parse(string s)
        {
            Rect res = new Rect();
            string[] ss = s.Split(' ');
            if (ss.Length == Dim)
                for (int i = 0; i < Dim; i++)
                    res.size[i] = int.Parse(ss[i]);
            else
                for (int i = 0; i < Dim; i++)
                {
                    res.coor[i] = int.Parse(ss[2 * i]);
                    res.size[i] = int.Parse(ss[2 * i + 1]);
                }
            return res;
        }
    }
}

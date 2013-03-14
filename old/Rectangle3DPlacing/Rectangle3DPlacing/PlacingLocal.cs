using System;

namespace Rectangle3DPlacing
{
    public class Placing
    {
        private Random random = new Random();

        private Rect[] rects;
        private Region region;

        public Placing(Rect[] rects, Region region)
        {
            // TODO: Проверка области размещения (должна быть хотя бы одна не фиксированная граница).
            this.region = region;

            this.rects = new Rect[rects.Length + 1];
            // Добавление вспомогательного объекта (начало центра координат).
            this.rects[0] = new Rect();
            for (int i = 0; i < rects.Length; i++)
                this.rects[i + 1] = rects[i];
            // TODO: Удалить все объекты, которые не помещаются в область размещения.
        }

        private class Perm
        {
            private int[] mm = null; // Массив, который хранит выборку.
            private int[] mt = null; // Массив, необходимый для быстрой проверки одинаковых элементов.

            public Perm(int dim, int number)
            {
                mm = new int[dim];
                mt = new int[number];
            }

            public int this[int index]
            {
                get
                {
                    return mm[index];
                }
            }

            public bool NextPerm()
            {
                int k = mm.Length - 1;

                while (true)
                {
                    if (mm[k] < mt.Length - 1)
                    {
                        mt[mm[k]]--;
                        mm[k]++;
                        mt[mm[k]]++;

                        if (mt[mm[k]] == 1)
                            return true;
                    }
                    else if (k > 0)
                    {
                        mt[mm[k]]--;
                        mm[k] = 0;
                        mt[0]++;
                        k--;
                    }
                    else
                        return false;
                }
            }
        }

        public void Find(double eps = 0)
        {
            region.Clear();
            R.Coor size = new R.Coor(); // Размер области размещения.
            R.Coor coor = new R.Coor(); // Точка размещения объекта.

            for (int i = 1; i < rects.Length; i++) // 0-ой объект, с нулевыми координатами и размерами, необходим для поиска точек размещения связанных с областью размещения.
            {
                Rect rect = rects[i];

                double obj_func = double.PositiveInfinity; // Значение функции цели при размещении текущего объекта.

                Perm perm = new Perm(Rect.Dim, i);
                do
                {
                    #region Заполнение возможной точки размещения новыми координатами.
                    for (int j = 0; j < Rect.Dim; j++)
                        rect.Min(j, rects[perm[j]].Max(j));
                    #endregion

                    #region Проверка попадания текущего объекта размещения в текущей точке размещения в область размещения.
                    if (region.IsContaine(rect, eps))
                        #region Проверка непересечения текущего объекта размещения в текущей точке размещения со всеми размещёнными объектами.
                        if (!rect.IsCollideWith(rects, i, eps))
                        {
                            #region Определение функции цели в текущей точке размещения.
                            double obj_func_temp = 1;
                            for (int j = 0; j < Rect.Dim; j++)
                                if (!region.Freez(j))
                                    obj_func_temp *= Math.Max(region.Size(j), rect.Max(j));
                            #endregion

                            #region Определение того, является ли текущая точка размещения лучше сохранённой (оптимальной).
                            double delta = obj_func - obj_func_temp;
                            for (int j = 0; j < Rect.Dim && Math.Abs(delta) <= eps; j++)
                                delta = coor[j] - rect.Min(j);
                            #endregion

                            #region Сохранение лучших значения функции цели, точки размещения и размеров области размещения.
                            if (delta - eps > 0)
                            {
                                obj_func = obj_func_temp;
                                
                                rect.CopyCoorTo(coor);

                                for (int j = 0; j < Rect.Dim; j++)
                                    size[j] = Math.Max(region.Size(j), rect.Max(j));
                            }
                            #endregion
                        }
                        #endregion
                    #endregion
                } while (perm.NextPerm());

                rect.CopyCoorFrom(coor); // Установка объекта в найденную точку.
                region.CopySizeFrom(size); // Установка новых размеров области.
            }
        }

        public bool FindBetter(double eps = 0)
        {
            #region Сохранение значения функции цели.
            double obj_func = region.ObjFunc();
            #endregion

            #region Сохранение размера области размещения.
            R.Coor size = new R.Coor();
            region.CopySizeTo(size);
            #endregion

            #region Сохранение координат размещения геометрических объектов.
            R.Coor[] coors = new R.Coor[rects.Length];
            for (int i = 0; i < rects.Length; i++)
            {
                coors[i] = new R.Coor();
                rects[i].CopyCoorTo(coors[i]);
            }
            #endregion

            Find(eps);

            #region Получение нового значения функции цели.
            double obj_func_temp = region.ObjFunc();
            #endregion

            #region Если старое значение функции цели лучще нового, то...
            if (obj_func <= obj_func_temp)
            {
                #region Востановление старого размера области размещения.
                region.CopySizeFrom(size);
                #endregion

                #region Востановление старых координат объектов размещения.
                for (int i = 0; i < rects.Length; i++)
                    rects[i].CopyCoorFrom(coors[i]);
                #endregion
            }
            #endregion

            return obj_func > obj_func_temp;
        }

        public void Mix(int mix_number = -1)
        {
            if (mix_number < 0)
                mix_number = rects.Length - 1;

            for (int i = 0; i < mix_number; i++)
            {
                int j1 = random.Next(1, rects.Length);
                int j2 = random.Next(1, rects.Length);

                Rect rect = rects[j1];
                rects[j1] = rects[j2];
                rects[j2] = rect;
            }
        }
    }
}

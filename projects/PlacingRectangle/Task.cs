using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;

namespace PlacingRectangle
{
    public class Task
    {
        public enum TaskEnum : int { RectangleHall = 0, Strip = 1, RectangleRegion = 2 };

        private TaskEnum task_index;
        public TaskEnum TaskIndex
        {
            get
            {
                return task_index;
            }
            set
            {
                task_index = value;
                Initialize();
            }
        }

        private int number_of_upgrade;
        public int NumberOfUpgrade
        {
            get
            {
                return number_of_upgrade;
            }
            set
            {
                number_of_upgrade = value;
            }
        }

        private Vector2d region_size = new Vector2d();
        public double RegionWidth
        {
            get
            {
                return region_size.X;
            }
            set
            {
                if (task_index == TaskEnum.RectangleRegion)
                {
                    region_size.X = value;
                    Initialize();
                }
            }
        }
        public double RegionHeight
        {
            get
            {
                return region_size.Y;
            }
            set
            {
                if (task_index == TaskEnum.RectangleRegion || task_index == TaskEnum.Strip)
                {
                    region_size.Y = value;
                    Initialize();
                }
            }
        }

        private List<Vector2d> objects_sizes;
        public int ObjectsCount
        {
            get
            {
                return objects_sizes.Count;
            }
        }
        public void ObjectsSizesAdd(int index, Vector2d object_size)
        {
            objects_sizes.Insert(index, object_size);
            Initialize();
        }
        public void ObjectsSizesDel(int index)
        {
            objects_sizes.RemoveAt(index);
            Initialize();
        }
        public BindingSource Objects_BindingSource()
        {
            BindingSource bs = new BindingSource();
            for (int i = 0; i < objects_sizes.Count; i++)
                bs.Add(objects_sizes[i]);
            return bs;
        }

        private Placement placement_opt;
        public Placement PlacementOpt
        {
            get
            {
                return placement_opt;
            }
        }
        private Placement placement_last;
        public Placement PlacementLast
        {
            get
            {
                return placement_last;
            }
        }

        private void Initialize()
        {
            if (task_index == TaskEnum.Strip)
                region_size.X = double.PositiveInfinity;
            if (task_index == TaskEnum.RectangleHall)
            {
                region_size.X = double.PositiveInfinity;
                region_size.Y = double.PositiveInfinity;
            }

            List<Vector2d> objects_size_temp = new List<Vector2d>();
            if (objects_sizes != null)
                for (int i = 0; i < objects_sizes.Count; i++)
                    objects_size_temp.Add(objects_sizes[i]);
            objects_sizes = objects_size_temp;

            placement_opt = Placement.Create(this, objects_sizes);
            placement_last = Placement.Create(this, objects_sizes);
        }
        public Task()
        {
            Initialize();
        }

        public void Calculate(bool is_auto_sort)
        {
            for (int i = 0; i <= number_of_upgrade; i++)
            {
                #region Итерация метода значимых переменных.
                #region Создание нового размещения и установка сортировки.
                placement_last = Placement.Create(this, objects_sizes);
                #region Установка сортировки для первого размещения.
                if (is_auto_sort)
                    #region Установка случайной сортировки.
                    placement_last.SortCreateRandom();
                    #endregion
                #endregion
                #endregion

                #region Расчёт нового размещения.
                placement_last.Calculate();
                #endregion

                #region Определение лучшего размещения.
                if (double.IsNaN(placement_opt.ObjectFunction) || placement_opt.ObjectFunction > placement_last.ObjectFunction)
                    placement_opt = placement_last;
                #endregion
                #endregion
            }
        }

        public void ReadLine(StreamReader sr)
        {
            sr.ReadLine(); // Тип задачи.
            task_index = (TaskEnum)Enum.Parse(task_index.GetType(), sr.ReadLine());

            sr.ReadLine(); // Количество итераций метода значимых переменных.
            number_of_upgrade = int.Parse(sr.ReadLine());

            sr.ReadLine(); // Размеры области размещения.
            region_size.X = double.Parse(sr.ReadLine());
            region_size.Y = double.Parse(sr.ReadLine());

            sr.ReadLine(); // Размеры объектов размещения.
            string[] s = sr.ReadLine().Split(' ');
            objects_sizes = new List<Vector2d>();
            for (int i = 0; i < s.Length; i += 2)
                objects_sizes.Add(new Vector2d { X = double.Parse(s[i]), Y = double.Parse(s[i + 1]) });

            sr.ReadLine(); // Лучшее размещение.
            placement_opt = Placement.Create(this, objects_sizes);
            placement_opt.ReadLine(sr);
            placement_last = placement_opt;
        }
        public void WriteLine(StreamWriter sw)
        {
            sw.WriteLine("Тип задачи:");
            sw.WriteLine(task_index.ToString());

            sw.WriteLine("Количество итераций метода значимых переменных:");
            sw.WriteLine(number_of_upgrade);

            sw.WriteLine("Размеры области размещения:");
            sw.WriteLine(region_size.X);
            sw.WriteLine(region_size.Y);

            sw.WriteLine("Размеры объектов размещения:");
            for (int i = 0; i < objects_sizes.Count; i++)
                if (i == objects_sizes.Count - 1)
                    sw.Write("{0} {1}", objects_sizes[i].X, objects_sizes[i].Y);
                else
                    sw.Write("{0} {1} ", objects_sizes[i].X, objects_sizes[i].Y);
            sw.WriteLine();

            sw.WriteLine("Лучшее размещение:");
            placement_opt.WriteLine(sw);
        }
    }
}

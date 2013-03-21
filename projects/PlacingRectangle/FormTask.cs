using System;
using System.IO;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;

namespace PlacingRectangle
{
    public partial class FormTask : Form
    {
        private Task task;
        private FormPlacement form_placement_opt;
        private FormPlacement form_placement_last;

        public FormTask()
        {
            InitializeComponent();

            task = new Task();

            cbTaskIndex.SelectedIndex = 0;
        }

        private void miLoadTask_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                task.ReadLine(sr);
                sr.Close();
                is_new_task = false;
                switch (task.TaskIndex)
                {
                    case Task.TaskEnum.RectangleHall:
                        cbTaskIndex.SelectedIndex = 0;
                        break;
                    case Task.TaskEnum.Strip:
                        cbTaskIndex.SelectedIndex = 1;
                        tbRegionHeight.Text = task.RegionHeight.ToString();
                        break;
                    case Task.TaskEnum.RectangleRegion:
                        cbTaskIndex.SelectedIndex = 2;
                        tbRegionHeight.Text = task.RegionHeight.ToString();
                        tbRegionWidth.Text = task.RegionWidth.ToString();
                        break;
                }
                is_new_task = true;
                nudTaskUpgradeNumber.Value = task.NumberOfUpgrade;
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
        }
        private void miSaveTask_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                task.WriteLine(sw);
                sw.Close();
            }
        }
        private void miClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miAddObjectLast_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите ширину и высоту прямоугольника:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                string[] s = form_temp.String.Split(' ');
                task.ObjectsSizesAdd(task.ObjectsCount, new Vector2d { X = double.Parse(s[0]), Y = double.Parse(s[1]) });
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch { }
        }
        private void miAddObjectByIndex_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите индекс, ширину и высоту прямоугольника:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                string[] s = form_temp.String.Split(' ');
                task.ObjectsSizesAdd(int.Parse(s[0]), new Vector2d { X = double.Parse(s[1]), Y = double.Parse(s[2]) });
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch { }
        }
        private void miAddObjects_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            FormTemp form_temp = new FormTemp() { Text = "Введите количество, минимум и максимум размера прямоугольника:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                string[] s = form_temp.String.Split(' ');
                int n = int.Parse(s[0]);
                float min = float.Parse(s[1]);
                float max = float.Parse(s[2]);
                for (int i = 0; i < n; i++)
                    task.ObjectsSizesAdd(task.ObjectsCount, new Vector2d { X = (max - min) * rand.NextDouble() + min, Y = (max - min) * rand.NextDouble() + min });
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch { }
        }
        private void miAddObjectsFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                try
                {
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();
                    string[] s = sr.ReadLine().Split(' ');
                    for (int i = 0; i < s.Length; i += 2)
                        task.ObjectsSizesAdd(task.ObjectsCount, new Vector2d { X = double.Parse(s[i]), Y = double.Parse(s[i + 1]) });
                    dgvObjects.DataSource = task.Objects_BindingSource();
                    if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                        form_placement_opt.Placement = task.PlacementOpt;
                    if (form_placement_last != null && !form_placement_last.IsDisposed)
                        form_placement_last.Placement = task.PlacementLast;
                }
                catch { }
                sr.Close();
            }
        }
        private void miDelObjectLast_Click(object sender, EventArgs e)
        {
            task.ObjectsSizesDel(task.ObjectsCount - 1);
            dgvObjects.DataSource = task.Objects_BindingSource();
            if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                form_placement_opt.Placement = task.PlacementOpt;
            if (form_placement_last != null && !form_placement_last.IsDisposed)
                form_placement_last.Placement = task.PlacementLast;
        }
        private void miDelObjectByIndex_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите индекс прямоугольника:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                task.ObjectsSizesDel(int.Parse(form_temp.String));
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch { }
        }
        private void miDelObjects_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите индекс и количество прямоугольников:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                string[] s = form_temp.String.Split(' ');
                int n = int.Parse(s[0]);
                int m = int.Parse(s[1]);
                for (int i = 0; i < m; i++)
                    task.ObjectsSizesDel(task.ObjectsCount - 1);
                dgvObjects.DataSource = task.Objects_BindingSource();
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch { }
        }
        private void miDelObjectsAll_Click(object sender, EventArgs e)
        {
            while (task.ObjectsCount != 0)
                task.ObjectsSizesDel(task.ObjectsCount - 1);
            dgvObjects.DataSource = task.Objects_BindingSource();
            if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                form_placement_opt.Placement = task.PlacementOpt;
            if (form_placement_last != null && !form_placement_last.IsDisposed)
                form_placement_last.Placement = task.PlacementLast;
        }

        private bool is_new_task = true;
        private void cbTaskIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTaskIndex.SelectedIndex)
            {
                case 0:
                    if (is_new_task)
                        task.TaskIndex = Task.TaskEnum.RectangleHall;
                    gbRegionHeight.Hide();
                    gbRegionWidth.Hide();
                    break;
                case 1:
                    if (is_new_task)
                        task.TaskIndex = Task.TaskEnum.Strip;
                    gbRegionHeight.Show();
                    gbRegionWidth.Hide();
                    break;
                case 2:
                    if (is_new_task)
                        task.TaskIndex = Task.TaskEnum.RectangleRegion;
                    gbRegionHeight.Show();
                    gbRegionWidth.Show();
                    break;
            }
            if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                form_placement_opt.Placement = task.PlacementOpt;
            if (form_placement_last != null && !form_placement_last.IsDisposed)
                form_placement_last.Placement = task.PlacementLast;
        }
        private void nudTaskUpgradeNumber_ValueChanged(object sender, EventArgs e)
        {
            task.NumberOfUpgrade = (int)(sender as NumericUpDown).Value;
        }
        private void tbRegionHeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (is_new_task)
                    task.RegionHeight = int.Parse(tbRegionHeight.Text);
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch
            {
                MessageBox.Show(this, "Высота задана не вещественным числом!", "Ошибка!");
            }
        }
        private void tbRegionWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (is_new_task)
                    task.RegionWidth = int.Parse(tbRegionWidth.Text);
                if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                    form_placement_opt.Placement = task.PlacementOpt;
                if (form_placement_last != null && !form_placement_last.IsDisposed)
                    form_placement_last.Placement = task.PlacementLast;
            }
            catch
            {
                MessageBox.Show(this, "Ширина задана не вещественным числом!", "Ошибка!");
            }
        }

        private void miTaskIndex_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text[0] == '+')
            {
                gbTaskIndex.Hide();
                (sender as ToolStripMenuItem).Text = "- Показывать тип задачи";
            }
            else
            {
                gbTaskIndex.Show();
                (sender as ToolStripMenuItem).Text = "+ Показывать тип задачи";
            }
        }
        private void miTaskUpgradeNumber_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text[0] == '+')
            {
                gbTaskUpgradeNumber.Hide();
                (sender as ToolStripMenuItem).Text = "- Показывать количество итераций";
            }
            else
            {
                gbTaskUpgradeNumber.Show();
                (sender as ToolStripMenuItem).Text = "+ Показывать количество итераций";
            }
        }
        private void miRegion_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text[0] == '+')
            {
                gbRegion.Hide();
                (sender as ToolStripMenuItem).Text = "- Показывать область размещения";
            }
            else
            {
                gbRegion.Show();
                (sender as ToolStripMenuItem).Text = "+ Показывать область размещения";
            }
        }
        private void miObjects_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text[0] == '+')
            {
                gbObjects.Hide();
                (sender as ToolStripMenuItem).Text = "- Показывать объекты размещения";
            }
            else
            {
                gbObjects.Show();
                (sender as ToolStripMenuItem).Text = "+ Показывать объекты размещения";
            }
        }

        private void miPlacementOpt_Click(object sender, EventArgs e)
        {
            if (form_placement_opt == null || form_placement_opt.IsDisposed)
                form_placement_opt = new FormPlacement() { Text = "Лучшее размещение", Location = new System.Drawing.Point(this.Width, 0) };
            form_placement_opt.Show();
            form_placement_opt.Placement = task.PlacementOpt;
            form_placement_opt.Activate();
            this.Activate();
        }
        private void miPlacementLast_Click(object sender, EventArgs e)
        {
            if (form_placement_last == null || form_placement_last.IsDisposed)
                form_placement_last = new FormPlacement() { Text = "Последнее размещение", Location = new System.Drawing.Point(this.Width, this.Height / 2) };
            form_placement_last.Show();
            form_placement_last.Placement = task.PlacementLast;
            form_placement_last.Activate();
            this.Activate();
        }

        private void miStep_Click(object sender, EventArgs e)
        {
            task.Calculate(true);

            if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                form_placement_opt.Placement = task.PlacementOpt;
            if (form_placement_last != null && !form_placement_last.IsDisposed)
                form_placement_last.Placement = task.PlacementLast;
        }
        private void miSteps_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите количество шагов:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                int n = int.Parse(form_temp.String);
                for (int i = 0; i < n; i++)
                    miStep_Click(sender, e);
            }
            catch { }
        }
        private void miStatistic_Click(object sender, EventArgs e)
        {
            if (form_placement_opt != null && !form_placement_opt.IsDisposed)
                form_placement_opt.Close();
            if (form_placement_last != null && !form_placement_last.IsDisposed)
                form_placement_last.Close();

            Random rand = new Random();
            FormTemp form_temp = new FormTemp() { Text = "Введите количество опытов:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                int count_of_oput = int.Parse(form_temp.String);
                StreamWriter sw = new StreamWriter(this.task.TaskIndex.ToString() + ".stc");
                sw.Close();

                for (int i_of_oput = 0; i_of_oput < count_of_oput; i_of_oput++)
                {
                    // Количество итераций метода значимых переменных при одном размещении.
                    int count_of_mzp = rand.Next(1, 10);
                    // Количество всех итераций метода значимых переменных.
                    int count_of_iteration = rand.Next(1, 10);
                    // Количество объектов (не больше 50).
                    int count_of_objects = rand.Next(1, 50);
                    // Размеры объектов от 10 до 110.
                    float min_of_objects = 100 * (float)rand.NextDouble() + 10;
                    float max_of_objects = 100 * (float)rand.NextDouble() + min_of_objects;
                    // Размеры области от 10 до 1000.
                    float min_of_region = 1000 * (float)rand.NextDouble() + 10;
                    float max_of_region = 1000 * (float)rand.NextDouble() + min_of_objects;
                    Task task = new Task();
                    // Создание объектов.
                    for (int i = 0; i < count_of_objects; i++)
                        task.ObjectsSizesAdd(0, new Vector2d { X = (max_of_objects - min_of_objects) * rand.NextDouble() + min_of_objects, Y = (max_of_objects - min_of_objects) * rand.NextDouble() + min_of_objects });
                    task.TaskIndex = this.task.TaskIndex;
                    task.RegionWidth = (max_of_region - min_of_region) * (float)rand.NextDouble() + min_of_region;
                    task.RegionHeight = (max_of_region - min_of_region) * (float)rand.NextDouble() + min_of_region;

                    // Поиск методом случайного поиска.
                    for (int i = 0; i < (count_of_mzp + 1) * count_of_iteration; i++)
                        task.Calculate(true);
                    double opt = task.PlacementOpt.ObjectFunction;


                    task.TaskIndex = task.TaskIndex;
                    task.NumberOfUpgrade = count_of_mzp;
                    // Поиск методом случайного поиска + методом значимых переменных.
                    for (int i = 0; i < count_of_iteration; i++)
                        task.Calculate(true);
                    double opt_mzp = task.PlacementOpt.ObjectFunction;

                    #region Сохранение статистики.
                    if (opt != 0 && !double.IsInfinity(opt) && !double.IsNaN(opt) && opt_mzp != 0 && !double.IsInfinity(opt_mzp) && !double.IsNaN(opt_mzp))
                    {
                        sw = new StreamWriter(task.TaskIndex.ToString() + ".stc", true);
                        int op = 0;
                        if (opt / opt_mzp - 1 > 0)
                            op = 1;
                        if (opt / opt_mzp - 1 < 0)
                            op = -1;
                        sw.WriteLine("{0} {1} {2} {3} {4}", i_of_oput, opt, opt_mzp, opt / opt_mzp - 1, op);
                        sw.Close();
                    }
                    else
                        i_of_oput--;
                    #endregion
                }
                MessageBox.Show(this, "Статистика сохранена в файле " + this.task.TaskIndex.ToString() + ".stc.", "Информация");
            }
            catch { }
        }

        private void miSpeed_Click(object sender, EventArgs e)
        {
            FormTemp form_temp = new FormTemp() { Text = "Введите интервал обновления:", Width = ClientSize.Width };
            form_temp.ShowDialog(this);
            try
            {
                timer1.Interval = int.Parse(form_temp.String);
            }
            catch { }
        }
        private void miStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void miStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            miStep_Click(null, EventArgs.Empty);
        }

        private void FormTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show(this, "Вы действительно хотите завершить работу с программой", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != System.Windows.Forms.DialogResult.Yes;
        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;

namespace PolygonPlacingTest
{
    public partial class FormMain : Form
    {
        private Random random;

        private string filename;

        private PolygonWithMinMax[] polygons;
        private Vector2d strip_vector;

        private PolygonWithMinMax polygon;

        private PlacingOpt placing;
        private double mu;
        private double beta;
        private double eps;

        private Thread thread;
        private ParameterizedThreadStart thread_start;

        private FormSettings.SettingsClass settings;
        
        private PointF offset = new PointF();
        private PointF location_old = new PointF();

        public FormMain()
        {
            InitializeComponent();

            random = new Random();

            filename = "example_input_data.txt";

            ReadFromFile(filename);

            polygon = null;

            placing = new PlacingOpt(polygons, strip_vector);
            placing.FillPoint();

            Text = "Размещение многоугольников (" + filename + ")";

            thread = null;
            thread_start = new ParameterizedThreadStart(MethodInThread);

            settings = new FormSettings.SettingsClass();
        }

        private void ReadFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            strip_vector = new Vector2d();

            string[] s = sr.ReadLine().Split(' ');
            strip_vector.X = double.Parse(s[0]);
            strip_vector.Y = double.Parse(s[1]);

            polygons = new PolygonWithMinMax[int.Parse(sr.ReadLine())];
            for (int i = 0; i < polygons.Length; i++)
            {
                polygons[i] = new PolygonWithMinMax();

                s = sr.ReadLine().Split(' ');
                polygons[i].Pole.X = double.Parse(s[0]);
                polygons[i].Pole.Y = double.Parse(s[1]);

                for (int j = 2; j < s.Length; j += 2)
                    polygons[i].Add(new Point2d() { X = double.Parse(s[j]), Y = double.Parse(s[j + 1]) });

                polygons[i].Check();
            }

            sr.Close();
        }

        private void SaveToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine("{0} {1}", strip_vector.X, strip_vector.Y);
            sw.WriteLine(polygons.Length);
            for (int i = 0; i < polygons.Length; i++)
            {
                sw.Write("{0} {1} ", polygons[i].Pole.X, polygons[i].Pole.Y);
                for (int j = 0; j < polygons[i].Count - 1; j++)
                    sw.Write("{0} {1} ", polygons[i][j].X, polygons[i][j].Y);
                sw.WriteLine("{0} {1}", polygons[i][polygons[i].Count - 1].X, polygons[i][polygons[i].Count - 1].Y);
            }

            sw.Close();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            textBox_width.Text = strip_vector.X.ToString();

            float width = (sender as PictureBox).Width;
            float width_max = Math.Max(width, (float)strip_vector.X);
            float height = (sender as PictureBox).Height;

            #region Установление границ для начального смещения.
            if (strip_vector.X < width)
                offset.X = 0;
            else
            {
                if (offset.X < width - (float)strip_vector.X)
                    offset.X = width - (float)strip_vector.X;
                if (offset.X > 0)
                    offset.X = 0;
            }

            if (strip_vector.Y < height)
                offset.Y = 0;
            else
            {
                if (offset.Y < (height - (float)strip_vector.Y) / 2)
                    offset.Y = (height - (float)strip_vector.Y) / 2;
                if (offset.Y > -(height - (float)strip_vector.Y) / 2)
                    offset.Y = -(height - (float)strip_vector.Y) / 2;
            }
            #endregion
            e.Graphics.TranslateTransform(offset.X, -offset.Y);

            e.Graphics.ScaleTransform(1, -1);
            e.Graphics.TranslateTransform(0, -height);
            e.Graphics.TranslateTransform(0, height / 2 - (float)strip_vector.Y / 2);

            e.Graphics.Clear(settings.ColorBackground);            

            e.Graphics.FillRectangle(settings.BrushStrip, 0, 0, width_max, (float)strip_vector.Y);
            e.Graphics.FillRectangle(settings.BrushStripUsed, 0, 0, (float)strip_vector.X, (float)strip_vector.Y);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (float x = 0; x < width_max; x += settings.FloatCell)
                e.Graphics.DrawLine(Pens.Gray, x, 0, x, (float)strip_vector.Y);

            for (float y = 0; y < (float)strip_vector.Y; y += settings.FloatCell)
                e.Graphics.DrawLine(Pens.Gray, 0, y, width_max, y);

            System.Drawing.Drawing2D.Matrix matrix;
            for (int i = 0; i < polygons.Length; i++)
            {
                PointF[] points = new PointF[polygons[i].Count];
                for (int j = 0; j < points.Length; j++)
                    points[j] = new PointF((float)polygons[i][j].X, (float)polygons[i][j].Y);

                matrix = e.Graphics.Transform;
                e.Graphics.TranslateTransform((float)polygons[i].Pole.X, (float)polygons[i].Pole.Y);
                //e.Graphics.TranslateTransform(3, 3);
                //e.Graphics.FillPolygon(Brushes.Black, points);
                //e.Graphics.TranslateTransform(-3, -3);
                if (polygons[i] == polygon)
                {
                    e.Graphics.FillPolygon(settings.BrushPolygonCurrent, points);
                    e.Graphics.DrawPolygon(settings.PenPolygonCurrent, points);
                }
                else
                {
                    e.Graphics.FillPolygon(settings.BrushPolygon, points);
                    e.Graphics.DrawPolygon(settings.PenPolygon, points);
                }
                e.Graphics.Transform = matrix;

                e.Graphics.FillEllipse(settings.BrushPoint, (float)polygons[i].CenterX - settings.FloatPoint, (float)polygons[i].CenterY - settings.FloatPoint, 2 * settings.FloatPoint, 2 * settings.FloatPoint);

                matrix = e.Graphics.Transform;
                e.Graphics.TranslateTransform(+(float)polygons[i].CenterX, +(float)polygons[i].CenterY);
                e.Graphics.ScaleTransform(1, -1);
                e.Graphics.TranslateTransform(-(float)polygons[i].CenterX, -(float)polygons[i].CenterY);
                e.Graphics.DrawString(i.ToString(), Font, settings.BrushPoint, (float)polygons[i].CenterX + settings.FloatPoint, (float)polygons[i].CenterY + settings.FloatPoint);
                e.Graphics.Transform = matrix;
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.DrawRectangle(settings.PenStripUsed, 0, 0, (float)strip_vector.X, (float)strip_vector.Y);
            e.Graphics.DrawRectangle(settings.PenStrip, 0, 0, width_max, (float)strip_vector.Y);
        }

        private void button_prestart_Click(object sender, EventArgs e)
        {
            double x = 10;
            for (int i = 0; i < polygons.Length; i++)
            {
                polygons[i].Pole.X = x + polygons[i].MinX;
                x = polygons[i].Pole.X + polygons[i].MaxX - polygons[i].MinX + 10;

                polygons[i].Pole.Y = (float)((strip_vector.Y - (polygons[i].MaxX - polygons[i].MinX) - 20) * random.NextDouble()) + polygons[i].MinY + 10;
            }
            strip_vector.X = x;

            placing.FillPoint();

            pictureBox.Invalidate();
        }

        private void checkBox_timer_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_timer.Visible = (sender as CheckBox).Checked;
            label_time.Visible = (sender as CheckBox).Checked;

            timer.Enabled = (sender as CheckBox).Checked;
        }

        private void numericUpDown_timer_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = (int)(sender as NumericUpDown).Value;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
            {
                #region Проверка и преобразование mu.
                try
                {
                    mu = double.Parse(textBox_mu.Text);
                    if (mu <= 0)
                        throw (new Exception());
                }
                catch
                {
                    MessageBox.Show(this, "mu - вещественное число больше 0.", "Некоректное значение mu!");
                }
                #endregion
                #region Проверка и преобразование beta.
                try
                {
                    beta = double.Parse(textBox_beta.Text);
                    if (beta <= 0 || 1 <= beta)
                        throw (new Exception());
                }
                catch
                {
                    MessageBox.Show(this, "beta - вещественное число больше 0 и меньше 1.", "Некоректное значение beta!");
                    return;
                }
                #endregion
                #region Проверка и преобразование eps.
                try
                {
                    eps = double.Parse(textBox_eps.Text);
                    if (eps <= 0)
                        throw (new Exception());
                }
                catch
                {
                    MessageBox.Show(this, "eps - вещественное число больше 0.", "Некоректное значение eps!");
                    return;
                }
                #endregion

                thread = new Thread(thread_start);
                thread.Start(this);
                if (checkBox_timer.Checked)
                    timer.Start();

                toolStripStatusLabel.Text = "Расчёт запущен...";
            }
            else
            {
                thread.Abort();

                placing.FillPolygons();

                pictureBox.Invalidate();
                toolStripStatusLabel.Text = "Расчёт остановлен!";
            }
        }

        private void MethodInThread(object o)
        {
            placing.CalculateStart(mu, beta, eps);

            (o as FormMain).pictureBox.Invalidate();
            (o as FormMain).toolStripStatusLabel.Text = "Расчёт окончен!";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (thread == null || !thread.IsAlive)
                timer.Stop();

            placing.FillPolygons();

            pictureBox.Invalidate();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                location_old = e.Location;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                double d = double.PositiveInfinity;
                int k = 0;
                for (int i = 0; i < polygons.Length; i++)
                {
                    double x = e.X - polygons[i].CenterX;
                    double y = pictureBox.ClientSize.Height / 2 + strip_vector.Y / 2 - e.Y - polygons[i].CenterY;
                    double dd = Math.Sqrt(x * x + y * y);
                    if (d > dd)
                    {
                        d = dd;
                        k = i;
                    }
                }
                if (d < 25)
                    polygon = polygons[k];
                else
                    polygon = null;

                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                offset.X += e.X - location_old.X;

                offset.Y -= e.Y - location_old.Y;

                location_old = e.Location;

                pictureBox.Invalidate();
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
        }


        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog.FileName;

                ReadFromFile(filename);

                placing = new PlacingOpt(polygons, strip_vector);
                placing.FillPoint();

                Text = "Размещение многоугольников (" + filename + ")";

                pictureBox.Invalidate();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile(filename);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;

                SaveToFile(filename);

                Text = "Размещение многоугольников (" + filename + ")";
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Temp;
using Opt.VD;

namespace TestOptVDFormApplication
{
    public partial class FormMain : Form
    {
        private Random rand = new Random();

        private List<Circle> circles = new List<Circle>();
        private VD<Circle, DeloneCircle> vd = null;

        private Brush brush_object = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        public FormMain()
        {
            InitializeComponent();

            InitializeVD();
        }

        private void InitializeVD()
        {
            Circle circle_i = new Circle() { X = this.rand.Next(this.ClientSize.Width), Y = this.rand.Next(this.ClientSize.Height) };
            Circle circle_j = new Circle() { X = this.rand.Next(this.ClientSize.Width), Y = this.rand.Next(this.ClientSize.Height) };
            Vector2d distanceVector = circle_j.Pole - circle_i.Pole;
            double distance = Math.Sqrt(distanceVector * distanceVector);
            circle_i.R = this.rand.NextDouble() * distance / 2;
            circle_j.R = this.rand.NextDouble() * distance / 2;
            this.circles.Add(circle_i);
            this.circles.Add(circle_j);

            this.vd = new VD<Circle, DeloneCircle>(circles[0], null, circles[1]);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                circles.Clear();

                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {
                    String[] g = sr.ReadLine().Split(' ');
                    Circle circle = new Circle() { R = double.Parse(g[0]), X = double.Parse(g[1]), Y = double.Parse(g[2]) };
                    circles.Add(circle);
                }
                sr.Close();

                vd = new VD<Circle, DeloneCircle>(circles[0], circles[1], null);
                for (int i = 2; i < circles.Count; i++)
                    vd.Insert(circles[i]);

                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (vd != null)
            {
                Triple<Circle, DeloneCircle> triple = vd.NextTriple(vd.NullTriple);
                while (triple != vd.NullTriple)
                {
                    if (!double.IsInfinity(triple.Delone_Circle.R))
                        try
                        {
                            e.Graphics.DrawEllipse(Pens.Silver, (float)(triple.Delone_Circle.X - triple.Delone_Circle.R), (float)(triple.Delone_Circle.Y - triple.Delone_Circle.R), 2 * (float)triple.Delone_Circle.R, 2 * (float)triple.Delone_Circle.R);
                        }
                        catch
                        {
                        }
                    else
                    {
                        e.Graphics.DrawLine(Pens.Silver,
                            (float)(triple.Delone_Circle.X - 1000 * triple.Delone_Circle.VX),
                            (float)(triple.Delone_Circle.Y - 1000 * triple.Delone_Circle.VY),
                            (float)(triple.Delone_Circle.X + 1000 * triple.Delone_Circle.VX),
                            (float)(triple.Delone_Circle.Y + 1000 * triple.Delone_Circle.VY));
                    }

                    triple = vd.NextTriple(triple);
                }

                for (int i = 0; i < circles.Count; i++)
                    e.Graphics.FillEllipse(brush_object, (float)(circles[i].X - circles[i].R), (float)(circles[i].Y - circles[i].R), 2 * (float)circles[i].R, 2 * (float)circles[i].R);

                //if (points != null)
                //{
                //    for (int i = 0; i < points.Count; i++)
                //        e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(100, 0, 255, 0)), (float)(points[i].X - data.R), (float)(points[i].Y - data.R), 2 * (float)data.R, 2 * (float)data.R);
                //}

                Text = circles.Count.ToString();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Circle data = new Circle() { R = rand.Next(10, 100), X = e.X, Y = e.Y };
            List<Point2d> points = TestAlgorithms.Точки_плотного_размещения(vd, data);
            if (points.Count > 0)
            {
                data.Center = points[0];
            }
            this.circles.Add(data);
            this.vd.Insert(data);
            Invalidate();
        }
    }
}

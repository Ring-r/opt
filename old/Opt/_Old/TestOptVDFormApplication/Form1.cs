using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using Opt.GeometricObjects;
using Opt.VD;

namespace TestOptVDFormApplication
{
    public partial class Form1 : Form
    {
        Random rand;
        List<Circle> circles;

        VD<Circle, DeloneCircle> vd;
        Brush brush_object;

        public Form1()
        {
            InitializeComponent();

            rand = new Random();

            circles = new List<Circle>();
            circles.Add(new Circle(20, 10, 50));
            circles.Add(new Circle(40, 150, 50));

            vd = new VD<Circle, DeloneCircle>(circles[0], null, circles[1]);

            brush_object = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
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
                    Circle circle = new Circle(double.Parse(g[0]), double.Parse(g[1]), double.Parse(g[2]));
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

        List<Opt.GeometricObjects.Point> points = new List<Opt.GeometricObjects.Point>();
        Circle data;
        private void алгоритмПоискаТочекРазмещенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data = new Circle(20, 0, 0);
            points = TestAlgorithms.Точки_плотного_размещения(vd, data);
            data.Center = points[0];
            circles.Add(data);


            vd.Insert(data);


            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            алгоритмПоискаТочекРазмещенияToolStripMenuItem_Click(null, EventArgs.Empty);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            data = new Circle(rand.Next(10, 50), e.X, e.Y);
            circles.Add(data);
            vd.Insert(data);
            Invalidate();


        }
    }
}

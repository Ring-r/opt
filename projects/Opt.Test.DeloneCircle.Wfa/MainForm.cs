using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Opt.Geometrics.Extentions.Wfa;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Extentions;

namespace Opt.Test.DeloneCircle.Wfa
{
    public partial class MainForm : Form
    {
        private readonly Random random = new Random();
        private readonly List<Circle2d> list = new List<Circle2d>();

        public MainForm()
        {
            InitializeComponent();
            this.timer.Interval = 500;
            this.timer.Start();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.Red));
            for (int i = 0; i < list.Count; ++i)
            {
                e.Graphics.FillAndDraw(brush, Pens.Black, this.list[i]);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.list.Count == 5)
            {
                Thread.Sleep(500);
                this.list.Clear();
            }
            if (this.list.Count != 3)
            {
                this.list.Add(this.CreateCircle());
            }
            else
            {
                Geometric2dWithPointScalar circle;
                circle = Geometric2dExt.Круг_Делоне(this.list[0], this.list[1], this.list[2]);
                try
                {
                    this.list.Add(new Circle2d() { Point = circle.Point.Copy, R = circle.Scalar });
                }
                catch { }
                circle = Geometric2dExt.Круг_Делоне(this.list[1], this.list[0], this.list[2]);
                try
                {
                    this.list.Add(new Circle2d() { Point = circle.Point.Copy, R = circle.Scalar });
                }
                catch { }
            }
            this.Invalidate();
        }

        private Circle2d CreateCircle(int rKoef = 10)
        {
            int r = this.random.Next(Math.Min(this.ClientSize.Width, this.ClientSize.Height) / rKoef);
            int x = this.random.Next(r, this.ClientSize.Width - r);
            int y = this.random.Next(r, this.ClientSize.Height - r);
            return new Circle2d() { X = x, Y = y, R = r };
        }
    }
}

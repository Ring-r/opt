using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Многоугольник
{
    public partial class Form1 : Form
    {
        Triangulation tr;
        ConvexPoligons cp;
        Polygon[] polygons;
        SemiinfiniteStrip st;
        GradientProjectionMethod grad;
        bool check = true;
        bool show = false;

        int iterator;

        List<double[]> p;

        List<double[]>[] Points;
        List<double[]>[] CopyPoints;


        public Form1()
        {
            InitializeComponent();

            iterator = 0;

            double[] A = ReadStrip();
            st = new SemiinfiniteStrip(A[0], A[1], (int)A[2]);

        }

        //считываем точки из файла
        private void Read(ref List<double[]> A, ref List<double[]> CopyA, ref List<double[]> p, string file)
        {
            string strline;
            string[] strAr;
            StreamReader sr = new StreamReader(file);

            strline = sr.ReadLine();
            strAr = strline.Split(' ');
            p.Add(new double[2]);
            p[p.Count - 1][0] = double.Parse(strAr[0]);
            p[p.Count - 1][1] = double.Parse(strAr[1]);

            strline = sr.ReadLine();
            strAr = strline.Split(' ');
            int n = int.Parse(strAr[0]);

            for (int i = 0; i < n; i++)
            {
                strline = sr.ReadLine();
                strAr = strline.Split(' ');
                A.Add(new double[2]);
                CopyA.Add(new double[2]);
                A[i][0] = double.Parse(strAr[0]);
                A[i][1] = double.Parse(strAr[1]);

                CopyA[i][0] = double.Parse(strAr[0]);
                CopyA[i][1] = double.Parse(strAr[1]);
            }
        }

        //считываем полосу из файла
        private double[] ReadStrip()
        {
            double[] A = new double[3];
            string strline;
            string[] strAr;
            StreamReader sr = new StreamReader("Strip.txt");

            for (int i = 0; i < 3; i++)
            {
                strline = sr.ReadLine();
                strAr = strline.Split(' ');
                A[i] = double.Parse(strAr[0]);
            }
            return A;
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            textBoxLength.Text = st.z.ToString();
            DrawComponents dr = new DrawComponents(DrawPanel);
            dr.Clear();
            Graphics g = e.Graphics;
            if (polygons!= null)
            {
                for (int i = 0; i < polygons.Count(); i++)
                {
                    dr.DrawPointsAndLines(Points[i], p[i], g);
                }
            }

            if (show)
            {
                if (polygons.Length != 0)
                {
                    for (int i = 0; i < polygons.Length; i++)
                    {
                        for (int j = 0; j < polygons[i].ListOfConvexPoligons.Count; j++)
                        {
                            dr.DrawConvexRectangles(polygons[i].ListOfConvexPoligons[j], polygons[i].polus, g);
                        }
                    }
                }
            }
            if (st.z != 0)
            {
                g.DrawLine(Pens.Green, (float)st.z, 0, (float)st.z, DrawPanel.Height);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iterator == 0)
            {
                grad.Step_0();
                DrawPanel_Paint(this, new PaintEventArgs(DrawPanel.CreateGraphics(), DrawPanel.ClientRectangle));

                iterator++;
            }
            else
            {

                check = grad.StepLocal();
                DrawPanel_Paint(this, new PaintEventArgs(DrawPanel.CreateGraphics(), DrawPanel.ClientRectangle));

                iterator++;

            }
            if (check == false)
            {
                timer1.Enabled = false;
                MessageBox.Show("The local minimum is getted!");
                check = true;

            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void polygons1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iterator = 0;
            st.n = 20;
            polygons = new Polygon[st.n];

            Points = new List<double[]>[st.n];
            CopyPoints = new List<double[]>[st.n];
            p = new List<double[]>();
            for (int i = 0; i < st.n; i++)
            {
                //if (i < st.n)
                //{
                Points[i] = new List<double[]>();
                CopyPoints[i] = new List<double[]>();
                Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i + 1) + ".txt");

                polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                //}
                // else if((i>st.n-1)&&(i<2*st.n))
                // {
                //     Points[i] = new List<double[]>();
                //     CopyPoints[i] = new List<double[]>();
                //     Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i - 19) + ".txt");

                //     polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);

                // }
                //else
                //{
                //    Points[i] = new List<double[]>();
                //    CopyPoints[i] = new List<double[]>();
                //    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i - 39) + ".txt");

                //    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);

                //}
            }
            grad = new GradientProjectionMethod(polygons, st, DrawPanel.Height, DrawPanel.Width);
            InitialEstimate ie = new InitialEstimate(polygons, DrawPanel.Height, DrawPanel.Width);
        }

        private void polygons2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iterator = 0;
            st.n = 16;
            polygons = new Polygon[st.n];

            Points = new List<double[]>[st.n];
            CopyPoints = new List<double[]>[st.n];
            p = new List<double[]>();
            for (int i = 0; i < st.n; i++)
            {
                if (i < 18)
                {
                    Points[i] = new List<double[]>();
                    CopyPoints[i] = new List<double[]>();
                    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i + 1) + "a.txt");

                    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                }
                else if ((i > 17) && (i < 36))
                {
                    Points[i] = new List<double[]>();
                    CopyPoints[i] = new List<double[]>();
                    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i - 17) + "a.txt");

                    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                }
                else
                {
                    Points[i] = new List<double[]>();
                    CopyPoints[i] = new List<double[]>();
                    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i - 35) + "a.txt");

                    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                }
            }
            grad = new GradientProjectionMethod(polygons, st, DrawPanel.Height, DrawPanel.Width);
            InitialEstimate ie = new InitialEstimate(polygons, DrawPanel.Height, DrawPanel.Width);
        }

        private void polygons3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iterator = 0;
            st.n = 8;
            polygons = new Polygon[st.n];

            Points = new List<double[]>[st.n];
            CopyPoints = new List<double[]>[st.n];
            p = new List<double[]>();
            for (int i = 0; i < st.n; i++)
            {
                if (i < 4)
                {
                    Points[i] = new List<double[]>();
                    CopyPoints[i] = new List<double[]>();
                    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i + 1) + "b.txt");

                    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                }
                else
                {
                    Points[i] = new List<double[]>();
                    CopyPoints[i] = new List<double[]>();
                    Read(ref Points[i], ref CopyPoints[i], ref p, "Polygon" + (i -3) + "b.txt");

                    polygons[i] = new Polygon(Points[i], CopyPoints[i], i, p[i]);
                }
            }
            grad = new GradientProjectionMethod(polygons, st, DrawPanel.Height, DrawPanel.Width);
            InitialEstimate ie = new InitialEstimate(polygons, DrawPanel.Height, DrawPanel.Width);
        }

        private void yesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            show = true;
            DrawPanel_Paint(this, new PaintEventArgs(DrawPanel.CreateGraphics(), DrawPanel.ClientRectangle));
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            show = false;
            DrawPanel_Paint(this, new PaintEventArgs(DrawPanel.CreateGraphics(), DrawPanel.ClientRectangle));
        }
    }
}

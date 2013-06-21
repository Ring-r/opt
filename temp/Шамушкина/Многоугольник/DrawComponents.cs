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
    class DrawComponents
    {
        private Panel DrawPanel;
        SemiinfiniteStrip st;

        public DrawComponents(Panel Draw)
        {
            this.DrawPanel = Draw;
        }



        //очищаем область
        public void Clear()
        {
            Graphics g = DrawPanel.CreateGraphics();
            g.Clear(Color.WhiteSmoke);
        }

        //рисуем точки
        public void DrawPointsAndLines(List<double[]> Points, double[] p, Graphics g)
        {
            Pen pen = new Pen(Color.Black, 1.5f);
            for (int i = 0; i < Points.Count - 1; i++)
            {
               // g.FillEllipse(Brushes.Red, (int)(p[0] + Points[i][0] - 2), (int)(DrawPanel.Height - (p[1] + Points[i][1]) - 2), 4, 4);
                g.DrawLine(pen, (int)(p[0] + Points[i][0]), (int)(DrawPanel.Height - (p[1] + Points[i][1])), (int)(p[0] + Points[i + 1][0]), (int)(DrawPanel.Height - (p[1] + Points[i + 1][1])));
            }
            //g.FillEllipse(Brushes.Red, (int)(p[0] + Points[Points.Count - 1][0] - 2), (int)(DrawPanel.Height - p[1] - Points[Points.Count - 1][1] - 2), 4, 4);
            g.DrawLine(pen, (int)(p[0] + Points[Points.Count - 1][0]), (int)(DrawPanel.Height - p[1] - Points[Points.Count - 1][1]), (int)(p[0] + Points[0][0]), (int)(DrawPanel.Height - p[1] - Points[0][1]));
        }
        //рисуем полученные треугольники
        public void DrawTriangles(List<List<double[]>> Triangles, Graphics g)
        {
            Pen pen = new Pen(Color.Green);
            for (int i = 0; i < Triangles.Count; i++)
            {
                g.DrawLine(pen, (int)Triangles[i][0][0], (int)(DrawPanel.Height - Triangles[i][0][1]), (int)Triangles[i][1][0], (int)(DrawPanel.Height - Triangles[i][1][1]));
                g.DrawLine(pen, (int)Triangles[i][1][0], (int)(DrawPanel.Height - Triangles[i][1][1]), (int)Triangles[i][2][0], (int)(DrawPanel.Height - Triangles[i][2][1]));
                g.DrawLine(pen, (int)Triangles[i][0][0], (int)(DrawPanel.Height - Triangles[i][0][1]), (int)Triangles[i][2][0], (int)(DrawPanel.Height - Triangles[i][2][1]));

            }
        }

        //рисуем полученные выпуклые многоугольники
        public void DrawConvexRectangles(List<double[]> Rectangle, double[] p, Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i < Rectangle.Count - 1; i++)
            {
                g.DrawLine(pen, (int)(p[0] + Rectangle[i][0]), (int)(DrawPanel.Height - p[1] - Rectangle[i][1]), (int)(p[0] + Rectangle[i + 1][0]), (int)(DrawPanel.Height - p[1] - Rectangle[i + 1][1]));
            }
            g.DrawLine(pen, (int)(p[0] + Rectangle[Rectangle.Count - 1][0]), (int)(DrawPanel.Height - p[1] - Rectangle[Rectangle.Count - 1][1]), (int)(p[0] + Rectangle[0][0]), (int)(DrawPanel.Height - p[1] - Rectangle[0][1]));
        }
    }
}

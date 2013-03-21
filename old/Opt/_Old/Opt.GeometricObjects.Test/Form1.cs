using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Opt.GeometricObjects;
using Opt.GeometricObjects.Extending;

using System.IO;

using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Opt.GeometricObjects.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Polygon polygon = new Polygon();
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                polygon.Insert(0, new Point(e.X, e.Y));
            else
                polygon.Remove(0);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            PolygonExtending.DrawFill(polygon, e.Graphics, Pens.Black, Brushes.Green);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //XmlTextReader xmlr = new XmlTextReader(openFileDialog1.FileName);
                //NetDataContractSerializer dcs = new NetDataContractSerializer();
                //polygon = (Polygon)dcs.ReadObject(xmlr);
                //xmlr.Close();

                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                PolygonExtending.Read(polygon, sr);
                sr.Close();
            }
            Invalidate();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //XmlTextWriter xmlw = new XmlTextWriter(saveFileDialog1.FileName, System.Text.Encoding.UTF8);
                //xmlw.Formatting = Formatting.Indented;
                //NetDataContractSerializer dcs = new NetDataContractSerializer();
                //dcs.WriteObject(xmlw, polygon);
                //xmlw.Close();

                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                PolygonExtending.Write(polygon, sw);
                sw.Close();
            }
        }
    }
}

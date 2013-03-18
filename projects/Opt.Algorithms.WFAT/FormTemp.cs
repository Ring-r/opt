using System;
using System.Windows.Forms;

namespace Opt.Algorithms.WFAT
{
    public partial class FormTemp : Form
    {
        public string Info
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public FormTemp()
        {
            InitializeComponent();
        }

        private void All_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
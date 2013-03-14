using System.Windows.Forms;

namespace PlacingRectangle
{
    public partial class FormTemp : Form
    {
        public string String
        {
            get
            {
                return textBox1.Text;
            }
        }

        public FormTemp()
        {
            InitializeComponent();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Escape)
                Close();
        }
    }
}

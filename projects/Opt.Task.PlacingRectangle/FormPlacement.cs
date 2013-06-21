using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlacingRectangle
{
    public partial class FormPlacement : Form
    {
        public FormPlacement()
        {
            InitializeComponent();
        }

        private Placement placement;
        public Placement Placement
        {
            set
            {
                placement = value;
                dgvObjects.DataSource = placement.Objects_BindingSource();
                dgvObjectsPlaced.DataSource = placement.ObjectsBusy_BindingSource();
                dgvObjectsUnplaced.DataSource = placement.ObjectsFree_BindingSource();
                tbObjectFunction.Text = placement.ObjectFunction.ToString();
                pbVisual.Invalidate();
            }
        }

        private void pbVisual_Paint(object sender, PaintEventArgs e)
        {
            if (placement != null)
                placement.Draw(e.Graphics, (sender as PictureBox).Width, (sender as PictureBox).Height, Pens.Black, Brushes.Yellow, Pens.Black, Brushes.Silver, Brushes.Black);
        }

        private void pbVisual_Resize(object sender, EventArgs e)
        {
            pbVisual.Invalidate();
        }
    }
}

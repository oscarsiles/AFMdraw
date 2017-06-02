using System.Windows.Forms;

namespace AFMdraw
{
    public partial class License : Form
    {
        public License()
        {
            InitializeComponent();
        }

        private void about_Paint(object sender, PaintEventArgs e)
        {
            //// create font
            //Font aboutFont = new Font("Microsoft Sans Serif", 8.0F);
            //SolidBrush aboutBrush = new SolidBrush(Color.Black);

            //// create upper-left corner point
            //PointF aboutPoint = new PointF(12.0F, 12.0F);

            //e.Graphics.DrawString(this.aboutText, aboutFont, aboutBrush, aboutPoint);
        }

        private void licenseText_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
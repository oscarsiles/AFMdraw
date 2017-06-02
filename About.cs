using System.Windows.Forms;

namespace AFMdraw
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public string email = "o.siles-brugge@sheffield.ac.uk";
        public string aboutText = "AFMdraw\n\nCreated by Oscar Siles Brügge\nUniversity of Sheffield\n\nIf you have any issues or suggestions for this software,\nplease contact me at";

        private void emailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + email);
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

        private void licensesButton_Click(object sender, System.EventArgs e)
        {
            License licenses = new License();
            licenses.StartPosition = FormStartPosition.CenterParent; // open on top of parent window
            licenses.ShowDialog();
        }
    }
}
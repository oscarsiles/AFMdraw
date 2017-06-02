using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AFMdraw
{
    public partial class ScriptWindow : Form
    {
        protected override bool ShowWithoutActivation { get { return true; } }
        protected override CreateParams CreateParams
        {
            get
            {
                //make sure Top Most property on form is set to false
                //otherwise this doesn't work
                int CP_NOCLOSE_BUTTON = 0x200;
                int WS_EX_TOPMOST = 0x00000008;

                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
                myCp.ExStyle |= WS_EX_TOPMOST;
                return myCp;
            }
        }

        // talk between parent form
        public event EventHandler UpdateDrawingButtonClicked;

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        public string windowTitleName { get; set; }

        public ScriptWindow()
        {
            InitializeComponent();
            this.Text = windowTitleName;
            this.Refresh();
        }

        public void UpdateScriptText(MemoryStream ms)
        {
            this.Text = windowTitleName;

            ms.Position = 0;
            this.scriptTextBox.LoadFile(ms, RichTextBoxStreamType.PlainText);
            this.scriptTextBox.SelectionStart = this.scriptTextBox.Text.Length;
            this.scriptTextBox.ScrollToCaret();
            ms.Flush();
        }

        public void ClearScriptText()
        {
            this.scriptTextBox.Text = "";
        }

        public MemoryStream TextToMemoryStream()
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(this.scriptTextBox.Text));
            return ms;
        }

        private void scriptTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.None)
            {
                //e.SuppressKeyPress = true;
            }
        }

        private void ScriptWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void updateDrawing_Click(object sender, EventArgs e)
        {
            if (UpdateDrawingButtonClicked != null)
            {
                UpdateDrawingButtonClicked(sender, e);
            }
        }
    }
}
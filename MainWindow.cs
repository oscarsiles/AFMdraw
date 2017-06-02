using AutoUpdaterDotNET;
using System;
using System.Windows.Forms;

namespace AFMdraw
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = ProjectMetadata.programName + " (v" + ProjectMetadata.fileVersion + ")";

            // start autoupdater check
            AutoUpdater.Start("serverXml");
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void newDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingWindow drawWin = new DrawingWindow();
            drawWin.RenameInputBox();
            if (drawWin.scriptName == null)
            {
                drawWin.Dispose();
            }
            else
            {
                drawWin.MdiParent = this;
                drawWin.Show();
            }
        }

        private void loadSourceMenuItem_Click(object sender, EventArgs e)
        {
            DrawingWindow drawWin = new DrawingWindow();
            if (drawWin.scriptName == null)
            {
                drawWin.Dispose();
            }
            else
            {
                drawWin.MdiParent = this;
                drawWin.Show();
            }
            drawWin.LoadSourceToolButton_Click(sender, e);
        }

        private void loadScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingWindow drawWin = new DrawingWindow();
            if (drawWin.scriptName == null)
            {
                drawWin.Dispose();
            }
            else
            {
                drawWin.MdiParent = this;
                drawWin.Show();
            }
            drawWin.LoadScriptToolButton_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.StartPosition = FormStartPosition.CenterParent; // open on top of parent window
            about.ShowDialog();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("ServerXml");
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingWindow duplicateWin = new DrawingWindow();

            duplicateWin = (DrawingWindow)this.ActiveMdiChild;
            duplicateWin.Rename("dup " + duplicateWin.GetName());
            duplicateWin.MdiParent = this;
            duplicateWin.Show();
        }
    }
}
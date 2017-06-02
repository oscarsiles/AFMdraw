using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AFMdraw
{
    public partial class SaveScriptDialog : Form
    {
        private List<Point> pxList = new List<Point>();
        private List<Point> pyList = new List<Point>();

        // set instrument limits
        private decimal minSpeedBruker = 0.1m;
        private decimal maxSpeedBruker = 20;
        private decimal minScaleBruker = 1;
        private decimal maxScaleBruker = 50;
        private decimal minSpeedWitec = 1;
        private decimal maxSpeedWitec = 1000;
        private decimal minScaleWitec = 1;
        private decimal maxScaleWitec = 500;

        private bool isSaved = false;

        private string scriptName;
        private string scriptType;
        private decimal scanSpeed;
        private decimal drawScale;
        private int gridSize;

        private string[] scriptTypeList = new string[] { "witec", "bruker" };

        public SaveScriptDialog(List<Point> list1, List<Point> list2, string name, string type, decimal speed, decimal scale, int size)
        {
            this.scriptName = name;
            this.scriptType = type;
            this.scanSpeed = speed;
            this.drawScale = scale;
            this.gridSize = size;
            this.pxList = list1;
            this.pyList = list2;
            InitializeComponent();
        }

        private void saveScriptDialog_Load(object sender, EventArgs e)
        {
            this.scriptTypeSelector.Items.AddRange(DrawingWindow.scriptTypeList);
            this.scriptTypeSelector.Text = this.scriptType;
            this.scanSpeedSelector.Value = this.scanSpeed;
            this.scaleSelector.Value = this.drawScale;

            if (scriptType == "bruker")
            {
                this.scanSpeedSelector.Enabled = false;
                this.brukerSpeedNotice.Visible = true;
            }
        }

        private void saveScriptDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.scriptType = this.scriptTypeSelector.Text;
            this.scanSpeed = this.scanSpeedSelector.Value;
            this.drawScale = this.scaleSelector.Value;
        }

        private void scriptTypeSelector_KeyDown(object sender, KeyEventArgs e)
        {
            // disable typing in the combobox
            e.SuppressKeyPress = true;
        }

        private void scriptTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.scriptTypeSelector.Text;
            if (type == "witec")
            {
                // enable speed selector
                this.scanSpeedSelector.Enabled = true;
                this.brukerSpeedNotice.Visible = false;

                this.scanSpeedSelector.Minimum = minSpeedWitec;
                this.scanSpeedSelector.Maximum = maxSpeedWitec;
                this.scaleSelector.Minimum = minScaleWitec;
                this.scaleSelector.Maximum = maxScaleWitec;
            }
            else if (type == "bruker")
            {
                // disable speed selector
                this.scanSpeedSelector.Enabled = false;
                this.brukerSpeedNotice.Visible = true;

                this.scanSpeedSelector.Minimum = minSpeedBruker;
                this.scanSpeedSelector.Maximum = maxSpeedBruker;
                this.scaleSelector.Minimum = minScaleBruker;
                this.scaleSelector.Maximum = maxScaleBruker;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            string extension;
            string type = this.scriptTypeSelector.Text.ToLower();
            if (type == "witec")
            {
                extension = "Text file | *.txt";
            }
            else if (type == "bruker")
            {
                extension = "C++ Source File | *.cpp";
            }
            else
            {
                extension = "*.* | *.*";
            }

            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Select save file location",
                InitialDirectory = Directory.GetCurrentDirectory(),
                // if bruker, cpp. if witec, txt
                Filter = extension,
                RestoreDirectory = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.fileLocationText.Text = ""; // clear textbox
                this.fileLocationText.AppendText(dialog.FileName); //using AppendText scrolls to end
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // check for empty boxes
            if (this.fileLocationText.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a save location.", "Error");
            }
            else
            {
                AfmScript script = new AfmScript(this.pxList, this.pyList, this.fileLocationText.Text, this.scriptName, (double)this.scaleSelector.Value, (double)this.scanSpeedSelector.Value, gridSize);
                script.SaveScript(this.scriptTypeSelector.Text);
                this.isSaved = true;
                this.Close();
                // save serialized file (this is now done in drawingWindow.cs, checking for isSaved boolean)
                //saveSource(this.fileLocationText.Text);
            }
        }

        public bool IsSaved()
        {
            return isSaved;
        }

        public string GetBackupPath()
        {
            return this.fileLocationText.Text;
        }

        public void SetParameters(ref string type, ref decimal speed, ref decimal scale)
        {
            type = this.scriptType;
            speed = this.scanSpeed;
            scale = this.drawScale;
        }

        private void saveParametersButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
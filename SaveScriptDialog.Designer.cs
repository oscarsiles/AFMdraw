namespace AFMdraw
{
    partial class SaveScriptDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.typeLabel = new System.Windows.Forms.Label();
            this.scriptTypeSelector = new System.Windows.Forms.ComboBox();
            this.scanSpeedSelector = new System.Windows.Forms.NumericUpDown();
            this.scanSpeedLabel = new System.Windows.Forms.Label();
            this.scaleSelector = new System.Windows.Forms.NumericUpDown();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.fileLocationText = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.brukerSpeedNotice = new System.Windows.Forms.Label();
            this.saveParametersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scanSpeedSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(15, 15);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(64, 13);
            this.typeLabel.TabIndex = 21;
            this.typeLabel.Text = "Script Type:";
            // 
            // scriptTypeSelector
            // 
            this.scriptTypeSelector.FormattingEnabled = true;
            this.scriptTypeSelector.Location = new System.Drawing.Point(100, 12);
            this.scriptTypeSelector.Name = "scriptTypeSelector";
            this.scriptTypeSelector.Size = new System.Drawing.Size(121, 21);
            this.scriptTypeSelector.TabIndex = 12;
            this.scriptTypeSelector.Text = "witec";
            this.scriptTypeSelector.SelectedIndexChanged += new System.EventHandler(this.scriptTypeSelector_SelectedIndexChanged);
            this.scriptTypeSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scriptTypeSelector_KeyDown);
            // 
            // scanSpeedSelector
            // 
            this.scanSpeedSelector.DecimalPlaces = 1;
            this.scanSpeedSelector.Location = new System.Drawing.Point(100, 45);
            this.scanSpeedSelector.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.scanSpeedSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scanSpeedSelector.Name = "scanSpeedSelector";
            this.scanSpeedSelector.Size = new System.Drawing.Size(120, 20);
            this.scanSpeedSelector.TabIndex = 13;
            this.scanSpeedSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // scanSpeedLabel
            // 
            this.scanSpeedLabel.AutoSize = true;
            this.scanSpeedLabel.Location = new System.Drawing.Point(15, 47);
            this.scanSpeedLabel.Name = "scanSpeedLabel";
            this.scanSpeedLabel.Size = new System.Drawing.Size(69, 13);
            this.scanSpeedLabel.TabIndex = 20;
            this.scanSpeedLabel.Text = "Scan Speed:";
            // 
            // scaleSelector
            // 
            this.scaleSelector.DecimalPlaces = 1;
            this.scaleSelector.Location = new System.Drawing.Point(100, 79);
            this.scaleSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scaleSelector.Name = "scaleSelector";
            this.scaleSelector.Size = new System.Drawing.Size(120, 20);
            this.scaleSelector.TabIndex = 14;
            this.scaleSelector.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Location = new System.Drawing.Point(15, 79);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(66, 13);
            this.scaleLabel.TabIndex = 18;
            this.scaleLabel.Text = "Scale ( μm ):";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(354, 110);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(30, 22);
            this.browseButton.TabIndex = 17;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(15, 114);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(79, 13);
            this.fileLabel.TabIndex = 15;
            this.fileLabel.Text = "Save Location:";
            // 
            // fileLocationText
            // 
            this.fileLocationText.Location = new System.Drawing.Point(100, 111);
            this.fileLocationText.Name = "fileLocationText";
            this.fileLocationText.Size = new System.Drawing.Size(248, 20);
            this.fileLocationText.TabIndex = 16;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(174, 147);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 19;
            this.saveButton.Text = "Save Script";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // brukerSpeedNotice
            // 
            this.brukerSpeedNotice.AutoSize = true;
            this.brukerSpeedNotice.Location = new System.Drawing.Point(221, 49);
            this.brukerSpeedNotice.Name = "brukerSpeedNotice";
            this.brukerSpeedNotice.Size = new System.Drawing.Size(181, 13);
            this.brukerSpeedNotice.TabIndex = 22;
            this.brukerSpeedNotice.Text = "Speed will be n, jump speed 20µm/s.";
            this.brukerSpeedNotice.Visible = false;
            // 
            // saveParametersButton
            // 
            this.saveParametersButton.Location = new System.Drawing.Point(12, 147);
            this.saveParametersButton.Name = "saveParametersButton";
            this.saveParametersButton.Size = new System.Drawing.Size(100, 23);
            this.saveParametersButton.TabIndex = 23;
            this.saveParametersButton.Text = "Save Parameters";
            this.saveParametersButton.UseVisualStyleBackColor = true;
            this.saveParametersButton.Click += new System.EventHandler(this.saveParametersButton_Click);
            // 
            // SaveScriptDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 185);
            this.Controls.Add(this.saveParametersButton);
            this.Controls.Add(this.brukerSpeedNotice);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.scriptTypeSelector);
            this.Controls.Add(this.scanSpeedSelector);
            this.Controls.Add(this.scanSpeedLabel);
            this.Controls.Add(this.scaleSelector);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.fileLocationText);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SaveScriptDialog";
            this.ShowIcon = false;
            this.Text = "Save Script";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.saveScriptDialog_FormClosing);
            this.Load += new System.EventHandler(this.saveScriptDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scanSpeedSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox scriptTypeSelector;
        private System.Windows.Forms.NumericUpDown scanSpeedSelector;
        private System.Windows.Forms.Label scanSpeedLabel;
        private System.Windows.Forms.NumericUpDown scaleSelector;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.TextBox fileLocationText;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label brukerSpeedNotice;
        private System.Windows.Forms.Button saveParametersButton;
    }
}
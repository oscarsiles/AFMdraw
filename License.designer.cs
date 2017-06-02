namespace AFMdraw
{
    partial class License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(License));
            this.licenseText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // licenseText
            // 
            this.licenseText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.licenseText.Location = new System.Drawing.Point(13, 13);
            this.licenseText.Multiline = true;
            this.licenseText.Name = "licenseText";
            this.licenseText.Size = new System.Drawing.Size(493, 493);
            this.licenseText.TabIndex = 1;
            this.licenseText.TabStop = false;
            this.licenseText.Text = resources.GetString("licenseText.Text");
            this.licenseText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.licenseText_KeyDown);
            // 
            // license
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 518);
            this.Controls.Add(this.licenseText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "license";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "License";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.about_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox licenseText;

    }
}
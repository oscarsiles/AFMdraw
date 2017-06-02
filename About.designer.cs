namespace AFMdraw
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.emailLink = new System.Windows.Forms.LinkLabel();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.licensesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // emailLink
            // 
            this.emailLink.AutoSize = true;
            this.emailLink.BackColor = System.Drawing.Color.Transparent;
            this.emailLink.Location = new System.Drawing.Point(116, 90);
            this.emailLink.Name = "emailLink";
            this.emailLink.Size = new System.Drawing.Size(152, 13);
            this.emailLink.TabIndex = 1;
            this.emailLink.TabStop = true;
            this.emailLink.Text = "o.siles-brugge@sheffield.ac.uk";
            this.emailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLink_LinkClicked);
            // 
            // aboutLabel
            // 
            this.aboutLabel.AutoSize = true;
            this.aboutLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutLabel.Location = new System.Drawing.Point(12, 12);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(263, 91);
            this.aboutLabel.TabIndex = 2;
            this.aboutLabel.Text = "AFMdraw\r\n\r\nCreated by Oscar Siles Brügge\r\nUniversity of Sheffield\r\n\r\nIf you have" +
    " any issues or suggestions for this software,\r\nplease contact me at: ";
            // 
            // licensesButton
            // 
            this.licensesButton.Location = new System.Drawing.Point(12, 113);
            this.licensesButton.Name = "licensesButton";
            this.licensesButton.Size = new System.Drawing.Size(75, 23);
            this.licensesButton.TabIndex = 3;
            this.licensesButton.Text = "License...";
            this.licensesButton.UseVisualStyleBackColor = true;
            this.licensesButton.Click += new System.EventHandler(this.licensesButton_Click);
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.licensesButton);
            this.Controls.Add(this.emailLink);
            this.Controls.Add(this.aboutLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "about";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.about_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel emailLink;
        private System.Windows.Forms.Label aboutLabel;
        private System.Windows.Forms.Button licensesButton;
    }
}
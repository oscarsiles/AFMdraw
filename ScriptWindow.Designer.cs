namespace AFMdraw
{
    partial class ScriptWindow
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
            this.scriptTextBox = new System.Windows.Forms.RichTextBox();
            this.borderPanel = new System.Windows.Forms.Panel();
            this.lineNumbersForRichText1 = new AFMdraw.LineNumbersForRichText();
            this.lineNumberBackPanel = new System.Windows.Forms.Panel();
            this.updateDrawing = new System.Windows.Forms.Button();
            this.borderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scriptTextBox
            // 
            this.scriptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scriptTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptTextBox.Location = new System.Drawing.Point(40, 0);
            this.scriptTextBox.Name = "scriptTextBox";
            this.scriptTextBox.Size = new System.Drawing.Size(450, 286);
            this.scriptTextBox.TabIndex = 0;
            this.scriptTextBox.Text = "";
            this.scriptTextBox.WordWrap = false;
            this.scriptTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scriptTextBox_KeyDown);
            // 
            // borderPanel
            // 
            this.borderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.borderPanel.BackColor = System.Drawing.Color.White;
            this.borderPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.borderPanel.Controls.Add(this.lineNumbersForRichText1);
            this.borderPanel.Controls.Add(this.scriptTextBox);
            this.borderPanel.Controls.Add(this.lineNumberBackPanel);
            this.borderPanel.Location = new System.Drawing.Point(13, 13);
            this.borderPanel.Name = "borderPanel";
            this.borderPanel.Size = new System.Drawing.Size(493, 290);
            this.borderPanel.TabIndex = 1;
            // 
            // lineNumbersForRichText1
            // 
            this.lineNumbersForRichText1.AutoSizing = false;
            this.lineNumbersForRichText1.BackColor = System.Drawing.SystemColors.Control;
            this.lineNumbersForRichText1.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText1.BackgroundGradientBetaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText1.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lineNumbersForRichText1.BorderLinesColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText1.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText1.BorderLinesThickness = 1F;
            this.lineNumbersForRichText1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lineNumbersForRichText1.DockSide = AFMdraw.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineNumbersForRichText1.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText1.GridLinesThickness = 1F;
            this.lineNumbersForRichText1.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText1.LineNumbersAntiAlias = false;
            this.lineNumbersForRichText1.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText1.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText1.LineNumbersLeadingZeroes = false;
            this.lineNumbersForRichText1.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText1.Location = new System.Drawing.Point(6, 0);
            this.lineNumbersForRichText1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText1.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.MarginLinesSide = AFMdraw.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText1.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText1.MarginLinesThickness = 1F;
            this.lineNumbersForRichText1.Name = "lineNumbersForRichText1";
            this.lineNumbersForRichText1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText1.ParentRichTextBox = this.scriptTextBox;
            this.lineNumbersForRichText1.SeeThroughMode = true;
            this.lineNumbersForRichText1.ShowBackgroundGradient = false;
            this.lineNumbersForRichText1.ShowBorderLines = false;
            this.lineNumbersForRichText1.ShowGridLines = false;
            this.lineNumbersForRichText1.ShowLineNumbers = true;
            this.lineNumbersForRichText1.ShowMarginLines = false;
            this.lineNumbersForRichText1.Size = new System.Drawing.Size(33, 286);
            this.lineNumbersForRichText1.TabIndex = 1;
            // 
            // lineNumberBackPanel
            // 
            this.lineNumberBackPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lineNumberBackPanel.BackColor = System.Drawing.SystemColors.Control;
            this.lineNumberBackPanel.Location = new System.Drawing.Point(-7, -2);
            this.lineNumberBackPanel.Name = "lineNumberBackPanel";
            this.lineNumberBackPanel.Size = new System.Drawing.Size(42, 290);
            this.lineNumberBackPanel.TabIndex = 2;
            // 
            // updateDrawing
            // 
            this.updateDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.updateDrawing.Location = new System.Drawing.Point(13, 309);
            this.updateDrawing.Name = "updateDrawing";
            this.updateDrawing.Size = new System.Drawing.Size(94, 23);
            this.updateDrawing.TabIndex = 2;
            this.updateDrawing.Text = "Update Drawing";
            this.updateDrawing.UseVisualStyleBackColor = true;
            this.updateDrawing.Click += new System.EventHandler(this.updateDrawing_Click);
            // 
            // ScriptWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 344);
            this.Controls.Add(this.updateDrawing);
            this.Controls.Add(this.borderPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScriptWindow";
            this.Text = "ScriptWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptWindow_FormClosing);
            this.borderPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox scriptTextBox;
        private System.Windows.Forms.Panel borderPanel;
        private LineNumbersForRichText lineNumbersForRichText1;
        private System.Windows.Forms.Panel lineNumberBackPanel;
        private System.Windows.Forms.Button updateDrawing;






    }
}
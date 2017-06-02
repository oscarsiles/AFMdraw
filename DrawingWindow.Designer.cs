namespace AFMdraw
{
    partial class DrawingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingWindow));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.paddingTool = new System.Windows.Forms.ToolStripLabel();
            this.nameToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripToolButton = new System.Windows.Forms.ToolStripSplitButton();
            this.spiralToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spiralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.starToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.squareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.letterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticesSelectorLabel = new System.Windows.Forms.ToolStripLabel();
            this.verticesSelector = new AFMdraw.NumericalToolStripTextBox();
            this.spiralNToolLabel = new System.Windows.Forms.ToolStripLabel();
            this.spiralNSelector = new AFMdraw.NumericalToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gridSizeToolLabel = new System.Windows.Forms.ToolStripLabel();
            this.gridSizeSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.browseImageToolButton = new System.Windows.Forms.ToolStripButton();
            this.clearImageToolButton = new System.Windows.Forms.ToolStripButton();
            this.gridToggleToolButton = new System.Windows.Forms.ToolStripButton();
            this.snapToGridToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolButton = new System.Windows.Forms.ToolStripButton();
            this.clearToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSourceToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolDropdownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveScriptToolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSourceToolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fixBoundariesToolButton = new System.Windows.Forms.ToolStripButton();
            this.drawingStatusStrip = new System.Windows.Forms.StatusStrip();
            this.drawingStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingStatusSeparator = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingStatusPad = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingStatusCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingStatusSeparator3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingStatusLineCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawingPanel = new AFMdraw.MyPanel();
            this.toolStrip.SuspendLayout();
            this.drawingStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paddingTool,
            this.nameToolButton,
            this.toolStripSeparator5,
            this.toolStripToolButton,
            this.verticesSelectorLabel,
            this.verticesSelector,
            this.spiralNToolLabel,
            this.spiralNSelector,
            this.toolStripSeparator2,
            this.gridSizeToolLabel,
            this.gridSizeSelector,
            this.toolStripSeparator6,
            this.browseImageToolButton,
            this.clearImageToolButton,
            this.gridToggleToolButton,
            this.snapToGridToolButton,
            this.toolStripSeparator3,
            this.undoToolButton,
            this.clearToolButton,
            this.toolStripSeparator4,
            this.loadSourceToolButton,
            this.saveToolDropdownButton,
            this.fixBoundariesToolButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 534);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(540, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // paddingTool
            // 
            this.paddingTool.Name = "paddingTool";
            this.paddingTool.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.paddingTool.Size = new System.Drawing.Size(5, 22);
            // 
            // nameToolButton
            // 
            this.nameToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nameToolButton.Image = ((System.Drawing.Image)(resources.GetObject("nameToolButton.Image")));
            this.nameToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nameToolButton.Name = "nameToolButton";
            this.nameToolButton.Size = new System.Drawing.Size(23, 22);
            this.nameToolButton.Text = "Rename...";
            this.nameToolButton.Click += new System.EventHandler(this.NameToolButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripToolButton
            // 
            this.toolStripToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spiralToolStripMenuItem1,
            this.spiralToolStripMenuItem,
            this.starToolStripMenuItem,
            this.polygonToolStripMenuItem,
            this.circleToolButton,
            this.squareToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.letterToolStripMenuItem});
            this.toolStripToolButton.Image = global::AFMdraw.Properties.Resources.line;
            this.toolStripToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToolButton.Name = "toolStripToolButton";
            this.toolStripToolButton.Size = new System.Drawing.Size(32, 22);
            this.toolStripToolButton.Text = "Line";
            // 
            // spiralToolStripMenuItem1
            // 
            this.spiralToolStripMenuItem1.Image = global::AFMdraw.Properties.Resources.spiral;
            this.spiralToolStripMenuItem1.Name = "spiralToolStripMenuItem1";
            this.spiralToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.spiralToolStripMenuItem1.Text = "Archimedean Spiral";
            this.spiralToolStripMenuItem1.Click += new System.EventHandler(this.spiralToolButton_Click);
            // 
            // spiralToolStripMenuItem
            // 
            this.spiralToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.array;
            this.spiralToolStripMenuItem.Name = "spiralToolStripMenuItem";
            this.spiralToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.spiralToolStripMenuItem.Text = "Array";
            this.spiralToolStripMenuItem.Click += new System.EventHandler(this.arrayToolButton_Click);
            // 
            // starToolStripMenuItem
            // 
            this.starToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.star;
            this.starToolStripMenuItem.Name = "starToolStripMenuItem";
            this.starToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.starToolStripMenuItem.Text = "Star";
            this.starToolStripMenuItem.Click += new System.EventHandler(this.starToolButton_Click);
            // 
            // polygonToolStripMenuItem
            // 
            this.polygonToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.polygon;
            this.polygonToolStripMenuItem.Name = "polygonToolStripMenuItem";
            this.polygonToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.polygonToolStripMenuItem.Text = "Polygon";
            this.polygonToolStripMenuItem.Click += new System.EventHandler(this.polygonToolButton_Click);
            // 
            // circleToolButton
            // 
            this.circleToolButton.Image = global::AFMdraw.Properties.Resources.circle;
            this.circleToolButton.Name = "circleToolButton";
            this.circleToolButton.Size = new System.Drawing.Size(177, 22);
            this.circleToolButton.Text = "Circle";
            this.circleToolButton.Click += new System.EventHandler(this.circleToolButton_Click);
            // 
            // squareToolStripMenuItem
            // 
            this.squareToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.square;
            this.squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            this.squareToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.squareToolStripMenuItem.Text = "Square";
            this.squareToolStripMenuItem.Click += new System.EventHandler(this.squareToolButton_Click);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.triangle;
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.triangleToolButton_Click);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Image = global::AFMdraw.Properties.Resources.line;
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolButton_Click);
            // 
            // letterToolStripMenuItem
            // 
            this.letterToolStripMenuItem.Name = "letterToolStripMenuItem";
            this.letterToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.letterToolStripMenuItem.Text = "Letter";
            this.letterToolStripMenuItem.Click += new System.EventHandler(this.letterToolButton_Click);
            // 
            // verticesSelectorLabel
            // 
            this.verticesSelectorLabel.Name = "verticesSelectorLabel";
            this.verticesSelectorLabel.Size = new System.Drawing.Size(51, 22);
            this.verticesSelectorLabel.Text = "Vertices:";
            this.verticesSelectorLabel.Visible = false;
            // 
            // verticesSelector
            // 
            this.verticesSelector.Name = "verticesSelector";
            this.verticesSelector.Size = new System.Drawing.Size(25, 25);
            this.verticesSelector.Text = "5";
            this.verticesSelector.Visible = false;
            this.verticesSelector.Leave += new System.EventHandler(this.verticesSelector_Leave);
            // 
            // spiralNToolLabel
            // 
            this.spiralNToolLabel.Name = "spiralNToolLabel";
            this.spiralNToolLabel.Size = new System.Drawing.Size(17, 22);
            this.spiralNToolLabel.Text = "n:";
            this.spiralNToolLabel.Visible = false;
            // 
            // spiralNSelector
            // 
            this.spiralNSelector.Name = "spiralNSelector";
            this.spiralNSelector.Size = new System.Drawing.Size(25, 25);
            this.spiralNSelector.Text = "1";
            this.spiralNSelector.Visible = false;
            this.spiralNSelector.Leave += new System.EventHandler(this.spiralNSelector_Leave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // gridSizeToolLabel
            // 
            this.gridSizeToolLabel.Name = "gridSizeToolLabel";
            this.gridSizeToolLabel.Size = new System.Drawing.Size(32, 22);
            this.gridSizeToolLabel.Text = "Grid:";
            // 
            // gridSizeSelector
            // 
            this.gridSizeSelector.AutoCompleteCustomSource.AddRange(new string[] {
            "512",
            "768",
            "1024"});
            this.gridSizeSelector.AutoSize = false;
            this.gridSizeSelector.DropDownWidth = 30;
            this.gridSizeSelector.Items.AddRange(new object[] {
            "512",
            "640",
            "768",
            "896",
            "1024"});
            this.gridSizeSelector.Name = "gridSizeSelector";
            this.gridSizeSelector.Size = new System.Drawing.Size(47, 23);
            this.gridSizeSelector.Text = "512";
            this.gridSizeSelector.SelectedIndexChanged += new System.EventHandler(this.gridSizeSelector_SelectedIndexChanged);
            this.gridSizeSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridSizeSelector_KeyDown);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // browseImageToolButton
            // 
            this.browseImageToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browseImageToolButton.Image = ((System.Drawing.Image)(resources.GetObject("browseImageToolButton.Image")));
            this.browseImageToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browseImageToolButton.Name = "browseImageToolButton";
            this.browseImageToolButton.Size = new System.Drawing.Size(23, 22);
            this.browseImageToolButton.Text = "Browse Image...";
            this.browseImageToolButton.Click += new System.EventHandler(this.browseImageToolButton_Click);
            // 
            // clearImageToolButton
            // 
            this.clearImageToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearImageToolButton.Enabled = false;
            this.clearImageToolButton.Image = ((System.Drawing.Image)(resources.GetObject("clearImageToolButton.Image")));
            this.clearImageToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearImageToolButton.Name = "clearImageToolButton";
            this.clearImageToolButton.Size = new System.Drawing.Size(23, 22);
            this.clearImageToolButton.Text = "Clear Image";
            this.clearImageToolButton.Click += new System.EventHandler(this.clearImageToolButton_Click);
            // 
            // gridToggleToolButton
            // 
            this.gridToggleToolButton.BackColor = System.Drawing.SystemColors.Control;
            this.gridToggleToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gridToggleToolButton.Image = global::AFMdraw.Properties.Resources.grid;
            this.gridToggleToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridToggleToolButton.Name = "gridToggleToolButton";
            this.gridToggleToolButton.Size = new System.Drawing.Size(23, 22);
            this.gridToggleToolButton.Text = "Gridlines";
            this.gridToggleToolButton.Click += new System.EventHandler(this.gridToggleToolButton_Click);
            // 
            // snapToGridToolButton
            // 
            this.snapToGridToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapToGridToolButton.Image = global::AFMdraw.Properties.Resources.noSnapGrid;
            this.snapToGridToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapToGridToolButton.Name = "snapToGridToolButton";
            this.snapToGridToolButton.Size = new System.Drawing.Size(23, 22);
            this.snapToGridToolButton.Text = "Snap to grid";
            this.snapToGridToolButton.Click += new System.EventHandler(this.snapToGridToolButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // undoToolButton
            // 
            this.undoToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoToolButton.Enabled = false;
            this.undoToolButton.Image = ((System.Drawing.Image)(resources.GetObject("undoToolButton.Image")));
            this.undoToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolButton.Name = "undoToolButton";
            this.undoToolButton.Size = new System.Drawing.Size(23, 22);
            this.undoToolButton.Text = "Undo";
            this.undoToolButton.Click += new System.EventHandler(this.undoToolButton_Click);
            this.undoToolButton.MouseEnter += new System.EventHandler(this.undoToolButton_MouseEnter);
            this.undoToolButton.MouseLeave += new System.EventHandler(this.undoToolButton_MouseLeave);
            // 
            // clearToolButton
            // 
            this.clearToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolButton.Image = ((System.Drawing.Image)(resources.GetObject("clearToolButton.Image")));
            this.clearToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolButton.Name = "clearToolButton";
            this.clearToolButton.Size = new System.Drawing.Size(23, 22);
            this.clearToolButton.Text = "Clear All...";
            this.clearToolButton.Click += new System.EventHandler(this.clearToolButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // loadSourceToolButton
            // 
            this.loadSourceToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadSourceToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadScriptToolStripMenuItem,
            this.loadSourceToolStripMenuItem});
            this.loadSourceToolButton.Image = ((System.Drawing.Image)(resources.GetObject("loadSourceToolButton.Image")));
            this.loadSourceToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadSourceToolButton.Name = "loadSourceToolButton";
            this.loadSourceToolButton.Size = new System.Drawing.Size(29, 22);
            this.loadSourceToolButton.Text = "Load Source...";
            // 
            // loadScriptToolStripMenuItem
            // 
            this.loadScriptToolStripMenuItem.Name = "loadScriptToolStripMenuItem";
            this.loadScriptToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.loadScriptToolStripMenuItem.Text = "Load Script...";
            this.loadScriptToolStripMenuItem.Click += new System.EventHandler(this.LoadScriptToolButton_Click);
            // 
            // loadSourceToolStripMenuItem
            // 
            this.loadSourceToolStripMenuItem.Name = "loadSourceToolStripMenuItem";
            this.loadSourceToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.loadSourceToolStripMenuItem.Text = "Load Source...";
            this.loadSourceToolStripMenuItem.Click += new System.EventHandler(this.LoadSourceToolButton_Click);
            // 
            // saveToolDropdownButton
            // 
            this.saveToolDropdownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolDropdownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveScriptToolButton,
            this.saveSourceToolButton});
            this.saveToolDropdownButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolDropdownButton.Image")));
            this.saveToolDropdownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolDropdownButton.Name = "saveToolDropdownButton";
            this.saveToolDropdownButton.Size = new System.Drawing.Size(29, 22);
            this.saveToolDropdownButton.Text = "Save...";
            // 
            // saveScriptToolButton
            // 
            this.saveScriptToolButton.Name = "saveScriptToolButton";
            this.saveScriptToolButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveScriptToolButton.Size = new System.Drawing.Size(185, 22);
            this.saveScriptToolButton.Text = "Script...";
            this.saveScriptToolButton.Click += new System.EventHandler(this.saveScriptToolButton_Click);
            // 
            // saveSourceToolButton
            // 
            this.saveSourceToolButton.Name = "saveSourceToolButton";
            this.saveSourceToolButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveSourceToolButton.Size = new System.Drawing.Size(185, 22);
            this.saveSourceToolButton.Text = "Source...";
            this.saveSourceToolButton.Click += new System.EventHandler(this.saveSourceToolButton_Click);
            // 
            // fixBoundariesToolButton
            // 
            this.fixBoundariesToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fixBoundariesToolButton.Image = global::AFMdraw.Properties.Resources.boundaries;
            this.fixBoundariesToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fixBoundariesToolButton.Name = "fixBoundariesToolButton";
            this.fixBoundariesToolButton.Size = new System.Drawing.Size(23, 22);
            this.fixBoundariesToolButton.Text = "Fix boundary lines";
            this.fixBoundariesToolButton.Click += new System.EventHandler(this.fixBoundariesToolButton_Click);
            // 
            // drawingStatusStrip
            // 
            this.drawingStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawingStatusText,
            this.drawingStatusSeparator,
            this.drawingStatusPad,
            this.drawingStatusCoords,
            this.drawingStatusSeparator3,
            this.drawingStatusLineCount});
            this.drawingStatusStrip.Location = new System.Drawing.Point(0, 559);
            this.drawingStatusStrip.Name = "drawingStatusStrip";
            this.drawingStatusStrip.Size = new System.Drawing.Size(540, 22);
            this.drawingStatusStrip.TabIndex = 1;
            this.drawingStatusStrip.Text = "drawingStatusStrip";
            // 
            // drawingStatusText
            // 
            this.drawingStatusText.Name = "drawingStatusText";
            this.drawingStatusText.Size = new System.Drawing.Size(42, 17);
            this.drawingStatusText.Text = "Ready.";
            // 
            // drawingStatusSeparator
            // 
            this.drawingStatusSeparator.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.drawingStatusSeparator.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.drawingStatusSeparator.Name = "drawingStatusSeparator";
            this.drawingStatusSeparator.Size = new System.Drawing.Size(4, 17);
            // 
            // drawingStatusPad
            // 
            this.drawingStatusPad.Name = "drawingStatusPad";
            this.drawingStatusPad.Size = new System.Drawing.Size(429, 17);
            this.drawingStatusPad.Spring = true;
            // 
            // drawingStatusCoords
            // 
            this.drawingStatusCoords.Name = "drawingStatusCoords";
            this.drawingStatusCoords.Size = new System.Drawing.Size(0, 17);
            // 
            // drawingStatusSeparator3
            // 
            this.drawingStatusSeparator3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.drawingStatusSeparator3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.drawingStatusSeparator3.Name = "drawingStatusSeparator3";
            this.drawingStatusSeparator3.Size = new System.Drawing.Size(4, 17);
            // 
            // drawingStatusLineCount
            // 
            this.drawingStatusLineCount.Name = "drawingStatusLineCount";
            this.drawingStatusLineCount.Size = new System.Drawing.Size(46, 17);
            this.drawingStatusLineCount.Text = "Lines: 0";
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingPanel.Location = new System.Drawing.Point(13, 13);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(514, 514);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingPanel_Paint);
            this.drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseDown);
            this.drawingPanel.MouseEnter += new System.EventHandler(this.drawingPanel_MouseEnter);
            this.drawingPanel.MouseLeave += new System.EventHandler(this.drawingPanel_MouseLeave);
            this.drawingPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseMove);
            // 
            // DrawingWindow
            // 
            this.ClientSize = new System.Drawing.Size(540, 581);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.drawingStatusStrip);
            this.Controls.Add(this.drawingPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DrawingWindow";
            this.Text = "New drawing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.drawingWindow_FormClosing);
            this.Load += new System.EventHandler(this.drawingWindow_Load);
            this.Enter += new System.EventHandler(this.drawingWindow_Enter);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawingWindow_MouseClick);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.drawingStatusStrip.ResumeLayout(false);
            this.drawingStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AFMdraw.MyPanel drawingPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel verticesSelectorLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton browseImageToolButton;
        private System.Windows.Forms.ToolStripButton clearImageToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton undoToolButton;
        private System.Windows.Forms.ToolStripButton clearToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton saveToolDropdownButton;
        private System.Windows.Forms.ToolStripMenuItem saveScriptToolButton;
        private System.Windows.Forms.ToolStripMenuItem saveSourceToolButton;
        private System.Windows.Forms.ToolStripLabel paddingTool;
        private System.Windows.Forms.ToolStripButton nameToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton gridToggleToolButton;
        private System.Windows.Forms.ToolStripLabel gridSizeToolLabel;
        private System.Windows.Forms.ToolStripComboBox gridSizeSelector;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSplitButton toolStripToolButton;
        private System.Windows.Forms.ToolStripMenuItem spiralToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spiralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem starToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private NumericalToolStripTextBox verticesSelector;
        private System.Windows.Forms.ToolStripLabel spiralNToolLabel;
        private NumericalToolStripTextBox spiralNSelector;
        private System.Windows.Forms.StatusStrip drawingStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusText;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusSeparator;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusPad;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusCoords;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel drawingStatusLineCount;
        private System.Windows.Forms.ToolStripButton snapToGridToolButton;
        private System.Windows.Forms.ToolStripButton fixBoundariesToolButton;
        private System.Windows.Forms.ToolStripMenuItem circleToolButton;
        private System.Windows.Forms.ToolStripDropDownButton loadSourceToolButton;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem letterToolStripMenuItem;
    }
}
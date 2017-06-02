using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFMdraw
{
    public partial class DrawingWindow : Form
    {
        private Point p1, p2, cursorPoint, startPoint; // individual points, to be then added to lists below
        private List<Point> p1List = new List<Point>();  // start Point
        private List<Point> p2List = new List<Point>();  // end Point
        private List<Point> m1List = new List<Point>(); // start cursor Point
        private List<Point> m2List = new List<Point>(); // end cursor Point
        private List<int> lastLinesAdded = new List<int>(); // count how many lines added previously, for undo operations
        private TextInfo p = new CultureInfo("en-US", false).TextInfo; // used for some text manipulation
        private int penWidth = 1;
        private int shiftSnapDistance = 4; // in pixels, radius
        private bool gridlines = false;
        private bool isGridSnap = false;
        private string shapeType = "line"; // default to line
        public string backupFilename;

        // source file variables (default to witec)
        public static int parameterNumber = 5;

        public string scriptName = "New drawing";
        public string scriptType = "bruker";
        public decimal scanSpeed = 1;
        public decimal drawScale = 25;
        public int gridSize = 512;

        private string[] shapeTypeList = new string[] { "line", "triangle", "square", "circle", "polygon", "star", "array", "spiral", "letter" };
        public static string[] scriptTypeList = new string[] { "witec", "bruker" };

        // create script window instance
        private ScriptWindow scriptWin;

        public DrawingWindow()
        {
            InitializeComponent();
        }

        // DRAWING METHODS
        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.drawingPanel.Focus();
            drawingWindow_Enter(sender, e);
            // cancel line on right click
            if (e.Button == MouseButtons.Right)
            {
                clearCoordinates();
                return;
            }

            // check for gridsnap
            Point t;
            if (isGridSnap)
            {
                t = gridSnap(e);
            }
            else
            {
                t = e.Location;
            }

            backupFilename = ""; // reset backup on any modification
            if (p1.X == 0 && p1.Y == 0) // mouse not been on panel yet
            {
                p1 = t;

                // check for shift pressed
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    p2 = t;

                    int count = p1List.Count();
                    if (count > 0) // first Point exists
                    {
                        Point snappedPoint = snapCheck(e, p1List, p2List, shiftSnapDistance);

                        if ((snappedPoint != p2List[count - 1]) && (snappedPoint != new Point(0, 0))) // make sure we actually moved, and is snap point
                        {
                            this.drawingStatusText.Text = "Successful snap.";
                            Shapes shape = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), p2List[count - 1], snappedPoint, ref p1List, ref p2List, ref lastLinesAdded);
                            shape.IsDrawn();
                            shape.AddPoints();
                            clearCoordinates();
                        }
                        else // no snapped point or too close to originating point
                        {
                            this.drawingStatusText.Text = p.ToTitleCase(shapeType) + " added.";
                            Shapes shape = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), p2List[count - 1], p2, ref p1List, ref p2List, ref lastLinesAdded);
                            shape.IsDrawn();
                            shape.AddPoints();
                            clearCoordinates();
                        }
                    }

                    this.drawingPanel.Refresh();
                }
            }
            else
            {
                p2 = t;

                if (p1 != p2) // only add to list if line
                {
                    // check for shift press, then do snap check
                    if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                    {
                        Point snappedPoint = snapCheck(e, p1List, p2List, shiftSnapDistance);
                        int count = p1List.Count();

                        // snapped point found
                        if (snappedPoint != new Point(0, 0))
                        {
                            this.drawingStatusText.Text = "Successful snap.";
                            Shapes shape = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), p1, snappedPoint, ref p1List, ref p2List, ref lastLinesAdded);
                            shape.IsDrawn();
                            shape.AddPoints();
                        }
                        else
                        {
                            this.drawingStatusText.Text = p.ToTitleCase(shapeType) + " added.";
                            Shapes shape = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), p1, p2, ref p1List, ref p2List, ref lastLinesAdded);
                            shape.IsDrawn();
                            shape.AddPoints();
                        }
                    }
                    else
                    {
                        this.drawingStatusText.Text = p.ToTitleCase(shapeType) + " added.";
                        Shapes shape = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), p1, p2, ref p1List, ref p2List, ref lastLinesAdded);
                        shape.IsDrawn();
                        shape.AddPoints();
                    }

                    this.undoToolButton.Enabled = true; // enable undo
                    //this.saveButton.Enabled = true;
                    //this.saveSourceButton.Enabled = true;
                }

                // Clear X and Y values, change window title, and redraw panel
                setUnsaved();
                this.drawingPanel.Refresh();
                m1List.Clear();
                m2List.Clear();
                clearCoordinates();
            }

            updateLineCount();
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            m1List.Clear();
            m2List.Clear();
            this.drawingPanel.Invalidate();

            // check for gridsnap
            Point t;
            if (isGridSnap)
            {
                t = gridSnap(e);
            }
            else
            {
                t = e.Location;
            }

            // update statusStrip with coordinates (to scale)
            string coordinates = (t.X * (drawScale / gridSize) - (drawScale / 2)).ToString(".0") + ", " + (t.Y * (drawScale / gridSize) - (drawScale / 2)).ToString(".0");
            this.drawingStatusCoords.Text = "(" + coordinates + ")";

            int count = p1List.Count();
            if (p1 == new Point(0, 0))
            {
                // make sure there are Points
                if (count == 0)
                {
                    this.drawingPanel.Refresh();
                    return;
                }
                // check for shift pressed
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    startPoint = p2List[count - 1];
                    cursorPoint = t;
                    Shapes shape1 = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), startPoint, cursorPoint, ref m1List, ref m2List, ref lastLinesAdded);
                    shape1.AddPoints();
                    this.drawingPanel.Refresh();
                }
                return;
            }
            startPoint = p1;
            cursorPoint = t;

            Shapes shape2 = new Shapes(shapeType, (int)Convert.ToDecimal(this.verticesSelector.Text), Convert.ToDecimal(this.spiralNSelector.Text), startPoint, cursorPoint, ref m1List, ref m2List, ref lastLinesAdded);
            shape2.AddPoints();
            this.drawingPanel.Refresh(); // refresh while moving mouse on panel
        }

        private void drawingPanel_MouseLeave(object sender, EventArgs e)
        {
            //clearCoordinates();
            //this.drawingPanel.Refresh();
            this.drawingStatusCoords.Text = "";
        }

        private void drawingPanel_MouseEnter(object sender, EventArgs e)
        {
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // gridlines (if toggled)
            if (gridlines)
            {
                using (var p = new Pen(Color.LightGray, 1))
                {
                    int gridlineSpacing = 16;
                    int gridlineNumber = gridSize / gridlineSpacing;

                    Point g1, g2, g3, g4;
                    for (int i = 0; i < gridlineNumber; i++)
                    {
                        g1 = new Point((gridSize / gridlineNumber) * (1 + i), 0);
                        g2 = new Point((gridSize / gridlineNumber) * (1 + i), gridSize);
                        g3 = new Point(0, (gridSize / gridlineNumber) * (1 + i));
                        g4 = new Point(gridSize, (gridSize / gridlineNumber) * (1 + i));
                        g.DrawLine(p, g1, g2);
                        g.DrawLine(p, g3, g4);
                    }
                }
            }

            // lets make a grid
            using (var p = new Pen(Color.Gray, 1))
            {
                Point grid1 = new Point(gridSize / 2, 0);
                Point grid2 = new Point(gridSize / 2, gridSize);
                Point grid3 = new Point(0, gridSize / 2);
                Point grid4 = new Point(gridSize, gridSize / 2);
                g.DrawLine(p, grid1, grid2);
                g.DrawLine(p, grid3, grid4);
            }

            // draw the lines in the lists, use rectangle when single dot detected (for arrays)
            using (var p = new Pen(Color.Black, penWidth))
            {
                for (int x = 0; x < p1List.Count; x++)
                {
                    if (p1List[x] == p2List[x]) // point
                    {
                        g.DrawRectangle(p, p1List[x].X, p1List[x].Y, 1, 1);
                    }
                    else
                    {
                        g.DrawLine(p, p1List[x], p2List[x]);
                    }
                }
            }

            // lets make a line on the mouse
            using (var p = new Pen(Color.Blue, penWidth))
            {
                if (m1List.Count != 0) // make sure not empty!
                {
                    for (int x = 0; x < m1List.Count; x++)
                    {
                        if (m1List[x] == m2List[x]) // point
                        {
                            g.DrawRectangle(p, m1List[x].X, m1List[x].Y, 1, 1);
                        }
                        else
                        {
                            g.DrawLine(p, m1List[x], m2List[x]);
                        }
                    }
                    //g.DrawLine(p, startPoint, cursorPoint);
                }
            }
        }

        private void drawingPanel_Leave(object sender, EventArgs e)
        {
        }

        private void drawingWindow_Load(object sender, EventArgs e)
        {
            this.Text = scriptName;
            this.Refresh();
            this.toolStrip.Renderer = new MySR();

            // testing
            openScriptWindow();
            scriptWin.UpdateDrawingButtonClicked += new EventHandler(scriptWin_updateDrawingButtonClick);
        }

        private void drawingWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //backupFilename = Path.GetFileNameWithoutExtension(this.fileLocationText.Text) + "-source.txt";

            int count = p1List.Count();

            if (!this.Text.Contains("*")) // close anyway on saved drawing
            {
                this.scriptWin.Dispose();
                this.Dispose();
                return;
            }
            // check if backup exists
            //if (File.Exists(backupFilename) == false)
            if (true)
            {
                var window = MessageBox.Show(
                    "No backup detected for " + scriptName + ". Exit anyway?\n(All unsaved progress will be lost)",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (window == DialogResult.No) // want to save progress
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.scriptWin.Dispose();
                    this.Dispose();
                }
            }
        }

        private void drawingWindow_Enter(object sender, EventArgs e)
        {
            titleBarClicked();
        }

        private void drawingWindow_MouseClick(object sender, MouseEventArgs e)
        {
            drawingWindow_Enter(sender, e);
        }

        // CUSTOM METHODS
        private void titleBarClicked()
        {
            List<Form> openForms = new List<Form>();

            // not optimal (getting there...)
            foreach (Form form in Application.OpenForms)
                openForms.Add(form); // need to do this, or Application.OpenForms will change in size, causing exception

            foreach (Form form in openForms)
            {
                if (form != this.scriptWin && form is ScriptWindow)
                {
                    form.Hide();
                }
            }

            this.scriptWin.Show();
        }

        private void clearCoordinates()
        {
            // clear all coords
            startPoint = new Point(0, 0);
            cursorPoint = new Point(0, 0);
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);
            m1List.Clear();
            m2List.Clear();
        }

        private void updateLineCount()
        {
            updateScriptWindow();

            this.drawingStatusLineCount.Text = "Lines: " + p1List.Count();
        }

        private void updateScriptWindow()
        {
            if (scriptWin == null) { openScriptWindow(); }

            if (p1List.Count() == 0)
            {
                scriptWin.ClearScriptText();
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    AfmScript script = new AfmScript(p1List, p2List, "script", scriptName, (double)drawScale, (double)scanSpeed, gridSize);
                    script.OutputCurrentMemoryStream(scriptType).WriteTo(ms);
                    scriptWin.UpdateScriptText(ms);
                }
            }

            scriptWin.windowTitleName = scriptName + " script source code";
        }

        private void setUnsaved()
        {
            if (!this.Text.Contains("*"))
            {
                this.Text = this.Text + " *";
            }
        }

        private void loadSource(string path)
        {
            SourceFile loadSource = new SourceFile();

            bool current = loadSource.LoadSerialized(ref p1List, ref p2List, ref lastLinesAdded, path);
            if (current)
            {
                loadSource.SetParameters(ref scriptName, ref scriptType, ref scanSpeed, ref drawScale, ref gridSize);
                resizeWindow(gridSize);
            }
            else
            {
                gridSize = 512;
                RenameInputBox();
                resizeWindow(gridSize);
            }

            // set statusbar
            updateLineCount();
            this.drawingStatusText.Text = scriptName + " loaded successfully.";

            // set buttons
            this.undoToolButton.Enabled = true;

            // refresh panel
            this.drawingPanel.Refresh();
        }

        private void saveSource(string path)
        {
            // call source save
            SourceFile saveSource = new SourceFile(scriptName, scriptType, scanSpeed, drawScale, gridSize);
            saveSource.SaveSerialized(ref p1List, ref p2List, ref lastLinesAdded, path, ref backupFilename);
            this.Text = scriptName;

            this.drawingStatusText.Text = "Source saved successfully.";
        }

        private void drawLine(Color color, int width, Point p1, Point p2)
        {
            using (var p = new Pen(color, width))
            {
                Graphics g = this.drawingPanel.CreateGraphics();
                g.DrawLine(p, p1, p2);
                g.Dispose();
            }
        }

        private void drawPoint(Color color, int width, Point p1)
        {
            using (var p = new Pen(color, width))
            {
                Graphics g = this.drawingPanel.CreateGraphics();
                g.DrawRectangle(p, p1.X, p1.Y, 1, 1);
                g.Dispose();
            }
        }

        private Point snapCheck(MouseEventArgs e, List<Point> list1, List<Point> list2, int snapDistance)
        {
            Point snappedPoint = new Point(0, 0);
            for (int x = e.X - snapDistance; x < e.X + snapDistance; x++)
            {
                snappedPoint = snapCheckY(e, list1, list2, x, snapDistance);
                if (snappedPoint != new Point(0, 0))
                {
                    break;
                }
            }
            return snappedPoint;
        }

        private Point snapCheckY(MouseEventArgs e, List<Point> list1, List<Point> list2, int x, int snapDistance)
        {
            for (int y = e.Y - snapDistance; y <= e.Y + snapDistance; y++)
            {
                // see if it finds any Points
                Point testPoint = new Point(x, y);
                //Console.WriteLine("Test Point: " + testPoint.ToString());
                Point snapPoint1 = new Point(0, 0);
                Point snapPoint2 = new Point(0, 0);
                bool foundPoint = false;

                // parallelized snap code :D
                Parallel.ForEach(list1, (Point, state) =>
                {
                    if (Point.Equals(testPoint))
                    {
                        snapPoint1 = Point;
                        foundPoint = true;
                        state.Break(); // break out of parallel loop
                    }
                });
                if (foundPoint == false)
                {
                    Parallel.ForEach(list2, (Point, state) => // check second list
                    {
                        if (Point.Equals(testPoint))
                        {
                            snapPoint2 = Point;
                            foundPoint = true;
                            state.Break();
                        }
                    });
                }

                //Console.WriteLine("Snap Point 1: " + snapPoint1.ToString());
                //Console.WriteLine("Snap Point 2: " + snapPoint2.ToString());

                // check if both found
                if (snapPoint1 == testPoint)
                {
                    return snapPoint1;
                }
                else if (snapPoint2 == testPoint)
                {
                    return snapPoint2;
                }
            }
            return new Point(0, 0);
        }

        private Point gridSnap(MouseEventArgs e)
        {
            int gridlineSpacing = 16;
            int gridlineNumber = gridSize / gridlineSpacing + 1;

            // make list of possible points
            List<Point> gridPointList = new List<Point>();
            for (int x = 0; x < gridlineNumber; x++)
            {
                for (int y = 0; y < gridlineNumber; y++)
                {
                    gridPointList.Add(new Point(16 * x, 16 * y));
                }
            }

            Point gridSnapPoint = new Point(0, 0);

            gridSnapPoint = snapCheck(e, gridPointList, gridPointList, gridlineSpacing / 2);

            return gridSnapPoint;
        }

        private void resizeWindow(int grid)
        {
            // panel 13 from edges (26 total), 16 additional horizontal; 25 toolstrip + 32 vertical extra + 22 status strip + 2 for panel borders
            this.Size = new Size(grid + 26 + 16 + 2, grid + 26 + 25 + 32 + 22 + 2);
            this.drawingPanel.Size = new Size(grid + 2, grid + 2); // +2 on each side due to 1 pixel borders around panel
            this.gridSizeSelector.Text = grid.ToString();
        }

        private List<Point> resizePoints(List<Point> pointList, int oldSize, int newSize)
        {
            double scalingFactor = (double)newSize / (double)oldSize;
            List<Point> newList = new List<Point>();

            foreach (Point point in pointList)
            {
                int newX = (int)(point.X * scalingFactor);
                int newY = (int)(point.Y * scalingFactor);
                newList.Add(new Point(newX, newY));
            }

            return newList;
        }

        private void fixPointBoundaries()
        {
            //bool wasPrevious = true; // just set true for first point, as it will always be in bounds
            // create temporary lists
            List<Point> txList = new List<Point>();
            List<Point> tyList = new List<Point>();
            List<int> tempLastAdded = new List<int>();
            Point intercept;

            int countOps = lastLinesAdded.Count();
            int counter1 = 0;
            for (int i = 0; i < countOps; i++) // go through undo operations
            {
                int ops = lastLinesAdded[i];
                int counter2 = counter1; // as we are counting up with counter1, don't want to cause exception

                // flow is a bit confusing, since we want to maintain undo operations
                // go through p1List in increments of lastLinesAdded, so undo operations can be fixed
                for (int j = counter2; j < counter2 + lastLinesAdded[i]; j++)
                {
                    // see flow chart (lab diary entry 05/03/2014)
                    if (inBounds(p1List[j]))
                    {
                        // P1 YES
                        if (inBounds(p2List[j]))
                        {
                            // P1 YES P2 YES
                            txList.Add(p1List[j]);
                            tyList.Add(p2List[j]);
                        }
                        else
                        {
                            // P1 YES P2 NO
                            intercept = calcBoundaryIntercept(p1List[j], p2List[j]);
                            txList.Add(p1List[j]);
                            tyList.Add(intercept);
                        }
                    }
                    else
                    {
                        // P1 NO
                        if (inBounds(p2List[j]))
                        {
                            // P1 NO P2 YES
                            intercept = calcBoundaryIntercept(p2List[j], p1List[j]);
                            txList.Add(intercept);
                            tyList.Add(p2List[j]);
                        }
                        else
                        {
                            // P1 NO P2 NO
                            // check to see if line crosses through panel
                            if (inBounds(calcBoundaryIntercept(p1List[j], p2List[j])))
                            {
                                Point intercept1 = calcBoundaryIntercept(p1List[j], p2List[j]);
                                Point intercept2 = calcBoundaryIntercept(p1List[j], p2List[j], true);
                                txList.Add(intercept1);
                                tyList.Add(intercept2);
                            }
                            else // remove one from lastLinesAdded if not
                            {
                                ops--;
                            }
                        }
                    }
                    counter1++;
                }
                tempLastAdded.Add(ops);
            }
            // now copy over temp
            p1List = txList;
            p2List = tyList;
            lastLinesAdded = tempLastAdded;

            updateLineCount();
            this.drawingPanel.Refresh();
        }

        private bool inBounds(Point point)
        {
            // checks to see if point is in grid boundaries (0,0) -> (grid, grid)
            // returns true if in bounds
            if (point.X > gridSize || point.X < 0)
            {
                return false;
            }
            else if (point.Y > gridSize || point.Y < 0)
            {
                return false;
            }

            return true;
        }

        private Point calcBoundaryIntercept(Point point1, Point point2, bool findSecondIntercept = false)
        {
            // point1 IN bounds, point 2 OUT of bounds <- needs rework?
            Point intercept = new Point(-1, -1);
            Point intercept1, intercept2, intercept3, intercept4;
            bool interceptFound = false;

            intercept1 = intersectLinesPoint(point1, point2, new Point(0, 0), new Point(gridSize, 0));
            if (inBounds(intercept1) && !interceptFound)
            {
                if (!findSecondIntercept) interceptFound = true;
                findSecondIntercept = false;
                intercept = intercept1;
            }

            intercept2 = intersectLinesPoint(point1, point2, new Point(gridSize, 0), new Point(gridSize, gridSize));
            if (inBounds(intercept2) && !interceptFound)
            {
                if (!findSecondIntercept) interceptFound = true;
                findSecondIntercept = false;
                intercept = intercept2;
            }

            intercept3 = intersectLinesPoint(point1, point2, new Point(gridSize, gridSize), new Point(0, gridSize));
            if (inBounds(intercept3) && !interceptFound)
            {
                if (!findSecondIntercept) interceptFound = true;
                findSecondIntercept = false;
                intercept = intercept3;
            }

            intercept4 = intersectLinesPoint(point1, point2, new Point(0, gridSize), new Point(0, 0));
            if (inBounds(intercept4) && !interceptFound)
            {
                if (!findSecondIntercept) interceptFound = true;
                findSecondIntercept = false;
                intercept = intercept4;
            }

            return intercept; // return invalid point if no intercept found
        }

        private Point intersectLinesPoint(Point A, Point B, Point C, Point D)
        {
            Point intercept;

            // from http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=geometry2
            // Use Ax+By=C equation of a line format

            double A1 = B.Y - A.Y;
            double B1 = A.X - B.X;
            double C1 = A1 * A.X + B1 * A.Y;
            double A2 = D.Y - C.Y;
            double B2 = C.X - D.X;
            double C2 = A2 * C.X + B2 * C.Y;

            double det = A1 * B2 - A2 * B1;

            if (det == 0) // parallel lines
            {
                return intercept = new Point(-1, -1); // return invalid point
            }
            else
            {
                var x = (B2 * C1 - B1 * C2) / det;
                var y = (A1 * C2 - A2 * C1) / det;

                // check to see if x/y are within the two points
                if (x < Math.Min(A.X, B.X) || x > Math.Max(A.X, B.X) || x < Math.Min(C.X, D.X) || x > Math.Max(C.X, D.X)) return new Point(-1, -1);
                if (y < Math.Min(A.Y, B.Y) || y > Math.Max(A.Y, B.Y) || y < Math.Min(C.Y, D.Y) || y > Math.Max(C.Y, D.Y)) return new Point(-1, -1);

                intercept = new Point((int)x, (int)y);
            }

            return intercept;
        }

        private void openScriptWindow()
        {
            this.scriptWin = new ScriptWindow();
            this.scriptWin.MdiParent = this.ParentForm;
            this.scriptWin.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 3, this.scriptWin.Size.Height);
            this.scriptWin.Dock = DockStyle.Right;
            this.scriptWin.Show();
        }

        public static DialogResult InputBox(string promptText, string title, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public void RenameInputBox()
        {
            string input = scriptName;
            if (InputBox("Please enter a script name:", "Rename Script", ref input) == DialogResult.OK)
            {
                scriptName = input;
                this.Text = scriptName;
                this.Refresh();
            }
            else
            {
                scriptName = null;
            }
        }

        public void Rename(string name)
        {
            scriptName = name;
        }

        public string GetName()
        {
            return scriptName;
        }

        private string[] readLinesFromFile(string path, int lines)
        {
            string[] stringLines = new string[lines];
            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < lines; i++)
            {
                stringLines[i] = sr.ReadLine();
            }
            return stringLines;
        }

        private void scriptWin_updateDrawingButtonClick(object sender, EventArgs e)
        {
            // DO STUFF
            AfmScript script = new AfmScript(p1List, p2List, scriptWin.TextToMemoryStream(), scriptName, (double)drawScale, (double)scanSpeed, gridSize);
            script.LoadScript(scriptType);
            script.SetLists(ref p1List, ref p2List);
            this.drawingPanel.Refresh();
        }

        // TOOLSTRIP CONTROLS
        private void NameToolButton_Click(object sender, EventArgs e)
        {
            RenameInputBox();
        }

        private void lineToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "line";
            this.toolStripToolButton.Text = "Line";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.line;
            this.verticesSelectorLabel.Visible = false;
            this.verticesSelector.Visible = false;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void letterToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "letter";
            this.toolStripToolButton.Text = "Letter";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.boundaries;
            this.verticesSelectorLabel.Text = "Letter:";
            this.verticesSelectorLabel.Visible = true;
            this.verticesSelector.Visible = false;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void triangleToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "triangle";
            this.toolStripToolButton.Text = "Triangle";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.triangle;
            this.verticesSelectorLabel.Visible = false;
            this.verticesSelector.Visible = false;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void squareToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "square";
            this.toolStripToolButton.Text = "Square";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.square;
            this.verticesSelectorLabel.Visible = false;
            this.verticesSelector.Visible = false;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void circleToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "circle";
            this.toolStripToolButton.Text = "Circle";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.circle;
            this.verticesSelectorLabel.Visible = false;
            this.verticesSelector.Visible = false;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void polygonToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "polygon";
            this.toolStripToolButton.Text = "Polygon";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.polygon;
            this.verticesSelectorLabel.Text = "Vertices:";
            this.verticesSelectorLabel.Visible = true;
            this.verticesSelector.Visible = true;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void starToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "star";
            this.toolStripToolButton.Text = "Star";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.star;
            this.verticesSelectorLabel.Text = "Vertices:";
            this.verticesSelectorLabel.Visible = true;
            this.verticesSelector.Visible = true;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void arrayToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "array";
            this.toolStripToolButton.Text = "Array";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.array;
            this.verticesSelectorLabel.Text = "Array Size:";
            this.verticesSelectorLabel.Visible = true;
            this.verticesSelector.Visible = true;
            this.spiralNToolLabel.Visible = false;
            this.spiralNSelector.Visible = false;
        }

        private void spiralToolButton_Click(object sender, EventArgs e)
        {
            shapeType = "spiral";
            this.toolStripToolButton.Text = "Spiral";
            this.toolStripToolButton.Image = AFMdraw.Properties.Resources.spiral;
            this.verticesSelectorLabel.Text = "a:";
            this.verticesSelectorLabel.Visible = true;
            this.verticesSelector.Visible = true;
            this.spiralNToolLabel.Visible = true;
            this.spiralNSelector.Visible = true;
        }

        private void verticesSelector_Leave(object sender, EventArgs e)
        {
            int value = (int)Convert.ToDecimal(this.verticesSelector.Text); // to avoid exception
            int min = 5;
            int max = 20;

            if (this.shapeType == "polygon" || this.shapeType == "star")
            {
                min = 5;
                max = 20;
            }
            else if (this.shapeType == "array")
            {
                min = 3;
                max = 20;
            }
            else if (this.shapeType == "spiral")
            {
                min = 1;
                max = 50;
            }

            // some data validation
            if (value < min)
            {
                this.verticesSelector.Text = min.ToString();
            }
            else if (value > max)
            {
                this.verticesSelector.Text = max.ToString();
            }
        }

        private void spiralNSelector_Leave(object sender, EventArgs e)
        {
            decimal value = Convert.ToDecimal(this.spiralNSelector.Text); // to avoid exception
            decimal min = -3;
            decimal max = 3;

            // some data validation
            if (value < min)
            {
                this.spiralNSelector.Text = min.ToString();
            }
            else if (value > max)
            {
                this.spiralNSelector.Text = max.ToString();
            }
        }

        private void gridSizeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newGridSize = Convert.ToInt32(this.gridSizeSelector.Text);

            // check if any change first
            if (newGridSize == gridSize)
            {
                return;
            }

            resizeWindow(newGridSize);
            p1List = resizePoints(p1List, gridSize, newGridSize);
            p2List = resizePoints(p2List, gridSize, newGridSize);
            gridSize = newGridSize;
        }

        private void gridSizeSelector_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void browseImageToolButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select background image",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Image Files|*.bmp;*.gif; *.jpg; *.jpeg; *.png",
                RestoreDirectory = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // add background image
                this.drawingPanel.BackgroundImage = Image.FromFile(dialog.FileName);
                this.clearImageToolButton.Enabled = true;
            }
        }

        private void clearImageToolButton_Click(object sender, EventArgs e)
        {
            this.drawingPanel.BackgroundImage = null;
            this.clearImageToolButton.Enabled = false;
        }

        private void undoToolButton_Click(object sender, EventArgs e)
        {
            // clear button click
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);

            // clear backup file name
            backupFilename = "";

            // how many operations have been done
            int countOps = lastLinesAdded.Count();

            int count = p1List.Count(); // get length of point list
            if (countOps > 0)
            {
                int lines = lastLinesAdded[countOps - 1]; // get number of lines from last line of

                for (int i = 0; i < lines; i++)
                {
                    p1List.RemoveAt(count - 1 - i); // hopefully this will remove all the added lines...
                    p2List.RemoveAt(count - 1 - i);
                }
                this.drawingPanel.Refresh();

                // reset button if no more lines
                if (countOps == 1)
                {
                    this.undoToolButton.Enabled = false;
                }

                // delete last line of operation counter
                lastLinesAdded.RemoveAt(countOps - 1);
            }
            else
            {
                // open error message (not needed, button not clickable)
            }
            setUnsaved();

            this.drawingStatusText.Text = "Undo successful.";
            updateLineCount();
            undoToolButton_MouseEnter(sender, e); //highlight next line, as mouse still over button
        }

        private void undoToolButton_MouseEnter(object sender, EventArgs e)
        {
            int countOps = lastLinesAdded.Count();
            int count = p1List.Count();

            // highlight last operation to red
            if (countOps > 0)
            {
                int lines = lastLinesAdded[countOps - 1];
                for (int i = 0; i < lines; i++)
                {
                    if (p1List[count - 1 - i] == p2List[count - 1 - i])
                    {
                        drawPoint(Color.Red, penWidth, p1List[count - 1 - i]);
                    }
                    else
                    {
                        drawLine(Color.Red, penWidth, p1List[count - 1 - i], p2List[count - 1 - i]);
                    }
                }
            }
        }

        private void undoToolButton_MouseLeave(object sender, EventArgs e)
        {
            int countOps = lastLinesAdded.Count();
            int count = p1List.Count();

            // reset last operation back to black
            if (countOps > 0)
            {
                int lines = lastLinesAdded[countOps - 1];
                for (int i = 0; i < lines; i++)
                {
                    drawLine(Color.Black, penWidth, p1List[count - 1 - i], p2List[count - 1 - i]);
                }
            }
        }

        private void clearToolButton_Click(object sender, EventArgs e)
        {
            int count = p1List.Count();

            if (count == 0) // don't do anything on empty drawing
            {
                return;
            }
            else // open prompt to ask if user wants to clear or not
            {
                var window = MessageBox.Show(
                    "Are you sure you wish to clear all data?\nAll unsaved progress will be lost.",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (window == DialogResult.No) // want to save progress
                {
                    return;
                }
            }
            // clear points lists, then disable buttons and refresh
            p1List.Clear();
            p2List.Clear();
            lastLinesAdded.Clear();
            setUnsaved();

            this.drawingStatusText.Text = "All lines cleared.";

            updateLineCount();
            this.undoToolButton.Enabled = false;
            //this.saveButton.Enabled = false;
            //this.saveSourceButton.Enabled = false;

            this.drawingPanel.Refresh();
        }

        private void gridToggleToolButton_Click(object sender, EventArgs e)
        {
            gridlines = !gridlines;
            if (this.snapToGridToolButton.Checked && this.gridToggleToolButton.Checked)
            {
                snapToGridToolButton_Click(sender, e);
            }

            this.gridToggleToolButton.Checked = !this.gridToggleToolButton.Checked;

            this.drawingPanel.Refresh();
        }

        private void snapToGridToolButton_Click(object sender, EventArgs e)
        {
            this.snapToGridToolButton.Checked = !this.snapToGridToolButton.Checked;
            isGridSnap = !isGridSnap;

            // change toolstrip image
            if (this.snapToGridToolButton.Checked)
            {
                this.snapToGridToolButton.Image = AFMdraw.Properties.Resources.snapGrid;

                gridlines = true;
                this.gridToggleToolButton.Checked = true;
            }
            else
            {
                this.snapToGridToolButton.Image = AFMdraw.Properties.Resources.noSnapGrid;
            }

            this.drawingPanel.Refresh();
        }

        public void LoadSourceToolButton_Click(object sender, EventArgs e)
        {
            // check to see if list is empty, if not ask warning
            int count = p1List.Count();
            if (count != 0)
            {
                var window = MessageBox.Show(
                    "Drawing not empty. Are you sure you wish to discard progress and load source file?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (window == DialogResult.No)
                {
                    return;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select source file",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Text file | *.txt",
                RestoreDirectory = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // check to make sure its a source file (only by filename!)
                if (dialog.FileName.Contains("-source.txt"))
                {
                    backupFilename = dialog.FileName;
                    loadSource(backupFilename);
                }
                else
                {
                    MessageBox.Show("Incompatible file type.\nMake sure to select a valid source file.", "Error");
                }
            }
            dialog.Dispose();
            this.Refresh();
        }

        public void LoadScriptToolButton_Click(object sender, EventArgs e)
        {
            int count = p1List.Count();
            if (count != 0)
            {
                var window = MessageBox.Show(
                    "Drawing not empty. Are you sure you wish to discard progress and load script file?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (window == DialogResult.No)
                {
                    return;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select script file",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Script file | *.txt; *.cpp",
                RestoreDirectory = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // check the script type, or discard if not a valid script type
                int noLinesToCheck = 5; // number of lines to check for correct "fingerprint"
                string[] linesToCheck = readLinesFromFile(dialog.FileName, noLinesToCheck);
                string fileType = "none"; // default to non-script

                foreach (string line in linesToCheck)
                {
                    if (line == "//----------------------- Lithography Commands ------------------------------")
                    {
                        fileType = "witec";
                        break;
                    }
                    if (line == "#include <litho.h>")
                    {
                        fileType = "bruker";
                        break;
                    }
                }

                // now either call loadScript or give error message
                if (fileType != "none")
                {
                    scriptType = fileType;
                    AfmScript script = new AfmScript(p1List, p2List, dialog.FileName, scriptName, (double)drawScale, (double)scanSpeed, gridSize);
                    script.LoadScript(fileType);
                    script.SetLists(ref p1List, ref p2List);
                }
                else
                {
                    var window = MessageBox.Show(
                        "No valid script type detected.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // enable undo, but only 1 line at a time
            for (int i = 0; i < p1List.Count(); i++) { lastLinesAdded.Add(1); }

            updateLineCount();
            this.drawingPanel.Refresh();
            dialog.Dispose();
        }

        public void saveSourceToolButton_Click(object sender, EventArgs e)
        {
            fixPointBoundaries();

            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Select source file save location",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Text file | *.txt",
                RestoreDirectory = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // save serialized file
                saveSource(dialog.FileName);
            }
            dialog.Dispose();
        }


        private void saveScriptToolButton_Click(object sender, EventArgs e)
        {
            if (lastLinesAdded.Count > 0) // to avoid deleting compatibility scripts
            {
                fixPointBoundaries();
            }

            SaveScriptDialog saveDialog = new SaveScriptDialog(p1List, p2List, scriptName, scriptType, scanSpeed, drawScale, gridSize);
            saveDialog.ShowDialog();
            bool isSaved = saveDialog.IsSaved();

            string backupPath = saveDialog.GetBackupPath();
            if (isSaved)
            {
                saveSource(saveDialog.GetBackupPath());
            }

            saveDialog.SetParameters(ref scriptType, ref scanSpeed, ref drawScale);
            saveDialog.Dispose();

            updateScriptWindow(); // in case parameters were changed

            this.drawingStatusText.Text = "Script saved successfully.";
        }

        private void fixBoundariesToolButton_Click(object sender, EventArgs e)
        {
            fixPointBoundaries();

            setUnsaved();

            this.drawingStatusText.Text = "Boundaries trimmed.";
        }

        // OVERRIDES
        protected override void WndProc(ref Message m)
        {
            const Int32 WM_NCLBUTTONCLK = 161;
            const Int32 WM_NCLBUTTONDBLCLK = 163;
            if (m.Msg == WM_NCLBUTTONCLK)
            {
                titleBarClicked(); // Implement this function and do what you need to do in this function
            }
            if (m.Msg == WM_NCLBUTTONDBLCLK) // double click
            {
                titleBarClicked();
            }
            base.WndProc(ref m);
        }
    }

    public partial class MyPanel : Panel
    {
        public MyPanel()
        {
            // this fixes flickering by using double buffering (DONT use g.Dispose() in the OnPaint!)
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }

    public partial class MySR : ToolStripSystemRenderer
    {
        public MySR()
        {
        }

        // fix white line bug
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //base.OnRenderToolStripBorder(e);
        }
    }

    public class NumericalToolStripTextBox : ToolStripTextBox
    {
        private readonly NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
        private char[] signs;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            signs = new[]
			{
				//char.Parse(numberFormatInfo.NumberDecimalSeparator),
				//char.Parse(numberFormatInfo.NumberGroupSeparator),
				char.Parse(numberFormatInfo.NegativeSign)
			};
            // check for signs
            bool exists = signs.Any(ch => e.KeyChar == ch);

            if (exists || ((char.IsNumber(e.KeyChar))) || (((int)e.KeyChar) == 8) || (((int)e.KeyChar) == 46))
            {
                return;
            }
            e.Handled = true;
        }
    }
}
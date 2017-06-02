using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AFMdraw
{
    public class AfmScript
    {
        private Assembly assembly = Assembly.GetExecutingAssembly();
        private List<Point> p1List = new List<Point>();
        private List<Point> p2List = new List<Point>();
        private string savePath;
        private string scriptName;
        private double drawScale;
        private double scanSpeed;
        private int gridSize;
        private int drawSpeedFast = 20; // for "jump" in bruker
        private int fudgeFactor = 1; // only needed for users who don't use shift, disabled for now
        private double scaleFactor;
        private bool isInternalStream = false; // are we loading a file, or an internal memorystream
        private MemoryStream memStream = new MemoryStream();

        public AfmScript(List<Point> list1, List<Point> list2, string path, string name, double scale, double speed, int grid)
        {
            p1List = list1;
            p2List = list2;
            savePath = path;
            scriptName = name;
            drawScale = scale;
            gridSize = grid;
            scaleFactor = drawScale / gridSize; // 512 pixel grid by default
            scanSpeed = speed;
            //drawSpeedFast = 2 * drawSpeed; // arbitrary doubling
        }

        public AfmScript(List<Point> list1, List<Point> list2, MemoryStream ms, string name, double scale, double speed, int grid)
        {
            p1List = list1;
            p2List = list2;
            memStream = ms;
            scriptName = name;
            drawScale = scale;
            gridSize = grid;
            scaleFactor = drawScale / gridSize; // 512 pixel grid by default
            scanSpeed = speed;
            isInternalStream = true;
        }

        public void SaveScript(string type, bool toFile = true)
        {
            TextInfo p = new CultureInfo("en-US", false).TextInfo;
            string methodName = "save" + p.ToTitleCase(type.ToLower()); // make sure type is in lower case, then capitalize first letter, and combine with prefix "save"

            // now call appropriate method based on script type
            MethodInfo mi = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance); // last bit allows calling private methods
            mi.Invoke(this, null);
            if (toFile)
            {
                saveMemoryStreamToFile(savePath, memStream);
            }
        }

        public void LoadScript(string type)
        {
            //TextInfo p = new CultureInfo("en-US", false).TextInfo;
            //string methodName = "load" + p.ToTitleCase(type.ToLower()); // make sure type is in lower case, then capitalize first letter, and combine with prefix "save"

            //// now call appropriate method based on script type
            //MethodInfo mi = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance); // last bit allows calling private methods
            //mi.Invoke(this, null);

            if (type.ToLower() == "bruker") { loadBruker(); }
            else if (type.ToLower() == "witec") { loadWitec(); }
        }

        private void saveBruker()
        {
            string type = "bruker";
            string padding = "        ";
            string drawSpeedPlaceholder = "ws";

            // make sure save file is cpp
            savePath = fixExtension(savePath, ".cpp");

            // lets convert our lines into time string
            TimeSpan t = TimeSpan.FromSeconds(calculateEstimatedRunTime());
            string estimatedTime = string.Format("({0:D2}h:{1:D2}m:{2:D2}s)", t.Hours, t.Minutes, t.Seconds);

            appendToStream(memStream, "// " + Path.GetFileName(savePath));
            appendToStream(memStream, "// " + scriptName + " " + estimatedTime);
            appendHeader(memStream, type);
            appendToStream(memStream, padding + String.Format("double ds = {0};  // set draw scale in microns", drawScale.ToString("0.00"))); // set draw scale!
            appendToStream(memStream, "");


            // LithoTranslate is the same as MR for witec! http://www.nanophys.kth.se/nanophys/facilities/nfl/afm/fast-scan/bruker-help/Content/Nanomanipulation/NanoScript%20Macros.htm
            
            // now move to start position (Point 1), and go to first end position
            double startPointX = p1List[0].X * scaleFactor - (drawScale / 2); // shift so origin is in center of panel
            double startPointY = p1List[0].Y * scaleFactor - (drawScale / 2);
            double endPointX = p2List[0].X * scaleFactor - (drawScale / 2);
            double endPointY = p2List[0].Y * scaleFactor - (drawScale / 2);

            // change to 
            startPointX = startPointX / drawScale; 
            startPointY = startPointY / drawScale;
            endPointX = endPointX / drawScale;
            endPointY = endPointY / drawScale;

            // find deltas
            double deltaX = endPointX - startPointX;
            double deltaY = endPointY - startPointY;

            appendToStream(memStream, padding + String.Format("LithoTranslate({0},{1},{2}); // start position", startPointX.ToString("0.0000") + " * ds", startPointY.ToString("0.0000") + " * ds", "js * 1")); // *1 allows program to correctly find jump points
            appendToStream(memStream, padding + String.Format("LithoSet({0},{1});", "lsSetpoint", "wv"));
            appendToStream(memStream, padding + String.Format("LithoPause(1);"));
            appendToStream(memStream, padding + String.Format("LithoTranslate({0},{1},{2});", deltaX.ToString("0.0000") + " * ds", deltaY.ToString("0.0000") + " * ds", drawSpeedPlaceholder));

            // go through all the Points remaining now, from list 2, while checking for any jumps
            for (int x = 1; x < p2List.Count; x++)
            {
                // calc start/end points and deltaX/Y
                startPointX = p1List[x].X * scaleFactor - (drawScale / 2); // shift so origin is in center of panel
                startPointY = p1List[x].Y * scaleFactor - (drawScale / 2);
                endPointX = p2List[x].X * scaleFactor - (drawScale / 2);
                endPointY = p2List[x].Y * scaleFactor - (drawScale / 2);

                startPointX = startPointX / drawScale;
                startPointY = startPointY / drawScale;
                endPointX = endPointX / drawScale;
                endPointY = endPointY / drawScale;

                // check for proximity of start/end of consecutive Points
                var deltaConsecX = p1List[x].X - p2List[x - 1].X;
                var deltaConsecY = p1List[x].Y - p2List[x - 1].Y;
                if (Math.Abs(deltaConsecX) >= fudgeFactor || Math.Abs(deltaConsecY) >= fudgeFactor)
                {
                    //startPointX = p1List[x].X * scaleFactor - (drawScale / 2);
                    //startPointY = p1List[x].Y * scaleFactor - (drawScale / 2);
                    var prevEndPointX = p2List[x - 1].X * scaleFactor - (drawScale / 2);
                    var prevEndPointY = p2List[x - 1].Y * scaleFactor - (drawScale / 2);

                    prevEndPointX = prevEndPointX / drawScale;
                    prevEndPointY = prevEndPointY / drawScale;

                    deltaX = startPointX - prevEndPointX;
                    deltaY = startPointY - prevEndPointY;

                    appendToStream(memStream, ""); // new line
                    appendToStream(memStream, padding + String.Format("LithoSet({0},{1});", "lsSetpoint", "jv"));
                    appendToStream(memStream, padding + String.Format("LithoPause(1);"));
                    appendToStream(memStream, padding + String.Format("LithoTranslate({0},{1},{2}); // jump", deltaX.ToString("0.0000") + " * ds", deltaY.ToString("0.0000") + " * ds", "js * 1"));
                    appendToStream(memStream, padding + String.Format("LithoSet({0},{1});", "lsSetpoint", "wv"));
                    appendToStream(memStream, padding + String.Format("LithoPause(1);"));
                }

                // check for point (from grid array)
                // FOR AFM WE IGNORE DOTS!
                //if (p1List[x] == p2List[x])
                //{
                //    appendToStream(memStream, padding + "LithoPause(10);  // dot");
                //    appendToStream(memStream, "");
                //    continue; // break off this iteration of the loop
                //}

                // continue as normal
                //endPointX = p2List[x].X * scaleFactor - (drawScale / 2);
                //endPointY = p2List[x].Y * scaleFactor - (drawScale / 2);
                deltaX = endPointX - startPointX;
                deltaY = endPointY - startPointY;
                appendToStream(memStream, padding + String.Format("LithoTranslate({0},{1},{2});", deltaX.ToString("0.0000") + " * ds", deltaY.ToString("0.0000") + " * ds", drawSpeedPlaceholder));
            }

            appendToStream(memStream, ""); // new line
            appendToStream(memStream, padding + String.Format("LithoSet({0},{1});", "lsSetpoint", "jv"));
            appendToStream(memStream, padding + String.Format("LithoPause(1);"));
            appendFooter(memStream, type);
            //saveMemoryStreamToFile(savePath, memStream); // lets save memorystream to file
        }

        private void saveWitec()
        {
            string type = "witec";

            // make sure save file is cpp
            savePath = fixExtension(savePath, ".txt");

            appendHeader(memStream, type);

            // lets convert our lines into time string
            TimeSpan t = TimeSpan.FromSeconds(calculateEstimatedRunTime());
            string estimatedTime = string.Format("({0:D2}h:{1:D2}m:{2:D2}s)", t.Hours, t.Minutes, t.Seconds);
            string scriptNameNew = scriptName + " " + estimatedTime;

            // want to write name and other separator
            string nameLine = @"//" + padBoth(" " + scriptNameNew + " ", 75);
            string separator = @"//---------------------------------------------------------------------------"; // 75 dashes
            appendToStream(memStream, nameLine);
            appendToStream(memStream, separator);

            // microscope up, and set scan speed
            appendToStream(memStream, @"MUMR 10  // Microscope Up");
            appendToStream(memStream, "SS " + scanSpeed.ToString());
            appendToStream(memStream, "");

            // now move to start position (Point 1), and go to first end position
            double startPointX = p1List[0].X * scaleFactor - (drawScale / 2); // shift so origin is in center of panel
            double startPointY = p1List[0].Y * scaleFactor - (drawScale / 2);
            double endPointX = p2List[0].X * scaleFactor - (drawScale / 2);
            double endPointY = p2List[0].Y * scaleFactor - (drawScale / 2);

            appendToStream(memStream, "JA " + startPointX.ToString("0.0") + ", " + startPointY.ToString("0.0") + @"  // Move to Start Position");
            appendToStream(memStream, @"MUMR -10  // Microscope Down");
            appendToStream(memStream, "MA " + endPointX.ToString("0.0") + ", " + endPointY.ToString("0.0"));

            // go through all the Points remaining now, from list 2, while checking for any jumps
            for (int x = 1; x < p2List.Count; x++)
            {
                // check for proximity of start/end of consecutive Points
                var deltaX = Math.Abs(p1List[x].X - p2List[x - 1].X);
                var deltaY = Math.Abs(p1List[x].Y - p2List[x - 1].Y);

                if (deltaX >= fudgeFactor || deltaY >= fudgeFactor) // now check how far apart points are, and jump if necessary
                {
                    startPointX = p1List[x].X * scaleFactor - (drawScale / 2);
                    startPointY = p1List[x].Y * scaleFactor - (drawScale / 2);
                    appendToStream(memStream, "");
                    appendToStream(memStream, @"MUMR 10  // Microscope Up");
                    appendToStream(memStream, "JA " + startPointX.ToString("0.0") + ", " + startPointY.ToString("0.0"));
                    appendToStream(memStream, @"MUMR -10  // Microscope Down");
                }

                // check for point (from grid array)
                if (p1List[x] == p2List[x])
                {
                    appendToStream(memStream, @"// Dot");
                    var pointX = p1List[x].X * scaleFactor - (drawScale / 2);
                    var pointY = p1List[x].Y * scaleFactor - (drawScale / 2);

                    for (int i = 0; i < 5; i++) // do 5 times
                    {
                        // make 0.1x0.1 micron square (should be close enough to a dot)
                        appendToStream(memStream, "MA " + (pointX + 0.1).ToString("0.0") + ", " + pointY.ToString("0.0"));
                        appendToStream(memStream, "MA " + (pointX + 0.1).ToString("0.0") + ", " + (pointY + 0.1).ToString("0.0"));
                        appendToStream(memStream, "MA " + pointX.ToString("0.0") + ", " + (pointY + 0.1).ToString("0.0"));
                        appendToStream(memStream, "MA " + pointX.ToString("0.0") + ", " + pointY.ToString("0.0"));
                    }

                    continue; // break off this iteration of the loop
                }

                // for neither point or close point, continue as normal
                endPointX = p2List[x].X * scaleFactor - (drawScale / 2);
                endPointY = p2List[x].Y * scaleFactor - (drawScale / 2);
                appendToStream(memStream, "MA " + endPointX.ToString("0.0") + ", " + endPointY.ToString("0.0"));
            }

            // move microscope up, and back to origin
            appendToStream(memStream, "");
            appendToStream(memStream, @"MUMR 10  // Microscope Up");
            appendToStream(memStream, @"JA 0, 0  // Back to Origin");
            appendToStream(memStream, "");

            // save script
            //saveMemoryStreamToFile(savePath, memStream);
        }

        private void loadBruker()
        {
            memStream = loadFileToMemoryStream(savePath);
            string[] lines = readAllLinesFromStream(memStream);

            if (!isInternalStream) // if not internal, need to load file
            {
                memStream = loadFileToMemoryStream(savePath);
            }
            string[] scriptLines = readAllLinesFromStream(memStream);
            List<string> linesList = new List<string>(scriptLines);
            List<PointF> t1List = new List<PointF>();
            List<PointF> t2List = new List<PointF>();

            // lists to find drawScale
            List<double> px1 = new List<double>();
            List<double> py1 = new List<double>();
            List<double> px2 = new List<double>();
            List<double> py2 = new List<double>();

            bool isAfterFirstPoint = false; // if true, add to p1List, if false add to p2List
            bool isSpeedSet = false; // make sure only first SS sets the speed
            bool firstPoint = true;
            bool isScaleSet = false;

            foreach (string line in linesList)
            {
                string command = line.Trim().Split(' ')[0].Split('(')[0]; // gets rid of leading spaces, and others (see example bruker script)

                if (command == "double")
                {
                    if (line.Contains("ws") && !isSpeedSet)
                    {
                        var numbers = extractDecimals(line);
                        scanSpeed = Convert.ToDouble(numbers[0]);
                        isSpeedSet = true;
                    }
                    else if (line.Contains("ds") && !isScaleSet)
                    {
                        var numbers = extractDecimals(line);
                        drawScale = Convert.ToDouble(numbers[0]);
                        isScaleSet = true;
                    }

                }
                else if (command == "LithoTranslate")
                {
                    if (!isScaleSet) { drawScale = 1; } // default to 1um

                    var coords = extractDecimals(line);
                    float xDiff = (float)coords[0] * (float)drawScale; // xDiff since its MoveRelative
                    float yDiff = (float)coords[1] * (float)drawScale;

                    if (firstPoint)
                    {
                        t1List.Add(new PointF(xDiff, yDiff));

                        px1.Add(xDiff);
                        py1.Add(yDiff);

                        firstPoint = false;
                        isAfterFirstPoint = true;
                    }
                    else
                    {
                        if (isAfterFirstPoint)
                        {
                            float x = xDiff + t1List[t1List.Count() - 1].X;
                            float y = yDiff + t1List[t1List.Count() - 1].Y;

                            t2List.Add(new PointF(x, y));
                            isAfterFirstPoint = false;

                            // set number lists
                            px2.Add(x);
                            py2.Add(y);
                        }
                        else if (coords.Length > 2) // jump
                        {
                            float x = xDiff + t2List[t2List.Count() - 1].X;
                            float y = yDiff + t2List[t2List.Count() - 1].Y;
                            t1List.Add(new PointF(x, y));
                            isAfterFirstPoint = true;

                            px1.Add(x);
                            py1.Add(y);
                        }
                        else
                        {

                            float x = xDiff + t2List[t2List.Count() - 1].X;
                            float y = yDiff + t2List[t2List.Count() - 1].Y;
                            t1List.Add(t2List[t2List.Count() - 1]);

                            px1.Add(t2List[t2List.Count() - 1].X);
                            py1.Add(t2List[t2List.Count() - 1].Y);

                            t2List.Add(new PointF(x, y));
                            isAfterFirstPoint = false;

                            // set number lists
                            px2.Add(x);
                            py2.Add(y);
                        }
                    }
                    //if (firstPoint)
                    //{
                    //    t1List.Add(new PointF(xDiff, yDiff));

                    //    px1.Add(xDiff);
                    //    py1.Add(yDiff);

                    //    firstPoint = false;
                    //    isAfterFirstPoint = true;
                    //    continue;
                    //}
                    //else if (coords.Length > 2) // therefore a jump, since speed is set to a number, and not n
                    //{
                    //    x = xDiff + t2List[t2List.Count() - 1].X;
                    //    y = yDiff + t2List[t2List.Count() - 1].Y;

                    //    t1List.Add(new PointF(x, y));

                    //    px1.Add(x);
                    //    py1.Add(y);
                    //    isAfterFirstPoint = true;

                    //    continue;
                    //}
                    //else if (isAfterFirstPoint)
                    //{
                    //    x = xDiff + t1List[t1List.Count() - 1].X;
                    //    y = yDiff + t1List[t1List.Count() - 1].Y;

                    //    t2List.Add(new PointF(x, y));

                    //    // set number lists
                    //    px2.Add(x);
                    //    py2.Add(y);

                    //    isAfterFirstPoint = false;
                    //}
                    //else
                    //{
                    //    t1List.Add(t2List[t2List.Count() - 1]);

                    //    px1.Add(t2List[t2List.Count() - 1].X);
                    //    py1.Add(t2List[t2List.Count() - 1].Y);

                    //    isAfterFirstPoint = true;
                    //}
                }
                else if (command == "LithoPause" && t1List.Count() > 0)
                {
                    //// dot
                    //t2List.Add(t1List[t1List.Count() - 1]);

                    //px2.Add(t1List[t1List.Count() - 1].X);
                    //py2.Add(t1List[t1List.Count() - 1].Y);
                }
            }

            //make sure lists are the same length, if not trim p1List (will always be longer(double check!))
            int listDiff = t1List.Count() - t2List.Count();
            if (listDiff > 0)
            {
                t1List.RemoveRange(t1List.Count - listDiff, listDiff);
            }
            else if (listDiff < 0)
            {
                t2List.RemoveRange(t2List.Count + listDiff, -listDiff);
            }

            // find largest number, and set scale accordingly (x2) - ONLY IF EXTERNAL FILE
            if (!isInternalStream && !isScaleSet)
            {
                var max1 = px1.Max();
                var max2 = py1.Max();
                var max3 = px2.Max();
                var max4 = py2.Max();

                var min1 = px1.Min();
                var min2 = py1.Min();
                var min3 = px2.Min();
                var min4 = py2.Min();

                var max = Math.Max(max1, max2);
                max = Math.Max(max, max3);
                max = Math.Max(max, max4);

                var min = Math.Min(min1, min2);
                min = Math.Min(min, min3);
                min = Math.Min(min, min4);

                max = Math.Max(max, Math.Abs(min));

                this.drawScale = Math.Round(((max + 1) * 2), 1);
            }

            this.scaleFactor = this.drawScale / this.gridSize;

            // now scale the points according to their size
            t1List = t1List.ConvertAll(new Converter<PointF, PointF>(scalePointsToGrid));
            t2List = t2List.ConvertAll(new Converter<PointF, PointF>(scalePointsToGrid));


            // convert over to Point from PointF
            p1List = t1List.ConvertAll(new Converter<PointF, Point>(PointFToPoint));
            p2List = t2List.ConvertAll(new Converter<PointF, Point>(PointFToPoint));
        }

        private void loadWitec()
        {
            // lets clear the lists
            p1List.Clear();
            p2List.Clear();

            if (!isInternalStream) // if not internal, need to load file
            {
                memStream = loadFileToMemoryStream(savePath);
            }
            string[] scriptLines = readAllLinesFromStream(memStream);
            List<string> linesList = new List<string>(scriptLines);
            List<PointF> t1List = new List<PointF>();
            List<PointF> t2List = new List<PointF>();

            // lists to find drawScale
            List<double> px1 = new List<double>();
            List<double> py1 = new List<double>();
            List<double> px2 = new List<double>();
            List<double> py2 = new List<double>();

            bool isAfterFirstPoint = false; // if true, add to p1List, if false add to p2List
            bool isSpeedSet = false; // make sure only first SS sets the speed
            bool firstPoint = true;

            foreach (string line in linesList)
            {
                string command = line.Split(' ')[0]; // split up each line into command + parameter(s)

                if (command == "SS" && !isSpeedSet) // set scan speed
                {
                    var numbers = extractDecimals(line);
                    scanSpeed = Convert.ToDouble(numbers[0]);
                    isSpeedSet = true;
                }
                else if (command == "JA")
                {
                    if (firstPoint)
                    {
                        firstPoint = false;
                    }
                    var coords = extractDecimals(line);
                    float x = (float)coords[0];
                    float y = (float)coords[1];
                    t1List.Add(new PointF(x, y));
                    isAfterFirstPoint = true; // add p2List next

                    // set number lists
                    px1.Add(x);
                    py1.Add(y);
                }
                else if (command == "MA")
                {
                    var coords = extractDecimals(line);
                    float x = (float)coords[0];
                    float y = (float)coords[1];

                    if (isAfterFirstPoint)
                    {
                        t2List.Add(new PointF(x, y));
                        isAfterFirstPoint = false;

                        // set number lists
                        px2.Add(x);
                        py2.Add(y);
                    }
                    else
                    {
                        if (firstPoint)
                        {
                            t1List.Add(new Point(0, 0));

                            px1.Add(0);
                            py1.Add(0);

                            firstPoint = false;
                        }
                        else
                        {
                            t1List.Add(t2List[t2List.Count() - 1]);

                            px1.Add(t2List[t2List.Count() - 1].X);
                            py1.Add(t2List[t2List.Count() - 1].Y);
                        }

                        t2List.Add(new PointF(x, y));
                        isAfterFirstPoint = false;

                        // set number lists
                        px2.Add(x);
                        py2.Add(y);
                    }
                }
                else if (command == "MR")
                {
                    var coords = extractDecimals(line);
                    float xDiff = (float)coords[0]; // xDiff since its MoveRelative
                    float yDiff = (float)coords[1];

                    if (firstPoint)
                    {
                        t1List.Add(new PointF(0, 0));

                        px1.Add(0);
                        py1.Add(0);

                        firstPoint = false;
                    }
                    else
                    {
                        t1List.Add(t2List[t2List.Count() - 1]);

                        px1.Add(t2List[t2List.Count() - 1].X);
                        py1.Add(t2List[t2List.Count() - 1].Y);
                    }

                    var x = xDiff + t1List[t1List.Count() - 1].X;
                    var y = yDiff + t1List[t1List.Count() - 1].Y;

                    t2List.Add(new PointF(x, y));

                    // set number lists
                    px2.Add(x);
                    py2.Add(y);
                }
            }

            // make sure lists are the same length, if not trim p1List (will always be longer (double check!))
            int listDiff = t1List.Count() - t2List.Count();
            if (listDiff > 0)
            {
                t1List.RemoveRange(t1List.Count - listDiff, listDiff);
            }
            else if (listDiff < 0)
            {
                t2List.RemoveRange(t2List.Count + listDiff, -listDiff);
            }

            // find largest number, and set scale accordingly (x2) - ONLY IF EXTERNAL FILE
            if (!isInternalStream)
            {
                var max1 = px1.Max();
                var max2 = py1.Max();
                var max3 = px2.Max();
                var max4 = py2.Max();

                var min1 = px1.Min();
                var min2 = py1.Min();
                var min3 = px2.Min();
                var min4 = py2.Min();

                var max = Math.Max(max1, max2);
                max = Math.Max(max, max3);
                max = Math.Max(max, max4);

                var min = Math.Min(min1, min2);
                min = Math.Min(min, min3);
                min = Math.Min(min, min4);

                max = Math.Max(max, Math.Abs(min));

                this.drawScale = Math.Round(((max + 1) * 2), 1);
            }
            
            this.scaleFactor = this.drawScale / this.gridSize;

            // now scale the points according to their size
            t1List = t1List.ConvertAll(new Converter<PointF, PointF>(scalePointsToGrid));
            t2List = t2List.ConvertAll(new Converter<PointF, PointF>(scalePointsToGrid));


            // convert over to Point from PointF
            p1List = t1List.ConvertAll(new Converter<PointF, Point>(PointFToPoint));
            p2List = t2List.ConvertAll(new Converter<PointF, Point>(PointFToPoint));
        }

        private void appendHeader(MemoryStream ms, string type)
        {
            var resourceName = "AFMdraw.headers." + type + "Header.txt"; // namespace.resource

            // don't use "using", or the base stream is closed...
            StreamWriter sw = new StreamWriter(ms);
            using (Stream input = assembly.GetManifestResourceStream(resourceName))
            {
                input.CopyTo(ms);
            }
            sw.Flush();
        }

        private void appendFooter(MemoryStream ms, string type)
        {
            var resourceName = "AFMdraw.footers." + type + "Footer.txt"; // namespace.resource

            // don't use "using", or the base stream is closed...
            StreamWriter sw = new StreamWriter(ms);
            using (Stream input = assembly.GetManifestResourceStream(resourceName))
            {
                input.CopyTo(ms);
            }
            sw.Flush();
        }

        private void appendToStream(MemoryStream ms, string text)
        {
            StreamWriter sw = new StreamWriter(ms);
            sw.WriteLine(text);
            sw.Flush();
        }

        private string[] readAllLinesFromStream(MemoryStream ms)
        {
            string[] lines = new string[] { };
            List<string> linesList = new List<string>();

            StreamReader sr = new StreamReader(ms);

            string line;
            //int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                linesList.Add(line);
                //i++;
            }

            lines = linesList.ToArray();

            return lines;
        }

        private void saveMemoryStreamToFile(string path, MemoryStream ms)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fs);
            }
        }

        private MemoryStream loadFileToMemoryStream(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                MemoryStream ms = new MemoryStream();
                ms.SetLength(fs.Length);
                fs.Read(ms.GetBuffer(), 0, (int)fs.Length);
                ms.Position = 0;
                return ms;
            }
        }

        public MemoryStream OutputCurrentMemoryStream(string type)
        {
            SaveScript(type, false);
            return memStream;
        }

        public void SetLists(ref List<Point> list1, ref List<Point> list2)
        {
            list1 = this.p1List;
            list2 = this.p2List;
        }

        private string padBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft, '-').PadRight(length, '-');
        }

        private string fixExtension(string path, string extension)
        {
            string savePath;

            if (path.Contains(extension) == false)
            {
                savePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + extension;
            }
            else
            {
                savePath = path;
            }

            return savePath;
        }

        private decimal[] extractDecimals(string input)
        {
            return Regex.Matches(input, @"[+-]?\d+(\.\d+)?")//this returns all the matches in input
                        .Cast<Match>()//this casts from MatchCollection to IEnumerable<Match>
                        .Select(x => decimal.Parse(x.Value))//this parses each of the matched string to decimal
                        .ToArray();//this converts IEnumerable<decimal> to an Array of decimal
        }

        private Point PointFToPoint(PointF pf)
        {
            return new Point(((int)pf.X), ((int)pf.Y));
        }

        private PointF scalePointsToGrid(PointF pointf)
        {
            float x = (float)((pointf.X + drawScale / 2) / scaleFactor);
            float y = (float)((pointf.Y + drawScale / 2) / scaleFactor);
            return new PointF(x, y);
        }

        private float findMaxCoordinate(PointF point)
        {
            return 0;
        }

        private int calculateEstimatedRunTime()
        {
            double time = 0;
            for (int i = 0; i < p1List.Count; i++)
            {
                time += calculateTimePerLine(p1List[i], p2List[i]);
            }

            return (int)time;
        }

        private double calculateTimePerLine(Point p1, Point p2)
        {
            var distance = distancePoints(p1, p2);
            var time = distance / (double)scanSpeed;

            return time;
        }

        private double distancePoints(Point p1, Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            double distancePixel = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            double distance = distancePixel * ((double)drawScale / gridSize); // adjust scaling

            return distance;
        }
    }
}
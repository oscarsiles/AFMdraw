using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AFMdraw
{
    internal class SourceFile
    {
        // fileversion goes up by 1 for every additional line of data (v.2 for just 2 lists of lines)
        private int sourceFileVersion = 4;

        private string sourceFileHeader = "//AFMdraw source file";
        private string scriptName;
        private string scriptType;
        private decimal scanSpeed;
        private decimal drawScale;
        private int gridSize;

        public SourceFile()
        {
        }

        public SourceFile(string name, string type, decimal speed, decimal scale, int grid)
        {
            this.scriptName = name;
            this.scriptType = type;
            this.scanSpeed = speed;
            this.drawScale = scale;
            this.gridSize = grid;
        }

        public void SaveSerialized(ref List<Point> pxList, ref List<Point> pyList, ref List<int> lastLinesAdded, string path, ref string backupFilename)
        {
            // save X/Y coordinates to source file
            string serializedList1 = serializePointList(pxList);
            string serializedList2 = serializePointList(pyList);
            string serializedParameters = serializeParameters();

            // save operations to source file as well
            string serializedLinesAdded = serializeIntList(lastLinesAdded);

            // make sure we aren't saving source files as -source-source-source.txt
            if (path.Contains("-source.txt") == false)
            {
                backupFilename = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "-source.txt";
            }
            else
            {
                backupFilename = path;
            }

            using (TextWriter tw = new StreamWriter(backupFilename))
            {
                tw.WriteLine(sourceFileHeader);
                tw.WriteLine(@"//file version: " + sourceFileVersion);
                tw.WriteLine(serializedList1);
                tw.WriteLine(serializedList2);
                tw.WriteLine(serializedLinesAdded);
                tw.WriteLine(serializedParameters);
            }
        }

        public bool LoadSerialized(ref List<Point> pxList, ref List<Point> pyList, ref List<int> lastLinesAdded, string path)
        {
            // WOULD BE GOOD TO LOAD ENTIRE FILE TO MEMORYSTREAM, AND MANIPULATE FROM THERE, LESS FILE IO (FUTURE WORK)
            bool currentVersion;
            int version;

            // check to see if compatible file, then check file version
            using (StreamReader tr = new StreamReader(path))
            {
                string firstLine = tr.ReadLine();

                if (firstLine != sourceFileHeader) //not a AFMdraw source file (or older version)
                {
                    MessageBox.Show("Incompatible file type.\nMake sure to select a valid source file.", "Error");
                    return false;
                }

                string versionLine = tr.ReadLine();
                version = Convert.ToInt32(Regex.Match(versionLine, @"\d+").Value);

                if (version < sourceFileVersion) // older file version, give warning that some functions may not work
                {
                    MessageBox.Show("Old source file version.\nSome non-core functions may not work properly.", "Warning");
                    currentVersion = false;
                }
                else if (version > sourceFileVersion)
                {
                    MessageBox.Show("Source file version not recognized.\nPlease select a compatible source file.", "Warning");
                    return false;
                }
                else { currentVersion = true; }
            }

            // lets clear the lists
            pxList.Clear();
            pyList.Clear();
            lastLinesAdded.Clear();

            // now load up the file into the two Point lists, and further data if file version new enough
            pxList = deserializePointList(path, 2);
            pyList = deserializePointList(path, 3);

            // load parameters

            if (version >= 3) // v3 added number of lines in each operation (for undo)
            {
                lastLinesAdded = deserializeIntList(path, 4);
            }
            if (version >= 4) // v4 added script parameters
            {
                string[] parameters = deserializeParameters(path, 5);
                //setParameters(parameters[0], parameters[1], Convert.ToDecimal(parameters[2]), Convert.ToDecimal(parameters[3]));
                this.scriptName = parameters[0];
                this.scriptType = parameters[1];
                this.scanSpeed = Convert.ToDecimal(parameters[2]);
                this.drawScale = Convert.ToDecimal(parameters[3]);
                this.gridSize = Convert.ToInt32(parameters[4]);
            }

            return currentVersion;
        }

        public void SetParameters(ref string name, ref string type, ref decimal speed, ref decimal scale, ref int grid)
        {
            name = this.scriptName;
            type = this.scriptType;
            speed = this.scanSpeed;
            scale = this.drawScale;
            grid = this.gridSize;
        }

        private string serializeIntList(List<int> list)
        {
            // adapted from below methods
            StringBuilder sb = new StringBuilder();
            foreach (int Int in list)
            {
                sb.Append(Convert.ToString(Int, 16));
                sb.Append(':'); // separated by colon
            }
            string serialized = sb.ToString();
            return serialized;
        }

        private string serializePointList(List<Point> list)
        {
            // http://stackoverflow.com/questions/4023436/best-way-to-store-listpoint-in-a-string-and-parse-back
            StringBuilder sb = new StringBuilder();
            foreach (Point Point in list)
            {
                sb.Append(Convert.ToString(Point.X, 16)); sb.Append('.');
                sb.Append(Convert.ToString(Point.Y, 16)); sb.Append(':');
            }
            string serialized = sb.ToString();
            return serialized;
        }

        private string serializeParameters()
        {
            string serialized = this.scriptName + ":" + this.scriptType + ":" + this.scanSpeed + ":" + this.drawScale + ":" + this.gridSize + ":";
            return serialized;
        }

        private List<int> deserializeIntList(string path, int lineNumber)
        {
            // method adapted from below
            List<int> restored = new List<int>();
            string value = default(string);
            int left = 0;
            int x = 0;
            string serialized;

            // open file
            string[] lines = File.ReadAllLines(path);

            // load desired line number to analyze
            serialized = lines[lineNumber];

            for (int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i] == ':')
                {
                    value = serialized.Substring(left, i - left);
                    left = i + 1;
                    x = Convert.ToInt32(value, 16);
                    restored.Add(x);
                }
            }
            return restored;
        }

        private List<Point> deserializePointList(string path, int lineNumber)
        {
            // http://stackoverflow.com/questions/4023436/best-way-to-store-listpoint-in-a-string-and-parse-back
            List<Point> restored = new List<Point>();
            string value = default(string);
            int left = 0;
            int x = 0, y = 0;
            string serialized;

            // lets open file

            string[] lines = File.ReadAllLines(path);

            serialized = lines[lineNumber];

            for (int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i] == '.')
                {
                    value = serialized.Substring(left, i - left);
                    left = i + 1;
                    x = Convert.ToInt32(value, 16);
                }
                else if (serialized[i] == ':')
                {
                    value = serialized.Substring(left, i - left);
                    left = i + 1;
                    y = Convert.ToInt32(value, 16);
                    restored.Add(new Point(x, y));
                }
            }
            return restored;
        }

        private string[] deserializeParameters(string path, int lineNumber)
        {
            string[] restored = new string[DrawingWindow.parameterNumber];
            string value = default(string);
            int left = 0;
            int x = 0;
            string serialized;

            // open the file
            string[] lines = File.ReadAllLines(path);
            serialized = lines[lineNumber];

            for (int i = 0; i < serialized.Length; i++)
            {
                if (serialized[i] == ':')
                {
                    value = serialized.Substring(left, i - left);
                    left = i + 1;
                    restored[x] = value;
                    x++;
                }
            }
            return restored;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace AFMdraw
{
    internal class Shapes
    {
        private Point p1, p2;
        private List<Point> pxList, pyList;
        private List<int> linesAdded;
        private string shapeType;
        private int nPoly;
        private decimal nSpiral = 1;
        private bool isDrawn = false;

        public Shapes(string shapeType, int n, decimal n2, Point p1, Point p2, ref List<Point> pxList, ref List<Point> pyList, ref List<int> linesAdded)
        {
            this.shapeType = shapeType;
            this.nPoly = n;
            this.nSpiral = n2;
            this.p1 = p1;
            this.p2 = p2;
            this.pxList = pxList;
            this.pyList = pyList;
            this.linesAdded = linesAdded;
            this.isDrawn = false;
        }

        public void IsDrawn()
        {
            this.isDrawn = true;
        }

        public void AddPoints()
        {
            // now call appropriate method based on script type
            MethodInfo mi = this.GetType().GetMethod(this.shapeType, BindingFlags.NonPublic | BindingFlags.Instance); // last bit allows calling private methods
            mi.Invoke(this, null);
        }

        private double distancePoints(Point p1, Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return distance;
        }

        private double anglePoints(Point p1, Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            double angleDiff = Math.Atan2(yDiff, xDiff);

            return angleDiff;
        }

        private List<Point> calculatePolygonVertices(int nSides, int nSideLength, Point ptFirstVertex, Point ptSecondVertex)
        {
            // modified from here:
            // http://stackoverflow.com/questions/4152239/c-sharp-draw-polygons-using-angle-degrees-and-trig
            if (nSides < 3)
                throw new ArgumentException("Polygons can't have less than 3 sides...");

            List<Point> aptsVertices = new List<Point>(nSides);
            var deg = (180.0 * (nSides - 2)) / nSides;
            var step = 360.0 / nSides;
            var rad = deg * (Math.PI / 180);

            // need to correct for 1st/second Point!
            var xDiff = ptSecondVertex.X - ptFirstVertex.X;
            var yDiff = ptSecondVertex.Y - ptFirstVertex.Y;
            rad = rad + Math.Atan2(yDiff, xDiff);

            double nSinDeg = Math.Sin(rad);
            double nCosDeg = Math.Cos(rad);

            aptsVertices.Add(ptFirstVertex);

            for (int i = 1; i < nSides; i++)
            {
                if (i == 1)
                {
                    aptsVertices.Add(ptSecondVertex);
                }
                else
                {
                    double x = aptsVertices[i - 1].X - nCosDeg * nSideLength;
                    double y = aptsVertices[i - 1].Y - nSinDeg * nSideLength;
                    aptsVertices.Add(new Point((int)x, (int)y));
                }

                //recalculate the degree for the next vertex
                deg -= step;
                rad = deg * (Math.PI / 180);
                rad = rad + Math.Atan2(yDiff, xDiff) + (step * Math.PI / 180);

                nSinDeg = Math.Sin(rad);
                nCosDeg = Math.Cos(rad);
            }
            return aptsVertices;
        }

        private void line()
        {
            pxList.Add(p1);
            pyList.Add(p2);
            if (isDrawn)
            {
                linesAdded.Add(1);
            }
        }

        private void letter()
        {
            // lets try T for now
            var xDiff = p2.X - p1.X;
            var yDiff = p2.Y - p1.Y;

            Point p3 = new Point(p2.X + 2 * yDiff, p2.Y - 2 * xDiff);
            Point p4 = new Point(p3.X + xDiff, p3.Y + yDiff);
            Point p5 = new Point(p4.X + yDiff, p4.Y - xDiff);
            Point p6 = new Point(p5.X - 3 * xDiff, p5.Y - 3 * yDiff);
            Point p7 = new Point(p6.X - yDiff, p6.Y + xDiff);
            Point p8 = new Point(p7.X + xDiff, p7.Y + yDiff);


            // A -> B
            pxList.Add(p1);
            pyList.Add(p2);
            // B -> C
            pxList.Add(p2);
            pyList.Add(p3);
            // C -> D
            pxList.Add(p3);
            pyList.Add(p4);
            // D -> A
            pxList.Add(p4);
            pyList.Add(p5);
            // A -> B
            pxList.Add(p5);
            pyList.Add(p6);
            // B -> C
            pxList.Add(p6);
            pyList.Add(p7);
            // C -> D
            pxList.Add(p7);
            pyList.Add(p8);
            // D -> A
            pxList.Add(p8);
            pyList.Add(p1);

            if (isDrawn)
            {
                linesAdded.Add(8);
            }
        }

        private void triangle() // equilateral for now
        {
            Point p3 = new Point();
            // calculate third Point (only in ONE direction!)
            double sin60 = Math.Sin(60 * Math.PI / 180);
            double cos60 = Math.Cos(60 * Math.PI / 180);

            p3.X = (int)(cos60 * (p1.X - p2.X) - sin60 * (p1.Y - p2.Y) + p2.X);
            p3.Y = (int)(sin60 * (p1.X - p2.X) + cos60 * (p1.Y - p2.Y) + p2.Y);

            // A -> B
            pxList.Add(p1);
            pyList.Add(p2);
            // B -> C
            pxList.Add(p2);
            pyList.Add(p3);
            // C -> A
            pxList.Add(p3);
            pyList.Add(p1);

            if (isDrawn)
            {
                linesAdded.Add(3);
            }
        }

        private void square()
        {
            var xDiff = p2.X - p1.X;
            var yDiff = p2.Y - p1.Y;

            Point p3 = new Point(p2.X + yDiff, p2.Y - xDiff);
            Point p4 = new Point(p3.X - xDiff, p3.Y - yDiff);

            // A -> B
            pxList.Add(p1);
            pyList.Add(p2);
            // B -> C
            pxList.Add(p2);
            pyList.Add(p3);
            // C -> D
            pxList.Add(p3);
            pyList.Add(p4);
            // D -> A
            pxList.Add(p4);
            pyList.Add(p1);

            if (isDrawn)
            {
                linesAdded.Add(4);
            }
        }

        private void circle()
        {
            double distance = distancePoints(p1, p2);

            double theta = 0;
            int maxStepsPerRotation = 50;
            int minStepsPerRotation = 10;
            int stepsPerRotation = minStepsPerRotation + (int)((distance / 256) * (maxStepsPerRotation - minStepsPerRotation));
            double increment = 2 * Math.PI / stepsPerRotation;

            var center = p1;
            var lastPoint = new Point((int)(center.X + distance * Math.Cos(theta)), (int)(center.Y + distance * Math.Sin(theta)));
            theta = theta + increment;

            int lineCount = 0;

            while (true)
            {
                var newPoint = new Point((int)(center.X + distance * Math.Cos(theta)), (int)(center.Y + distance * Math.Sin(theta)));

                pxList.Add(lastPoint);
                pyList.Add(newPoint);

                theta = theta + increment;

                lastPoint = newPoint;
                lineCount++;

                if (lineCount == stepsPerRotation)
                {
                    break;
                }
            }

            if (isDrawn)
            {
                linesAdded.Add(lineCount);
            }
        }

        private void polygon() // needs some work, wonky for small size or high vertex count polygons
        {
            List<Point> vertexPoints = new List<Point>();

            // calculate side length
            double a = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

            vertexPoints = calculatePolygonVertices(nPoly, (int)a, p1, p2);

            // make sure p1 != p2
            if (p1 == p2)
            {
                return;
            }

            // now add to lists
            for (int i = 0; i < nPoly - 1; i++)
            {
                pxList.Add(vertexPoints[i]);
                pyList.Add(vertexPoints[i + 1]);
            }
            pxList.Add(vertexPoints[nPoly - 1]);
            pyList.Add(vertexPoints[0]);

            if (isDrawn)
            {
                linesAdded.Add(nPoly);
            }
        }

        private void star()
        {
            // same as for polygon, but change vertex addition algorithm
            List<Point> vertexPoints = new List<Point>();

            // calculate side length
            double a = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

            vertexPoints = calculatePolygonVertices(nPoly, (int)a, p1, p2);

            // make sure p1 != p2
            if (p1 == p2)
            {
                return;
            }

            // need 2 cases: odd/even vertex number

            // ODD
            if (nPoly % 2 != 0)
            {
                int first;
                int second;
                for (int i = 0; i < nPoly; i++)
                {
                    first = (2 * i) % nPoly;
                    second = (first + Convert.ToInt32(Math.Floor((decimal)(2 % (nPoly - 1))))) % nPoly; // oh god why
                    pxList.Add(vertexPoints[first]);
                    pyList.Add(vertexPoints[second]);
                }
            }
            else // EVEN
            {
                int first;
                int second;

                // first polygon
                for (int i = 0; i < (nPoly / 2); i++)
                {
                    first = (2 * i) % nPoly;
                    second = (first + Convert.ToInt32(Math.Floor((decimal)(2 % (nPoly - 1))))) % nPoly; // oh god why
                    pxList.Add(vertexPoints[first]);
                    pyList.Add(vertexPoints[second]);
                }

                // second polygon
                for (int i = 0; i < (nPoly / 2); i++)
                {
                    first = (2 * i + 1) % nPoly;
                    second = (first + Convert.ToInt32(Math.Floor((decimal)(2 % (nPoly - 1))))) % nPoly; // oh god why
                    pxList.Add(vertexPoints[first]);
                    pyList.Add(vertexPoints[second]);
                }
            }

            if (isDrawn)
            {
                linesAdded.Add(nPoly);
            }
        }

        private void array()
        {
            // add 10 "points" to simulate holding laser still
            // NOT above, just move by 1 micron back and forth 10 times (in snomScript.cs)
            int exposures = 1;

            // initialize point array
            Point[,] pointArray = new Point[nPoly, nPoly];

            // x/y distance between points
            var xDiff = (p2.X - p1.X) / (nPoly - 1);
            var yDiff = (p2.Y - p1.Y) / (nPoly - 1);

            for (int x = 0; x < nPoly; x++)
            {
                for (int y = 0; y < nPoly; y++)
                {
                    int pointX = p1.X + (y * xDiff) + (x * yDiff);
                    int pointY = p1.Y - (x * xDiff) + (y * yDiff);
                    pointArray[x, y] = new Point(pointX, pointY);

                    // add to list of points
                    for (int i = 0; i < exposures; i++)
                    {
                        pxList.Add(pointArray[x, y]);
                        pyList.Add(pointArray[x, y]);
                    }
                }
            }

            if (isDrawn)
            {
                linesAdded.Add(exposures * (int)Math.Pow(nPoly, 2)); // 10 * n^2 lines were added
            }
        }

        private void spiral()
        {
            // http://mathworld.wolfram.com/ArchimedeanSpiral.html

            // difference between two points (everything in radians)
            double angleDiff = anglePoints(p1, p2);
            double maxRadius = distancePoints(p1, p2);

            double theta = 0.1; // to avoid dividing by 0 for -ve n values
            int maxStepsPerRotation = 50;
            int minStepsPerRotation = 10;
            int stepsPerRotation = minStepsPerRotation; // starting number
            double increment = 2 * Math.PI / stepsPerRotation;
            double a = nPoly;
            double n = (double)nSpiral;

            // set how many rotations, based on position of mouse after click

            var center = p1;
            var lastPoint = center;

            bool keepGoing = true;
            int lineCount = 0;

            while (keepGoing)
            {
                var r = a * Math.Pow(theta, 1 / n);

                if (r >= maxRadius)
                {
                    break;
                }

                if (lineCount > 2000) // don't want too many lines
                {
                    break;
                }

                var newPoint = new Point((int)(center.X + r * Math.Cos(theta + angleDiff)), (int)(center.Y + r * Math.Sin(theta + angleDiff)));

                pxList.Add(lastPoint);
                pyList.Add(newPoint);

                // calculate new parameter values
                stepsPerRotation = minStepsPerRotation + (int)((r / maxRadius) * (maxStepsPerRotation - minStepsPerRotation));
                increment = 2 * Math.PI / stepsPerRotation;
                theta = theta + increment;

                lastPoint = newPoint;
                lineCount++;
            }

            if (isDrawn)
            {
                linesAdded.Add(lineCount);
            }
        }
    }
}
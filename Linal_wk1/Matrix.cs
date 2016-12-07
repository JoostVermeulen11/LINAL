using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Linal_wk1
{
    public class Matrix
    {
        private static int blokSize = 50;
        public double xPos { get; set; }
        public double yPos { get; set; }
        public double deltaX { get; set; }
        public double deltaY { get; set; }

        private Rectangle _matrix;

        public Matrix(double xPos, double yPos, double deltaX, double deltaY)
        {
            _matrix = new Rectangle();
            _matrix.Fill = RandomColor.GetRandomBrush();


            _matrix.Height = deltaY * blokSize;
            _matrix.Width = deltaX * blokSize;

            Canvas.SetLeft(_matrix, (xPos * blokSize));
            Canvas.SetTop(_matrix, (yPos * blokSize));

            this.xPos = xPos;
            this.yPos = yPos;
            this.deltaX = deltaX;
            this.deltaY = deltaY;            
        }

        public void Scale(double factorX, double factorY)
        {
            /*
             *| factorX  0 | x as vergroten
             *| 0  factorY | y as vergroten
             *          *
             *| a  d  e |
             *| b  c  f |
             */

            List<double> scaleMatrix = new List<double>();
            
            scaleMatrix.Add(factorX);
            scaleMatrix.Add(factorY);

            List<double> xAs = new List<double>() { xPos, xPos, xPos + deltaX, xPos + deltaX };
            List<double> newXAs = new List<double>();
            List<double> yAs = new List<double>() { yPos, yPos, yPos + deltaY, yPos + deltaY };
            List<double> newYAs = new List<double>();

            foreach (double number in xAs)
            {
                double newNumber = (scaleMatrix[0] * number) - xPos;
                newXAs.Add(newNumber);
            }

            foreach (double number in yAs)
            {
                double newNumber = (scaleMatrix[1] * number) - yPos;
                newYAs.Add(newNumber);
            }

            if(factorX > 0)
            {
                _matrix.Width = (newXAs[2] - newXAs[0]) * blokSize;
                _matrix.Height = (newYAs[2] - newYAs[0]) * blokSize;
            }
        }

        public Brush getColor()
        {
            return _matrix.Fill;
        }

        public Rectangle getMatrix()
        {
            return _matrix;
        }

    }
}

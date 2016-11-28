using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Linal_wk1
{
    class Matrix
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
            _matrix.Width = deltaY * blokSize;

            Canvas.SetLeft(_matrix, (xPos * blokSize));
            Canvas.SetTop(_matrix, (yPos * blokSize));

            this.xPos = xPos;
            this.yPos = yPos;
            this.deltaX = deltaX;
            this.deltaY = deltaY;            
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

using Petzold.Media2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Linal_wk1
{
    public class Vector : Shape
    {
        private ArrowLine _vector;
        public double xPos { get; set; }
        public double yPos { get; set; }
        public double deltaX { get; set; }
        public double deltaY { get; set; }

        public double Length { get; set; }
        private static int blokSize = 50;

        public Vector(double x, double y, double deltaX, double deltaY)
        {            
            _vector = new ArrowLine();
            _vector.Stroke = RandomColor.GetRandomBrush();      
            _vector.StrokeThickness = 5;

            this.xPos = x;
            this.yPos = y;
            this.deltaX = deltaX;
            this.deltaY = deltaY;

            _vector.X1 = (x * blokSize);
            _vector.X2 = ((x + deltaX) * blokSize);
            _vector.Y1 = (y * blokSize);
            _vector.Y2 = ((y + deltaY) * blokSize);
            
            Length = Math.Sqrt(Math.Pow(x + deltaX - x, 2) + Math.Pow(y + deltaY - y, 2)); //stelling van pythagoras            
        }

        public ArrowLine getVector()
        {
            return _vector;
        }

        public void Scale(double factorX, double factorY)
        {
            if (factorX < 0)
            {      
                deltaX /= (factorX * -1);
                _vector.X2 = (xPos + deltaX) * blokSize;               
            }
            else
            {
                deltaX *= factorX;
                _vector.X2 = (xPos + deltaX) * blokSize;
            }
            if(factorY < 0)
            {           
                deltaY /= (factorY * -1);
                _vector.Y2 = (yPos + deltaY) * blokSize;
            }
            else
            {
                deltaY *= factorY;
                _vector.Y2 = (yPos + deltaY) * blokSize;
            }           
        }

        public static Vector ADD(Vector vector1, Vector vector2)
        {
            double x = vector1.xPos;
            double y = vector1.yPos;

            double deltaX = vector1.deltaX + vector2.deltaX;
            double deltaY = vector1.deltaY + vector2.deltaY;

            return new Vector(x, y, deltaX, deltaY);
        }

        public static Vector DISTRACT(Vector vector1, Vector vector2)
        {
            double x = vector1.xPos;
            double y = vector1.yPos;

            double deltaX = vector1.deltaX - vector2.deltaX;
            double deltaY = vector1.deltaY - vector2.deltaY;

            return new Vector(x, y, deltaX, deltaY);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

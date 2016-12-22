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
    public class Vector
    {
        private ArrowLine _vector;
        public double xPos { get; set; }
        public double yPos { get; set; }
        public double deltaX { get; set; }
        public double deltaY { get; set; }
        public double deltaZ { get; set; }
        public double deltaW { get; set; }

        public double Length { get; set; }
        private static int blokSize = 50;

        public Vector(double x, double y, double deltaX, double deltaY)
        {            
            _vector = new ArrowLine();
            _vector.Stroke = RandomColor.GetRandomBrush();      
            _vector.StrokeThickness = 5;

            xPos = x;
            yPos = y;
            this.deltaX = deltaX;
            this.deltaY = deltaY;

            _vector.X1 = (x * blokSize);
            _vector.X2 = ((x + deltaX) * blokSize);
            _vector.Y1 = (y * blokSize);
            _vector.Y2 = ((y + deltaY) * blokSize);
         }

        public Vector(double x, double y, double z)
        {
            this.deltaX = x;
            this.deltaY = y;
            this.deltaZ = z;
            this.deltaW = 1;
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
            double deltaZ = vector1.deltaZ + vector2.deltaZ;

            return new Vector(x, y, deltaX, deltaY);
        }

        public static Vector SUBTRACT(Vector vector1, Vector vector2)
        {
            double deltaX = vector1.deltaX - vector2.deltaX;
            double deltaY = vector1.deltaY - vector2.deltaY;
            double deltaZ = vector1.deltaZ - vector2.deltaZ;
            
            return new Vector(deltaX, deltaY, deltaZ);
        }

        public void normalize()
        {
            double normalize = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ));

            deltaX = deltaX / normalize;
            deltaY = deltaY / normalize;
            deltaZ = deltaZ / normalize;
        }

        // volgorde voor het uitrekenen
        // 1.   V1y*V2z - V2y*V1z     
        // 2.   V2x*V1z - V1x*V2z 
        // 3.   V1x*V2y - V2x*V1y  
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            double newX = (v1.deltaY * v2.deltaZ) - (v2.deltaY * v1.deltaZ);
            double newY = (v2.deltaX * v1.deltaZ) - (v1.deltaX * v2.deltaZ);
            double newZ = (v1.deltaX * v2.deltaY) - (v2.deltaX * v1.deltaY);

            return new Vector(newX, newY, newZ);
        }

        // volgorde voor het uitrekenen
        //  V1x*V2x + V1y*V2Y + V1z*V2z
        public static double InProduct(Vector v1, Vector v2)
        {            
            return (v1.deltaX * v2.deltaX) + (v1.deltaY * v2.deltaY) + (v1.deltaZ * v2.deltaZ);
        }
    }
}

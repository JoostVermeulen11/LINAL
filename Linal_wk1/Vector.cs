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
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }
        public String Direction { get; set; }
        public double Length { get; set; }

        private static int blokSize = 50;

        public Vector(double x1, double x2, double y1, double y2)
        {            
            _vector = new ArrowLine();
            _vector.Stroke = RandomColor.GetRandomBrush();      
            _vector.StrokeThickness = 5;

            _vector.X1 = (x1 * blokSize);
            _vector.X2 = (x2 * blokSize);
            _vector.Y1 = (y1 * blokSize);
            _vector.Y2 = (y2 * blokSize);
            
            this.x1 = _vector.X1;
            this.x2 = _vector.X2;
            this.y1 = _vector.Y1;
            this.y2 = _vector.Y2;

            if (y2 > y1)
                Direction = "UP";
            else
                Direction = "DOWN";

            Length = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); //stelling van pythagoras
            
        }

        public ArrowLine getVector()
        {
            return _vector;
        }

        public void Scale(int factor)
        {
            double newX, newY;
            /**
             *laagste x van hoogste afhalen, zelfde bij y, dan allebei x de schaal doen, heb je de nieuwe grootte.  
             */
            if (x1 < x2)
                newX = x2 - x1;
            else
                newX = x1 - x2;

            if (y1 < y2)
                newY = y2 - y1;
            else
                newY = y1 - y2;

            newX = newX / 50;
            newY = newY / 50;

            if (factor > 0)
            {
                //make greater
                newX = newX * factor;
                newY = newY * factor;

                //make x2 longer
                if (x2 > x1)
                {                    
                    _vector.X2 = _vector.X1 + (newX * blokSize);
                    x2 = _vector.X2;
                }
                else
                {
                    _vector.X2 = _vector.X1 - (newX * blokSize);
                    x2 = _vector.X2;
                }

                //make y2 longer
                if(y2 > y1)
                {
                    _vector.Y2 = _vector.Y1 + (newY * blokSize);
                    y2 = _vector.Y2;
                }
                else
                {
                    _vector.Y2 = _vector.Y1 - (newY * blokSize);
                    y2 = _vector.Y2;
                }              
            }
            else
            {
                //make smaller
                //make from negative positive numbers
                newX = (newX / factor) * -1;
                newY = (newY / factor) * -1;

                //make x2 smaller
                if (x2 > x1)
                {
                    _vector.X2 = _vector.X2 - (newX * blokSize);
                    x2 = _vector.X2;
                }
                else
                {
                    _vector.X2 = _vector.X2 + (newX * blokSize);
                    x2 = _vector.X2;
                }

                //TODO
                //make y2 smaller
                if (y2 > y1)
                {
                    _vector.Y2 = _vector.Y2 - (newY * blokSize);
                    y2 = _vector.Y2;
                }
                else
                {
                    _vector.Y2 = _vector.Y2 + (newY * blokSize);
                    y2 = _vector.Y2;
                }

            }
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

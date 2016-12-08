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
        private static int blockSize = 50;
        private Rectangle _matrix;
        private Brush _color;
        private Point xy1, xy2, xy3, xy4;
        public double[,] matrix { get; set; }

        public int width
        {
            get
            {
                return matrix.GetLength(1);
            }
        }


        public int height
        {
            get
            {
                return matrix.GetLength(0);
            }
        }

        public Matrix(double[,] arr)
        {
            matrix = arr;

            _color = RandomColor.GetRandomBrush();
        }

        public void multiply(Matrix m1)
        {
            if (m1.width != height)
            {
                return;
            }

            double[,] ma1 = m1.matrix;
            double[,] ma2 = matrix;

            double[,] result = new double[m1.height, width];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < m1.width; k++)
                    {
                        result[i, j] = result[i, j] + ma1[i, k] * ma2[k, j];
                    }
                }
            }

            matrix = result;
        }

        public void translate(Matrix m1)
        {
            
        }

        public void drawMatrix()
        {
            /*
             *    xy1     xy2
             * 
             *    xy3     xy4
             */
            _matrix = new Rectangle();

            for (int i = 0; i < height; i+=2)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == 0)
                    {
                        xy1 = new Point(matrix[i, j], matrix[i + 1, j]);
                    }
                    if (j == 1)
                    {
                        xy2 = new Point(matrix[i, j], matrix[i + 1, j]);
                    }
                    if (j == 2)
                    {
                        xy3 = new Point(matrix[i, j], matrix[i + 1, j]);
                    }
                    if (j == 3)
                    {
                        xy4 = new Point(matrix[i, j], matrix[i + 1, j]);
                    }
                }
            }

            _matrix.Width = (xy4.X - xy3.X) * blockSize;
            _matrix.Height = (xy1.Y - xy3.Y) * blockSize;

            Canvas.SetLeft(_matrix, xy3.X * blockSize);
            Canvas.SetTop(_matrix, xy3.Y * blockSize);

            _matrix.Fill = _color;
        }

        public Brush getColor()
        {
            return _matrix.Fill;
        }

        public Rectangle getRectangle()
        {
            return _matrix;
        }
    }
}

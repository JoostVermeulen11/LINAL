﻿using System;
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
    public class Matrix2D
    {
        //TODO scale and rotate origin from matrices
        private static int blockSize = 50;
        private Polygon _surface;
        private PointCollection _collection;
        private Point[] points;
        public double Degrees, X, Y;
        public bool animate = false;
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

        public Matrix2D(double[,] arr)
        {
            matrix = arr;
            
            _surface = new Polygon();
            _collection = new PointCollection();
            points = new Point[4];
            _surface.Stroke = Brushes.Black;   
            _surface.Fill = RandomColor.GetRandomBrush();
            _surface.StrokeThickness = 3;
        }

        public void Rotate(double degrees)
        {
            double radians = ConvertToRadians(degrees);
            
            Matrix2D rotationMatrix = createRotationMatrix(Math.Cos(radians), Math.Sin(radians));

            Scale(rotationMatrix);
        }

        public void Scale(Matrix2D m1)
        {
            if (m1.width != height)
            {
                return;
            }

            double[,] ma1 = m1.matrix;
            double[,] ma2 = matrix;

            double[,] result = new double[m1.height, width];

            for (int i = 0; i < result.GetLength(0);/*height*/ i++)
            {
                for (int j = 0; j < result.GetLength(1);/*width*/ j++)
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

        public void Translate(Matrix2D m1)
        {
            if (m1.width != height)
            {
                return;
            }

            double[,] ma1 = m1.matrix;
            double[,] ma2 = matrix;

            double[,] result = new double[m1.height, width];

            for (int i = 0; i < result.GetLength(0);/*height*/ i++)
            {
                for (int j = 0; j < result.GetLength(1);/*width*/ j++)
                {
                    for (int k = 0; k < m1.width; k++)
                    {
                        if (i == 2)
                            result[i, j] = 1;
                        else
                            result[i, j] = ma1[i, 2] + ma2[i, j];
                    }
                }
            }

            matrix = result;
        }


        public static Matrix2D createIdentityMatrix(double translateX, double TranslateY)
        {
            return new Matrix2D(new double[,] {
                {1,0,translateX},
                {0,1,TranslateY},
                {0,0,1}                    
            });
        }

        public static Matrix2D createScaleMatrix(double scaleX, double scaleY)
        {
            return new Matrix2D(new double[,] {
                {scaleX,0,0},
                {0,scaleY,0},
                {0,0,0}
            });
        }

        private Matrix2D createRotationMatrix(double cos, double sin)
        {
            return new Matrix2D(new double[,] {
                {cos,   sin*-1, 0},
                {sin    ,cos, 0},
                {0,0,0},
            });
        }

        public void drawMatrix()
        {
            /*
             *    xy1-----xy4
             *     |       |
             *    xy2-----xy3
             */
            for (int i = 0; i < height; i+=5)
            {
                for (int j = 0; j < width; j++)
                {
                    points[j] = new Point(matrix[i, j] * blockSize, matrix[i + 1, j] * blockSize);
                }
            }

            // clear collection before adding new points
            _collection.Clear();

            foreach (Point p in points)
            {
                _collection.Add(p);
            }
                                  
            // add last point once again to make the square complete
            //_collection.Add(points[0]);     
            _surface.Points = _collection;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public Brush getColor()
        {
            return _surface.Stroke;
        }

        public Polygon getLine()
        {
            return _surface;
        }
    }
}

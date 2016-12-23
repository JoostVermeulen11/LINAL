using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Linal_wk1.Model
{
    class Kubus
    {
        private List<Polygon> shapeList;
        public Matrix3D matrix;
        private Point3D[] points;
        public double VectorEye { get; set; }
        public double LookAtY { get; set; }
        public double LookAtX { get; set; }


        public Kubus()
        {
            VectorEye = 150;
            LookAtY = 0;
            LookAtX = 0;

            points = new Point3D[8];
            shapeList = new List<Polygon>();

            populate();
        }   
        
        public void populate()
        {
            shapeList.Clear();

            matrix = new Matrix3D(new double[,]
            {
                {0,50,0,0,0,50,50,50},
                {0,0,50,0,50,0,50,50},
                {0,0,0,50,50,50,0,50},
                {1,1,1,1,1,1,1,1}
            });

            Matrix3D perspectiveProjectionMatrix = Matrix3D.PerspectiveProjectionMatrix(10, 400, 90);
            Matrix3D cameraMatrix = Matrix3D.CameraMatrix(new Vector(VectorEye, VectorEye, VectorEye), new Vector(LookAtX, LookAtY, 0), new Vector(0, 1, 0));

            Matrix3D weergaveVectorenMatrix = perspectiveProjectionMatrix * cameraMatrix * matrix;

            // Naberekening 
            matrix = weergaveVectorenMatrix.naberekening(700, 700);
        }  
        
        public void Draw()
        {
            for (int i = 0; i < matrix.width; i++)
            {
                points[i] = new Point3D() { X = matrix.matrix[0, i], Y = matrix.matrix[1, i], Z = matrix.matrix[2,i] };
            }

            //shape 1: bottom
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Orange,
                Points = new PointCollection(){
                    new Point(points[0].X, points[0].Y),
                    new Point(points[1].X, points[1].Y),
                    new Point(points[5].X, points[5].Y),
                    new Point(points[3].X, points[3].Y)                   
                }
            });

            //shape 2: back
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Brown,
                Points = new PointCollection(){
                    new Point(points[0].X, points[0].Y),
                    new Point(points[1].X, points[1].Y),
                    new Point(points[6].X, points[6].Y),
                    new Point(points[2].X, points[2].Y)
                }
            });
            
            //shape 3: left
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Green,
                Points = new PointCollection(){
                    new Point(points[3].X, points[3].Y),
                    new Point(points[0].X, points[0].Y),
                    new Point(points[2].X, points[2].Y),
                    new Point(points[4].X, points[4].Y)
                }
            });

            //shape 3: front
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Yellow,
                Points = new PointCollection(){
                    new Point(points[3].X, points[3].Y),
                    new Point(points[5].X, points[5].Y),
                    new Point(points[7].X, points[7].Y),
                    new Point(points[4].X, points[4].Y)
                }
            });

            //shape 3: top
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Red,
                Points = new PointCollection(){
                    new Point(points[2].X, points[2].Y),
                    new Point(points[6].X, points[6].Y),
                    new Point(points[7].X, points[7].Y),
                    new Point(points[4].X, points[4].Y)
                }
            });

            //shape 3: right
            shapeList.Add(new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Blue,
                Points = new PointCollection(){
                    new Point(points[5].X, points[5].Y),
                    new Point(points[1].X, points[1].Y),
                    new Point(points[6].X, points[6].Y),
                    new Point(points[7].X, points[7].Y)
                }
            });
        }  

        public List<Polygon> getKubus()
        {
            return shapeList;
        }
    }
}

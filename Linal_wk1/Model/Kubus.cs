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
        private Matrix3D perspectiveProjectionMatrix;
        private Matrix3D cameraMatrix;
        private Matrix3D weergaveVectorenMatrix;
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

            matrix = new Matrix3D(new double[,]
            {
                {0,50,0,0,0,50,50,50},
                {0,0,50,0,50,0,50,50},
                {0,0,0,50,50,50,0,50},
                {1,1,1,1,1,1,1,1}
            });

            populate();
        }

        public void populate()
        {
            shapeList.Clear();

            perspectiveProjectionMatrix = Matrix3D.PerspectiveProjectionMatrix(10, 400, 90);
            cameraMatrix = Matrix3D.CameraMatrix(new Vector(VectorEye, VectorEye, VectorEye), new Vector(LookAtX, LookAtY, 0), new Vector(0, 1, 0));

            weergaveVectorenMatrix = perspectiveProjectionMatrix * cameraMatrix * matrix;

            // Naberekening 
            matrix = weergaveVectorenMatrix.naberekening(700, 700);
        }

        public void rotate3D(Point3D p1, Point3D p2)
        {
            Vector v = null;
            Point3D? over = null;

            if (p2 == null)
            {
                v = new Vector(0, 0, 0, p1.X, p1.Y, p1.Z);
            }
            else
            {
                // Yes this is ugly.
                v = new Vector(p1.X, p1.Y, p1.Z, p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
                over = p1;
            }

            Matrix rotation = MatrixFactory.Get3DRotationMatrix(angle, v, over);

            rotation.Multiply(this);
            _data = rotation.GetData();
        }
        public void RotateX()
        {
            Matrix3D temp = matrix.RotateX(2, false);
            matrix = temp * matrix;
        }
        public void RotateY()
        {
            Matrix3D temp = matrix.RotateY(0.5, false);
            matrix = temp * matrix;
        }
        public void RotateZ()
        {
            Matrix3D temp = matrix.RotateZ(2, false);
            matrix = temp * matrix;
        }

        public void Draw()
        {
            shapeList.Clear();
            for (int i = 0; i < matrix.width; i++)
            {
                points[i] = new Point3D() { X = matrix.matrix[0, i], Y = matrix.matrix[1, i], Z = matrix.matrix[2, i] };
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

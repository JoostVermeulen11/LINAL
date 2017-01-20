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
        public Matrix3D matrix,weergavenMatrix;
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

            //kubus matrix
            matrix = new Matrix3D(new double[,]
            {
                {0,25,0,0,0,25,25,25},
                {0,0,25,0,25,0,25,25},
                {0,0,0,25,25,25,0,25},
                {1,1,1,1,1,1,1,1}
            });

            weergavenMatrix = matrix;        
        }

        public void rotate3D(Point3D p1)
        {
            Vector v = new Vector(0, 0, 0, p1.X, p1.Y, p1.Z);
            Point3D over = new Point3D() { X = 0, Y = 0, Z = 0 };

            Matrix3D rotation = Matrix3D.Get3DRotationMatrix(10, v, over);

            matrix = rotation * matrix;
        }

        public void rotate3D(Point3D p1, Point3D p2)
       {
            Vector v = new Vector(p1.X, p1.Y, p1.Z, p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            // Initialize empty point.
            Point3D over = new Point3D() { X = 0, Y = 0, Z = 0 };
                      
            // Yes this is ugly.
            over = p1;
           
            Matrix3D rotation = Matrix3D.Get3DRotationMatrix(1, v, over);

            matrix = rotation * matrix;
        }

        public void RotateX()
        {
            Matrix3D temp = Matrix3D.RotateX(1, false, false);
            matrix = temp * matrix;
        }

        public void RotateY()
        {
            Matrix3D temp = Matrix3D.RotateY(1, false, false);
            matrix = temp * matrix;
        }

        public void RotateZ()
        {
            Matrix3D temp = Matrix3D.RotateZ(1, false, false);
            matrix = temp * matrix;
        }

        public void Translate(double x, double y, double z)
        {
            Matrix3D translationMatrix = Matrix3D.createTranslationMatrix(x, y, z);
            matrix = translationMatrix * matrix;
        }

        public void Draw()
        {
            shapeList.Clear();

            perspectiveProjectionMatrix = Matrix3D.PerspectiveProjectionMatrix(10, 400, 90);
            cameraMatrix = Matrix3D.CameraMatrix(new Vector(VectorEye, VectorEye, VectorEye), new Vector(LookAtX, LookAtY, 0), new Vector(0, 1, 0));

            weergaveVectorenMatrix = perspectiveProjectionMatrix * cameraMatrix * matrix;

            // Naberekening 
            weergavenMatrix = weergaveVectorenMatrix.naberekening(700);
            
            for (int i = 0; i < weergavenMatrix.width; i++)
            {
                points[i] = new Point3D() { X = weergavenMatrix.matrix[0, i], Y = weergavenMatrix.matrix[1, i], Z = weergavenMatrix.matrix[2, i] };
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

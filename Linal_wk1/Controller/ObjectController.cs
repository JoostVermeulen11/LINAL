using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linal_wk1.Model;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media.Media3D;

namespace Linal_wk1
{
    class ObjectController
    {
        private List<Kubus> kubusList;
        private MainWindow _main;
        private Timer _timer; 

        public ObjectController(MainWindow _main)
        {
            this._main = _main;           
            kubusList = new List<Kubus>();
            createObjects();

            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Elapsed += new ElapsedEventHandler(Animate);

            Draw();
        }

        private void createObjects()
        {           
            kubusList.Add(new Kubus());         
        }

        private void Animate(object sender, EventArgs e)
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
               
            //});
        }
          
        public void TranslateMatrix(double x, double y, Matrix2D m)
        {
            Matrix2D translateMatrix = Matrix2D.createIdentityMatrix(x,y);
            m.Translate(translateMatrix);
            Draw();
        }

        public void RotateMatrix(double degrees, Matrix2D m)
        {
            m.Rotate(degrees);
            Draw();
        }

        public void RotateSpecificPoint(Matrix2D m, double degrees, double xPoint, double yPoint)
        {
            Matrix2D translateMatrix = Matrix2D.createIdentityMatrix(xPoint * -1, yPoint * -1);
            m.Translate(translateMatrix);
            
            m.Rotate(degrees);

            translateMatrix = Matrix2D.createIdentityMatrix(xPoint, yPoint);
            m.Translate(translateMatrix);

            Draw();
        }

        public void Draw()
        {
            _main.Assenstelsel.Children.Clear();
            foreach (var kubus in kubusList)
            {
                kubus.Draw();
                foreach (var shape in kubus.getKubus())
                {
                    _main.Assenstelsel.Children.Add(shape);
                }                
            }
        }

        public void zoomIn()
        {
            foreach (var kubus in kubusList)
            {
                kubus.VectorEye--;
                kubus.Draw();
            }
            Draw();
        }

        public void zoomOut()
        {
            foreach (var kubus in kubusList)
            {
                kubus.VectorEye++;
                kubus.Draw();
            }
            Draw();
        }

        public void LookatYUp()
        {
            foreach (var kubus in kubusList)
            {
                kubus.LookAtY--;
                kubus.Draw();
            }
            Draw();
        }

        public void LookatYDown()
        {
            foreach (var kubus in kubusList)
            {
                kubus.LookAtY++;
                kubus.Draw();
            }
            Draw();
        }

        public void LookatXRight()
        {
            foreach (var kubus in kubusList)
            {
                kubus.LookAtX++;
                kubus.Draw();
            }
            Draw();
        }

        public void LookatXLeft()
        {
            foreach (var kubus in kubusList)
            {
                kubus.LookAtX--;
                kubus.Draw();
            }
            Draw();
        }

        public void rotateOver(Point3D p1)
        {
            foreach (var kubus in kubusList)
            {
                kubus.rotate3D(p1);
            }
            Draw();
        }

        public void rotateOver(Point3D p1, Point3D p2)
        {
            foreach (var kubus in kubusList)
            {
                kubus.rotate3D(p1, p2);
            }
            Draw();
        }

        public void RotateX()
        {
            foreach (var kubus in kubusList)
            {
                kubus.RotateX();
            }
            Draw();
        }
        public void RotateY()
        {
            foreach (var kubus in kubusList)
            {
                kubus.RotateY();
            }
            Draw();
        }
        public void RotateZ()
        {
            foreach (var kubus in kubusList)
            {
                kubus.RotateZ();
            }
            Draw();
        }

        public void translate(double x, double y, double z)
        {
            foreach (var kubus in kubusList)
            {
                kubus.Translate(x,y,z);
            }
            Draw();
        }

        public Timer getTimer()
        {
            return _timer;
        }      
    }
}

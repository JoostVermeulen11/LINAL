﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linal_wk1.Model;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace Linal_wk1
{
    class ObjectController
    {
        private List<Vector> vectorList;
        private List<Matrix2D> matrix2DList;
        private List<Matrix3D> matrix3DList;
        private MainWindow _main;
        private Timer _timer; 
        public Matrix2D SelectedMatrix;

        public ObjectController(MainWindow _main)
        {
            this._main = _main;
            vectorList = new List<Vector>();
            matrix2DList = new List<Matrix2D>();
            matrix3DList = new List<Matrix3D>();
            createObjects();

            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Elapsed += new ElapsedEventHandler(Animate);

            Draw();
        }

        private void createObjects()
        {
            vectorList.Add(new Vector(3, 3, 3, 1));
            vectorList.Add(new Vector(3, 3, 2, 2));

            matrix2DList.Add(new Matrix2D(new double[,]
            {
                {3,3,2,2},
                {10,9,9,10},
                {1,1,1,1}
            }));

            //hierbij is vector eye 10,10,10,1 en lookAt is 0,0,0,1 en up is 0,1,0,1
            matrix2DList.Add(new Matrix2D(new double[,]
            {
                {1,1,2,2},
                {2,1,1,2},
                {1,1,1,1}
            }));
            
            matrix3DList.Add(new Matrix3D(new double[,]
            {
                {0,1,0,0,0,1,1,1},
                {0,0,1,0,1,0,1,1},
                {0,0,0,1,1,1,0,1},
                {1,1,1,1,1,1,1,1}
            }));

            //vragen wat far en near voor waardes moeten hebben. en vragen welke vectoren naberekend moeten worden. daarna de matrix nog tekenen
            Matrix3D cameraMatrix = Matrix3D.CameraMatrix(new Vector(10,10,10), new Vector(0,0,0), new Vector(0,1,0));
            Matrix3D perspectiveProjectionMatrix = Matrix3D.PerspectiveProjectionMatrix(1/*geen idee*/, 1/*geen idee*/, 90);

        }

        private void Animate(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var matrix in matrix2DList)
                {
                    if(matrix.animate)
                        RotateSpecificPoint(matrix, matrix.Degrees, matrix.X, matrix.Y);

                }
            });
        }

        public void ADDVector(Vector v1, Vector v2)
        {
            vectorList.Add(Vector.ADD(v1, v2));
            Draw();
        }

        public void ScaleVector(double x, double y, Vector v)
        {
            v.Scale(x, y);
            Draw();
        }

        public void ScaleMatrix(double x, double y, Matrix2D m)
        {
            Matrix2D scaleMatrix = Matrix2D.createScaleMatrix(x,y);
            m.Scale(scaleMatrix);
            Draw();
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
            foreach (var vector in vectorList)
            {
                _main.Assenstelsel.Children.Add(vector.getVector());
            }
            foreach (var matrix in matrix2DList)
            {
                matrix.drawMatrix();
                _main.Assenstelsel.Children.Add(matrix.getLine());
            }
        }

        public List<Vector> getVectors()
        {
            return vectorList;
        }

        public Timer getTimer()
        {
            return _timer;
        }

        public List<Matrix2D> getMatrixes()
        {
            return matrix2DList;
        }
    }
}

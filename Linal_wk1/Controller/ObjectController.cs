using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;

namespace Linal_wk1
{
    class ObjectController
    {
        private List<Vector> vectorList;
        private List<Matrix> matrixList;
        private MainWindow _main;
        private Timer _timer;

        public ObjectController(MainWindow _main)
        {
            this._main = _main;
            vectorList = new List<Vector>();
            matrixList = new List<Matrix>();
            createObjects();

            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += new ElapsedEventHandler(Animate);

            Draw();
        }

        private void createObjects()
        {
            vectorList.Add(new Vector(4, 4, 4, 1));
            vectorList.Add(new Vector(4, 4, 2, 2));

            matrixList.Add(new Matrix(new double[,]
            {
                {11,11,12,12},
                {2,1,1,2},
                {1,1,1,1}
            }));

            //matrixList.Add(new Matrix(new double[,]
            //{
            //    {1,1,2,2},
            //    {2,1,1,2},
            //    {1,1,1,1}
            //}));
        }

        [STAThread]
        private void Animate(object sender, EventArgs e)
        {          
            foreach(var matrix in matrixList)
            {
                RotateSpecificPoint(matrix, 10, 11, 1);
            }
        }

        public void ADDVector(Vector v1, Vector v2)
        {
            Vector.ADD(v1, v2);
            Draw();
        }

        public void ScaleVector(double x, double y, Vector v)
        {
            v.Scale(x, y);
            Draw();
        }

        public void ScaleMatrix(double x, double y, Matrix m)
        {
            Matrix scaleMatrix = Matrix.createScaleMatrix(x,y);
            m.Scale(scaleMatrix);
            Draw();
        }

        public void TranslateMatrix(double x, double y, Matrix m)
        {
            Matrix translateMatrix = Matrix.createIdentityMatrix(x,y);
            m.Translate(translateMatrix);
            Draw();
        }

        public void RotateMatrix(double degrees, Matrix m)
        {
            m.Rotate(degrees);
            Draw();
        }

        public void RotateSpecificPoint(Matrix m, double degrees, double xPoint, double yPoint)
        {
            Matrix translateMatrix = Matrix.createIdentityMatrix(xPoint * -1, yPoint * -1);
            m.Translate(translateMatrix);

            //rotate
            m.Rotate(degrees);

            //translate back
            translateMatrix = Matrix.createIdentityMatrix(xPoint, yPoint);
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
            foreach (var matrix in matrixList)
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

        public List<Matrix> getMatrixes()
        {
            return matrixList;
        }
    }
}

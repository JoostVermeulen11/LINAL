using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linal_wk1.Model
{
    class Matrix3D
    {
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

        public Matrix3D(double[,] arr)
        {
            matrix = arr;
        }

        public Matrix3D()
        {

        }

        public static Matrix3D CameraMatrix(Vector vectorEye, Vector lookAt, Vector up)
        {
            Vector z = Vector.SUBTRACT(vectorEye, lookAt);
            z.normalize();

            Vector y = up;
            y.normalize();

            Vector x = Vector.CrossProduct(y, z);
            x.normalize();

            y = Vector.CrossProduct(z,x);
            y.normalize();

            double inProductX = Vector.InProduct(x, vectorEye) * -1;
            double inProductY = Vector.InProduct(y, vectorEye) * -1;
            double inProductZ = Vector.InProduct(z, vectorEye) * -1;
            
            return new Matrix3D(new double[,] {
                {x.deltaX, x.deltaY, x.deltaZ, inProductX},
                {y.deltaX, y.deltaY, y.deltaZ, inProductY},
                {z.deltaX, z.deltaY, z.deltaZ, inProductZ},
                {0,0,0,1}
            });
        }

        public static Matrix3D PerspectiveProjectionMatrix(double near, double far, double fieldOfView)
        {
            //Gebruik de volgende formule: 𝑠𝑐𝑎𝑙𝑒 = 𝑛𝑒𝑎𝑟 ∗ tan(𝛼 ∗ 0.5)
            //Let op: a is hier in radialen, zet je graden dus eerst om!
            double scale = near * Math.Tan(ConvertToRadians(fieldOfView) * 0.5);

            //waardes voor in de projectiematrix berekenen
            double value1 = (far * -1) / (far - near);
            double value2 = ((far * -1) * near) / (far - near);

            return new Matrix3D(new double[,] {
                {scale,0,0,0},
                {0,scale,0,0},
                {0,0,value1,-1},
                {0,0,value2,0}
            });
        }

        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Linal_wk1.Model
{
    class Matrix3D
    {
        public static Matrix3D operator *(Matrix3D leftMatrix, Matrix3D rightMatrix)
        {
            return leftMatrix.Multiply(rightMatrix);        
        }

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

        public Matrix3D Multiply(Matrix3D m1)
        {
            if (width != m1.height)
            {
                return null;
            }

            double[,] ma1 = matrix;
            double[,] ma2 = m1.matrix;

            double[,] result = new double[height, m1.width];

            for (int i = 0; i < result.GetLength(0);/*height*/ i++)
            {
                for (int j = 0; j < result.GetLength(1);/*width*/ j++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        result[i, j] += ma1[i, k] * ma2[k, j];
                    }
                }
            }
            return new Matrix3D(result);
        }    
        
        public Matrix3D naberekening(double screenWidth, double screenHeight)
        {
            // first width, then height of canvas: both 700

            for (int i = 0; i < width; i++)
            {
                matrix[0, i] = (screenWidth / 2) + ((matrix[0, i] + 1) / matrix[3, i]) * screenHeight * 0.5;
                matrix[1, i] = (screenWidth / 2) + ((matrix[1, i] + 1) / matrix[3, i]) * screenHeight * 0.5; 
                matrix[2, i] = matrix[2,i] * -1; 
            }

            return new Matrix3D(matrix);
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

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

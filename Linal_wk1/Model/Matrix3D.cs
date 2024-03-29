﻿using System;
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
        
        public Matrix3D naberekening(double screenSize)
        {
            // first width, then height of canvas: both 700

            for (int i = 0; i < width; i++)
            {
                matrix[0, i] = (screenSize / 2) + ((matrix[0, i] + 1) / matrix[3, i]) * screenSize * 0.5;
                matrix[1, i] = (screenSize / 2) + ((matrix[1, i] + 1) / matrix[3, i]) * screenSize * 0.5; 
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
            double scale = near * Math.Tan(((Math.PI / 180) * fieldOfView) * 0.5);

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

        public static Matrix3D RotateX(double degrees, bool inverse, bool convert)
        {
            if(!convert)
                degrees = ConvertToRadians(degrees);

            double rotate1 = Math.Cos(degrees);
            double rotate2 = inverse ? -Math.Sin(degrees) : Math.Sin(degrees);

            Matrix3D rotateMatrix = new Matrix3D(new double[,]
            {
                {1,0,0,0},
                {0,rotate1,-rotate2,0},
                {0,rotate2,rotate1,0},
                {0,0,0,1}               
            });

            return rotateMatrix;
        }
        public static Matrix3D RotateY(double degrees, bool inverse, bool convert)
        {
            if(!convert)
                degrees = ConvertToRadians(degrees);

            double rotate1 = Math.Cos(degrees);
            double rotate2 = inverse ? -Math.Sin(degrees) : Math.Sin(degrees);

            Matrix3D rotateMatrix = new Matrix3D(new double[,]
            {
                {rotate1, 0, -rotate2,0},  
                {0,1,0,0},
                {rotate2,0,rotate1, 0},
                {0,0,0,1}
            });

            return rotateMatrix;
        }

        public static Matrix3D RotateZ(double degrees, bool inverse, bool convert)
        {
            if(!convert)
                degrees = ConvertToRadians(degrees);

            double rotate1 = Math.Cos(degrees);
            double rotate2 = inverse ? -Math.Sin(degrees) : Math.Sin(degrees);

            Matrix3D rotateMatrix = new Matrix3D(new double[,]
            {
                {rotate1, -rotate2,0, 0},
                {rotate2, rotate1, 0, 0},
                {0,0,1,0},
                {0,0,0,1}
            });

            return rotateMatrix;
        }

        public static Matrix3D Get3DRotationMatrix(double degrees, Vector rotationVector, Point3D translateOver)
        {
            // TODO: Identity matrix goed creëren.
            Matrix3D rotationMatrix = Matrix3D.createIdentityMatrix();

            // Step 1
            if (translateOver != null)
            {
                Matrix3D translation =
                    createTranslationMatrix(-translateOver.X, -translateOver.Y, -translateOver.Z);
                rotationMatrix = rotationMatrix * translation;
            }

            // Step 2
            double t1 = Math.Atan2(rotationVector.deltaZ, rotationVector.deltaX);
            Matrix3D yRotation = RotateY(t1, true, true);
            //yRotation.Multiply(rotationMatrix);
            rotationMatrix = rotationMatrix * yRotation;

            // Step 3
            // Pythagoras to calculate the angle
            double newX = Math.Sqrt(rotationVector.deltaX * rotationVector.deltaX + rotationVector.deltaZ * rotationVector.deltaZ);
            double t2 = Math.Atan2(rotationVector.deltaY, newX);
            Matrix3D zRotation = RotateZ(t2, true, true);
            rotationMatrix = rotationMatrix * zRotation;

            // Step 4
            Matrix3D rotate = RotateX(ConvertToRadians(degrees), false, true);
            rotationMatrix = rotationMatrix * rotate;

            // Step 5
            Matrix3D reverseZRotation = RotateZ(t2, false, true);
            rotationMatrix = rotationMatrix * reverseZRotation;

            // Step 6
            Matrix3D reverseYRotation = RotateY(t1, false, true);
            rotationMatrix = rotationMatrix * reverseYRotation;

            // Step 7
            if (translateOver != null)
            {
                Matrix3D translation = createTranslationMatrix(translateOver.X, translateOver.Y, translateOver.Z);
                rotationMatrix = rotationMatrix * translation;
            }           

            // I sincerely hope we're done now.
            return rotationMatrix;
        }
               
        public static Matrix3D createIdentityMatrix()
        {
            return new Matrix3D(new double[,] {
                {1,0,0,0},
                {0,1,0,0},
                {0,0,1,0},
                {0,0,0,1}    
            });
        }

        public static Matrix3D createTranslationMatrix(double x, double y, double z)
        {
            return new Matrix3D(new double[,] {
                {1,0,0,x},
                {0,1,0,y},
                {0,0,1,z},
                {0,0,0,1}    
            });
        }

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

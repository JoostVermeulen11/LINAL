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
            return null;
        }

        public static Matrix3D perspectiveProjectionMatrix()
        {
            return null;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

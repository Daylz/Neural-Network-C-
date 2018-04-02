using System;

namespace Daylz.Mathf
{
    public static class MatrixExtension
    {
        static Random rand = new Random();
        
        /// <summary>
        ///     Adds matrix b to matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Matrix b</param>
        public static void Add(this float[,] a, float[,] b)
        {
            if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
            {
                throw new Exception("Error adding float[,]: parameter has to be of the same dimensions");
            }

            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] += b[row, column];
                }
            }
        }

        /// <summary>
        ///     Adds vector b to matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Vector b</param>
        public static void Add(this float[,] a, float[] b)
        {
            if (a.GetLength(1) != b.Length)
            {
                throw new Exception("Error adding float[,]: parameter has to be of the same dimensions");
            }

            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] += b[column];
                }
            }
        }

        /// <summary>
        ///     Adds constant b to each element of matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Constant b</param>
        public static void Add(this float[,] a, float b)
        {
            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] += b;
                }
            }
        }

        /// <summary>
        ///     Adds matrix b to matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Matrix b</param>
        public static void Add(this float[][] a, float[][] b)
        {
            for (int row = 0; row < a.Length; row++)
            {
                for (int column = 0; column < a[row].Length; column++)
                {
                    a[row][column] += b[row][column];
                }
            }
        }

        /// <summary>
        ///     Adds vector b to matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Vector b</param>
        public static void Add(this float[][] a, float[] b)
        {
            for (int row = 0; row < a.Length; row++)
            {
                for (int column = 0; column < a[row].Length; column++)
                {
                    a[row][column] += b[column];
                }
            }
        }

        /// <summary>
        ///     Adds constant b to each element of matrix a
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Constant b</param>
        public static void Add(this float[][] a, float b)
        {
            for (int row = 0; row < a.Length; row++)
            {
                for (int column = 0; column < a[row].Length; column++)
                {
                    a[row][column] += b;
                }
            }
        }

        /// <summary>
        ///     Adds constant b to each element of vector a
        /// </summary>
        /// <param name="a">Vector a</param>
        /// <param name="b">Constant b</param>
        public static void Add(this float[] a, float b)
        {
            for (int row = 0; row < a.Length; row++)
            {
                a[row] += b;
            }
        }

        /// <summary>
        ///     Adds vector B to vector A and returns it in a new vector
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Vector b</param>
        public static float[] RAdd(this float[] a, float[] b)
        {
            float[] result = new float[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        /// <summary>
        ///     Substracts matrix B from matrix A
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Matrix b</param>
        public static void Sub(this float[,] a, float[,] b)
        {
            if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
            {
                throw new Exception("Error substracting float[,]: parameter has to be of the same dimensions");
            }

            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] -= b[row, column];
                }
            }
        }

        /// <summary>
        ///     Substracts constant B from each element of matrix A
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Constant b</param>
        public static void Sub(this float[,] a, float b)
        {
            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] -= b;
                }
            }
        }

        /// <summary>
        ///     Multiplies matrices A and B and returns the result into a new matrix
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Matrix b</param>
        /// <returns>Result matrix of a*b as a float[,]</returns>
        public static float[,] RMultiply(this float[,] a, float[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
            {
                throw new Exception("Error multiplying float[,] by float[,]: columns of A don't match rows of B");
            }

            int rowsA = a.GetLength(0);
            int columnsA = a.GetLength(1);
            int columnsB = b.GetLength(1);
            float[,] result = new float[rowsA, columnsB];

            for (int columnB = 0; columnB < columnsB; columnB++)
            {
                for (int rowA = 0; rowA < rowsA; rowA++)
                {
                    for (int i = 0; i < columnsA; i++)
                    {
                        result[rowA, columnB] += a[rowA, i] * b[i, columnB];
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Multiplies a matrix and a vector using dot product into a new matrix
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Matrix b</param>
        /// <returns>Result matrix of a*b as a float[,]</returns>
        public static float[] RMultiply(this float[,] a, float[] b)
        {
            if (a.GetLength(1) != b.Length)
            {
                throw new Exception("Error multiplying float[,] by float[,]: columns of A don't match rows of B");
            }

            int rowsA = a.GetLength(0);
            int columnsA = a.GetLength(1);
            int columnsB = b.Length;
            float[] result = new float[rowsA];

            for (int columnB = 0; columnB < columnsB; columnB++)
            {
                for (int rowA = 0; rowA < rowsA; rowA++)
                {
                    for (int i = 0; i < columnsA; i++)
                    {
                        result[rowA] += a[rowA, i] * b[i];
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Multiplies each element of matrix A by constant B
        /// </summary>
        /// <param name="a">Matrix a</param>
        /// <param name="b">Constant b</param>
        public static void Scale(this float[,] a, float b)
        {
            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] *= b;
                }
            }
        }

        /// <summary>
        ///     Fills the matrix with random values using the normal/Gaussian distribution
        /// </summary>
        /// <param name="a">Matrix to be filled with random values</param>
        public static void FillRandomGaussian(this float[,] a)
        {
            for (int row = 0; row < a.GetLength(0); row++)
            {
                for (int column = 0; column < a.GetLength(1); column++)
                {
                    a[row, column] = rand.NextGaussianFloat();
                }
            }
        }

        /// <summary>
        ///     Fills the matrix with random values using the normal/Gaussian distribution
        /// </summary>
        /// <param name="a">Matrix to be filled with random values</param>
        public static void FillRandomGaussian(this float[][] a)
        { 
            for (int row = 0; row < a.Length; row++)
            {
                for (int column = 0; column < a[row].Length; column++)
                {
                    a[row][column] = rand.NextGaussianFloat();
                }
            }
        }

        /// <summary>
        ///     Turns a matrix into a displayable string
        /// </summary>
        /// <param name="a">Matrix to be stringified</param>
        /// <returns></returns>
        public static string Stringify(this float[,] a)
        {
            string str = "";

            int rows = a.GetLength(0);
            int columns = a.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                str += "{ " + a[row, 0];

                for (int column = 1; column < columns; column++)
                {
                    str += ", " + a[row, column];
                }

                str += " }\n";
            }

            return str;
        }

        /// <summary>
        ///     Turns a matrix into a displayable string
        /// </summary>
        /// <param name="a">Matrix to be stringified</param>
        /// <returns></returns>
        public static string Stringify(this float[][] a)
        {
            string str = "";

            for (int row = 0; row < a.Length; row++)
            {
                str += "{ " + a[row][0];

                for (int column = 1; column < a[row].Length; column++)
                {
                    str += ", " + a[row][column];
                }

                str += " }\n";
            }

            return str;
        }

        /// <summary>
        ///     Turns a vector into a displayable string
        /// </summary>
        /// <param name="a">Matrix to be stringified</param>
        /// <returns></returns>
        public static string Stringify(this float[] a)
        {
            string str = "";

            str += "{ " + a[0];

            for (int i = 1; i < a.Length; i++)
            {
                str += ", " + a[i];
            }

            str += " }\n";

            return str;
        }
    }
}
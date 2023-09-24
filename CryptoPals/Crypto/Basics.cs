namespace CryptoPals
{
    public class Basics
    {
        public static byte[] GetRow(byte[,] matrix, int rowNumber)
        {
            var returnValue = new byte[matrix.GetLength(1)];

            for (var i = 0; i < returnValue.Length; i++)
            {
                returnValue[i] = matrix[rowNumber, i];
            }

            return returnValue;
        }
    }
}

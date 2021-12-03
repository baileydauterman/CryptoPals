using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class PlaintextCore
    {
        public static double EnglishScore(string message)
        {
            return message.Aggregate(0.0, (current, c) =>
            {
                if (char.IsControl(c))
                {
                    return current - 5;
                }

                if (c == ' ')
                {
                    return current + 10;
                }

                if (char.IsUpper(c))
                {
                    return current + 2;
                }

                if (char.IsLetterOrDigit(c))
                {
                    return current + 5;
                }

                return current;
            });
        }
    }
}

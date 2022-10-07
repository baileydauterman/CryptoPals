using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals.Data
{
    internal class DownloadWebData
    {
        public static byte[] ByteArray(string destination)
        {
            using (WebClient client = new())
            {
                var output = client.DownloadData(destination);
                return output;
            }
        }
    }
}

using System;
using System.IO;
using System.Text;

namespace datx_csharp
{
    public class District
    {
        private byte[] data;

        private long indexSize;


        public District(string name)
        {
            data = File.ReadAllBytes(name);

            indexSize = Util.bytesToLong(data[0], data[1], data[2], data[3]);
        }

        public string[] find(string ips)
        {
            if (!Util.isIPv4Address(ips))
            {
                throw new IPv4FormatException();
            }
            long val = Util.ip2long(ips);
            int low = 0;
            int mid = 0;
            int high = (int)((indexSize - 262144 - 262148) / 13) - 1;
            int pos = 0;
            while (low <= high)
            {
                mid = (low + high) / 2;
                pos = mid * 13 + 262148;

                long start = Util.bytesToLong(data[pos], data[pos + 1], data[pos + 2], data[pos + 3]);
                long end = Util.bytesToLong(data[pos + 4], data[pos + 5], data[pos + 6], data[pos + 7]);
                if (val > end)
                {
                    low = mid + 1;
                }
                else if (val < start)
                {
                    high = mid - 1;
                }
                else
                {
                    long off = Util.bytesToLong(data[pos + 11], data[pos + 10], data[pos + 9], data[pos + 8]);
                    int len = (int)data[pos + 12];

                    int offset = (int)(off - 262144 + indexSize);

                    byte[] loc = new byte[len];
                    Array.Copy(data, offset, loc, 0, len);

                    return Encoding.UTF8.GetString(loc).Split('\t');
                }
            }

            return null;
        }
    }
}

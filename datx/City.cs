using System;
using System.IO;
using System.Text;

namespace datx_csharp
{
    public class City
    {
        private byte[] data;

        private long indexSize;

        public City(string name)
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
            int start = 262148;
            int low = 0;
            int mid = 0;
            int high = (int)((indexSize - 262144 - 262148) / 9) - 1;
            int pos = 0;
            while (low <= high)
            {
                mid = (int)((low + high) / 2);
                pos = mid * 9;

                long s = 0;
                if (mid > 0)
                {
                    int pos1 = (mid - 1) * 9;
                    s = Util.bytesToLong(data[start + pos1], data[start + pos1 + 1], data[start + pos1 + 2], data[start + pos1 + 3]);
                }

                long end = Util.bytesToLong(data[start + pos], data[start + pos + 1], data[start + pos + 2], data[start + pos + 3]);
                if (val > end)
                {
                    low = mid + 1;
                }
                else if (val < s)
                {
                    high = mid - 1;
                }
                else
                {

                    byte b = 0;
                    long off = Util.bytesToLong(b, data[start + pos + 6], data[start + pos + 5], data[start + pos + 4]);
                    long len = Util.bytesToLong(b, b, data[start + pos + 7], data[start + pos + 8]);

                    int offset = (int)(off - 262144 + indexSize);

                    byte[] loc = new byte[len];
                    Array.Copy(data, offset, loc, 0, (int)len);

                    return Encoding.UTF8.GetString(loc).Split('\t');
                }
            }

            return null;
        }
    }
}

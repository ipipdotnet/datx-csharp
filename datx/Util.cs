using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace datx_csharp
{
    public class Util
    {
        private static Regex IPV4_PATTERN = new Regex("^(([1-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){1}(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){2}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

        public static bool isIPv4Address(string input)
        {
            return IPV4_PATTERN.IsMatch(input);
        }

        public static long bytesToLong(byte a, byte b, byte c, byte d)
        {
            return int2long(((a & 0xff) << 24) | ((b & 0xff) << 16) | ((c & 0xff) << 8) | (d & 0xff));
        }

        private static int str2Ip(string ip)
        {
            string[] ss = ip.Split('.');
            int a, b, c, d;
            a = int.Parse(ss[0]);
            b = int.Parse(ss[1]);
            c = int.Parse(ss[2]);
            d = int.Parse(ss[3]);
            return (a << 24) | (b << 16) | (c << 8) | d;
        }

        public static int str2Ip2(string ip)
        {
            try
            {
                byte[] bytes = Dns.GetHostAddresses(ip).First().GetAddressBytes();

                return ((bytes[0] & 0xFF) << 24) |
                       ((bytes[1] & 0xFF) << 16) |
                       ((bytes[2] & 0xFF) << 8) |
                       bytes[3];
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return 0;
        }


        public static long ip2long(string ip)
        {
            return int2long(str2Ip(ip));
        }

        private static long int2long(int i)
        {
            long l = i & 0x7fffffffL;
            if (i < 0)
            {
                l |= 0x080000000L;
            }
            return l;
        }
    }
}

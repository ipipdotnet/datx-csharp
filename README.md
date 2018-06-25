# datx-csharp

IPIP.net官方支持的解析datx格式的C#代码

## 示例代码
<pre>
<code>
using System;
using System.IO;

namespace datx_csharp
{
    class Program
    {
        static string ToString(string[] loc) => string.Join(", ", loc ?? new[] { "null" });

        static void Main(string[] args)
        {
            try
            {
                City city = new City("mydata4vipday4.datx"); // 城市库

                Console.WriteLine(ToString(city.find("8.8.8.8")));
                Console.WriteLine(ToString(city.find("255.255.255.255")));

                District district = new District("quxian.datx");//区县库

                Console.WriteLine(ToString(district.find("1.12.0.0")));
                Console.WriteLine(ToString(district.find("223.255.127.256"))); //.256 throw IPv4FormatException

                BaseStation baseStation = new BaseStation("station_ip.datx"); // 基站库

                Console.WriteLine(ToString(baseStation.find("8.8.8.8")));
                Console.WriteLine(ToString(baseStation.find("223.221.121.0")));

            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.StackTrace);
            }
            catch (IPv4FormatException ipex)
            {
                Console.WriteLine(ipex.StackTrace);
            }
            Console.ReadKey(true);
        }
    }
}

</code>
</pre>
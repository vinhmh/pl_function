using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test_function_IOT_demo
{
    class Program
    {
        IntPtr h = IntPtr.Zero;

        static void Main(string[] args)
        {
            //get device
            GetDevice();

            // GetRealTimeLog();
            demo abc = new demo("abc");
            abc.GetRealTimeLog();
            Console.Read();
        }
        // get device plug in C3
        public static void GetDevice()
        {
            int ret = 0, k = 0;
            string str = "";
            string[] tmp = null;
            string[] feild = null;
            byte[] buffer = new byte[64 * 1024];
            string udp = "UDP";
            string adr = "255.255.255.255";


            ret = SearchDevice(udp, adr, ref buffer[0]);
            Console.WriteLine(ret);
            if (ret >= 0)
            {
                str = Encoding.Default.GetString(buffer);
                str = str.Replace("\r\n", "\t");
                tmp = str.Split('\t');

                foreach (string sub_tmp in tmp)
                {
                    k = 0;
                    string[] sub_str = sub_tmp.Split(',');

                    // MAC
                    feild = sub_str[k++].Split('=');

                    // IP
                    feild = sub_str[k++].Split('=');
                    demo abc = new demo("abc");
                    abc.ConnectToDevice(feild[1]);
                    // NetMask
                    feild = sub_str[k++].Split('=');

                    // GATEIPAddress
                    feild = sub_str[k++].Split('=');

                    // SN
                    feild = sub_str[k++].Split('=');

                    // Device
                    feild = sub_str[k++].Split('=');

                    // Ver
                    feild = sub_str[k++].Split('=');
                }
            }
        }


        // param sample
        // "protocol=RS485,port=COM2,baudrate=38400bps,deviceid=1,timeout=50000, passwd=”;
        // 

        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();

        //4.11 call SearchDevice function
        [DllImport("plcommpro.dll", EntryPoint = "SearchDevice")]
        public static extern int SearchDevice(string commtype, string address, ref byte buffer);
        public class demo
        {
            IntPtr h = IntPtr.Zero;
            string abc;
            public demo(string ABC)
            {
                abc = ABC;
            }
            public void ConnectToDevice(string ip)
            {
                string str = "protocol=TCP,ipaddress="+ip+",port=4370,timeout=2000,passwd=";
                Console.WriteLine(str);
                if (IntPtr.Zero == h)
                {
                    h = Connect(str);
                    if (h != IntPtr.Zero)
                    {
                        Console.WriteLine("Connect success");
                    }
                    else
                    {
                        Console.WriteLine("Connect failed");
                    }

                }
                else
                {
                    Console.WriteLine("Connect failed 22");
                }
            }
            //4.1  call connect function
            [DllImport("C:\\WINDOWS\\SysWOW64\\plcommpro.dll", EntryPoint = "Connect")]
            public static extern IntPtr Connect(string Parameters);

            //4.10 call GetRTLog function

            [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
            public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);

            public void GetRealTimeLog()
            {
                IntPtr h = IntPtr.Zero;
                int ret = 0, buffersize = 256;
                string str = "";
                string[] tmp = null;
                byte[] buffer = new byte[256];

                if (IntPtr.Zero != h)
                {

                    ret = GetRTLog(h, ref buffer[0], buffersize);
                    Console.WriteLine("res{0}",ret);
                    if (ret >= 0)
                    {
                        str = Encoding.Default.GetString(buffer);
                        Console.WriteLine("log return");
                        Console.WriteLine(str);
                    }
                }
                // return ret;
            }
        }
    }
}

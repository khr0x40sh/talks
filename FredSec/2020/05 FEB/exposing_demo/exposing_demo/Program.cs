using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace exposing_demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int dwell = 1000;
            while (true)
            {
                Console.WriteLine("Hello from Main!");
                System.Threading.Thread.Sleep(dwell);
            }
        }
    }
}

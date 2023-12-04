using System;
using System.IO;
using System.Collections.Generic; 

namespace SharpUp
{
    class MainConsoleClass
    {
        private static List<string> ToIgnore = new List<string>(); //list that contains the files or paths to ignore
        private static string WhatToBkUp;
        private static string WhereToBkUp;

        public static void Options()
        {
            Console.WriteLine("Example sharpup [path or file to backup] [path where to back up] -i [files or directories to ignore]");
            Console.WriteLine("-h        Show this menu");
            Console.WriteLine("-i        Select files or directories to ignore");

        }

        private static void GetInptConsole()
        {
            while(true)
            {
                Console.Write(">> ");
                string option = Console.ReadLine();
            }
        }

        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                GetInptConsole();
            }
            else if(args.Length >= 1)
            {
                switch(args[0])
                {
                    case "-h":
                        Options();
                        break;
                    case "-i":
                        if(args.Length >= 2)
                        {
                            for (int i = 1; i < args.Length; i++)
                            {
                                ToIgnore.Add(args[i]);
                            }
                        }
                        break;
                    default:
                        try
                        {
                            if ((File.Exists(args[0]) || Directory.Exists(args[0])) && (Directory.Exists(args[1])))
                            {
                                WhatToBkUp = args[0];
                                WhereToBkUp = args[1];
                            }
                            else
                            {
                                Console.WriteLine("Please provide arguments as showed by these option: ");
                                Options();
                                GetInptConsole(); 
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                            Console.WriteLine("Please provide arguments as showed by these option: ");
                            Options();
                            GetInptConsole();
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please provide arguments as showed by these option:");
                Options();
                GetInptConsole();
            }
        }
    }
}

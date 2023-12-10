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

        // added because the user might want to change the folders/files to ignore
        static void ChangeIgnore(ref List<string> toIgnore)
        {
            if(toIgnore.Count == 0)
            {
                while (true)
                {
                    
                    Console.WriteLine("You do not have anything to ignore, Do you want to change that?(y/n) ");
                    char wantsToChange;
                    bool worked = Char.TryParse(Console.ReadLine().ToLower(), out wantsToChange);

                    if (worked == true && wantsToChange == 'y')
                    {
                        break;
                    }
                    else if (worked == true && wantsToChange == 'n')
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You did not introduce a valid value");
                    }

                }

                Console.WriteLine("Insert the files/folders to ignore. (Insert STOP to stop)");

                while(true)
                {
                    string ignore = Console.ReadLine();
                    if (ignore.ToUpper() != "STOP")
                    {
                        toIgnore.Add(ignore);
                    }
                    else
                        break;
                }

            }
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

                                try
                                {
                                    if(args[2] == "-i")
                                    {
                                        for (int i = 3; i < args.Length; i++)
                                        {
                                            ToIgnore.Add(args[i]);
                                        }

                                        BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp, ToIgnore);
                                    }
                                    else
                                    {
                                        ChangeIgnore(ref ToIgnore);
                                        if (ToIgnore.Count != 0)
                                        {
                                            BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp, ToIgnore);
                                        }
                                        else
                                        {
                                            BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp);
                                        }

                                    }
                                }
                                catch(Exception e)
                                {
                                    ChangeIgnore(ref ToIgnore);
                                    if (ToIgnore.Count != 0)
                                    {
                                        BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp, ToIgnore);
                                    }
                                    else
                                    {
                                        BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp);
                                    }
                                }
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

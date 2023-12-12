using System;
using System.IO;
using System.Collections.Generic; 

namespace SharpUp
{
    class MainConsoleClass
    {
        private static List<string> FToIgnore = new List<string>(); //list that contains the files to ignore
        private static List<string> DToIgnore = new List<string>(); //list that contains the paths to ignore
        private static string WhatToBkUp;
        private static string WhereToBkUp;

        public static void Options()
        {
            Console.WriteLine("Example sharpup [directory to backup] [path where to back up] -i [files or directories to ignore]");
            Console.WriteLine("-h        Show this menu");
            Console.WriteLine("====CONSOLE-MODE=======");
            Console.WriteLine("fi [ARGS]                      Files to igonore");
            Console.WriteLine("di [ARGS]                      Directories to ignore");
            Console.WriteLine("backup                         Start the back up");
            Console.WriteLine("what [File/path to backup]     Directory to backup");
            Console.WriteLine("where [BackUp destination]     Select backup destination");
            Console.WriteLine("exit           Exit the app");
        }

        // added because the user might want to change the folders/files to ignore
        static void ChangeIgnore(ref List<string> fToIgnore, ref List<string> dToIgnore)
        {
            bool shouldIbreak = false;
            if(fToIgnore.Count == 0)
            {
                while (true)
                {
                    
                    Console.WriteLine("You do not have any file to ignore, Do you want to change that?(y/n) ");
                    char wantsToChange;
                    bool worked = Char.TryParse(Console.ReadLine().ToLower(), out wantsToChange);

                    if (worked == true && wantsToChange == 'y')
                    {
                        break;
                    }
                    else if (worked == true && wantsToChange == 'n')
                    {

                        shouldIbreak = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You did not introduce a valid value");
                    }

                }

                Console.WriteLine("Insert the files to ignore. (Insert STOP to stop)");

                while(shouldIbreak == false)
                {
                    string ignore = Console.ReadLine();
                    if (ignore.ToUpper() != "STOP")
                    {
                        fToIgnore.Add(ignore);
                    }
                    else
                        break;
                }

            }
            else if(dToIgnore.Count == 0)
            {
                while (true)
                {

                    Console.WriteLine("You do not have any path to ignore, Do you want to change that?(y/n) ");
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

                Console.WriteLine("Insert the folders to ignore. (Insert STOP to stop)");

                while (true)
                {
                    string ignore = Console.ReadLine();
                    if (ignore.ToUpper() != "STOP")
                    {
                        dToIgnore.Add(ignore);
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

                switch(option)
                {
                    case "exit":
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    case "fi":
                        string[] fiIgArr = option.Split(' ');
                        for (byte i = 0; i < fiIgArr.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            else
                                FToIgnore.Add(fiIgArr[i]);
                        }
                        break;
                    case "di":
                        string[] diIgArr = option.Split(' ');
                        for (byte i = 0; i < diIgArr.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            else
                                DToIgnore.Add(diIgArr[i]);
                        }
                        break;
                    case "backup":
                        if ((WhatToBkUp != string.Empty && WhereToBkUp != string.Empty))
                        {
                            BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp, FToIgnore, DToIgnore);
                        }
                        break;
                    case "what":
                        string[] what = option.Split(' ');
                        if (Directory.Exists(what[1]))
                            WhatToBkUp = what[1];
                        else
                            Console.WriteLine("Invalid path/file");
                        break;
                    case "where":
                        string[] where = option.Split(' ');
                        if (Directory.Exists(where[1]))
                            WhatToBkUp = where[1];
                        else
                            Console.WriteLine("Invalid path");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
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
                    default:
                        try
                        {
                            if ((Directory.Exists(args[0])) && (Directory.Exists(args[1])))
                            {
                                WhatToBkUp = args[0];
                                WhereToBkUp = args[1];

                                    
                                ChangeIgnore(ref FToIgnore, ref DToIgnore);
                                if (FToIgnore.Count != 0)
                                {
                                    BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp, FToIgnore);
                                }
                                else
                                {
                                    BackUp bk = new BackUp(WhatToBkUp, WhereToBkUp);
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

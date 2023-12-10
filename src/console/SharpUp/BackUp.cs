using System;
using System.IO;
using System.Collections.Generic; 

namespace SharpUp
{
    public class BackUp
    {
        
        public BackUp(string whatToBkUp, string whereToBkUp, List<string> toIgnore)
        {
            BkUp(whatToBkUp, whereToBkUp, toIgnore);
        }

        public BackUp(string whatToBkUp, string whereToBkUp)
        {
            BkUp(whatToBkUp, whereToBkUp);
        }

        static void RecursiveIg(string WhatToBkUp, string WhereToBkUp, List<string> ToIgnore, bool recursive = true)
        {
            bool canRoll = true;

            var dir = new DirectoryInfo(WhatToBkUp);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(WhereToBkUp);

            foreach (FileInfo file in dir.GetFiles())
            {
                int idx = file.ToString().LastIndexOf("/"); // I added this bc otherwise it would be /path/to/file instead of /file
                string FIle = file.ToString().Substring(idx + 1);
                Console.WriteLine(FIle);
                foreach(var i in ToIgnore)
                {
                    if(FIle == i)
                    {
                        canRoll = false;
                        break;
                    }
                }
                if (canRoll == true)
                {
                    string targetFilePath = Path.Combine(WhereToBkUp, file.Name);
                    file.CopyTo(targetFilePath);
                }
                else
                    canRoll = true;
            }

            canRoll = true;

            if (recursive)
            {
                
                foreach (DirectoryInfo subDir in dirs)
                {
                    int idx = subDir.ToString().LastIndexOf("/"); // I added this bc otherwise it would be /path/to/dir instead of /dir
                    string DIr = subDir.ToString().Substring(idx + 1);
                    foreach (var i in ToIgnore)
                    {
                        if (DIr == i)
                        {
                            canRoll = false;
                            break;
                        }
                    }
                    if (canRoll)
                    {
                        string newDestinationDir = Path.Combine(WhereToBkUp, subDir.Name);
                        RecursiveIg(subDir.FullName, newDestinationDir, ToIgnore, true);
                    }
                    else
                        canRoll = true;
                }
            }
        }

        static void Recursive(string WhatToBkUp, string WhereToBkUp, bool recursive = true)
        {
            var dir = new DirectoryInfo(WhatToBkUp);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(WhereToBkUp);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(WhereToBkUp, file.Name);
                file.CopyTo(targetFilePath);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(WhereToBkUp, subDir.Name);
                    Recursive(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        static bool BkUp(string WhatToBackUp, string WhereToBackUp)
        {
            Recursive(WhatToBackUp, WhereToBackUp);
            return true;
        }

        static bool BkUp(string WhatToBackUp, string WhereToBackUp, List<string> toIgnore)
        {
            RecursiveIg(WhatToBackUp, WhereToBackUp, toIgnore);
            return true;
        }

    }
}

using System;
using System.IO;
using System.Collections.Generic; 

namespace SharpUp
{
    public class BackUp
    {
        private static string WhatToBkUp;
        private static string WhereToBkUp;
        private static List<string> ToIgnore;

        public BackUp(string whatToBkUp, string whereToBkUp, List<string> toIgnore)
        {
            WhatToBkUp = whatToBkUp;
            WhereToBkUp = whereToBkUp;
            ToIgnore = toIgnore;
        }

        public BackUp(string whatToBkUp, string whereToBkUp)
        {
            WhatToBkUp = whatToBkUp;
            WhereToBkUp = whereToBkUp;
        }
    }
}

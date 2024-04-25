using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPractise
{
    public static class IntExtension
    {
        public static int Digits(this int num)
        {
            if (num < 0) return num.ToString().Length - 1;
            return num.ToString().Length;
        }
    }
}

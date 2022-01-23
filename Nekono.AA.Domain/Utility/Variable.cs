using System;
using System.Collections.Generic;
using System.Text;

namespace Nekono.AA.Domain.Utility
{
    public class Variable
    {
        public static string TranDate ()=> DateTime.Now.ToString("yyyy/MM/dd");
        public static string TranTime() => DateTime.Now.ToString("HH:mm:ss");
    }
}

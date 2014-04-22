using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1
{
    class Timer
    {
        private static int _timeout = Environment.TickCount + 1000;

        public static void Reset()
        {
            _timeout = Environment.TickCount + 1000;
        }

        public static bool Timeout()
        {
            return Environment.TickCount > _timeout;
        }

    }
}

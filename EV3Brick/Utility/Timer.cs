using System;

namespace PrgSps2Gr1.Utility
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

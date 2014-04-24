using System;

namespace PrgSps2Gr1.Utility
{
    public class Timer
    {
        #region Constants declaration

        /// <summary>
        /// A set of predefined timeouts for the Timer.
        /// </summary>
        public struct TickTime
        {
            /// <summary>
            /// <code>ExtraShort</code> = 100 ms.
            /// </summary>
            public static readonly int ExtraShort = 100;
            /// <summary>
            /// <code>VeryShort</code> = 500 ms.
            /// </summary>
            public static readonly int VeryShort = 500;
            /// <summary>
            /// <code>Short</code> = 1 sec.
            /// </summary>
            public static readonly int Short = 1000;
            /// <summary>
            /// <code>Medium</code> = 3 sec.
            /// </summary>
            public static readonly int Medium = 3000;
            /// <summary>
            /// <code>High</code> = 5 sec.
            /// </summary>
            public static readonly int High = 5000;
            /// <summary>
            /// <code>VeryHigh</code> = 7 sec.
            /// </summary>
            public static readonly int VeryHigh = 7000;
            /// <summary>
            /// <code>ExtraHigh</code> = 10 sec.
            /// </summary>
            public static readonly int ExtraHigh = 10000;
        }

        #endregion

        private static int _tickTimeout = TickTime.Short;
        private static int _timeout = Environment.TickCount + _tickTimeout;

        
        /// <summary>
        /// This trigger time is set to initialize the new tick timeout.
        /// This also automatically resets the timer for convenience
        /// reasons.
        /// </summary>
        public static int TickTimeout
        {
            get { return _tickTimeout; }
            set
            {
                _tickTimeout = value;
                Reset();
            }
        }

        /// <summary>
        /// (Re)Sets the TickCount with a new timeout.
        /// </summary>
        public static void Reset()
        {
            _timeout = Environment.TickCount + _tickTimeout;
        }

        /// <summary>
        /// Returns if the tick timeout aleady occured.
        /// </summary>
        /// <returns></returns>
        public static bool IsTimeout()
        {
            return Environment.TickCount > _timeout;
        }
    }
}

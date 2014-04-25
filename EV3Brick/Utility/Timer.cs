using System;

namespace PrgSps2Gr1.Utility
{
    public class Ev3Timer
    {
        #region Constants declaration

        /// <summary>
        /// A set of predefined timeouts for the Ev3Timer.
        /// </summary>
        public struct TickTime
        {
            /// <summary>
            /// <code>Shortest</code> = 100 ms.
            /// </summary>
            public static readonly int Shortest = 100;
            /// <summary>
            /// <code>Shorter</code> = 500 ms.
            /// </summary>
            public static readonly int Shorter = 500;
            /// <summary>
            /// <code>Short</code> = 1 sec.
            /// </summary>
            public static readonly int Short = 1000;
            /// <summary>
            /// <code>Medium</code> = 2 sec.
            /// </summary>
            public static readonly int Medium = 2000;
            /// <summary>
            /// <code>Long</code> = 3 sec.
            /// </summary>
            public static readonly int Long = 3000;
            /// <summary>
            /// <code>Longer</code> = 5 sec.
            /// </summary>
            public static readonly int Longer = 5000;
            /// <summary>
            /// <code>Longest</code> = 7 sec.
            /// </summary>
            public static readonly int Longest = 7000;
        }

        #endregion

        private int _tickTimeout = TickTime.Short;
        private int _timeout;

        public Ev3Timer()
        {
            _timeout = Environment.TickCount + _tickTimeout;
        }


        /// <summary>
        /// This trigger time is set to initialize the new tick timeout.
        /// This also automatically resets the timer for convenience
        /// reasons.
        /// </summary>
        public int TickTimeout
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
        public void Reset()
        {
            _timeout = Environment.TickCount + _tickTimeout;
        }

        /// <summary>
        /// Returns if the tick timeout aleady occured.
        /// </summary>
        /// <returns></returns>
        public bool IsTimeout()
        {
            return Environment.TickCount > _timeout;
        }
    }
}

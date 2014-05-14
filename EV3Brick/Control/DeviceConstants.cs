using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSGrp1Grp2.Cunt.Control
{
    public class DeviceConstants
    {
        public struct Speed
        {
            /// <summary>
            /// <code>Slowest</code> = 10 %
            /// </summary>
            public static readonly sbyte Slowest = 10;
            /// <summary>
            /// <code>Slower</code> = 20 %
            /// </summary>
            public static readonly sbyte Slower = 20;
            /// <summary>
            /// <code>Slow</code> = 30 %
            /// </summary>
            public static readonly sbyte Slow = 30;
            /// <summary>
            /// <code>Medium</code> = 50 %
            /// </summary>
            public static readonly sbyte Medium = 50;
            /// <summary>
            /// <code>Fast</code> = 70 %
            /// </summary>
            public static readonly sbyte Fast = 70;
            /// <summary>
            /// <code>Faster</code> = 80 %
            /// </summary>
            public static readonly sbyte Faster = 80;
            /// <summary>
            /// <code>Fastest</code> = 100 %
            /// </summary>
            public static readonly sbyte Fastest = 100;
        }

        public enum TurnDirection
        {
            Left, Right
        }
        
    }
}

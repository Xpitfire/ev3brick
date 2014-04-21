using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1
{
    class EventQueue
    {
        private static readonly Queue<Action> PrimaryEventQueue = new Queue<Action>();
        private static readonly Queue<Action> SecondaryEventQueue = new Queue<Action>();
        private static readonly object Sync = new object();

        public static Queue<Action> Primary
        {
            get { lock(Sync) return PrimaryEventQueue; }
        }

        public static Queue<Action> Secondary
        {
            get { lock (Sync) return SecondaryEventQueue; }
        }

    }
}

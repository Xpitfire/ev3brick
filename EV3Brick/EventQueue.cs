using System;
using System.Collections.Generic;

namespace PrgSps2Gr1
{
    class EventQueue
    {
        private static readonly Queue<string> StateEventQueue = new Queue<string>();
		private static readonly Queue<Action> CommandEventQueue = new Queue<Action>();

        internal static Queue<string> State
        {
            get { return StateEventQueue; }
        }

        internal static string LastState { get; set; }

        internal static Queue<Action> Command
		{
			get {return CommandEventQueue; }
		}

    }
}

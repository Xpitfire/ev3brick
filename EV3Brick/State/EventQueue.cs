using System;
using System.Collections.Generic;
using PrgSps2Gr1.Logging;

namespace PrgSps2Gr1.State
{
    class EventQueue
    {
        private static readonly object SyncState = new object();
        private static readonly object SyncCommand = new object();
        private static readonly Queue<string> StateEventQueue = new Queue<string>();
		private static readonly Queue<Action> CommandEventQueue = new Queue<Action>();
        private static string _lastState;

        internal static string LastState
        {
            get
            {
                lock (SyncState)
                {
                    if (ProgramEv3Sps2Gr1.IsDebug)
                    {
                        Logger.Log("[Debug] LastState: " + _lastState);
                    }
                    return _lastState;
                }
            } 
            set
            {
                lock (SyncState)
                {
                    if (ProgramEv3Sps2Gr1.IsDebug)
                    {
                        Logger.Log("[Debug] LastState: " + _lastState);
                    }
                    _lastState = value;
                }
            }
        }

        private static Queue<string> State
        {
            get {
                lock (SyncState)
                {
                    return StateEventQueue;
                }
            }
        }

        private static Queue<Action> Command
        {
            get {
                lock (SyncCommand)
                {
                    return CommandEventQueue;
                }
            }
        }

        public static void EnqueueState(string s)
        {
            State.Enqueue(s);
        }

        public static string DequeueState()
        {
            return State.Dequeue();
        }

        public static int GetStateCount()
        {
            return State.Count;
        }

        public static void EnqueueCommand(Action a)
        {
            Command.Enqueue(a);
        }

        public static Action DequeueCommand()
        {
            return Command.Dequeue();
        }

        public static int GetCommandCount()
        {
            return Command.Count;
        }

    }
}

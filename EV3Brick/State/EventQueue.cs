using System;
using System.Collections.Generic;
using Sps2Gr1.InTeam.Logging;

namespace Sps2Gr1.InTeam.State
{
    class EventQueue
    {
        private static readonly object SyncState = new object();
        private static readonly object SyncCommand = new object();
        private static readonly Queue<string> StateEventQueue = new Queue<string>();
		private static readonly Queue<Action> CommandEventQueue = new Queue<Action>();
        private static string _lastState;

        #region State Queue Operations

        /// <summary>
        /// Access thread save to a queue instance.
        /// </summary>
        private static Queue<string> State
        {
            get {
                lock (SyncState)
                {
                    return StateEventQueue;
                }
            }
        }

        /// <summary>
        /// Enques a state only if the queue does not contain this it.
        /// </summary>
        /// <param name="s"></param>
        public static void EnqueueState(string s)
        {
            if (!State.Contains(s))
            {
                State.Enqueue(s);
            }
        }

        public static string DequeueState()
        {
            return State.Dequeue();
        }

        /// <summary>
        /// Return the number of elements in the state queue.
        /// </summary>
        /// <returns></returns>
        public static int GetStateCount()
        {
            return State.Count;
        }

        /// <summary>
        /// Returns the last operated state.
        /// </summary>
        internal static string LastState
        {
            get { lock (SyncState) { return _lastState; } }
            set { lock (SyncState) { _lastState = value; } }
        }

        #endregion

        #region Command Queue Operations

        /// <summary>
        /// Access thread save to a queue instance.
        /// </summary>
        private static Queue<Action> Command
        {
            get
            {
                lock (SyncCommand)
                {
                    return CommandEventQueue;
                }
            }
        }

        /// <summary>
        /// Enqueues an action if the queue does not already contain it.
        /// </summary>
        /// <param name="a"></param>
        public static void EnqueueCommand(Action a)
        {
            if (!Command.Contains(a))
            {
                Command.Enqueue(a);
            }
        }

        public static Action DequeueCommand()
        {
            return Command.Dequeue();
        }

        /// <summary>
        /// Remove all elements from the command queue.
        /// </summary>
        public static void ClearCommandQueue()
        {
            if (Command.Count > 0)
            {
                Command.Clear();
            }
        }

        /// <summary>
        /// Return the number of elements in the state queue.
        /// </summary>
        /// <returns></returns>
        public static int GetCommandCount()
        {
            return Command.Count;
        }

        #endregion

    }
}

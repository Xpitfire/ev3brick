using System;
using System.Collections.Generic;
using Sps2Gr1.InTeam.Logging;

namespace Sps2Gr1.InTeam.State
{
    class EventQueue
    {
        private readonly object _syncState = new object();
        private readonly object _syncCommand = new object();
        private readonly Queue<string> _stateEventQueue = new Queue<string>();
        private readonly Queue<Command> _commandEventQueue = new Queue<Command>();
        private string _lastState;
        private StateController _controller;

        public struct StateLevel
        {
            public const int Level1 = 1;
            public const int Level2 = 2;
            public const int Level3 = 3;
        }

        public struct CommandLevel
        {
            public const int Level1 = 1;
            public const int Level2 = 2;
            public const int Level3 = 3;
        }

        #region State Queue Operations

        /// <summary>
        /// Access thread save to a queue instance.
        /// </summary>
        private Queue<string> State
        {
            get {
                lock (_syncState)
                {
                    return _stateEventQueue;
                }
            }
        }

        /// <summary>
        /// Enques a state only if the queue does not contain this it.
        /// </summary>
        /// <param name="s"></param>
        public void EnqueueState(string s)
        {
            if (!State.Contains(s))
            {
                State.Enqueue(s);
            }
        }

        public string DequeueState()
        {
            return State.Dequeue();
        }

        /// <summary>
        /// Return the number of elements in the state queue.
        /// </summary>
        /// <returns></returns>
        public int GetStateCount()
        {
            return State.Count;
        }

        /// <summary>
        /// Returns the last operated state.
        /// </summary>
        internal string LastState
        {
            get { lock (_syncState) { return _lastState; } }
            set { lock (_syncState) { _lastState = value; } }
        }

        #endregion

        public EventQueue(StateController controller)
        {
            _controller = controller;
        }

        #region Command Queue Operations

        /// <summary>
        /// Access thread save to a queue instance.
        /// </summary>
        private Queue<Command> Command
        {
            get
            {
                lock (_syncCommand)
                {
                    return _commandEventQueue;
                }
            }
        }

        /// <summary>
        /// Enqueues an action if the queue does not already contain it.
        /// </summary>
        /// <param name="a"></param>
        public void EnqueueCommand(Command a)
        {
            if (!Command.Contains(a) && a.GetCommandLevel() <= _controller.CurrentState.GetStateLevel() )
            {
                Command.Enqueue(a);
            }
        }

        public Command DequeueCommand()
        {
            return Command.Dequeue();
        }

        /// <summary>
        /// Remove all elements from the command queue.
        /// </summary>
        public void ClearCommandQueue()
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
        public int GetCommandCount()
        {
            return Command.Count;
        }

        #endregion

    }
}

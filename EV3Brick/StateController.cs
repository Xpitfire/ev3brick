using System;
using System.Threading;
using Sps2Gr1.InTeam.State;
using Sps2Gr1.InTeam.State.Init;
using Sps2Gr1.InTeam.Logging;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam
{
    public class StateController
    {
        private readonly object _sync = new object();
        private static bool _isAlive = true;
        private AState _curAState;

        #region Global Condition Properties

        public static bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public static bool IsDebug { get; set; }

        #endregion

        /// <summary>
		/// Initializes the robot instance. <See cref="StateController"/> class.
		/// </summary>
        public StateController(string firstState)
        {
            Logger.Log("Creating new State instance --> initializing...");
            // set first state object
            _curAState = StateTypeConstants.InitializeStartup(firstState, this);
        }
        
        #region Sate Controller

        /// <summary>
        /// Gets or sets thread save the controller state, which is used for the
        /// current update sequence.
        /// </summary>
        /// <value>Controller AState.</value>
        internal AState CurrentState
        {
            get { lock (_sync) return _curAState; }
            set { lock (_sync) _curAState = value; }
        }

		/// <summary>
		/// Update the EV3 robot.
		/// </summary>
        public void Run()
        {
            Logger.Log("Starting program update thread --> Run...");
            while (IsAlive)
            {
                lock (_sync)
                {
                    CurrentState.Update();
                }
                Thread.Sleep(Ev3Timer.TickTime.Shortest);
            }
        }

        #endregion

        // ------------------------------ static methods ------------------------------

        /// <summary>
        /// Stop the execution of the program and set the global
        /// IsAlive instance to false (with time delay, 5 sec).
        /// </summary>
        public static void Exit()
        {
            Logger.Log("Bye bye... :) ");
            IsAlive = false;
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

    }

}

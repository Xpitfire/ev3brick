using System;
using System.Threading;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State;
using PrgSps2Gr1.State.Init;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1
{
    public class ProgramEv3Sps2Gr1
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
		/// Initializes the robot instance. <See cref="PrgSps2Gr1.ProgramEv3Sps2Gr1"/> class.
		/// </summary>
        public ProgramEv3Sps2Gr1()
        {
            Logger.Log("Creating new State instance --> initializing...");
            _curAState = new InitImpl(this);
        }
        
        #region Sate Controller

        /// <summary>
        /// Gets or sets thread save the controller state, which is used for the
        /// current update sequence.
        /// </summary>
        /// <value>Controller AState.</value>
        internal AState ProgramAState
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
                    ProgramAState.Update();
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

		/// <summary>
		/// The entry point of the program.
		/// </summary>
        public static void Main()
        {
            try
            {
                IsDebug = false;
                Logger.Log("Debugging disabled.");
                // start the robot AState update thread
                new ProgramEv3Sps2Gr1().Run();
            }
            catch (Exception ex)
            {
                Logger.Log("ERROR: Exception occured!" + ex.Message); 
                Thread.Sleep(5000); 
            }
        }

    }

}

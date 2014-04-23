using System;
using System.Threading;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State;
using PrgSps2Gr1.State.Init;

namespace PrgSps2Gr1
{
    public class ProgramEv3Sps2Gr1
    {
        private readonly object _sync = new object();
        private static bool _isAlive = true;
        private AState _curAState;

        public static bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public static bool IsDebug { get; set; }

        /// <summary>
		/// Initializes the robot instance. <See cref="PrgSps2Gr1.ProgramEv3Sps2Gr1"/> class.
		/// </summary>
        public ProgramEv3Sps2Gr1()
        {
            Logger.Log("Creating new State instance --> initializing...");
            _curAState = new InitImpl(this);
        }

		/// <summary>
		/// Gets or sets the controller AState.
		/// </summary>
		/// <value>Controller AState.</value>
        internal AState ProgramAState
        {
            get { lock(_sync) return _curAState; }
            set { lock(_sync) _curAState = value; }
        }

		/// <summary>
		/// Stop the execution of the program.
		/// </summary>
        public void Exit()
        {
            IsAlive = false;
			Thread.Sleep(5000);
            Environment.Exit(0);
        }

		/// <summary>
		/// Run the EV3 robot.
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
                Thread.Sleep(100);
            }
        }

        // ------------------------------ static methods ------------------------------

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

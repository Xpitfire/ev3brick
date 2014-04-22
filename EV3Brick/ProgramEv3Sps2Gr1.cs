using System;
using System.Threading;
using MonoBrickFirmware.Display;
using PrgSps2Gr1.States;
using PrgSps2Gr1.States.Init;

namespace PrgSps2Gr1
{
    class ProgramEv3Sps2Gr1
    {
        private readonly object _sync = new object();
        private State _curState;

        public static bool IsAlive { get; private set; }

        /// <summary>
		/// Initializes the robot instance. <See cref="PrgSps2Gr1.ProgramEv3Sps2Gr1"/> class.
		/// </summary>
        private ProgramEv3Sps2Gr1()
        {
            IsAlive = true;
            _curState = new InitImpl(this);
        }

		/// <summary>
		/// Gets or sets the controller state.
		/// </summary>
		/// <value>Controller state.</value>
        public State ProgramState
        {
            get { lock(_sync) return _curState; }
            set { lock(_sync) _curState = value; }
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
            while (IsAlive)
            {
                lock (_sync)
                {
                    ProgramState.Update();
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
                var prg = new ProgramEv3Sps2Gr1();
                // start the robot state update thread
                var thread = new Thread(prg.Run);
                thread.Start();
            }
            catch (Exception ex)
            {
                LcdConsole.WriteLine("Exception occured!" + ex.Message); 
                Thread.Sleep(5000); 
            }
        }

    }

}

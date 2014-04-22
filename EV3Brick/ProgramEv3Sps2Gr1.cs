using System;
using System.Threading;
using MonoBrickFirmware.Display;
using PrgSps2Gr1.State;
using PrgSps2Gr1.State.Init;

namespace PrgSps2Gr1
{
    class ProgramEv3Sps2Gr1
    {
        private readonly object _sync = new object();
        private AState _curAState;

        public static bool IsAlive { get; private set; }

        /// <summary>
		/// Initializes the robot instance. <See cref="PrgSps2Gr1.ProgramEv3Sps2Gr1"/> class.
		/// </summary>
        private ProgramEv3Sps2Gr1()
        {
            IsAlive = true;
            _curAState = new InitImpl(this);
        }

		/// <summary>
		/// Gets or sets the controller AState.
		/// </summary>
		/// <value>Controller AState.</value>
        public AState ProgramAState
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
                var prg = new ProgramEv3Sps2Gr1();
                // start the robot AState update thread
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

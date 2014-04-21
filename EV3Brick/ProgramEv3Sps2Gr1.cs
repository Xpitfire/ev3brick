using System;
using System.Threading;
using MonoBrickFirmware.Display;

namespace PrgSps2Gr1
{
    class ProgramEv3Sps2Gr1
    {
        private readonly object _sync = new object();
        private State _curState;
        private Boolean _isAlive;

        public bool IsAlive 
        {
            get { return _isAlive; }
        }

		/// <summary>
		/// Initializes the robot instance. <See cref="PrgSps2Gr1.ProgramEv3Sps2Gr1"/> class.
		/// </summary>
        private ProgramEv3Sps2Gr1()
        {
            _isAlive = true;
            _curState = new InitStartupImpl(this);
        }

		/// <summary>
		/// Gets or sets the controller state.
		/// </summary>
		/// <value>Controller state.</value>
        public State ControllerState
        {
            get { lock(_sync) return _curState; }
            set { lock(_sync) _curState = value; }
        }

		/// <summary>
		/// Stop the execution of the robot.
		/// </summary>
        public void Exit()
        {
            _isAlive = false;
            Environment.Exit(0);
        }

		/// <summary>
		/// Run the EV3 robot.
		/// </summary>
        public void Run()
        {
            while (_isAlive)
            {
                lock (_sync)
                {
                    ControllerState.Update();
                }
                Thread.Sleep(50);
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
                prg.Run();
            }
            catch (Exception ex)
            {
                LcdConsole.WriteLine("Exception occured!" + ex.Message); 
                Thread.Sleep(10000); 
            }
        }

    }

}

using System;
using System.Threading;

namespace PrgSps2Gr1
{
    class ProgramEv3Sps2Gr1
    {
        private readonly object _sync = new object();
        private State _curState;
        private Boolean _isAlive;

        private ProgramEv3Sps2Gr1()
        {
            _isAlive = true;
            _curState = new InitStartupImpl(this);
        }

        public State ControllerState
        {
            get { lock(_sync) return _curState; }
            set { lock(_sync) _curState = value; }
        }

        public void Exit()
        {
            _isAlive = false;
            Environment.Exit(0);
        }

        public void Run()
        {
            while (_isAlive)
            {
                ControllerState.Update();
                Thread.Sleep(50);
            }
        }

        // ------------------------------ static methods ------------------------------

        public static void Main()
        {
            var prg = new ProgramEv3Sps2Gr1();
            prg.Run();
        }

    }

}

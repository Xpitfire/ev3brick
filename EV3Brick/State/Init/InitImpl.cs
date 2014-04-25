using System;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State.Normal;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.State.Init
{
    class InitImpl : AState
    {
        public const string Name = "Init";

        internal InitImpl(ProgramEv3Sps2Gr1 project)
        {
            // set the abstract state controller instance
            Controller = project;
            // set timeout for state change
            StateTimer.TickTimeout = Ev3Timer.TickTime.Short;
            EventQueue.EnqueueCommand(superScanning);
            PerformSingleAction();
        }

        protected override void PerformRecurrentAction()
        {
            // initialize components
            if (StateTimer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override sealed void PerformSingleAction()
        {
            // TODO implement action
            Logger.Log("Color sensor initialization not implemented!");
            Ev3.InitSpinScanner();
        }

        private void superScanning()
        {
            Logger.Log("do somthing;!!");
        }

        public override object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}

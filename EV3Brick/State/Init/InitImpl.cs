using System;
using System.Threading;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State.Normal;
using Timer = PrgSps2Gr1.Utility.Timer;

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
            Timer.TickTimeout = Timer.TickTime.Short;
        }

        protected override void PerformRecurrentAction()
        {
            // initialize components
            if (Timer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalDriveImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            // TODO implement action
            Logger.Log("Color sensor initialization not implemented!");
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

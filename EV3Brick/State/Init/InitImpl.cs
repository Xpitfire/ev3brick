using System;
using SPSGrp1Grp2.Cunt.State.Normal;
using SPSGrp1Grp2.Cunt.Logging;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State.Init
{
    class InitImpl : AState
    {
        public const string Name = "Init";
        private const int Level = EventQueue.StateLevel.Level1;

        internal InitImpl(StateController controller)
        {
            // set the abstract state controller instance
            Controller = controller;
            // set timeout for state change
            StateTimer.TickTimeout = Ev3Timer.TickTime.Short;
            PerformSingleAction();
        }

        protected override void PerformRecurrentAction()
        {
        }

        protected override sealed void PerformSingleAction()
        {
            Logger.Log("Wait for user input.");
        }

        public override int GetStateLevel()
        {
            return Level;
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

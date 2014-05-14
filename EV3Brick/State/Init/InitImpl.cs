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

        public InitImpl(StateController controller)
        {
            // set the abstract state controller instance
            Controller = controller;
            // set timeout for state change
            StateTimer.TickTimeout = Ev3Timer.TickTime.Short;
            PerformSingleAction();
        }

        internal InitImpl()
        {
        }

        protected override void PerformRecurrentAction()
        {
        }

        protected override sealed void PerformSingleAction()
        {
            // reset all movements
            Ev3.StopAllMovements();

            Logger.Log("Escape == Exit");
            Logger.Log("Enter  == Pause / Resume");
            Logger.Log("Up     == Init color");
            Logger.Log("Down   == Search Mode");
            Logger.Log("Left   == Reset");
            Logger.Log("Right  == Flee Mode");

            Logger.Log("Wait for user input...");
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

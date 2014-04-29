using System;
using Sps2Gr1.InTeam.State.Normal;
using Sps2Gr1.InTeam.Logging;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Init
{
    class InitImpl : AState
    {
        public const string Name = "Init";
        private const EventQueue.StateLevel Level = EventQueue.StateLevel.Level1;

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
            // do nothing
        }

        protected override sealed void PerformSingleAction()
        {
            Logger.Log("Color sensor initialization...");
            Ev3.InitColor();
        }

        public override EventQueue.StateLevel GetStateLevel()
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

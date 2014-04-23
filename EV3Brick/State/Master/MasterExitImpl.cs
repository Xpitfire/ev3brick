using System;

namespace PrgSps2Gr1.State.Master
{
    class MasterExitImpl : AState
    {
        public const string Name = "MasterExit";

        protected override void PerformAction()
        {
            Ev3.WriteLine("Stopping motors");
            Ev3.StopAllMovements();
            Ev3.WriteLine("Exiting application...");
            base.Controller.Exit();
        }

        public override void Log()
        {
            throw new NotImplementedException();
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

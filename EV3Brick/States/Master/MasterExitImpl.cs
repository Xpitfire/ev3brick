using System;

namespace PrgSps2Gr1.States.Master
{
    class MasterExitImpl : State
    {
        public const string Name = "MasterExit";

        protected override void PerformAction()
        {
            Ev3.WriteLine("Stopping motors");
            Ev3.StopAllMovements();
            Ev3.WriteLine("Exiting application...");
            base.Controller.Exit();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}

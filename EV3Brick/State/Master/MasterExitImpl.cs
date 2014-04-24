using System;
using PrgSps2Gr1.Logging;

namespace PrgSps2Gr1.State.Master
{
    class MasterExitImpl : AState
    {
        public const string Name = "MasterExit";

        protected override void PerformAction()
        {
            Logger.Log("Stopping motors");
            Ev3.StopAllMovements();
            Logger.Log("Exiting application...");
            base.Controller.Exit();
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

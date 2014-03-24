using System;

namespace PrgSps2Gr1
{
    class NormalDriveImpl : State
    {
        protected override void PerformAction()
        {
            Ev3.VehicleDirve(25);
        }

        public override String ToString()
        {
            return "NormalDrive";
        }

    }
}

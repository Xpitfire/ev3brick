using System;

namespace PrgSps2Gr1
{
    class NormalDriveImpl : State
    {
        protected override void PerformAction()
        {
            Ev3.VehicleDrive((sbyte) Ev3Utilities.Speed.Slow);
        }

        public override String ToString()
        {
            return "NormalDrive";
        }

    }
}

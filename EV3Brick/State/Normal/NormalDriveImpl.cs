using System;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.Logging;
using Timer = PrgSps2Gr1.Utility.Timer;

namespace PrgSps2Gr1.State.Normal
{
    class NormalDriveImpl : AState
    {
        public const string Name = "NormalDrive";

        internal NormalDriveImpl()
        {
            Timer.TickTimeout = Timer.TickTime.Short;
        }

        protected override void PerformRecurrentAction()
        {
            if (Timer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleDrive((sbyte)DeviceConstants.Speed.Slow);
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

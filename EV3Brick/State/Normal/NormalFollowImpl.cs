using System;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.State.Normal
{
    class NormalFollowImpl : AState
    {
        public const string Name = "NormalFollow";

        internal NormalFollowImpl()
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout() && Ev3.HasLostObject())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleDrive(DeviceConstants.Speed.Medium);
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

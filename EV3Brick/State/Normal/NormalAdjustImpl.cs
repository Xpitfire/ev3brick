using Sps2Gr1.InTeam.Control;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalAdjustImpl : AState
    {
        public const string Name = "NormalAdjust";

        internal NormalAdjustImpl() {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleDrive(DeviceConstants.Speed.Slowest);
        }

        public override object[] Debug(object[] args)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}

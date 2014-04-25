using PrgSps2Gr1.Control;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.State.Normal
{
    class NormalAdjustImpl : AState
    {
        public const string Name = "NormalAdjust";

        internal NormalAdjustImpl() {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            // TODO implements search procedure...
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

using Sps2Gr1.InTeam.Control;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalAdjustImpl : AState
    {
        public const string Name = "NormalAdjust";
        private const EventQueue.StateLevel Level = EventQueue.StateLevel.Level3;

        internal NormalAdjustImpl() {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                StateEventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleDrive(DeviceConstants.Speed.Slowest);
        }

        public override EventQueue.StateLevel GetStateLevel()
        {
            return Level;
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

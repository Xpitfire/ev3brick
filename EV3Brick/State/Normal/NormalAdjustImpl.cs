using SPSGrp1Grp2.Cunt.Control;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State.Normal
{
    class NormalAdjustImpl : AState
    {
        public const string Name = "NormalAdjust";
        private const int Level = EventQueue.StateLevel.Level3;

        internal NormalAdjustImpl() {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Longer;
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
            Ev3.VehicleDrive(DeviceConstants.Speed.Slow);
        }

        public override int GetStateLevel()
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

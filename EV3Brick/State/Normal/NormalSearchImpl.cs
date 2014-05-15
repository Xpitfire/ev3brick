using System.Threading;

namespace SPSGrp1Grp2.Cunt.State.Normal
{
    class NormalSearchImpl : AState
    {
        public const string Name = "NormalSearch";
        private const int Level = EventQueue.StateLevel.Level3;

        internal NormalSearchImpl()
        {
            StateTimer.TickTimeout = Utility.Ev3Timer.TickTime.Short;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                StateEventQueue.EnqueueState(NormalAdjustImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.StopAllMovements();
            Ev3.SpinVehicle();
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

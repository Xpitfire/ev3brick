using System.Threading;

namespace PrgSps2Gr1.State.Normal
{
    class NormalSearchImpl : AState
    {
        public const string Name = "NormalSearch";

        internal NormalSearchImpl()
        {
            StateTimer.TickTimeout = Utility.Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            // TODO implements search procedure...
            if (StateTimer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalAdjustImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.StopAllMovements();
            Ev3.SpinVehicle();
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
